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
    [Route("rulesmngt/v1/sync")]
    public class SyncController : ControllerBase
    {
        private ILogger Logger;
        private ICatalogSync CatalogSync;
        
        public SyncController(IConfiguration configuration, ILoggerFactory loggerFactory, ICatalogSync catalogsync)
        {
            this.Logger = loggerFactory.CreateLogger<DummyController>();
            this.CatalogSync = catalogsync;
        }
       
        [HttpGet]
        public async Task<ActionResult> DoSync()
        {
            string companyCode = string.Empty;
            try
            {
                Task.Run(() => CatalogSync.Sync());
            }   
            catch (Exception e)
            {
                Logger.LogError("Unable to manually sync catalogs.", e);
            } 
            return Ok();
        }
    }
}
