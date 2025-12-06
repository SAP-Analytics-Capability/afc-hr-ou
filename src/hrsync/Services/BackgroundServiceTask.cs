using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using hrsync.Interface;
using hrsync.Models;
using hrsync.Models.Configuration;
using hrsync.Helpers;

using System.Linq;
using System.Globalization;

namespace hrsync.Services
{
    public class BackgroundServiceTask : BackgroundService
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private IOptions<SchedulerOptions> Scheduler;
        private IOptions<ProxyData> ProxyData;
        private IOptions<HrGlobalConfiguration> hrGlobalConfiguration;
        private IReportData ReportData;
        private IHrMasterdataOuData hrmasterdataouData;
        private readonly IHrSchedulerConfigurationData hrSchedulerConfigurationData;
        private IExtractionCustomService extractionCustomService;
        private IExtractionFullService extractionFullService;
        private IMailSender MailSender;

        public BackgroundServiceTask(
            IConfiguration configuration,
            ILoggerFactory loggerFactory,
            IOptions<SchedulerOptions> scheduler,
            IOptions<ProxyData> proxydata,
            IOptions<HrGlobalConfiguration> hrGlobalConfiguration,
            IReportData reportdata,
            IHrMasterdataOuData hrmasterdataouData,
            IHrSchedulerConfigurationData hrSchedulerConfigurationData,
            IMailSender mailSender,
            IExtractionCustomService extractionCustomService,
            IExtractionFullService extractionFullService
            )
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<BackgroundServiceTask>();
            this.Scheduler = scheduler;
            this.ProxyData = proxydata;
            this.hrGlobalConfiguration = hrGlobalConfiguration;
            this.ReportData = reportdata;
            this.hrmasterdataouData = hrmasterdataouData;
            this.hrSchedulerConfigurationData = hrSchedulerConfigurationData;
            this.MailSender = mailSender;
            this.extractionCustomService = extractionCustomService;
            this.extractionFullService = extractionFullService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<HrmasterdataOuList> hrMasterdataOuList = new List<HrmasterdataOuList>();
            HrmasterdataOuList synclist = new HrmasterdataOuList();
            //DateTime initservicetime = DateTime.Now;
            string[] servicemessages = new string[3];

            //Report currentReport = new Report();

            Logger.LogInformation(string.Format("{0} - Hrsync Scheduler: Scheduler is starting",DateTime.Now));

            stoppingToken.Register(() =>
                Logger.LogInformation(string.Format("{0} - Hrsync Scheduler: Scheduler is stopping", DateTime.Now)));
                //Logger.LogDebug(string.Format("hrsync: - {0} Scheduler is stopping", DateTime.Now)));

            DateTime serviceTime = DateTime.Now;
            bool canStart = false;
            HrSchedulerConfiguration dbconfig;
            HrSchedulerConfiguration dbconfig_custom;
            string min_interval = "0";
            string min_interval_custom = "0";
            string min_interval_full = "0";
            string default_min_interval = "30";
            int delay_check_conf_minute = 1;

            while (!stoppingToken.IsCancellationRequested)
            {

                // //////////////////////
                // INIZIO SEZIONE FULL
                // //////////////////////

                delay_check_conf_minute = 1;
                serviceTime = DateTime.Now;
                canStart = false;

                dbconfig = hrSchedulerConfigurationData.GetHrSchedulerConfiguration();
                if (dbconfig != null)
                {
                    canStart = CanStart(dbconfig.ToSchedulerOptions(dbconfig), serviceTime);
                    min_interval_full = dbconfig.MinInterval;
                }
                else
                {
                    min_interval_full = default_min_interval;
                }

                if (canStart)
                {

                    Logger.LogInformation(string.Format("FULL hrsync: {0} - Scheduler start to elaborate request.", DateTime.Now));
                    
                    extractionFullService.MakeUOFullExtraction(hrMasterdataOuList, synclist, serviceTime);
                    
                    // waiting next run
                    min_interval = min_interval_full;
                    int ts = Convert.ToInt32(serviceTime.AddMinutes(Convert.ToInt32(min_interval)).Subtract(DateTime.Now).TotalMinutes);
                    if (ts < 0)
                    {
                        ts = 0;
                    }
                    Logger.LogInformation(string.Format("{0} - Hrsync Scheduler is in waiting for the rest of configurated interval. The delay will be {1} minutes.", DateTime.Now, TimeSpan.FromMinutes(ts)));
                    await Task.Delay(TimeSpan.FromMinutes(ts));
                    Logger.LogInformation(string.Format("{0} - Hrsync Scheduler restarted to read configuration (DaysDenied,Hour,Minute,Active,FlagSync).", DateTime.Now));
                    delay_check_conf_minute = 0;
                }

                // ///////////////////
                // FINE SEZIONE FULL
                // ///////////////////

                // //////////////////////
                // INIZIO SEZIONE CUSTOM
                // //////////////////////

                serviceTime = DateTime.Now;
                canStart = false;


                dbconfig_custom = hrSchedulerConfigurationData.GetHrSchedulerConfigurationCustom();

                if (dbconfig_custom != null)
                {
                    canStart = CanStart(dbconfig_custom.ToSchedulerOptions(dbconfig_custom), serviceTime);
                    min_interval_custom = dbconfig_custom.MinInterval;
                }
                else
                {
                    min_interval_custom = default_min_interval;
                }

                if (canStart)
                {

                    Logger.LogInformation(string.Format("CUSTOM hrsync: {0} - Scheduler start to elaborate request.", DateTime.Now));
                    extractionCustomService.MakeUOCustomExtraction(hrMasterdataOuList, synclist, serviceTime);

                    // waiting next run
                    min_interval = min_interval_custom;
                    int ts = Convert.ToInt32(serviceTime.AddMinutes(Convert.ToInt32(min_interval)).Subtract(DateTime.Now).TotalMinutes);
                    if (ts < 0)
                    {
                        ts = 0;
                    }
                    Logger.LogInformation(string.Format("{0} - Hrsync Scheduler is in waiting for the rest of configurated interval. The delay will be {1} minutes.", DateTime.Now, TimeSpan.FromMinutes(ts)));
                    await Task.Delay(TimeSpan.FromMinutes(ts));
                    Logger.LogInformation(string.Format("{0} - Hrsync Scheduler restarted to read configuration (DaysDenied,Hour,Minute,Active,FlagSync).", DateTime.Now));
                    delay_check_conf_minute = 0;
                }

                // ///////////////////
                // FINE SEZIONE CUSTOM
                // ///////////////////

                // wait 1 minutes (delay_check_conf_minute) to check configuration because the granularity of configuration is 1 minute
                if (delay_check_conf_minute != 0)
                {
                    Logger.LogInformation(string.Format("{0} - Hrsync Scheduler is in waiting for {1} minutes before to read configuration (DaysDenied,Hour,Minute,Active,FlagSync).", DateTime.Now, delay_check_conf_minute));
                    await Task.Delay(TimeSpan.FromMinutes(delay_check_conf_minute));
                    Logger.LogInformation(string.Format("{0} - Hrsync Scheduler restarted to read configuration (DaysDenied,Hour,Minute,Active,FlagSync).", DateTime.Now));
                }
            }
        }


        private bool CanStart(SchedulerOptions scheduler, DateTime serviceTime)
        {

            if (scheduler.Active == 0)
            {
                return false;
            }

            if (scheduler.DaysDenied != null && scheduler.DaysDenied.Count > 0)
            {

                foreach (string item in scheduler.DaysDenied)
                {
                    short d = Convert.ToInt16(item);
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    if (d == serviceTime.Day || DateTime.Now.Day == (daysInMonth - 20))
                    {
                        return false;
                    }
                }

                if (scheduler.HoursPermitted != null && scheduler.HoursPermitted.Count > 0)
                {
                    foreach (string item in scheduler.HoursPermitted)
                    {
                        short h = Convert.ToInt16(item);
                        if (h == serviceTime.Hour)
                        {
                            if (Convert.ToInt16(scheduler.MinutePermitted) == -1 || serviceTime.Minute == Convert.ToInt16(scheduler.MinutePermitted))
                            {
                                Logger.LogInformation("{0} - Scheduler HRSYNC can start because Active = 1, day in DayDenied not present, Hour and Minute is permitted or -1", DateTime.Now);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            else if (scheduler.HoursPermitted != null && scheduler.HoursPermitted.Count > 0)
            {
                foreach (string item in scheduler.HoursPermitted)
                {
                    short h = Convert.ToInt16(item);
                    if (h == serviceTime.Hour && serviceTime.Minute == Convert.ToInt16(scheduler.MinutePermitted))
                    {
                        Logger.LogInformation("{0} - Scheduler HRSYNC can start because Active = 1, DayDenied is null, Hour and Minute is permitted", DateTime.Now);
                        return true;
                    }
                }
            }
            else
            {
                Logger.LogCritical(string.Format("hrsync {0} - CRITICAL - Please note that no configuration has been found to sync SAP HR.", DateTime.Now));
            }
            return false;
        }
    }
}
