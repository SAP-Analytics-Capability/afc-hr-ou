using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Models;
using masterdata.Models.rulesmngt;

namespace masterdata.Interfaces
{
    public interface ICompanyRulesAdapter
    {       
        Task<List<CompanyRules>> GetCompanies();   

        Task<CompanyRules> GetCompanyById(string companyRules);

        Task<List<CompanyRules>> GetCompanyByDescPerimeter(string companyRulesCode, string companyRulesDesc, CancellationToken cancellationToken);
        Task<List<CompanyRules>> GetCompanyBySapHRCode(string companyRulesDesc, CancellationToken cancellationToken);
        Task<List<CompanyRules>> GetCompanyBynewPrimoDesc(string newPrimoDesc, CancellationToken cancellationToken);

        Task<CompanyRules> GetCompanyBynewPrimoSapHRCode(string newPrimoSapHRCode);
    }
}