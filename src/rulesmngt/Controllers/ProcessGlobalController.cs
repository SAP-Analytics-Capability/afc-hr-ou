using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using rulesmngt.Interfaces;
using rulesmngt.Models;

namespace rulesmngt.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("rulesmngt/v1/processglobal")]
    public class ProcessGlobalController : ControllerBase
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private IProcessGlobalData processGlobalData;
        
        public ProcessGlobalController(IConfiguration configuration, ILoggerFactory loggerFactory, IProcessGlobalData processGlobalData)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<ProcessGlobalController>();
            this.processGlobalData = processGlobalData;
        }
			
        [HttpGet]
        public ActionResult<List<ProcessGlobal>> GetProcessGlobals()
        {
            List<ProcessGlobal> processGlobalList = processGlobalData.GetProcessGlobals();        
            return processGlobalList;
        }

       
        [HttpGet("getbyid/q")]
        public ActionResult<ProcessGlobal> GetProcessGlobal([FromQuery(Name = "id")]int processGlobalId)
        {
            ProcessGlobal processGlobal = processGlobalData.GetProcessGlobal(processGlobalId);
            return processGlobal;
        }
    }
}
