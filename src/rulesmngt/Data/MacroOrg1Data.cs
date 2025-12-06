using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class MacroOrg1Data : IMacroOrg1Data 
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public MacroOrg1Data(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertMacroOrg1(MacroOrg1 macroOrg1)
        {
            //TODO
        }
        
        public List<MacroOrg1> GetMacroOrg1s()
        {
            List<MacroOrg1> macroOrg1s;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    macroOrg1s = (from mo in context.MacroOrg1 select mo).ToList<MacroOrg1>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetMacroOrg1s: " + e.Message);
            }

            return macroOrg1s;
        }

        public MacroOrg1 GetMacroOrg1ById(int macroOrg1Id)
        {
            MacroOrg1 macroOrg1;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    macroOrg1 = (from mo in context.MacroOrg1 
                                 where mo.MacroOrg1Id == macroOrg1Id 
                                 select mo).SingleOrDefault<MacroOrg1>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetMacroOrg1ById: " + e.Message);
            }

            return macroOrg1;
        }        

        public MacroOrg1 GetMacroOrg1ByCode(string macroOrg1Code)
        {
            MacroOrg1 macroOrg1;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    macroOrg1 = (from mo in context.MacroOrg1
                                 where string.Equals(mo.MacroOrg1Code, macroOrg1Code,
                                                     StringComparison.OrdinalIgnoreCase) 
                                 select mo).SingleOrDefault<MacroOrg1>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetMacroOrg1ByCode: " + e.Message);
            }

            return macroOrg1;  
        }

        public MacroOrg1 GetMacroOrg1ByDesc(string macroOrg1Desc)
        {
            MacroOrg1 macroOrg1;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    macroOrg1 = (from mo in context.MacroOrg1
                                 where string.Equals(mo.Desc, macroOrg1Desc,
                                                     StringComparison.OrdinalIgnoreCase) 
                                 select mo).SingleOrDefault<MacroOrg1>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetMacroOrg1ByDesc: " + e.Message);
            }

            return macroOrg1;  
        }
    }
}