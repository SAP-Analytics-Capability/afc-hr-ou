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
    [Route("rulesmngt/v1/responsability")]
    public class ResonsabilityController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private IResponsability responsabilityData;


        public ResonsabilityController(IConfiguration configuration, ILoggerFactory loggerFactory, IResponsability responsabilityData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<ActivityListController>();
            this.responsabilityData = responsabilityData;

        }
        // GET api/values
        [HttpGet]
        public ActionResult<List<Responsability>> GetResponsability()
        {
            List<Responsability> ResponsabilityList = responsabilityData.GetResponsability();        
            return ResponsabilityList;
        }

        // GET api/values/5
        [HttpGet("getbynewprimocode/q")]
        public ActionResult<List<Responsability>> GetByNewprimoCode([FromQuery(Name = "new_primo")]string newPrimoCode)
        {
            List<Responsability> responsability;

            try
            {
                
                responsability = responsabilityData.GetResponsabilityByNewPrimo(newPrimoCode);
            }   
            catch (Exception e)
            {
                responsability = null;
                this.logger.LogError(e, "An error occurred while excuting AreaController.GetAreaById. Message: {0}.", e.Message);
            } 

            return responsability;
        }
    }
}
