using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using hrsync.Interface;
using hrsync.Models;
using hrsync.Models.Configuration;
using hrsync.Services;

namespace hrsync.Controllers
{
    [Authorize]
    [Route("hrsync/v1/hrsync/extractionfullsync")]
    public class HrSyncExtractionFullSyncController : Controller
    {
        private readonly ILogger Logger;
        private IExtractionFullService extractionFullService;
        private readonly IHrSchedulerConfigurationData hrSchedulerConfigurationData;

        public HrSyncExtractionFullSyncController(
            ILoggerFactory loggerFactory,
            IExtractionFullService extractionFullService,
            IHrSchedulerConfigurationData hrSchedulerConfigurationData)
        {
            this.Logger = loggerFactory.CreateLogger<ExtractionFullService>();
            this.extractionFullService = extractionFullService;
            this.hrSchedulerConfigurationData = hrSchedulerConfigurationData;
        }

        [HttpGet]
        public async Task<ActionResult> HrSyncDataFullSyncExtraction()
        {
            List<HrmasterdataOuList> hrMasterdataOuList = new List<HrmasterdataOuList>();
            HrmasterdataOuList synclist = new HrmasterdataOuList();
            DateTime serviceTime;

            try
            {
                HrSchedulerConfiguration dbconfig;
                serviceTime = DateTime.Now;
                bool canStart = true;

                dbconfig = hrSchedulerConfigurationData.GetHrSchedulerConfiguration();
                if (dbconfig != null)
                {
                    canStart = CanStartToday(dbconfig.ToSchedulerOptions(dbconfig), serviceTime);
                    if (canStart)
                    {
                        Logger.LogInformation(string.Format("FULL SYNC hrsync: {0} - Extraction can start today to elaborate request.", DateTime.Now));
                        extractionFullService.MakeUOFullExtraction(hrMasterdataOuList, synclist, serviceTime);
                        Logger.LogInformation(string.Format("FULL SYNC hrsync: {0} - Extraction is finished.", DateTime.Now));
                        return Ok("Extraction Full Sync is finished!");
                    }
                }

                return Ok("Extraction Full Sync not started! Not possible today!");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting HrSyncData.");
                return StatusCode(500, "Unable to get HR Data");
            }
        }

        private bool CanStartToday(SchedulerOptions scheduler, DateTime serviceTime)
        {

            if (scheduler.Active != 0)
            {
                Logger.LogInformation("{0} - HRSYNC Full Sync can't start because Active = 1 (scheduler HRSYNC already active)", DateTime.Now);
                return false;
            }

            if (scheduler.DaysDenied != null && scheduler.DaysDenied.Count > 0)
            {
                foreach (string item in scheduler.DaysDenied)
                {
                    short d = Convert.ToInt16(item);
                    // int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    // if (d == serviceTime.Day || DateTime.Now.Day == (daysInMonth - 20))
                    if (d == serviceTime.Day)
                    {
                        Logger.LogInformation("{0} - HRSYNC Full Sync can't start because today in DaysDenied", DateTime.Now);
                        return false;
                    }
                }
            }
            else
            {
                Logger.LogCritical(string.Format("hrsync {0} - CRITICAL - Please note that no configuration has been found to sync SAP HR.", DateTime.Now));
            }

            Logger.LogInformation("{0} - HRSYNC Full Sync can start because Active = 0 (scheduler HRSYNC is not already active) and today is not in DaysDenied", DateTime.Now);
            return true;
        }

    }

}

