using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using rulesmngt.Interfaces;
using rulesmngt.Models;

namespace rulesmngt.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("rulesmngt/v1/activitylist")]
    public class ActivityListController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private IActivityListData activityListData;
        private readonly IClientAuthentication ClientAuthentication;


        public ActivityListController(IConfiguration configuration, ILoggerFactory loggerFactory, IActivityListData activityListData, IClientAuthentication clientAuthentication)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<ActivityListController>();
            this.activityListData = activityListData;
            this.ClientAuthentication = clientAuthentication;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<ActivityList>> GetActivityLists()
        {
            List<ActivityList> activityList;
            
            try
            {
                activityList = activityListData.GetActivityLists(); 
            }   
            catch (Exception e)
            {
                activityList = null;
                this.logger.LogError(e, "An error occurred while excuting ActivityListController.GetActivityLists. Message: {0}.", e.Message);
            }   

            return activityList;
        }

        // GET api/values/5
        [HttpGet("getbyname/q")]
        public ActionResult<ActivityList> GetActivityListByName([FromQuery(Name = "name")]string activityName)
        {
            ActivityList activityList;

            try
            {
                activityList = activityListData.GetActivityListByName(activityName);
            }   
            catch (Exception e)
            {
                activityList = null;
                this.logger.LogError(e, "An error occurred while excuting ActivityListController.GetActivityListById. Message: {0}.", e.Message);
            } 

            return activityList;
        }

        [HttpGet("getbydesc/q")]
        public ActionResult<ActivityList> GetActivityListByDesc([FromQuery(Name = "desc")]string activityDesc)
        {
            ActivityList activityList;

            try
            {
                activityList = activityListData.GetActivityListByDesc(activityDesc);
            }   
            catch (Exception e)
            {
                activityList = null;
                this.logger.LogError(e, "An error occurred while excuting ActivityListController.GetActivityListByDesc. Message: {0}.", e.Message);
            } 

            return activityList;
        }

    }
}
