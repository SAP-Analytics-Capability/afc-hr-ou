using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class BpcCodeData : IBpcData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public BpcCodeData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertBpc(Bpc bpcCode)
        {
            //TODO
        }

        public List<Bpc> GetBpc()
        {
            List<Bpc> bpcList;
            
            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    bpcList = (from bpc in context.Bpc select bpc).ToList<Bpc>();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error in method GetBpc: " + e.Message);
            }

            return bpcList;
        }

       public Bpc GetBpcById(Int32 bpcId)
        {
            Bpc bpc;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    bpc = (from b in context.Bpc 
                           where b.BpcId == bpcId
                           select b).SingleOrDefault<Bpc>();                           
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetBpcById: " + e.Message );
            }

            return bpc;
        }

        public Bpc GetBpcByCode(string bpcCode)
        {
            Bpc bpc;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    bpc = (from b in context.Bpc 
                           where string.Equals(b.bpcCode, bpcCode,
                                               StringComparison.OrdinalIgnoreCase) 
                           select b).SingleOrDefault<Bpc>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetBpcByCode: " + e.Message );
            }

            return bpc;
        }

        public Bpc GetBpcByDesc(string bpcDesc)
        {
            Bpc bpc;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    bpc = (from b in context.Bpc 
                           where string.Equals(b.Desc, bpcDesc,
                                               StringComparison.OrdinalIgnoreCase) 
                           select b).SingleOrDefault<Bpc>();                           
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetBpcByDesc: " + e.Message );
            }

            return bpc;
        }
    }
}