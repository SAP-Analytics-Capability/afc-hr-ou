using System;
using System.Collections.Generic;
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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using masterdata.Models.SnowCostCenterResults;
using masterdata.Models.Configuration;

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v1/elaboration")]
    public class ElaborationController : ControllerBase
    {
        private readonly ILogger Logger;
        private IElaborationService ElaborationService;

        public ElaborationController( ILoggerFactory loggerFactory, IElaborationService elaborationService)
        {
            this.Logger = loggerFactory.CreateLogger<ElaborationController>();
            this.ElaborationService = elaborationService;
        }

        [HttpGet]
        public async Task<ActionResult> MakeElaboration()
        {
            try
            {
                Task.Run(() => ElaborationService.MakeElaboration());
                return Ok("Elaboration is in progress...");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to make elaboration.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}