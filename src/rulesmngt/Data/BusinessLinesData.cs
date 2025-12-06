using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class BusinessLinesData : IBusinessLinesData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public BusinessLinesData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertBusinessLines(BusinessLines businessLines)
        {
            //TODO
        }

        public List<BusinessLines> GetBusinessLines()
        {
            List<BusinessLines> allBusinessLines;
            
            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    allBusinessLines = context.BusinessLines
                                              .Include(bl => bl.CompanyRules.Area)
                                              .Include(bl => bl.CompanyRules.Perimeter)
                                              .Include(bl => bl.CompanyRules.CodeNation)
                                              .ToList<BusinessLines>();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error in method GetBusinessLines: " + e.Message);
            }

            return allBusinessLines;
        }

        public BusinessLines GetBusinessLinesById(int businessLinesId)
        {
            BusinessLines businessLine;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    businessLine =  context.BusinessLines
                                        .Include(bl => bl.CompanyRules.Area)
                                        .Include(bl => bl.CompanyRules.Perimeter)
                                        .Include(bl => bl.CompanyRules.CodeNation)
                                        .Where(bl => bl.BusinessLinesId == businessLinesId)
                                        .SingleOrDefault<BusinessLines>();                                           
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetBusinessLineById: " + e.Message);
            }

            return businessLine;
        }

        public List<BusinessLines> GetBusinessLinesByName(string BlName)
        {
            List<BusinessLines> businessLines;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    businessLines =  context.BusinessLines
                                            .Include(bl => bl.CompanyRules.Area)
                                            .Include(bl => bl.CompanyRules.Perimeter)
                                            .Include(bl => bl.CompanyRules.CodeNation)
                                            .Where(bl => string.Equals(bl.BusinessLinesName, BlName,
                                                                    StringComparison.OrdinalIgnoreCase))
                                            .ToList<BusinessLines>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetBusinessByDesc: " + e.Message);
            }

            return businessLines;
        }
    }
}