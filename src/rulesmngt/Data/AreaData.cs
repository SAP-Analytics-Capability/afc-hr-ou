using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class AreaData : IAreaData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public AreaData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertArea(Models.Area area)
        {
            throw new NotImplementedException();
        }

        public List<Area> GetAreas()
        {
            List<Area> areas;
            
            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    areas = (from a in context.Area select a).ToList<Area>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetAreas: " + e.Message );
            }

            return areas;
        }

        public Area GetAreaById(int areaId)  
        {
            Area area;
            
            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    area = (from a in context.Area
                            where a.AreaId == areaId
                            select a).SingleOrDefault<Area>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetAreaById: " + e.Message );
            }

            return area;
        }

        public Area GetAreaByCode(string areaCode)  
        {
            Area area;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    area = (from a in context.Area
                            where string.Equals(a.AreaCode, areaCode,
                                                StringComparison.OrdinalIgnoreCase) 
                            select a).SingleOrDefault<Area>();                            
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetAreaByCode: " + e.Message );
            }

            return area;
        }
    }
}