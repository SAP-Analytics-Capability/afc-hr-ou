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
    [Route("hrsync/v1/extractioncustom")]
    public class HrSyncExtractionCustomController : Controller
    {
        private readonly ILogger Logger;
        private IExtractionCustomService extractionCustomService;

        public HrSyncExtractionCustomController(
            ILoggerFactory loggerFactory,
            IExtractionCustomService extractionCustomService)
        {
            this.Logger = loggerFactory.CreateLogger<BackgroundServiceTask>();
            this.extractionCustomService = extractionCustomService;
        }

        [HttpGet]
        public async Task<ActionResult> HrSyncDataCustomExtraction()
        {
            List<HrmasterdataOuList> hrMasterdataOuList = new List<HrmasterdataOuList>();
            HrmasterdataOuList synclist = new HrmasterdataOuList();
            DateTime serviceTime = DateTime.Now;

            try
            {
                Task.Run(() => extractionCustomService.MakeUOCustomExtraction(hrMasterdataOuList, synclist, serviceTime));
                //int c = synclist.hrmasterdataou.Count;
                return Ok("Extraction Custom is in progress...");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting HrSyncData.");
                return StatusCode(500, "Unable to get HR Data");
            }
        }
    }

}