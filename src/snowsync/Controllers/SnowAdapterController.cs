using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using snowsync.Interfaces;
using snowsync.Models;

namespace snowsync.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("snowsync/v1/associations")]
    public class SnowAdapterController : Controller
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private readonly ISnowAdatpter SnowAdapter;
        
        public SnowAdapterController(IConfiguration configuration, ILoggerFactory loggerFactory, ISnowAdatpter snowadapter)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<SnowAdapterController>();
            this.SnowAdapter = snowadapter;
        }

        [HttpPost]
        public ActionResult Post([FromBody]string body)
        {   
            return Ok("request received");
        }

        [HttpPut("{id}")]
        //[HttpPut]
        //public ActionResult Put([FromBody]string body)
        public ActionResult Put(int id, [FromBody]string body)
        {
            return Ok("request received for object "+id);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok("request received for object "+id);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RootObject>> GetCostCenterAssociation()
        {
            RootObject resultCC = new RootObject();
            try
            {
                Task<RootObject> resultCCTask = SnowAdapter.getSnowCCUOResults(CancellationToken.None);
                resultCCTask.Wait();

                if (resultCCTask.IsCompleted)
                {
                    resultCC = resultCCTask.Result;
                    if (resultCC != null)
                    {
                        return Ok(resultCC);
                    }
                    else
                    {
                        return StatusCode(200, "No Cost Center found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get results from Service Now.");
                return StatusCode(500, "Internal Server Error");
            }
            return StatusCode(500);
        }
    }
}