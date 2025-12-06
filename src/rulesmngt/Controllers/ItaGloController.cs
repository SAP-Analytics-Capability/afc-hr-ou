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
    [Route("rulesmngt/v1/itaglo")]
    public class ItaGloController : ControllerBase
    {

        private IConfiguration Configuration;
        private ILogger logger;
        private IItaGloData itaGloData;

        public ItaGloController(IConfiguration configuration, ILoggerFactory loggerFactory, IItaGloData itaGloData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<IEntityData>();
            this.itaGloData = itaGloData;
        }

        [HttpGet]
        public ActionResult<List<ItaGlo>> GetItaGlos()
        {
            List<ItaGlo> itaGlos;

            try
            {
                itaGlos = itaGloData.GetItaGlos();
            }
            catch (Exception e)
            {
                itaGlos = null;
                this.logger.LogError(e, "An error occurred while excuting EntityController.GetEntities. Message: {0}.", e.Message);
            }

            return itaGlos;
        }

        [HttpGet("getitaglo/q")]
        public ActionResult<ItaGlo> GetEntityByNewPrimo([FromQuery(Name = "saphrcode")]string sapHrCode)
        {
            ItaGlo itaGlo;

            try
            {
                itaGlo = itaGloData.GetItaGloByCode(sapHrCode);
            }   
            catch (Exception e)
            {
                itaGlo = null;
                this.logger.LogError(e, "An error occurred while excuting EntityController.GetEntityByName. Message: {0}.", e.Message);
            } 
            
            return itaGlo;
        }
    }
}