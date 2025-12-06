using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using masterdata.Models;
using masterdata.Interfaces;

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v1/companies")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CompanyScopeController : ControllerBase
    {
        private readonly ILogger Logger;
        private readonly IClientAuthentication ClientAuthentication;
        private readonly ICompanyScopeAdapter _CompanyScopeAdapter;

        public CompanyScopeController(ILoggerFactory loggerFactory,
                                             IClientAuthentication clientAuthentication,
                                             ICompanyScopeAdapter companyScopeAdapter)
        {
            this.Logger = loggerFactory.CreateLogger<ActivityAssociationController>();
            this.ClientAuthentication = clientAuthentication;
            this._CompanyScopeAdapter = companyScopeAdapter;
        }

        [HttpGet]
        public ActionResult<List<CompanyScope>> GetAllCompanies()
        {
            List<CompanyScope> companies = new List<CompanyScope>();

            try
            {
                companies = _CompanyScopeAdapter.GetAllCompanies().Result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting CompanyRulesController.GetCompanyRulesDesc. Message: {0}.", ex.Message);
                return StatusCode(500, "Internal server error");
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
                    company = _CompanyScopeAdapter.GetCompanyById(id).Result;
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
                Logger.LogError(ex, string.Format("An error occurred while excuting GetCompanyById. Message: {0}.", ex.Message));
                return StatusCode(500, "Internal server error");
            }
            return company;
        }

        [HttpPost]
        public ActionResult<CompanyScope> InsertNewCompanyy([FromBody] CompanyScope cs)
        {
            CompanyScope company = new CompanyScope();
            try
            {
                if (cs != null)
                {
                    company = _CompanyScopeAdapter.InsertNewCompany(cs).Result;
                    if (company != null)
                    {
                        return company;
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
                Logger.LogError(ex, string.Format("An error occurred while excuting InsertNewCompany. Message: {0}.", ex.Message));
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("q")]
        public IActionResult UpdateCompany([FromQuery(Name = "id")] int id, [FromBody]CompanyScope cs)
        {
            CompanyScope company = new CompanyScope();
            try
            {
                if (cs == null || cs.RecordId!=id)
                {
                    Logger.LogError("activity_association object sent from client is null.");
                    return BadRequest();
                }
                else
                {
                    company = _CompanyScopeAdapter.UpdateCompany(cs, id).Result;
                    if (company == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(company);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Something went wrong inside UpdateCompany action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("q")]
        public IActionResult DeleteCompany([FromQuery(Name = "id")] int id)
        {
            try
            {
                bool wasDeleted = _CompanyScopeAdapter.DeleteCompany(id).Result;
                if (wasDeleted)
                    return Ok($"The company with ID: {id} was deleted successfully");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Something went wrong inside DeleteCompany action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}