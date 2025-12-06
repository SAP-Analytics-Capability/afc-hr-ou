using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

using bwsync.Helpers;
using bwsync.Interfaces;
using bwsync.Models;

namespace bwsync.Controllers
{
    [Route("bwsync/v1/bwsync")]
    [ApiController]
    public class BwSyncController : Controller
    {
        private ILogger Logger;
        private IBwSyncDataAdapter bwSyncDataAdapter;
        private readonly IClientAuthentication ClientAuthentication;

        public BwSyncController(ILoggerFactory loggerFactory, IBwSyncDataAdapter hrSyncAdapter, IClientAuthentication clientAuthentication)
        {
            this.bwSyncDataAdapter = hrSyncAdapter;
            this.Logger = loggerFactory.CreateLogger<BwSyncController>();
            this.ClientAuthentication = clientAuthentication;
        }


        [HttpGet("q")]
        public IActionResult GetBwSyncData([FromQuery(Name = "ZVAR_MACRO_ORG1")]string ZVarMacroOrg1, 
                                           [FromQuery(Name = "ZVAR_MACRO_ORG2")] string ZVarMacroOrg2, 
                                           [FromQuery(Name = "ZVAR_VCS")]string ZVarVcs,
                                           [FromQuery(Name = "COMPANY")]string companyCode,
                                           [FromQuery(Name = "ZVAR_PROCESS_CODE")]string ProcessCode,
                                           [FromQuery(Name = "ZVAR_ORGANIZ_CODE")]string OrganizationCode) 
        {
            try
            {
                ApiFilteringChecker.BWApiFilterChecker(ZVarMacroOrg1, ZVarMacroOrg2, ZVarVcs);

                Task<BwMasterDataList> bwSyncTask = bwSyncDataAdapter.GetSapBWData(CancellationToken.None, ZVarMacroOrg1, ZVarMacroOrg2, ZVarVcs, companyCode,ProcessCode, OrganizationCode);
                bwSyncTask.Wait();

                BwMasterDataList bwMasterDataList = null;
                ActionResult result = null;

                if (bwSyncTask.IsCompletedSuccessfully)
                {
                    bwMasterDataList = bwSyncTask.Result;

                    if (bwMasterDataList != null)
                    { 
                        result = Ok(bwMasterDataList);
                    }
                    else
                    {
                        bwMasterDataList = new BwMasterDataList();
                        result = Ok(bwMasterDataList);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting BwSyncData.");
                return StatusCode(500, new Error(string.Format("An error occurred while excuting BwSyncData. Exception message: {0}.", ex.Message)));
            }
        }
        
        [HttpGet("f")]
        public IActionResult GetBwSyncDataForFixedTest([FromQuery(Name = "target")]string target,
                                                        [FromQuery(Name = "proxy")]string useproxy) 
        {
            try
            {
                Task<BwMasterDataList> bwSyncTask = bwSyncDataAdapter.GetFixedResultForTest(CancellationToken.None, target, useproxy.ToUpper());
                bwSyncTask.Wait();

                BwMasterDataList bwMasterDataList = null;
                ActionResult result = null;

                if (bwSyncTask.IsCompletedSuccessfully)
                {
                    bwMasterDataList = bwSyncTask.Result;

                    if (bwMasterDataList != null)
                    { 
                        result = Ok(bwMasterDataList);
                    }
                    else
                    {
                        bwMasterDataList = new BwMasterDataList();
                        result = Ok(bwMasterDataList);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting BwSyncData.");
                return StatusCode(500, new Error(string.Format("An error occurred while excuting BwSyncData. Exception message: {0}.", ex.Message)));
            }

        }
    }
}