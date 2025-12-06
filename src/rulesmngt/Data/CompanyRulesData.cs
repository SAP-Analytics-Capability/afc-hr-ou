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
    public class CompanyRulesData : ICompanyRulesData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public CompanyRulesData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertCompany(CompanyRules company)
        {
            throw new NotImplementedException();
        }

        public List<CompanyRules> GetCompanies()
        {
            List<CompanyRules> companies;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    companies = context.CompanyRules
                                       .Include(cr => cr.Area)
                                       .Include(cr => cr.Perimeter)
                                       .Include(cr => cr.CodeNation)
                                       .Include(cr => cr.BusinessLines)
                                       .ToList<CompanyRules>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetCompanyByDesc: " + e.Message);
            }

            return companies;
        }

        public CompanyRules GetCompanyById(int companyRulesId)
        {
            CompanyRules companyRules;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    companyRules = context.CompanyRules
                                          .Where(cr => cr.CompanyCode == companyRulesId)
                                          .Include(cr => cr.Area)
                                          .Include(cr => cr.Perimeter)
                                          .Include(cr => cr.CodeNation)
                                          .Include(cr => cr.BusinessLines)
                                          .SingleOrDefault<CompanyRules>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetCompanyById: " + e.Message);
            }

            return companyRules;
        }

        public List<CompanyRules> GetCompanyRulesSapDescAfcDesc(string companyRulesDesc, string perimeter)
        {
            List<CompanyRules> companyRules;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    companyRules = context.CompanyRules
                                          .Where(cr => string.Equals(cr.SapHrCode, companyRulesDesc,
                                                                     StringComparison.OrdinalIgnoreCase)
                                                                     && string.Equals(cr.Perimeter.PerimeterDesc, perimeter))
                                          .Include(cr => cr.Area)
                                          .Include(cr => cr.Perimeter)
                                          .Include(cr => cr.CodeNation)
                                          .Include(cr => cr.BusinessLines)
                                          .ToList<CompanyRules>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetCompanyByDesc: " + e.Message);
            }

            return companyRules;
        }

        public List<CompanyRules> GetCompanyRulesDesc(string companyRulesDesc)
        {
            List<CompanyRules> companyRules;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    companyRules = context.CompanyRules
                                          .Where(cr => string.Equals(cr.SapHrCode, companyRulesDesc,
                                                                     StringComparison.OrdinalIgnoreCase))
                                          .Include(cr => cr.Area)
                                          .Include(cr => cr.Perimeter)
                                          .Include(cr => cr.CodeNation)
                                          .Include(cr => cr.BusinessLines)
                                          .ToList<CompanyRules>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetCompanyByDesc: " + e.Message);
            }

            return companyRules;
        }

        public List<CompanyRules> GetCompanyByNewPrimoDesc(string newPrimoDesc)
        {
            List<CompanyRules> companyRules;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    companyRules = context.CompanyRules
                                          .Where(cr => string.Equals(cr.NewPrimoDesc, newPrimoDesc,
                                                                     StringComparison.OrdinalIgnoreCase))
                                          .Include(cr => cr.Area)
                                          .Include(cr => cr.Perimeter)
                                          .Include(cr => cr.CodeNation)
                                          .Include(cr => cr.BusinessLines)
                                          .ToList<CompanyRules>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetCompanyBynewPrimoCode: " + e.Message);
            }

            return companyRules;
        }

        public List<CompanyRules> GetCompanyByNewPrimoSapHRCode(string newPrimoSapHRCode)
        {
            List<CompanyRules> companyRules;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    companyRules = context.CompanyRules
                                          .Where(cr => string.Equals(cr.SapHrCode, newPrimoSapHRCode,
                                                                     StringComparison.OrdinalIgnoreCase))
                                          .Include(cr => cr.Area)
                                          .Include(cr => cr.Perimeter)
                                          .Include(cr => cr.CodeNation)
                                          .Include(cr => cr.BusinessLines)
                                          .ToList<CompanyRules>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetCompanyBynewPrimoCode: " + e.Message);
            }

            return companyRules;
        }
    }
}