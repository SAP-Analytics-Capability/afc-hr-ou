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
    [Route("rulesmngt/v1/entity")]
    public class EntityController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private IEntityData entityData;
        
        public EntityController(IConfiguration configuration, ILoggerFactory loggerFactory, IEntityData entityData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<IEntityData>();
            this.entityData = entityData;
        }

        [HttpGet]
        public ActionResult<List<Entity>> GetEntities()
        {
            List<Entity> entities;

            try
            {
                entities = entityData.GetEntities();
            }
            catch (Exception e)
            {
                entities = null;
                this.logger.LogError(e, "An error occurred while excuting EntityController.GetEntities. Message: {0}.", e.Message);
            }
                   
            return entities;
        }

       
        [HttpGet("getbynewprimocode/q")]
        public ActionResult<Entity> GetEntityByNewPrimo([FromQuery(Name = "newprimocode")]string entityCode)
        {
            Entity entity;

            try
            {
                entity = entityData.GetEntityByCode(entityCode);
            }   
            catch (Exception e)
            {
                entity = null;
                this.logger.LogError(e, "An error occurred while excuting EntityController.GetEntityByName. Message: {0}.", e.Message);
            } 
            
            return entity;
        }

        [HttpGet("getbycode/q")]
        public ActionResult<Entity> GetEntityByCode([FromQuery(Name = "code")]string entityCode)
        {
            Entity entity;

            try
            {
                entity = entityData.GetEntityByCode(entityCode);
            }   
            catch (Exception e)
            {
                entity = null;
                this.logger.LogError(e, "An error occurred while excuting EntityController.GetEntityByCode. Message: {0}.", e.Message);
            } 

            return entity;
        }

        [HttpGet("getbygbl/q")]
        public ActionResult<List<Entity>> GetEntityByGbl([FromQuery(Name = "gbl")]string entityGbl)
        {
            List<Entity> entity;

            try
            {
                entity = entityData.GetEntityByGbl(entityGbl);
            }   
            catch (Exception e)
            {
                entity = null;
                this.logger.LogError(e, "An error occurred while excuting EntityController.GetEntityByGbl. Message: {0}.", e.Message);
            } 

            return entity;
        }

    }
}
