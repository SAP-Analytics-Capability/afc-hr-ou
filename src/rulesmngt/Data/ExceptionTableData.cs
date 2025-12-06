using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class ExceptionTableData : IExceptionTableData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public ExceptionTableData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertException(ExceptionTable exeption)
        {
            throw new NotImplementedException();
        }

        public List<ExceptionTable> GetExceptions()
        {
            List<ExceptionTable> exceptions;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    exceptions = (from e in context.ExceptionTable select e).ToList<ExceptionTable>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetExceptions: " + e.Message);
            }

            return exceptions;
        }

        public ExceptionTable GetExceptionByTypoUo(string typoUo)
        {
            ExceptionTable exceptionTable;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    exceptionTable = (from e in context.ExceptionTable 
                              where string.Equals(e.TipoUo, typoUo,
                                                  StringComparison.OrdinalIgnoreCase)
                                                  && string.Equals(e.Active, "1")
                              select e).SingleOrDefault<ExceptionTable>();

                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetExceptionByTypoUo: " + e.Message);
            }

            return exceptionTable;         
        }

        public List<ExceptionTable> GetExceptionByTypoUoGblPrev(string typoUo, string gblPrev)
        {
            List<ExceptionTable> exceptionTables;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    exceptionTables = context.ExceptionTable
                                                 .Where(e => string.Equals(e.ValueTipoUo, typoUo, StringComparison.OrdinalIgnoreCase) &&
                                                                             string.Equals(e.ValueGblPrevalent, gblPrev)
                                                                             && string.Equals(e.Active, "1"))
                                                 .ToList<ExceptionTable>();

                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetExceptionByTypoUo: " + e.Message);
            }

            return exceptionTables;         
        }
         
    }
}