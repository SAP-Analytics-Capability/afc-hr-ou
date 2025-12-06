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
// using masterdata.Data;

namespace masterdata.Services
{
    public class ElaborationService : IElaborationService
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
        // private readonly SchedulerConfiguration SchedulerConfiguration;
        private readonly IDBCleaning DBCleaning;

        public ElaborationService(IConfiguration configuration,
                                        ILoggerFactory loggerFactory,
                                        IOptions<SnowAdapterConfiguration> snwoconfiguration,
                                        IOptions<SchedulerOptions> scheduler,
                                        IOptions<ProxyData> proxydata,
                                        IResultAdapter resultAdapter,
                                        IResultData resultdata,
                                        IOuCCAssociation ouassociation,
                                        IHrMasterdataOuData hrmasterdata,
                                        IClientAccessData clientaccessdata,
                                        ISchedulerConfigurationData schedulerConfigurationData,
                                        // SchedulerConfiguration schedulerConfiguration,
                                        IDBCleaning dbCleaning)
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
            // this.SchedulerConfiguration = schedulerConfiguration;
            this.DBCleaning = dbCleaning;
        }

        public void MakeElaboration()
        {
            // DateTime startTime = DateTime.Now;

            Logger.LogInformation("Elaboration is computing", DateTime.Now);

            DateTime lastaccess = ClientAccessData.AddNewAccess(SchedulerCommons.MasterScheduler, "FULL_V2");
/*          
            bool isDBClean;
            isDBClean = DBCleaning.ElaborationDBCleaning();
*/
            OUAssociation.ComputeAssociationV2("", lastaccess);

            if (!ClientAccessData.UpdateAccess(SchedulerCommons.MasterScheduler, "FULL_V2"))
            {
                Logger.LogWarning("Unable to update the client access for the Scheduler Master. Please, check the log.");
                //Implement send mail method
            }

            Logger.LogInformation("Master Scheduler is waiting", DateTime.Now);

            // int ts = Convert.ToInt32(startTime.AddMinutes((config[0].IntervalInMinutes)).Subtract(DateTime.Now).TotalMinutes);

            // await Task.Delay(TimeSpan.FromMinutes(ts), stoppingToken);
        }

        public void MakeElaborationFullSync()
        {
            // DateTime startTime = DateTime.Now;

            Logger.LogInformation("{0} - Elaboration Full Sync is computing", DateTime.Now);

            string today_str = DateTime.Now.ToString("dd");
            int day = int.Parse(today_str);
            // estraggo schedulazione master_scheduler di oggi
            List<SchedulerConfiguration> dbconfig_fullsync = SchedulerConfigurationData.GetConfigurationByDay(day);
            // e vedo se la schedulazione è attiva per oggi e se il flag_sync è Y
            if (dbconfig_fullsync.Count == 1 && dbconfig_fullsync[0].Active == 0)
            {

                // Esegui la FULL_V2
                Logger.LogInformation("{0} - Elaboration Full Sync launched because today is in DayOfMonth and Active = 0 (Master Scheduler not activated.)", DateTime.Now);
                DateTime lastaccess = ClientAccessData.AddNewAccess(SchedulerCommons.MasterScheduler, "FULL_V2_SYNC");
                /*          
                            bool isDBClean;
                            isDBClean = DBCleaning.ElaborationDBCleaning();
                */
                OUAssociation.ComputeAssociationV2("", lastaccess);

                if (!ClientAccessData.UpdateAccess(SchedulerCommons.MasterScheduler, "FULL_V2_SYNC"))
                {
                    Logger.LogWarning("Unable to update the client access for the Scheduler Master. Please, check the log.");
                    //Implement send mail method
                }
            }
            else
            {
                Logger.LogInformation("{0} - Elaboration Full Sync NOT launched", DateTime.Now);
            }

        }

    }
}
