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
    [Route("rulesmngt/v1/area")]
    public class AreaController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private IAreaData areaData;


        public AreaController(IConfiguration configuration, ILoggerFactory loggerFactory, IAreaData areaData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<ActivityListController>();
            this.areaData = areaData;

        }
        // GET api/values
        [HttpGet]
        public ActionResult<List<Area>> GetAreas()
        {
            List<Area> areaList = areaData.GetAreas();        
            return areaList;
        }

        // GET api/values/5
        [HttpGet("getbyid/q")]
        public ActionResult<Area> GetAreaById([FromQuery(Name = "id")]string areaId)
        {
            Area area;

            try
            {
                int areaIdInt = Int32.Parse(areaId);
                area = areaData.GetAreaById(areaIdInt);
            }   
            catch (Exception e)
            {
                area = null;
                this.logger.LogError(e, "An error occurred while excuting AreaController.GetAreaById. Message: {0}.", e.Message);
            } 

            return area;
        }

         [HttpGet("getbycode/q")]
        public ActionResult<Area> GetAreaByCode([FromQuery(Name = "code")]string areaCode)
        {
            Area area;

            try
            {
                area = areaData.GetAreaByCode(areaCode);
            }   
            catch (Exception e)
            {
                area = null;
                this.logger.LogError(e, "An error occurred while excuting AreaController.GetAreaByCode. Message: {0}.", e.Message);
            } 

            return area;
        }
		
    }
}
