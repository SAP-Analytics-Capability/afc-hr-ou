using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using masterdata.Models;
using masterdata.Interfaces;
using masterdata.Helpers.Adapters;

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v1/catalog")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger Logger;
        private ICatalogManagementAdapter CatalogAdapter;

        public CatalogController(ILoggerFactory loggerFactory, ICatalogManagementAdapter catalogadapter)
        {
            this.Logger = loggerFactory.CreateLogger<CatalogController>();
            this.CatalogAdapter = catalogadapter;
        }

        [HttpGet("activities")]
        public ActionResult<ActivityAssociation> GetActivities()
        {
            try
            {
                Task<List<ActivityAssociation>> task = CatalogAdapter.GetActivityAssociationCatalog(CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the activity catalog.");
                return StatusCode(500);
            }

            return NotFound();
        }

        [HttpGet("activities/q")]
        public ActionResult<ActivityAssociation> GetActivityByID([FromQuery(Name = "id")] int id)
        {
            try
            {
                Task<ActivityAssociation> task = CatalogAdapter.GetActivityAssociationByID(id, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the activity.");
                return StatusCode(500);
            }

            return NotFound();
        }

        [HttpPost("activities/add/item")]
        public ActionResult<CatalogManager> AddActivity([FromBody] ActivityAssociation activity)
        {
            try
            {
                Task<CatalogManager> task = CatalogAdapter.AddActivty(activity, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the activity.");
                return StatusCode(500);
            }

            return NotFound();
        }

        [HttpPost("activities/add/items")]
        public ActionResult<CatalogManager> AddActivities([FromBody] List<ActivityAssociation> activities)
        {
            try
            {
                Task<List<CatalogManager>> task = CatalogAdapter.AddActivties(activities, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the company.");
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost("activity/update/q")]
        public ActionResult<CatalogManager> UpdateActivity([FromQuery(Name = "id")] int id, [FromBody] ActivityAssociation activitiy)
        {
            try
            {
                Task<CatalogManager> task = CatalogAdapter.UpdateActivity(id, activitiy, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the activity.");
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost("activities/remove")]
        public ActionResult<List<CatalogManager>> RemovesActivities([FromBody] List<int> ids)
        {
            try
            {
                Task<List<CatalogManager>> task = CatalogAdapter.RemovesActivities(ids, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the activity.");
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost("activity/remove/q")]
        public ActionResult<CatalogManager> RemoveActivity([FromQuery(Name = "id")] int id)
        {
            try
            {
                Task<CatalogManager> task = CatalogAdapter.RemoveActivity(id, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the activity.");
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet("companies")]
        public ActionResult<CompanyScope> GetCompanies()
        {
            try
            {
                Task<List<CompanyScope>> task = CatalogAdapter.GetCompanyScopeCatalog(CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the comapany scope.");
                return StatusCode(500);
            }

            return NotFound();
        }

        [HttpGet("company/q")]
        public ActionResult<CompanyScope> GetCompanyByID([FromQuery(Name = "id")] int id)
        {
            try
            {
                Task<CompanyScope> task = CatalogAdapter.GetCompanyByID(id, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the company.");
                return StatusCode(500);
            }

            return NotFound();
        }

        [HttpPost("company/add/item")]
        public ActionResult<CatalogManager> AddCompany([FromBody] CompanyScope company)
        {
            try
            {
                //CompanyScope company = 
                Task<CatalogManager> task = CatalogAdapter.AddCompany(company, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the company.");
                return StatusCode(500);
            }

            return NotFound();
        }

        [HttpPost("company/add/items")]
        public ActionResult<CatalogManager> AddCompanies([FromBody] List<CompanyScope> companies)
        {
            try
            {
                Task<List<CatalogManager>> task = CatalogAdapter.AddCompanies(companies, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the company.");
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost("company/update/item/q")]
        public ActionResult<CatalogManager> UpdateCompany([FromQuery(Name = "id")] int id, [FromBody] CompanyScope company)
        {
            try
            {
                Task<CatalogManager> task = CatalogAdapter.UpdateCompany(id, company, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the company.");
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost("company/remove/item/q")]
        public ActionResult<CatalogManager> RemoveCompany([FromQuery(Name = "id")] int id)
        {
            try
            {
                Task<CatalogManager> task = CatalogAdapter.RemoveCompany(id, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the company.");
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost("company/remove/items")]
        public ActionResult<CatalogManager> RemovesCompanies([FromBody] List<int> ids)
        {
            try
            {
                Task<List<CatalogManager>> task = CatalogAdapter.RemovesCompanies(ids, CancellationToken.None);
                task.Wait();

                if (task.IsCompleted)
                {
                    if (task.Result != null)
                    {
                        return Ok(task.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get the company.");
                return StatusCode(500);
            }

            return Ok();
        }
    }
}