using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using rulesmngt.Models;
using rulesmngt.Models.SnowConsolidationResult;
using rulesmngt.Models.SnowTranscodingResult;
using rulesmngt.Models.Configuration;
using rulesmngt.Helpers;
using rulesmngt.Interfaces;
using rulesmngt.Helpers.Adapters;
using rulesmngt.Interfaces.Adapters;
using rulesmngt.Helpers.Utils;

namespace rulesmngt.Services
{
    public class BackgroundServiceSnowTask : BackgroundServiceSnow
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private IOptions<SnowCatalogConfiguration> SnowConfiguration;
        private IOptions<ProxyData> ProxyData;
        private ISnowAdatpter SnowAdapter;
        private readonly ISchedulerConfigurationData SchedulerConfigurationData;
        private IOptions<SchedulerOptions> Scheduler;
        private IMailSender MailSender;
        private ICatalogUtils CatalogUtils;
        private IEventLogUtils EventLogUtils;
        // private SchedulerCommons SchedulerCommons;
        private IFullSync FullSync;

        public BackgroundServiceSnowTask(IConfiguration configuration,
                                        ILoggerFactory loggerFactory,
                                        IOptions<SnowCatalogConfiguration> snwoconfiguration,
                                        IOptions<ProxyData> proxydata,
                                        ISnowAdatpter snowAdatpter,
                                        ISchedulerConfigurationData schedulerConfigurationData,
                                        IOptions<SchedulerOptions> scheduler,
                                        IMailSender mailSender,
                                        ICatalogUtils catalogUtils,
                                        IEventLogUtils eventLogUtils,
                                        // SchedulerCommons schedulerCommons,
                                        IFullSync fullSync)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<BackgroundServiceSnow>();
            this.SnowConfiguration = snwoconfiguration;
            this.ProxyData = proxydata;
            this.SnowAdapter = snowAdatpter;
            this.SchedulerConfigurationData = schedulerConfigurationData;
            this.Scheduler = scheduler;
            this.CatalogUtils = catalogUtils;
            this.EventLogUtils = eventLogUtils;
            // this.SchedulerCommons = schedulerCommons;
            this.FullSync = fullSync;
            this.MailSender = mailSender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // solo per test mail
            // List<string> notificationTest = new List<string>();
            // notificationTest.Add(string.Format("SYSTEM TEST - Notifica Catalog Scheduler", DateTime.Now));
            // notificationTest.Add(string.Format("{0} - Catalog Scheduler: is starting", DateTime.Now));
            // MailSender.SendMail(notificationTest);
            // solo per test mail fine

            Logger.LogInformation(string.Format("{0} - Catalog Scheduler: The background catalog sync is starting", DateTime.Now.ToString()));
            DateTime start = DateTime.Now;

            while (!stoppingToken.IsCancellationRequested)
            {
                List<SchedulerConfiguration> config = null;
                if ((config = SchedulerConfigurationData.GetConfigurationByName(SchedulerCommons.CatalogScheduler)) != null)
                {
                    bool canStart = SchedulerCommons.SchedulerCanStart(config);
                    if (canStart)
                    {
                        List<string> notificationInit = new List<string>();
                        // notificationInit.Add(string.Format("AFC AWS Catalog Scheduler STARTED"));
                        // notificationInit.Add(string.Format("Catalog Scheduler STARTED"));
                        // MailSender.SendMail(notificationInit);
                        string notificationStartFullSync = "";

                        try
                        {
                            Logger.LogInformation(string.Format("{0} - Catalog Scheduler: reading/writing the activity catalog.", DateTime.Now.ToString()));
                            Task <RootObjectTranscoding> transcodingTask = SnowAdapter.getTranscodingActivity(CancellationToken.None);
                            transcodingTask.Wait();
                            if (transcodingTask.IsCompletedSuccessfully && transcodingTask.Result != null)
                            {
                                List<ActivityAssociation> newCatalog = CatalogUtils.ConvertToAfcActivityCatalog(transcodingTask.Result);
                                if (CatalogUtils.ElaborationDBCleaningTranscoding() && newCatalog.Count > 0)
                                {
                                    CatalogUtils.AddTranscoding(newCatalog);
                                }

                                if (CatalogUtils.isEmptyTranscoding())
                                {
                                    CatalogUtils.RollbackTranscoding();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.LogError(string.Format("{0} - Catalog Scheduler: Unable to read/write the transcoding catalog", DateTime.Now.ToString()), e.Message);
                            notificationInit.Clear();
                            notificationInit.Add(string.Format("AFC AWS Catalog Scheduler ERROR"));
                            notificationInit.Add(string.Format("Catalog Scheduler: Unable to read/write the transcoding catalog: {0}", e.Message));
                            MailSender.SendMail(notificationInit);
                        }

                        try
                        {
                            Logger.LogInformation(string.Format("{0} - Catalog Scheduler: reading/writing the company catalog.", DateTime.Now.ToString()));
                            Task<RootObjectConsolidation> consolidationTask = SnowAdapter.getConsolidationCatalog(CancellationToken.None);
                            consolidationTask.Wait();
                            if (consolidationTask.IsCompletedSuccessfully && consolidationTask.Result != null)
                            {
                                List<CompanyScope> newCatalog = CatalogUtils.ConvertToAfcConsolidateCatalog(consolidationTask.Result);

                                if (CatalogUtils.ElaborationDBCleaningConsolidating() && newCatalog.Count > 0)
                                {
                                    CatalogUtils.AddConsolidated(newCatalog);
                                }

                                if (CatalogUtils.isEmptyConsolidate())
                                {
                                    CatalogUtils.RollbackConsolidated();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.LogError(string.Format("{0} - Catalog Scheduler: Unable to read/write the company catalog.", DateTime.Now.ToString()), e);
                            notificationInit.Clear();
                            notificationInit.Add(string.Format("AFC AWS Catalog Scheduler ERROR"));
                            notificationInit.Add(string.Format("Catalog Scheduler: Unable to read/write the company catalog: {0}", e.Message));
                            MailSender.SendMail(notificationInit);
                        }

                        try
                        {

                            // recupero il valore flag_sync per il giorno della settimana corrente ed eseguo la FULL sincrona
                            Logger.LogInformation(string.Format("{0} - Catalog Scheduler: verify and launch Full Sync.", DateTime.Now.ToString()));
                            string flag_sync_var = "N";
                            string sync_post_service = "";
                            int today_week = DateTime.Now.DayOfWeek.GetHashCode();
                            foreach (SchedulerConfiguration sc_sync in config)
                            {
                                if (today_week == sc_sync.DayOfMonth)
                                {
                                    flag_sync_var = sc_sync.FlagSync;
                                    sync_post_service = sc_sync.SyncPostService;
                                    break;
                                }
                            }

                            if (flag_sync_var == "Y" && sync_post_service != "")
                            {
                                notificationStartFullSync = " - Full Sync Launched";
                                Logger.LogInformation(string.Format("{0} - Catalog Scheduler: Full Sync - init", DateTime.Now.ToString()));
                                int res = FullSync.fullSync(flag_sync_var, sync_post_service);
                                Logger.LogInformation(string.Format("{0} - Catalog Scheduler: Full Sync - end", DateTime.Now.ToString()));
                            }
                            else 
                            {
                                Logger.LogInformation(string.Format("{0} - Catalog Scheduler: Full Sync - not started FlagSync not Y or PostService is null", DateTime.Now.ToString()));
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.LogError(string.Format("{0} - Catalog Scheduler: Unable to launch Full Sync.", DateTime.Now.ToString()), e);
                            notificationInit.Clear();
                            notificationInit.Add(string.Format("AFC AWS Catalog Scheduler ERROR"));
                            notificationInit.Add(string.Format("Catalog Scheduler: Unable to launch Full Sync: {0}", e.Message));
                            MailSender.SendMail(notificationInit);
                        }

                        notificationInit.Clear();
                        notificationInit.Add(string.Format("AFC AWS Catalog Scheduler END"));
                        notificationInit.Add(string.Format("Catalog Scheduler terminato! {0}", notificationStartFullSync));
                        MailSender.SendMail(notificationInit);

                    }
                }
                else
                {
                    Logger.LogWarning(string.Format("{0} - Catalog Scheduler - No configuration found. The scheduler will try run again in 29 min", DateTime.Now.ToString()), DateTime.Now);
                }

                int minuteToHour = 60 - (DateTime.Now.Minute - 1);
                Logger.LogInformation(string.Format("{0} - Catalog Scheduler is in waiting for {1} minutes until to next hour.", DateTime.Now.ToString(), minuteToHour));
                await Task.Delay(TimeSpan.FromMinutes(minuteToHour));
                Logger.LogInformation(string.Format("{0} - Catalog Scheduler restarted to read configuration [WeekDay(day_of_month),Hour(time_of_day),Active,FlagSync].", DateTime.Now.ToString()));
            }
        }
    }
}
