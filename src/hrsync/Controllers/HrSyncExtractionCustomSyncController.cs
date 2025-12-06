using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using hrsync.Interface;
using hrsync.Models;
using hrsync.Services;
using hrsync.Models.Configuration;

namespace hrsync.Controllers
{
    [Authorize]
    [Route("hrsync/v1/hrsync/extractioncustomsync")]
    public class HrSyncExtractionCustomSyncController : Controller
    {
        private readonly ILogger Logger;
        private IExtractionCustomService extractionCustomService;
        private readonly IHrSchedulerConfigurationData hrSchedulerConfigurationData;

        public HrSyncExtractionCustomSyncController(
            ILoggerFactory loggerFactory,
            IExtractionCustomService extractionCustomService,
            IHrSchedulerConfigurationData hrSchedulerConfigurationData)
        {
            this.Logger = loggerFactory.CreateLogger<ExtractionCustomService>();
            this.extractionCustomService = extractionCustomService;
            this.hrSchedulerConfigurationData = hrSchedulerConfigurationData;
        }

        [HttpGet]
        public async Task<ActionResult> HrSyncDataCustomSyncExtraction()
        {
            List<HrmasterdataOuList> hrMasterdataOuList = new List<HrmasterdataOuList>();
            HrmasterdataOuList synclist = new HrmasterdataOuList();
            DateTime serviceTime = DateTime.Now;

            try
            {

                HrSchedulerConfiguration dbconfig_custom;
                serviceTime = DateTime.Now;
                bool canStart = true;

                dbconfig_custom = hrSchedulerConfigurationData.GetHrSchedulerConfigurationCustom();

                if (dbconfig_custom != null)
                {
                    canStart = CanStartToday(dbconfig_custom.ToSchedulerOptions(dbconfig_custom), serviceTime);
                    if (canStart)
                    {
                        Logger.LogInformation(string.Format("CUSTOM SYNC hrsync: {0} - Extraction can start today to elaborate request.", DateTime.Now));
                        // Task.Run(() => extractionCustomService.MakeUOCustomExtraction(hrMasterdataOuList, synclist, serviceTime));
                        extractionCustomService.MakeUOCustomExtraction(hrMasterdataOuList, synclist, serviceTime);
                        Logger.LogInformation(string.Format("CUSTOM SYNC hrsync: {0} - Extraction is finished.", DateTime.Now));
                        return Ok("Extraction Custom Sync is finished!");
                    }
                }

                return Ok("Extraction Custom Sync not started! Not possible today!");

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
