using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class ResponsabilityData : IResponsability
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public ResponsabilityData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertResponsability(Responsability responsability)
        {
            //TODO
        }

        public List<Responsability> GetResponsabilityByNewPrimo(string newPrimoCode)
        {
            List<Responsability> responsability;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    responsability = (from v in context.Responsability
                                      where v.NewPrimoCode == newPrimoCode
                                      select v).ToList();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetResponsabilityByNewPrimo: " + e.Message);
            }

            return responsability;
        }

        public List<Responsability> GetResponsability()
        {
            List<Responsability> ResponsabilityList;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    ResponsabilityList = (from responsability in context.Responsability select responsability).ToList<Responsability>();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error in method GetResponsability: " + e.Message);
            }

            return ResponsabilityList;
        }
    }
}