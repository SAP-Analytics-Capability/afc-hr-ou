using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface ICompanyRulesData
    {
        void InsertCompany(CompanyRules company);     

        List<CompanyRules> GetCompanies();

        CompanyRules GetCompanyById(int companyRulesId);

        List<CompanyRules> GetCompanyRulesSapDescAfcDesc(string companyRulesDesc, string perimeter);

        List<CompanyRules> GetCompanyRulesDesc(string companyRulesDesc);

        List<CompanyRules> GetCompanyByNewPrimoDesc(string newPrimoCode);

        List<CompanyRules> GetCompanyByNewPrimoSapHRCode(string newPrimoSapHRCode);
    }
}