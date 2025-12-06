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
    [Route("rulesmngt/v1/DummyController")]
    public class DummyController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private ICompanyScopeData _CompanyData;
        
        public DummyController(IConfiguration configuration, ILoggerFactory loggerFactory, ICompanyScopeData companyData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<DummyController>();
            this._CompanyData = companyData;
        }
       
        [HttpGet("q")]
        public ActionResult<string> GetCompany([FromQuery(Name = "entity")]string entity)
        {
            string companyCode = string.Empty;
            try
            {
                companyCode = _CompanyData.GetCompanyByGlobalCode(entity);
            }   
            catch (Exception e)
            {
                this.logger.LogError(e, "An error occurred while excuting DummyController, Message: {0}.", e.Message);
            } 
            return companyCode;
        }
    }
}
