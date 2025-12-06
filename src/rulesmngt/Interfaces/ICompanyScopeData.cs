using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface ICompanyScopeData
    {
        List<CompanyScope> GetCompanyScopeBySAPCode(string sapcode);
        List<CompanyScope> GetCompanyScopeByNewPrimoDesc(string newPrimoDesc);

        List<CompanyScope> GetCompanyScopeBySAPCodeAndPerimeter(string sapcode, string perimeter);
        CompanyScope AddCompanyScope(CompanyScope cs);
        List<CompanyScope> GetAllCompanies();
        CompanyScope GetCompanyById(int recordId);

        string GetCompanyByGlobalCode(string entity);

        CompanyScope UpdateCompany(CompanyScope c);
        bool DeleteCompany(CompanyScope c);
    }
}