using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using rulesmngt.Data;
using rulesmngt.Interfaces;
using rulesmngt.Models;

namespace rulesmngt.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("rulesmngt/v1/businesslines")]
    public class BusinessLinesController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private IBusinessLinesData businessLinesData;
        private readonly IClientAuthentication ClientAuthentication;


        public BusinessLinesController(IConfiguration configuration, ILoggerFactory loggerFactory, IBusinessLinesData businessLinesData, IClientAuthentication clientAuthentication)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<ActivityListController>();
            this.businessLinesData = businessLinesData;
            this.ClientAuthentication = clientAuthentication;
        }
        
        [HttpGet]
        public ActionResult<List<BusinessLines>> GetBusinessLines()
        {
            List<BusinessLines> businessLinesList;

            try
            {                
                businessLinesList = businessLinesData.GetBusinessLines();   
            }
            catch (Exception e)
            {
                businessLinesList = null;
                this.logger.LogError(e, "An error occurred while excuting BusinessLinesController.GetBusinessLines. Message: {0}.", e.Message);
            }     

            return businessLinesList;
        }
       
        [HttpGet("getbyid/q")]
        public ActionResult<BusinessLines> GetBusinessLinesById([FromQuery(Name = "id")]string businessLinesId)
        {
            BusinessLines businessLines;

            try
            {
                int businessLinesIdInt = Int32.Parse(businessLinesId);
                businessLines = businessLinesData.GetBusinessLinesById(businessLinesIdInt);
            }   
            catch (Exception e)
            {
                businessLines = null;
                this.logger.LogError(e, "An error occurred while excuting BusinessLinesController.GetBusinessLinesCodeById. Message: {0}.", e.Message);
            } 

            return businessLines;
        }
         
        [HttpGet("getbyname/q")]
        public ActionResult<List<BusinessLines>> GetBusinessLinesByName([FromQuery(Name = "name")]string businessLinesName)
        {
            List<BusinessLines> businessLines;

            try
            {
                businessLines = businessLinesData.GetBusinessLinesByName(businessLinesName);
            }   
            catch (Exception e)
            {
                businessLines = null;
                this.logger.LogError(e, "An error occurred while excuting BusinessLinesController.GetByusinessLinesByDesc. Message: {0}.", e.Message);
            } 

            return businessLines;
        }
		
    }
}
