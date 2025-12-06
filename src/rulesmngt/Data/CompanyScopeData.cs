using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using rulesmngt.Models.Configuration;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace rulesmngt.Data
{
    public class CompanyScopeData : ICompanyScopeData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;
        private ILogger Logger;

        public CompanyScopeData(IOptions<DatabaseConfiguration> databaseConfiguration, ILoggerFactory loggerfactory)
        {
            this.databaseConfiguration = databaseConfiguration;
            this.Logger = loggerfactory.CreateLogger<CompanyScopeData>();
        }

        public List<CompanyScope> GetCompanyScopeByNewPrimoDesc(string newPrimoDesc)
        {
            List<CompanyScope> associationlist = null;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    associationlist = (from cs in context.CompanyScope
                                       where string.Equals(cs.AfcNewPrimoDescr, newPrimoDesc, StringComparison.OrdinalIgnoreCase)
                                       select cs).ToList<CompanyScope>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get company scope", DateTime.Now));
            }

            return associationlist;
        }
        public List<CompanyScope> GetCompanyScopeBySAPCode(string sapcode)
        {
            List<CompanyScope> associationlist = null;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    associationlist = (from cs in context.CompanyScope
                                       where string.Equals(cs.PoSapGlobalCode, sapcode, StringComparison.OrdinalIgnoreCase)
                                       select cs).ToList<CompanyScope>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get company scope", DateTime.Now));
            }

            return associationlist;
        }

        public List<CompanyScope> GetCompanyScopeBySAPCodeAndPerimeter(string sapcode, string perimeter)
        {
            List<CompanyScope> associationlist = null;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    associationlist = (from cs in context.CompanyScope
                                       where string.Equals(cs.PoSapGlobalCode, sapcode, StringComparison.OrdinalIgnoreCase) &&
                                             string.Equals(cs.AfcPerimeterDescr, perimeter, StringComparison.OrdinalIgnoreCase)
                                       select cs).ToList<CompanyScope>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get company scope", DateTime.Now));
            }

            return associationlist;
        }
        public CompanyScope AddCompanyScope(CompanyScope cs)
        {
            CompanyScope company = null;
            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    EntityEntry<CompanyScope> ee = context.Add(cs);
                    context.SaveChanges();
                    company = ee.Entity;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to add new ActivityAssociation");
            }
            return company;
        }

        public List<CompanyScope> GetAllCompanies()
        {
            List<CompanyScope> companies = null;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    companies = context.CompanyScope.ToList<CompanyScope>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get company scope", DateTime.Now));
            }

            return companies;
        }

        public CompanyScope GetCompanyById(int recordId)
        {
            CompanyScope companyScope = null;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    companyScope = context.CompanyScope.Find(recordId);
                    // CreateObjectSet<CompanyScope>().SingleOrDefault(e => e.recordId == recordId);
                    // companyScope = (from cs in context.CompanyScope
                    //                    where cs.RecordId == recordId
                    //                    select cs).SingleOrDefault<CompanyScope>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get company scope", DateTime.Now));
            }

            return companyScope;
        }
        public CompanyScope UpdateCompany(CompanyScope c)
        {
            CompanyScope company = null;
            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    EntityEntry<CompanyScope> ee = context.Update(c);
                    context.SaveChanges();
                    company = ee.Entity;
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to apdate syncDAteTime of Result", DateTime.Now));
            }
            return company;
        }
        public bool DeleteCompany(CompanyScope oldcompany)
        {
            bool isRemove = false;
            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    context.CompanyScope.Remove(oldcompany);
                    context.SaveChanges();
                    isRemove = true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to delete old company.", DateTime.Now));
            }
            return isRemove;
        }

        public string GetCompanyByGlobalCode(string globalCode)
        {
            string companyCode = string.Empty;
            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    companyCode = (from cs in context.CompanyScope
                                   where cs.PoSapGlobalCode == globalCode
                                   select cs.AfcCompanyCode).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get company code.", DateTime.Now));
            }

            return companyCode;
        }
    }
}