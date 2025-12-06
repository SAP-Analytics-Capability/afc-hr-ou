using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using masterdata.Models;
using masterdata.Interfaces.Adapters;
using masterdata.Helpers;
using masterdata.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using masterdata.Models.SnowCostCenterResults;
using masterdata.Models.Configuration;

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v1/elaborationmasterdatafullsync")]
    public class ElaborationFullSyncController : ControllerBase
    {
        private readonly ILogger Logger;
        private IElaborationService ElaborationService;
        private ISchedulerConfigurationData SchedulerConfigurationData;

        public ElaborationFullSyncController( ILoggerFactory loggerFactory
                                            , IElaborationService elaborationService
                                            , ISchedulerConfigurationData schedulerConfigurationData)
        {
            this.Logger = loggerFactory.CreateLogger<ElaborationController>();
            this.ElaborationService = elaborationService;
            this.SchedulerConfigurationData = schedulerConfigurationData;
        }

        [HttpGet]
        public async Task<ActionResult> MakeElaborationFullSync()
        {
            try
            {
                List<SchedulerConfiguration> dbconfig = SchedulerConfigurationData.GetConfigurationByName(SchedulerCommons.MasterScheduler);
                if (SchedulerCanStartToday(dbconfig))
                {
                    Task.Run(() => ElaborationService.MakeElaborationFullSync());
                    return Ok("Elaboration Full Sync is in progress ...");
                }
                else
                {
                    return Ok("Elaboration Full Sync not started ...");
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to make elaboration full sync.");
                return StatusCode(500, "Internal server error");
            }
        }

        public bool SchedulerCanStartToday(List<SchedulerConfiguration> config)
        {

            bool canstart = (config != null && config.Count > 0);
            if (canstart)
            {
                int today = DateTime.Now.Day;
                // int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int dayZero = -1;
                SchedulerConfiguration dayZeroConfig = new SchedulerConfiguration();

                foreach (SchedulerConfiguration sc in config)
                {
                    if ((canstart = (today == sc.DayOfMonth && sc.Active == 0
                        )))
                    {
                        Logger.LogInformation("{0} - Full Sync MASTERDATA can start because DayOfMonth is scheduled and Active = 0 (Scheduler not activated)", DateTime.Now);
                        break;
                    }

                    if (sc.DayOfMonth == 0)
                    {
                        dayZeroConfig = sc;
                        dayZero = sc.DayOfMonth;
                        break;
                    }
                }

                if (dayZeroConfig.DayOfMonth == dayZero && dayZeroConfig.Active == 0
                    )
                {
                    Logger.LogInformation("{0} - Full Sync Masterdata can start because DayOfMonth = 0 and Active = 0 (Scheduler not activated)", DateTime.Now);
                    canstart = true;
                }

                if (!canstart)
                {
                    Logger.LogInformation("{0} - Full Sync Masterdata can't start because today not in DayOfMonth or Active = 1 (Scheduler Masterdata is already activated).", DateTime.Now);
                }
            }
            else
            {
                Logger.LogInformation("{0} - Full Sync Masterdata can't start because nothing scheduled in hrscheduledconfiguration", DateTime.Now);
            }
            //return (safemode == 1 ? true : canstart);
            return canstart;
        }
    }
}
