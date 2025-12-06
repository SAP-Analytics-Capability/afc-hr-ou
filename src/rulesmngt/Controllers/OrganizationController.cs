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
    [Route("rulesmngt/v1/organization")]
    public class OrganizationController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private IOrganizationData organizationData;
        
        public OrganizationController(IConfiguration configuration, ILoggerFactory loggerFactory, IOrganizationData organizationData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<OrganizationController>();
            this.organizationData = organizationData;
        }


        [HttpGet]
        public ActionResult<List<Organization>> GetOrganizations()
        {
            List<Organization> organizations;

            try
            {
                organizations = organizationData.GetOrganizations(); 
            }
            catch (Exception e)
            {
                organizations = null;
                this.logger.LogError(e, "An error occurred while excuting OrganizationController.GetOrganizations. Message: {0}.", e.Message);
            }

            return organizations;
        }

       
        [HttpGet("getbyid/q")]
        public ActionResult<Organization> GetOrganizationById([FromQuery(Name = "id")]string organizationId)
        {
            Organization organization;

            try
            {
                int organizationIdInt = Int32.Parse(organizationId);
                organization = organizationData.GetOrganizationById(organizationIdInt);
            }   
            catch (Exception e)
            {
                organization = null;
                this.logger.LogError(e, "An error occurred while excuting OrganizationController.GetOrganizationById. Message: {0}.", e.Message);
            }

            return organization;
        }

        [HttpGet("getbycode/q")]
        public ActionResult<Organization> GetOrganizationByCode([FromQuery(Name = "code")]string organizationCode)
        {
            Organization organization;

            try
            {
                organization = organizationData.GetOrganizationByCode(organizationCode);
            }   
            catch (Exception e)
            {
                organization = null;
                this.logger.LogError(e, "An error occurred while excuting OrganizationController.GetOrganizationByDesc. Message: {0}.", e.Message);
            }

            return organization;
        }

        [HttpGet("getbydesc/q")]
        public ActionResult<Organization> GetOrganizationByDesc([FromQuery(Name = "desc")]string organizationDesc)
        {
            Organization organization;

            try
            {
                organization = organizationData.GetOrganizationByDesc(organizationDesc);
            }   
            catch (Exception e)
            {
                organization = null;
                this.logger.LogError(e, "An error occurred while excuting OrganizationController.GetOrganizationByDesc. Message: {0}.", e.Message);
            }

            return organization;
        }
		
    }
}
