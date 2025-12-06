using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using masterdata.Models;
using masterdata.Interfaces.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using masterdata.Helpers;
using masterdata.Interfaces;
using masterdata.Models.SnowCostCenterResults;
using masterdata.Data;
using System.Threading;

namespace masterdata.Controllers
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("masterdata/v1/snow")]
    public class SnowAdatpterController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private readonly ISnowAdatpter snowCCUOAdapter;
        private readonly IResultData ResultData;
        private readonly IClientAccessData ClientAccessData;

        public SnowAdatpterController(IConfiguration configuration,
                                        ILoggerFactory loggerFactory,
                                        ISnowAdatpter snowCCUOAdapter,
                                        IClientAuthentication clientAuthentication,
                                        IResultData resultData,
                                        IClientAccessData clientaccessdata)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<SnowAdatpterController>();
            this.snowCCUOAdapter = snowCCUOAdapter;
            this.ResultData = resultData;
            this.ClientAccessData = clientaccessdata;
        }

        /// <summary>This endpoint triggers the call to Service Now endpoint and it gets the user confirmation about the association proposed by the computation.</summary>
        [HttpGet]
        public ActionResult<List<Result>> GetAggancioCC()
        {
            List<Result> snowresult = new List<Result>();
            try
            {
                DateTime lastaccess = ClientAccessData.AddNewAccess("OPREPORTING", "OREP");
                snowresult = ResultData.GetSnowResult(lastaccess);
                bool IsUpdated = ClientAccessData.UpdateAccess("OPREPORTING", "OREP");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get results from Service Now.");
                return StatusCode(500, "Internal Server Error");
            }
            return Ok(snowresult);
        }

        [HttpGet("q")]
        public ActionResult<ResultList> GetResultsByAssociationTime([FromQuery(Name = "date")]string associationTime)
        {
            ResultList resultlist = new ResultList();
            try
            {
                List<Result> results = ResultData.GetResultsByAssociationTime(associationTime);
                resultlist.results = results;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while getting information of the association between cost centers and organizational units.");
                return StatusCode(500, new Error(string.Format("Unable to get association. The endpoint returns with the following exception message: {0}", ex.Message)));

            }
            return Ok(resultlist);
        }

    }
}