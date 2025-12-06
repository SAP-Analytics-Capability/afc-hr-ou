using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using hrsync.Interface;
using hrsync.Models;
using hrsync.Services;

namespace hrsync.Controllers
{
    [Authorize]
    [Route("hrsync/v1/extraction")]
    public class HrSyncExtractionController : Controller
    {
        private readonly ILogger Logger;
        private IExtractionService extractionService;

        public HrSyncExtractionController(
            ILoggerFactory loggerFactory,
            IExtractionService extractionService)
        {
            this.Logger = loggerFactory.CreateLogger<BackgroundServiceTask>();
            this.extractionService = extractionService;
        }

        [HttpGet]
        public async Task<ActionResult> HrSyncDataExtraction()
        {
            List<HrmasterdataOuList> hrMasterdataOuList = new List<HrmasterdataOuList>();
            HrmasterdataOuList synclist = new HrmasterdataOuList();
            DateTime serviceTime = DateTime.Now;

            try
            {
                Task.Run(() => extractionService.MakeUOExtraction(hrMasterdataOuList, synclist, serviceTime));
                //int c = synclist.hrmasterdataou.Count;
                return Ok("Extraction is in progress...");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting HrSyncData.");
                return StatusCode(500, "Unable to get HR Data");
            }
        }
    }

}