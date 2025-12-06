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
    [Route("rulesmngt/v1/companyrules")]
    public class CompanyRulesController : ControllerBase
    {

        private IConfiguration Configuration;
        private ILogger logger;
        private ICompanyRulesData companyRulesDataData;
        
        public CompanyRulesController(IConfiguration configuration, ILoggerFactory loggerFactory, ICompanyRulesData companyRulesDataData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<CompanyRulesController>();
            this.companyRulesDataData = companyRulesDataData;
        }

        [HttpGet]
        public ActionResult<List<CompanyRules>> GetCompanyRules()
        {
            List<CompanyRules> companuRules = companyRulesDataData.GetCompanies();        
            return companuRules;
        }
       
        [HttpGet("getbyid/q")]
        public ActionResult<CompanyRules> GetCompanyRulesById([FromQuery(Name = "id")]string companyRulesId)
        {
            CompanyRules companyRules;

            try
            {
                int id = Int32.Parse(companyRulesId);
                companyRules = companyRulesDataData.GetCompanyById(id);
            }
            catch (Exception e)
            {
                companyRules = null;
                this.logger.LogError(e, "An error occurred while excuting CompanyRulesController.GetCompanyRulesById. Message: {0}.", e.Message);
            }

            return companyRules;
        }

        [HttpGet("getbydescper/q")]
        public ActionResult<List<CompanyRules>> GetCompanyRulesSapDescAfcDesc([FromQuery(Name = "sap_desc")]string companyRulesDesc,[FromQuery(Name = "afc_desc")]string perimeter)
        {
            List<CompanyRules> companyRules;

            try
            {
                companyRules = companyRulesDataData.GetCompanyRulesSapDescAfcDesc(companyRulesDesc,perimeter);
            }   
            catch (Exception e)
            {
                companyRules = null;
                this.logger.LogError(e, "An error occurred while excuting CompanyRulesController.GetCompanyRulesDesc. Message: {0}.", e.Message);
            } 

            return companyRules;
        }

        [HttpGet("getbydesc/q")]
        public ActionResult<List<CompanyRules>> GetCompanyRulesDesc([FromQuery(Name = "company_desc")]string companyRulesDesc)
        {
            List<CompanyRules> companyRules;

            try
            {
                companyRules = companyRulesDataData.GetCompanyRulesDesc(companyRulesDesc);
            }   
            catch (Exception e)
            {
                companyRules = null;
                this.logger.LogError(e, "An error occurred while excuting CompanyRulesController.GetCompanyRulesDesc. Message: {0}.", e.Message);
            } 

            return companyRules;
        }

       
        [HttpGet("newprimodesc/q")]
        public ActionResult<List<CompanyRules>> GetCompanyByNewPrimoDesc([FromQuery(Name = "newprimo")]string newprimo)
        {
            List<CompanyRules> companyRules;

            try
            {
                companyRules = companyRulesDataData.GetCompanyByNewPrimoDesc(newprimo);
            }   
            catch (Exception e)
            {
                companyRules = null;
                this.logger.LogError(e, "An error occurred while excuting CompanyRulesController.GetCompanyByNewPrimoCode. Message: {0}.", e.Message);
            } 

            return companyRules;
        }

        [HttpGet("newprimosapcod/q")]
        public ActionResult<List<CompanyRules>> GetCompanyByNewPrimoSapHRCode([FromQuery(Name = "newprimosap")]string newprimoSap)
        {
            List<CompanyRules> companyRules;

            try
            {
                companyRules = companyRulesDataData.GetCompanyByNewPrimoSapHRCode(newprimoSap);
            }   
            catch (Exception e)
            {
                companyRules = null;
                this.logger.LogError(e, "An error occurred while excuting CompanyRulesController.GetCompanyByNewPrimoSapHRCode. Message: {0}.", e.Message);
            } 

            return companyRules;
        }
		
    }
}
