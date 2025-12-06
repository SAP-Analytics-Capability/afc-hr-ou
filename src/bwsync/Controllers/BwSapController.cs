using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using bwsync.Models.Sap;
using bwsync.Interfaces;
using bwsync.Models;

namespace bwsync.Controllers
{
    [Authorize]
    [Route("bwsync/v1/bwsap")]
    [ApiController]
    public class BwSapController : Controller
    {
        private ILogger logger;
        private IBwSapDataAdapter bwSapDataAdapter;
        private readonly IClientAuthentication ClientAuthentication;

        public BwSapController(ILoggerFactory loggerFactory, IBwSapDataAdapter bwSapDataAdapter, IClientAuthentication clientAuthentication)
        {
            this.bwSapDataAdapter = bwSapDataAdapter;
            this.logger = loggerFactory.CreateLogger<BwSapController>();
            this.ClientAuthentication = clientAuthentication;
        }
        

        [HttpGet("getmastersapbwdata/q")]
        public IActionResult GetMasterSapBWData()      
        {
            try
            {
                Task<BwSapDataList> bwMasterTask = this.bwSapDataAdapter.GetMasterBWData(CancellationToken.None);
                bwMasterTask.Wait();

                BwSapDataList bwSapDataList = null;
                ActionResult result = null;

                if (bwMasterTask.IsCompletedSuccessfully)
                {
                    bwSapDataList = bwMasterTask.Result;

                    if (bwSapDataList != null)
                    { 
                        //to do 
                        result = Ok(bwSapDataList);
                    }
                    else
                    {
                        //lista vuota se è andato in errore chiamata o se non ha restituito nulla
                        bwSapDataList = new BwSapDataList();
                        result = Ok(bwSapDataList);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "An error occurred while excuting GetMasterSapBWData. Message: {0}.", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, String.Format("An error occurred while excuting GetMasterSapBWData. Message: {0}.", e.Message));
            }
        }
    }
}