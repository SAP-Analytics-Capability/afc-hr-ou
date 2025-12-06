using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class ItaGloData : IItaGloData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public ItaGloData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public List<ItaGlo> GetItaGlos()
        {
            List<ItaGlo> itaGlos;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    itaGlos = (from e in context.ItaGlo select e).ToList<ItaGlo>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetEntityByCode: " + e.Message);
            }

            return itaGlos;
        }

        public ItaGlo GetItaGloByCode(string sapHrGlobalCode)
        {
            ItaGlo itaGlo;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    itaGlo = (from c in context.ItaGlo
                              where string.Equals(c.SapHrGlobalCode, sapHrGlobalCode,
                                                  StringComparison.OrdinalIgnoreCase)
                              select c).SingleOrDefault<ItaGlo>();

                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetEntityByCode: " + e.Message);
            }

            return itaGlo;
        }


    }
}