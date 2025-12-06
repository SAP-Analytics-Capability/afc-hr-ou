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
    [Route("rulesmngt/v1/activitymapping")]
    public class ActivityMappingController : ControllerBase
    {

        private IConfiguration Configuration;
        private ILogger logger;
        private IActivityMappingData activityMappingData;


        public ActivityMappingController(IConfiguration configuration, ILoggerFactory loggerFactory, IActivityMappingData activityMappingData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<ActivityListController>();
            this.activityMappingData = activityMappingData;

        }


        [HttpGet]
        public ActionResult<List<ActivityMapping>> GetActivityMappings()
        {
            List<ActivityMapping> activityMappings;

            try
            {
                activityMappings = activityMappingData.GetActivityMappings();
            }
            catch (Exception e)
            {
                activityMappings = null;
                this.logger.LogError(e, "An error occurred while excuting ActivityMappingController.GetActivityMappings. Message: {0}.", e.Message);
            }

            return activityMappings;
        }

        [HttpGet("getbyid/q")]
        public ActionResult<ActivityMapping> GetActivityMappingById([FromQuery(Name = "id")]string activityId)
        {
            ActivityMapping activityMapping;

            try
            {
                activityMapping = activityMappingData.GetActivityMapping(activityId);
            }
            catch (Exception e)
            {
                activityMapping = null;
                this.logger.LogError(e, "An error occurred while excuting ActivityMappingController.GetActivityMappingById. Message: {0}.", e.Message);
            }

            return activityMapping;
        }

        [HttpGet("getbyobjtype/q")]
        public ActionResult<List<ActivityMapping>> GetActivityMappingByCodeEPrev([FromQuery(Name = "objectid")]string objectId,
                                                                                 [FromQuery(Name = "typecode")]string typeCode)
        {
            List<ActivityMapping> activityMappings;

            try
            {
                activityMappings = activityMappingData.GetActivityMappingByCodeEPrev(objectId, typeCode);
            }
            catch (Exception e)
            {
                activityMappings = null;
                this.logger.LogError(e, "An error occurred while excuting ActivityMappingController.GetActivityMappingByBpcDesc. Message: {0}.", e.Message);
            }

            return activityMappings;
        }

    }
}
