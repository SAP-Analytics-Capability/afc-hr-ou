using System;
using System.Collections.Generic;
using System.Linq;
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

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v1/testbwsync")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class BwDataTestController : ControllerBase
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private readonly IBwSyncAdapter retrieveBwsyncAdapter;

        public BwDataTestController(IConfiguration configuration, ILoggerFactory loggerFactory, IBwSyncAdapter retrieveBwsyncAdapter)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<BwDataTestController>();
            this.retrieveBwsyncAdapter = retrieveBwsyncAdapter;
        }

        /// <summary>This endpoint returns the result of the association of the Cost Center and Organizational Units.</summary>

        //public ActionResult<IEnumerable<AssociationResult>> GetAssociation([FromQuery(Name = "organizational_code")] int? organizationCode, 
        // [FromQuery(Name = "start_date")] DateTime? startDate, [FromQuery(Name = "end_date")] DateTime? endDate)
        [HttpGet]
        public ActionResult<IEnumerable<BwMasterObject>> Test()
        {
            try
            {
                Task<List<BwMasterObject>> CostCenterListTask = retrieveBwsyncAdapter.GetBwMasterDatas(CancellationToken.None, "bSER", "actSEse99", "","EC","","");
                CostCenterListTask.Wait();
                List<BwMasterObject> costCentersList = CostCenterListTask.Result;

                if (CostCenterListTask.IsCompleted)
                {
                    return Ok(CostCenterListTask.Result);
                }
                else
                {
                    return StatusCode(200, "No Cost Center found."); 
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get association.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}