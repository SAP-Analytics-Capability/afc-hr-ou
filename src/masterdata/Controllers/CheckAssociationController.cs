using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using masterdata.Models;
using masterdata.Helpers;
using masterdata.Interfaces;
using masterdata.Interfaces.Adapters;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v1/checkAssociation")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CheckAssociationController : ControllerBase
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private readonly IClientAuthentication ClientAuthentication;
        private IResultData ResultData;

        public CheckAssociationController(IConfiguration configuration,
                                        ILoggerFactory loggerFactory,
                                        IClientAuthentication clientAuthentication,
                                        IResultData resultData)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<AssociationController>();
            this.ClientAuthentication = clientAuthentication;
            this.ResultData = resultData; 
        }

        [HttpGet("q")]
        public ActionResult<Response> CheckAssociation([FromQuery(Name = "ouc")] string oucode)
        {
            Response res = new Response();
            try
            {
                List<Result> results = ResultData.GetResultsByOUC(oucode);
                if(results.Count==0)
                {
                    res.message="insert";
                }
                else
                {
                    res.message="modifica";
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while getting information about association information");
                return StatusCode(500, new Error(string.Format("ou_code not found. The endpoint returns with the following exception message: {0}", ex.Message)));

            }

            return Ok(res);
        }

    }
}