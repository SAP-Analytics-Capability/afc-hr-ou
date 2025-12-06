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
    public class BackgroundServiceTask : BackgroundService
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

        public BackgroundServiceTask(IConfiguration configuration,
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
            this.Logger = loggerFactory.CreateLogger<BackgroundServiceTask>();
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
            Logger.LogInformation("Starting BACKGROUND SERVICE scheduler");

            var t = Task.Run(async delegate
              {
                  await Task.Delay(TimeSpan.FromMinutes(20), stoppingToken);
              });
            t.Wait();

            stoppingToken.Register(() =>
                Logger.LogDebug($" Scheduler is stopping"));

            while (!stoppingToken.IsCancellationRequested)
            {
                List<SchedulerConfiguration> config = SchedulerConfigurationData.GetConfigurationByName(SchedulerCommons.OldScheduler);

                if (SchedulerCommons.SchedulerCanStart(config, Convert.ToInt32(Scheduler.Value.SafeMode)))
                {
                    DateTime lastaccess = ClientAccessData.AddNewAccess(SchedulerCommons.OldScheduler, "FULL");
                    Logger.LogInformation("Scheduler is computing", DateTime.Now);

                    OUAssociation.ComputeAssociation("insert", lastaccess);

                    if (!ClientAccessData.UpdateAccess(SchedulerCommons.OldScheduler, "FULL"))
                    {
                        Logger.LogWarning("Unable to update the client access for the Scheduler. Please, check the log.");
                    }
                    Logger.LogInformation("Scheduler is waiting", DateTime.Now);
                    await Task.Delay(TimeSpan.FromMinutes(SchedulerCommons.GetInterval(config, SchedulerCommons.OldScheduler)), stoppingToken);
                }
            }
        }


        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }

}