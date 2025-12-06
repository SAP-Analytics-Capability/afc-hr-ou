using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class ActivityListData : IActivityListData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public ActivityListData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertActivityList(ActivityList activityList)
        {
            //TODO
        }

        public List<ActivityList> GetActivityLists()
        {
            List<ActivityList> activityList;

            try
            {            
                using (var context = new RulesContext(databaseConfiguration))
                {
                    activityList = (from al in context.ActivityList select al).ToList<ActivityList>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetActivityLists: " + e.Message );
            }

            return activityList;
        }

        public ActivityList GetActivityListByName(string activityListName)
        {
            ActivityList activityList;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    activityList = (from al in context.ActivityList 
                                    where string.Equals(al.ActivityName, activityListName, 
                                                        StringComparison.OrdinalIgnoreCase)
                                    select al).SingleOrDefault<ActivityList>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetActivityListById: " + e.Message );
            }

            return activityList;
        }

        public ActivityList GetActivityListByDesc(string activityListDesc)
        {
            ActivityList activityList;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    activityList = (from al in context.ActivityList 
                                    where string.Equals(al.Desc, activityListDesc, 
                                                        StringComparison.OrdinalIgnoreCase)
                                    select al).SingleOrDefault<ActivityList>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetActivityListByDesc: " + e.Message );
            }

            return activityList;
        }
    }
}