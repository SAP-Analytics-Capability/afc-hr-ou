using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class ActivityMappingData : IActivityMappingData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public ActivityMappingData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertActivityMapping(ActivityMapping actMapping)
        {
            //TODO
        }

        public List<ActivityMapping> GetActivityMappings()
        {
            List<ActivityMapping> actvityMappings;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    actvityMappings = context.ActivityMapping
                                             .Include(am => am.BpcCode)
                                             .Include(am => am.MacroOrg1)
                                             .Include(am => am.MacroOrg2)
                                             .Include(am => am.Organization)
                                             .Include(am => am.Vcs)
                                             .ToList<ActivityMapping>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetActivityMappings: " + e.Message);
            }

            return actvityMappings;
        }

        public ActivityMapping GetActivityMapping(string activityMappingId)
        {
            throw new NotImplementedException();
        }

        public List<ActivityMapping> GetActivityMappingByBpcDesc(string bpcDesc)
        {
            List<ActivityMapping> activityMappingList;

            using (var context = new RulesContext(databaseConfiguration))
            {
                try
                {
                    activityMappingList = context.ActivityMapping
                                                 .Include(am => am.BpcCode)
                                                 .Include(am => am.MacroOrg1)
                                                 .Include(am => am.MacroOrg2)
                                                 .Include(am => am.Organization)
                                                 .Include(am => am.Vcs)                                                    
                                                 .Where(am => string.Equals(am.BpcCode.Desc, bpcDesc, 
                                                                            StringComparison.OrdinalIgnoreCase))
                                                 .ToList<ActivityMapping>();
                } 
                catch (Exception e)
                {
                    throw new System.ArgumentException("Error in method GetActivityMappingByBpcDesc: " + e.Message );
                }
            }

            return activityMappingList;
        }
        public List<ActivityMapping> GetActivityMappingByCodeEPrev(string objectid, string typeCode)
        {
            List<ActivityMapping> activityMappingList;

            using (var context = new RulesContext(databaseConfiguration))
            {
                try
                {
                    activityMappingList = context.ActivityMapping
                                                 .Include(am => am.BpcCode)
                                                 .Include(am => am.MacroOrg1)
                                                 .Include(am => am.MacroOrg2)
                                                 .Include(am => am.Organization)
                                                 .Include(am => am.Vcs)                                                    
                                                 .Where(am => string.Equals(am.ObjectId, typeCode, StringComparison.OrdinalIgnoreCase) &&
                                                                             string.Equals(am.ObjectType ,typeCode))
                                                 .ToList<ActivityMapping>();
                } 
                catch (Exception e)
                {
                    throw new System.ArgumentException("Error in method GetActivityMappingByBpcDesc: " + e.Message );
                }
            }

            return activityMappingList;
        }
    }
}