using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Globalization;
using masterdata.Models;
using masterdata.Helpers;
using masterdata.Interfaces;
using masterdata.Interfaces.Adapters;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Controllers
{
    [Route("masterdata/v1/status")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class StatusController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;

        public StatusController(IConfiguration configuration,
                                        ILoggerFactory loggerFactory)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<AssociationController>();
        }

        [HttpGet]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<Status> GetStatus()
        {
            Status s = new Status();
            s.AccessTimestamp ="";
            s.AccessType="A";
            s.Caller="b";
            s.ExitTimestap="cc";
            s.ProcessStatus="I";
            return Ok(s);
        }
    }
}