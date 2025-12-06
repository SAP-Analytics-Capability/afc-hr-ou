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

using masterdata.Models;
using masterdata.Models.SnowCostCenterResults;
using masterdata.Models.Configuration;
using masterdata.Helpers;
using masterdata.Interfaces;

namespace masterdata.Services
{
    public class BackgroundServiceMasterTask : BackgroundServiceMaster
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private IOptions<SnowAdapterConfiguration> SnowConfiguration;
        private IOptions<SchedulerOptions> Scheduler;
        private IOptions<ProxyData> ProxyData;
        private IResultAdapter ResultAdapter;
        private readonly IResultData ResultData;
        private readonly IOuCCAssociation OUAssociation;
        private readonly IHrMasterdataOuData HRMasterData;
        private readonly IClientAccessData ClientAccessData;
        private readonly ISchedulerConfigurationData SchedulerConfigurationData;

        public BackgroundServiceMasterTask(IConfiguration configuration,
                                        ILoggerFactory loggerFactory,
                                        IOptions<SnowAdapterConfiguration> snwoconfiguration,
                                        IOptions<SchedulerOptions> scheduler,
                                        IOptions<ProxyData> proxydata,
                                        IResultAdapter resultAdapter,
                                        IResultData resultdata,
                                        IOuCCAssociation ouassociation,
                                        IHrMasterdataOuData hrmasterdata,
                                        IClientAccessData clientaccessdata,
                                        ISchedulerConfigurationData schedulerConfigurationData)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<BackgroundServiceMasterTask>();
            this.SnowConfiguration = snwoconfiguration;
            this.Scheduler = scheduler;
            this.ProxyData = proxydata;
            this.ResultAdapter = resultAdapter;
            this.ResultData = resultdata;
            this.OUAssociation = ouassociation;
            this.HRMasterData = hrmasterdata;
            this.ClientAccessData = clientaccessdata;
            this.SchedulerConfigurationData = schedulerConfigurationData;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation(string.Format("{0} - Master Scheduler is starting",DateTime.Now));

            stoppingToken.Register(() =>
                Logger.LogInformation(string.Format("{0} - Master Scheduler is stopping", DateTime.Now)));

            while (!stoppingToken.IsCancellationRequested)
            {
                List<SchedulerConfiguration> config = SchedulerConfigurationData.GetConfigurationByName(SchedulerCommons.MasterScheduler);

                if (SchedulerCommons.SchedulerCanStart(config, Convert.ToInt32(Scheduler.Value.SafeMode)))
                {
                    DateTime startTime = DateTime.Now;

                    Logger.LogInformation(string.Format("{0} - MASTER SCHEDULER IS NOW RUNNING", DateTime.Now));
                    DateTime lastaccess = ClientAccessData.AddNewAccess(SchedulerCommons.MasterScheduler, "FULL_V2");

                    OUAssociation.ComputeAssociationV2("", lastaccess);

                    if (!ClientAccessData.UpdateAccess(SchedulerCommons.MasterScheduler, "FULL_V2"))
                    {
                        Logger.LogWarning("Unable to update the client access for the Scheduler Master. Please, check the log.");
                    }

                }

                // wait for to next hour
                int minuteToHour = 60 - (DateTime.Now.Minute - 1);
                Logger.LogInformation(string.Format("{0} - Master Scheduler is waiting for {1} minutes until the next hour", DateTime.Now, TimeSpan.FromMinutes(minuteToHour)));
                await Task.Delay(TimeSpan.FromMinutes(minuteToHour));
                Logger.LogInformation(string.Format("{0} - Master Scheduler restarted to read configuration [DayOfMonth,Hour(time_of_day),Active]", DateTime.Now));
            }
        }
    }
}