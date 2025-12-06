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
using rulesmngt.Helpers;

namespace rulesmngt.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("rulesmngt/v1/companyscopes")]
    public class CompanyScopeController : ControllerBase
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private ICompanyScopeData CompanyRulesData;

        public CompanyScopeController(IConfiguration configuration, ILoggerFactory loggerFactory, ICompanyScopeData companyRulesData)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<CompanyScopeController>();
            this.CompanyRulesData = companyRulesData;
        }

        [HttpGet("getbydescper/q")]
        public ActionResult<List<CompanyRules>> GetCompanyRulesSapDescAfcDesc([FromQuery(Name = "sap_desc")]string sap, [FromQuery(Name = "afc_desc")]string perimeter)
        {
            List<CompanyRules> companyrules = new List<CompanyRules>();
            string errormessage = string.Empty;

            try
            {
                companyrules = ScopeConverter.ConvertFromScopes(CompanyRulesData.GetCompanyScopeBySAPCodeAndPerimeter(sap, perimeter), out errormessage);
                if (companyrules == null && !string.IsNullOrEmpty(errormessage))
                {
                    Logger.LogError(string.Format("{0} - Unable to get and/or compute conversion. Please check the following error: {1}", DateTime.Now, errormessage));
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting CompanyRulesController.GetCompanyRulesDesc. Message: {0}.", ex.Message);
            }

            return companyrules;
        }

        [HttpGet("getbydesc/q")]
        public ActionResult<List<CompanyRules>> GetCompanyRulesDesc([FromQuery(Name = "company_desc")]string sap)
        {
            List<CompanyRules> companyrules = new List<CompanyRules>();
            string errormessage = string.Empty;

            try
            {
                companyrules = ScopeConverter.ConvertFromScopes(CompanyRulesData.GetCompanyScopeBySAPCode(sap), out errormessage);
                if (companyrules == null && !string.IsNullOrEmpty(errormessage))
                {
                    Logger.LogError(string.Format("{0} - Unable to get and/or compute conversion. Please check the following error: {1}", DateTime.Now, errormessage));
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting CompanyRulesController.GetCompanyRulesDesc. Message: {0}.", ex.Message);
            }

            return companyrules;
        }

        [HttpGet("newprimodesc/q")]
        public ActionResult<List<CompanyRules>> GetCompanyRulesNewDesc([FromQuery(Name = "newprimo")]string sap)
        {
            List<CompanyRules> companyrules = new List<CompanyRules>();
            string errormessage = string.Empty;

            try
            {
                companyrules = ScopeConverter.ConvertFromScopes(CompanyRulesData.GetCompanyScopeByNewPrimoDesc(sap), out errormessage);
                if (companyrules == null && !string.IsNullOrEmpty(errormessage))
                {
                    Logger.LogError(string.Format("{0} - Unable to get and/or compute conversion. Please check the following error: {1}", DateTime.Now, errormessage));
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting CompanyRulesController.GetCompanyRulesDesc. Message: {0}.", ex.Message);
            }

            return companyrules;
        }
        [HttpGet("companies")]
        public ActionResult<List<CompanyScope>> GetAllCompanies()
        {
            List<CompanyScope> companies = new List<CompanyScope>();

            try
            {
                companies = CompanyRulesData.GetAllCompanies();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting CompanyRulesController.GetCompanyRulesDesc. Message: {0}.", ex.Message);
            }

            return companies;
        }
        [HttpGet("q")]
        public ActionResult<CompanyScope> GetCompanyById([FromQuery(Name = "id")] int id)
        {
            CompanyScope company = new CompanyScope();

            try
            {
                if (id > 0)
                {
                    // Int32.TryParse(companyId, out int id);
                    company = CompanyRulesData.GetCompanyById(id);
                    if (company == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("An error occurred while excuting ActivityMappingController.GetActivityById. Message: {0}.", ex.Message));
                return StatusCode(500, "Internal server error");
            }

            return company;
        }

        [HttpPost]
        public ActionResult InsertNewCompany([FromBody] CompanyScope cs)
        {
            CompanyScope result = null;
            try
            {
                if (cs != null)
                {
                    result = CompanyRulesData.AddCompanyScope(cs);
                    if (result != null)
                    {
                        return CreatedAtAction(nameof(GetCompanyById), new { id = cs.RecordId }, cs);
                    }
                    else
                    {
                        return StatusCode(500, "Internal server error");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("An error occurred while excuting ActivityMappingController.CreateActivityAssociation. Message: {0}.", ex.Message));
                return NoContent();
            }

        }
        [HttpPut("update/q")]
        public ActionResult<CompanyScope> UpdateCompany([FromQuery(Name = "id")] int id, [FromBody]CompanyScope company)
        {
            CompanyScope c = null;
            try
            {
                if (company == null || company.RecordId != id)
                {
                    Logger.LogError("company object sent from client is null.");
                    return BadRequest();
                }
                else
                {
                    c = CompanyRulesData.UpdateCompany(company);
                    if (c == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(c);
                    }
                    //return NoContent();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("remove/q")]
        public IActionResult DeleteCompany([FromQuery(Name = "id")] int id)
        {
            try
            {
                CompanyScope company = CompanyRulesData.GetCompanyById(id);
                if (company == null)
                {
                    Logger.LogError($"company with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    CompanyRulesData.DeleteCompany(company);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Something went wrong inside DeleteCompany action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("remove")]
        public IActionResult DeleteCompanies([FromBody] List<int> ids)
        {
            try
            {
                List<int> notremoved = new List<int>();

                foreach (int id in ids)
                {
                    CompanyScope company = CompanyRulesData.GetCompanyById(id);
                    if (company != null)
                    {
                        if (!CompanyRulesData.DeleteCompany(company))
                        {
                            notremoved.Add(id);
                        }
                    }
                    else
                    {
                        
                    }
                }

                if (notremoved != null && notremoved.Count > 0)
                {
                    return StatusCode(400, ids);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Something went wrong deleting activities", DateTime.Now));
                return StatusCode(500);
            }
            return Ok();
        }
    }
}