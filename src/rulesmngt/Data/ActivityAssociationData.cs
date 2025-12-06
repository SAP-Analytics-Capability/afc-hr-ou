using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using rulesmngt.Models.Configuration;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace rulesmngt.Data
{
    public class ActivityAssociationData : IActivityAssociationData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;
        private ILogger Logger;

        public ActivityAssociationData(IOptions<DatabaseConfiguration> databaseConfiguration, ILoggerFactory loggerfactory)
        {
            this.databaseConfiguration = databaseConfiguration;
            this.Logger = loggerfactory.CreateLogger<ActivityAssociationData>();
        }

        public List<ActivityAssociation> GetActivityAssociations()
        {
            List<ActivityAssociation> associationlist = null;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    associationlist = (from al in context.ActivityAssociation select al).ToList<ActivityAssociation>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get the full list of the activity associations.", DateTime.Now));
            }

            return associationlist;
        }

        public List<ActivityAssociation> GetActivityAssociationsByBPC(string description)
        {
            List<ActivityAssociation> activityMappingList = null;

            using (var context = new RulesContext(databaseConfiguration))
            {
                try
                {
                    activityMappingList = context.ActivityAssociation
                                                 .Where(am => string.Equals(am.BpcDesc, description,
                                                                            StringComparison.OrdinalIgnoreCase))
                                                 .ToList<ActivityAssociation>();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, string.Format("{0} - Unable to get the list of activity associations by BPC description", DateTime.Now));
                }
            }

            return activityMappingList;
        }

        public List<ActivityAssociation> GetActivityAssociationsByCodeEPrev(string objectid, string typeCode)
        {
            List<ActivityAssociation> activityMappingList = null;

            int appoggio = Convert.ToInt32(objectid);
            string objectidConv = appoggio.ToString();


            using (var context = new RulesContext(databaseConfiguration))
            {
                try
                {
                    activityMappingList = context.ActivityAssociation
                                                 .Where(am => string.Equals(am.PoObjectId, objectidConv, StringComparison.OrdinalIgnoreCase) &&
                                                                             string.Equals(am.ObjectTypeId, typeCode))
                                                 .ToList<ActivityAssociation>();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, string.Format("{0} - Unable to get the list of activity associations by BPC description", DateTime.Now));
                }
            }

            return activityMappingList;
        }
        public ActivityAssociation AddActivityAssociation(ActivityAssociation aa)
        {
            ActivityAssociation activity = null;
            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    EntityEntry<ActivityAssociation> ee = context.Add(aa);
                    context.SaveChanges();
                    activity = ee.Entity;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to add new ActivityAssociation");
            }
            return activity;
        }

        public List<ActivityAssociation> GetAllActivities()
        {
            List<ActivityAssociation> activities = null;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    activities = context.ActivityAssociation.ToList<ActivityAssociation>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get company scope", DateTime.Now));
            }

            return activities;
        }

         public ActivityAssociation GetActivityById(int recordId)
        {
            ActivityAssociation activityAssociation  = null;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    activityAssociation = context.ActivityAssociation.Find(recordId);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get company scope", DateTime.Now));
            }

            return activityAssociation;
        }
         public ActivityAssociation UpdateActivity(ActivityAssociation a)
        {
            ActivityAssociation activity = null;
            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    EntityEntry<ActivityAssociation> ee = context.Update(a);
                    context.SaveChanges();
                    activity = ee.Entity;
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to apdate syncDAteTime of Result", DateTime.Now));
            }
            return activity;
        }

        public bool DeleteActivity(ActivityAssociation oldactivity)
        {
            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    context.ActivityAssociation.Remove(oldactivity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to delete old activity.", DateTime.Now));
                return false;
            }
            return true;
        }
    }
}