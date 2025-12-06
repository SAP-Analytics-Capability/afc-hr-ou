using System;
using System.Collections.Generic;

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
    [Route("rulesmngt/v1/activityassociations")]
    public class ActivityAssociationContoller : ControllerBase
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private IActivityAssociationData ActivityAssociationData;

        public ActivityAssociationContoller(IConfiguration configuration,
                                            ILoggerFactory loggerfactory,
                                            IActivityAssociationData activityassociationdata)
        {
            this.Configuration = configuration;
            this.Logger = loggerfactory.CreateLogger<ActivityAssociationContoller>();
            this.ActivityAssociationData = activityassociationdata;
        }

        [HttpGet]
        public ActionResult<List<ActivityMapping>> GetActivityAssociation()
        {
            List<ActivityMapping> activitymappinglist = new List<ActivityMapping>();
            string errormessage = string.Empty;

            try
            {
                activitymappinglist = ActivityConverter.ConvertFromAssociations(ActivityAssociationData.GetActivityAssociations(), out errormessage);
                if (activitymappinglist == null && !string.IsNullOrEmpty(errormessage))
                {
                    Logger.LogError(string.Format("{0} - Unable to get and/or compute conversion. Please check the following error: {1}", DateTime.Now, errormessage));
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get the full list of activity associations.", DateTime.Now));
            }

            return activitymappinglist;
        }

        [HttpGet("getbybpcdesc/q")]
        public ActionResult<List<ActivityMapping>> GetActivityAssociationByBCPDesc([FromQuery(Name = "bpcdesc")]string description)
        {
            List<ActivityMapping> activitymappinglist = new List<ActivityMapping>();
            string errormessage = string.Empty;
            if (description.Contains("ecommercial"))
                description = description.Replace("ecommercial", "&");

            try
            {
                activitymappinglist = ActivityConverter.ConvertFromAssociations(ActivityAssociationData.GetActivityAssociationsByBPC(description), out errormessage);
                if (activitymappinglist == null && !string.IsNullOrEmpty(errormessage))
                {
                    Logger.LogError(string.Format("{0} - Unable to get and/or compute conversion. Please check the following error: {1}", DateTime.Now, errormessage));
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get the full list of activity associations.", DateTime.Now));
            }

            return activitymappinglist;
        }


        [HttpGet("getbyobjtype/q")]
        public ActionResult<List<ActivityMapping>> GetActivityMappingByCodeEPrev([FromQuery(Name = "objectid")]string objectId,
                                                                                 [FromQuery(Name = "typecode")]string typeCode)
        {
            List<ActivityMapping> activityMappings = null;
            string errormessage = string.Empty;

            try
            {
                activityMappings = ActivityConverter.ConvertFromAssociations(ActivityAssociationData.GetActivityAssociationsByCodeEPrev(objectId, typeCode), out errormessage);
            }
            catch (Exception ex)
            {
                activityMappings = null;
                Logger.LogError(ex, string.Format("An error occurred while excuting ActivityMappingController.GetActivityMappingByBpcDesc. Message: {0}.", ex.Message));
            }

            return activityMappings;
        }

        [HttpGet("getgroup/q")]
        public ActionResult<List<ActivityAssociationGroup>> GetGroup([FromQuery(Name = "objectid")]string objectId,
                                                                    [FromQuery(Name = "typecode")]string typeCode)
        {
            List<ActivityAssociation> activitymappings = null;
            List<ActivityAssociationGroup> grouplist = new List<ActivityAssociationGroup>();
            List<String> groupMacroOrg1 = new List<string>();

            try
            {
                activitymappings = ActivityAssociationData.GetActivityAssociationsByCodeEPrev(objectId, typeCode);

                if (activitymappings == null)
                {
                    Logger.LogError(string.Format("Unable to get activity mappings."));
                }
                else
                {
                    if (activitymappings.Count > 0)
                    {
                        foreach (ActivityAssociation item in activitymappings)
                        {
                            /*
                             * il raggruppamento avviene solo su afc_macroorg1, ma è soddisfatto anche su typology_object, bpc_code , po_object_abbr
                             * per ogni coppia po_object_id, object_type_id
                             * Questa query lo dimostra:
                               select f.po_object_id, f.object_type_id, f.typology_object , f.afc_macroorg1 , f.bpc_code , f.po_object_abbr
                                , count(0)
                                from
                                (
                                select a.po_object_id, a.object_type_id, a.typology_object , a.afc_macroorg1 , a.bpc_code , a.po_object_abbr
                                from 
                                afchr_int_schema.activityassociation a
                                group by
                                a.po_object_id, a.object_type_id, a.typology_object , a.afc_macroorg1 , a.bpc_code , a.po_object_abbr
                                ) f
                                group by f.po_object_id, f.object_type_id, f.typology_object , f.afc_macroorg1 , f.bpc_code , f.po_object_abbr
                                 having count(0) > 1
                             */
                            if (!grouplist.Exists(x => x.MacroOrg1 == item.AfcMacroorg1) )
                            {
                                ActivityAssociationGroup groupitem = new ActivityAssociationGroup();
                                groupitem.TypologyObject = item.TypologyObject;
                                groupitem.BpcCode = item.BpcCode;
                                groupitem.MacroOrg1 = item.AfcMacroorg1;
                                groupitem.PoObjectAbbr = item.PoObjectAbbr;

                                grouplist.Add(groupitem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("An error occurred while excuting ActivityMappingController.GetActivityMappingByBpcDesc. Message: {0}.", ex.Message));
            }

            return grouplist;
        }

        [HttpGet("activities")]
        public ActionResult<List<ActivityAssociation>> GetAllActivities()
        {
            List<ActivityAssociation> activities = new List<ActivityAssociation>();

            try
            {
                activities = ActivityAssociationData.GetAllActivities();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{0} - An error occurred while excuting CompanyRulesController.GetCompanyRulesDesc", DateTime.Now);
            }

            return activities;
        }

        [HttpGet("q")]
        public ActionResult<ActivityAssociation> GetActivityById([FromQuery(Name = "id")] int id)
        {
            ActivityAssociation activity = new ActivityAssociation();

            try
            {
                if (id != 0)
                {
                    activity = ActivityAssociationData.GetActivityById(id);
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
                return StatusCode(500, "Internal server error");
            }

            return activity;
        }

        [HttpPost("add/item")]
        public ActionResult<CatalogManager> InsertNewActivity([FromBody] ActivityAssociation aa)
        {
            ActivityAssociation result = null;
            try
            {
                if (aa != null)
                {
                    aa.BpcCode = aa.PoBpcCode;
                    aa.BpcDesc = aa.PoDescription;
                    aa.BpcObjectId = aa.PoObjectId;
                    aa.BpcObjectAbbr = aa.PoObjectAbbr;
                    result = ActivityAssociationData.AddActivityAssociation(aa);

                    if (result != null)
                    {
                        return CreatedAtAction(nameof(GetActivityById), new { id = aa.RecordId }, aa);
                    }
                    else
                    {
                        return StatusCode(500);
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

        [HttpPost("add/items")]
        public ActionResult InsertNewActivities([FromBody] List<ActivityAssociation> list)
        {
            if (list != null && list.Count > 0)
            {
                try
                {
                    List<CatalogManager> result = new List<CatalogManager>();
                    int i = 0;

                    foreach (ActivityAssociation aa in list)
                    {
                        ActivityAssociation single = null;
                        if (aa != null)
                        {
                            aa.BpcCode = aa.PoBpcCode;
                            aa.BpcDesc = aa.PoDescription;
                            aa.BpcObjectId = aa.PoObjectId;
                            aa.BpcObjectAbbr = aa.PoObjectAbbr;

                            if ((single = ActivityAssociationData.AddActivityAssociation(aa)) == null)
                            {
                                result.Add(new CatalogManager(++i));
                            }
                        }
                    }

                    if (result != null && result.Count > 0)
                    {
                        StatusCode(404, result);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, string.Format("{0} - An error occurred while adding new items.", DateTime.Now));
                    return StatusCode(500);
                }
                return Ok();
            }
            else
            {
                return StatusCode(204);
            }
        }

        [HttpPut("update/q")]
        public IActionResult UpdateActivity([FromQuery(Name = "id")] int id, [FromBody]ActivityAssociation activity)
        {
            ActivityAssociation a = null;
            try
            {
                if (activity == null || activity.RecordId != id)
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
                    a = ActivityAssociationData.UpdateActivity(activity);
                    if (a == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(a);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("remove/q")]
        public IActionResult DeleteActivity([FromQuery(Name = "id")]int id)
        {
            try
            {
                ActivityAssociation activity = ActivityAssociationData.GetActivityById(id);
                if (activity != null && id !=0)
                {
                    if (!ActivityAssociationData.DeleteActivity(activity))
                    {
                        return StatusCode(400);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Something went wrong deleting activity with id {0}", DateTime.Now, id));
                return StatusCode(500, ex.ToString());
            }
            return Ok();
        }

        [HttpPost("remove")]
        public IActionResult DeleteActivities([FromBody] List<int> ids)
        {
            try
            {
                List<int> notremoved = new List<int>();
                if (ids != null)
                {
                    foreach (int id in ids)
                    {
                        ActivityAssociation activity = ActivityAssociationData.GetActivityById(id);
                        if (activity != null)
                        {
                            if (!ActivityAssociationData.DeleteActivity(activity))
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