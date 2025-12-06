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
    [Route("masterdata/v1/activities")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ActivityAssociationController : ControllerBase
    {
        private readonly ILogger Logger;
        private readonly IClientAuthentication ClientAuthentication;
        private readonly IActivityAssociationAdapter _ActivityAdapter;

        public ActivityAssociationController(ILoggerFactory loggerFactory,
                                             IClientAuthentication clientAuthentication,
                                             IResultData resultData,
                                             IActivityAssociationAdapter activityAdapter)
        {
            this.Logger = loggerFactory.CreateLogger<ActivityAssociationController>();
            this.ClientAuthentication = clientAuthentication;
            this._ActivityAdapter = activityAdapter;
        }

        [HttpGet]
        public ActionResult<List<ActivityAssociation>> GetAllActivities()
        {
            List<ActivityAssociation> activities = new List<ActivityAssociation>();

            try
            {
                activities = _ActivityAdapter.GetAllActivityAssociation().Result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting CompanyRulesController.GetCompanyRulesDesc. Message: {0}.", ex.Message);
            }

            return activities;
        }
        [HttpGet("q")]
        public ActionResult<ActivityAssociation> GetActivityById([FromQuery(Name = "id")] int id)
        {
            ActivityAssociation activity = new ActivityAssociation();

            try
            {
                if (id > 0)
                {
                    activity = _ActivityAdapter.GetSingleActivityAssociation(id).Result;
                    if (activity == null)
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
                return NotFound();
            }

            return activity;
        }

        [HttpPost]
        public ActionResult<ActivityAssociation> InsertNewActivity([FromBody] ActivityAssociation aa)
        {
            ActivityAssociation activity = new ActivityAssociation();
            try
            {
                if (aa != null)
                {
                    aa.BpcCode = aa.PoBpcCode;
                    aa.BpcDesc = aa.PoDescription;
                    aa.BpcObjectId = aa.PoObjectId;
                    aa.BpcObjectAbbr = aa.PoObjectAbbr;
                    activity = _ActivityAdapter.PostActivityAssociation(aa).Result;
                    
                                        if (activity != null)
                    {
                        return activity;
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
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("q")]
        public IActionResult UpdateActivity([FromQuery(Name = "id")] int id, [FromBody]ActivityAssociation activity)
        {
            try
            {
                if (activity == null || activity.RecordId!=id)
                {
                    Logger.LogError("activity_association object sent from client is null.");
                    return BadRequest();
                }
                else
                {
                    activity.BpcCode = activity.PoBpcCode;
                    activity.BpcDesc = activity.PoDescription;
                    activity.BpcObjectId = activity.PoObjectId;
                    activity.BpcObjectAbbr = activity.PoObjectAbbr;
                    activity = _ActivityAdapter.PutActivityAssociation(activity, id).Result;
                    if (activity==null)
                    {
                           return NotFound();
                    }
                    else
                    {
                        return Ok(activity);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("q")]
        public IActionResult DeleteActivity([FromQuery(Name = "id")] int id)
        {
            try
            {
                bool wasDeleted = _ActivityAdapter.DeleteActivityAssociation(id).Result;
                if (wasDeleted)
                    return Ok($"The activity with ID: {id} was deleted successfully");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Something went wrong inside DeleteActivity action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}