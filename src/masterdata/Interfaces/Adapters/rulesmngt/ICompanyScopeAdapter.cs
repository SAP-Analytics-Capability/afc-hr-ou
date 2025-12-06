using System.Collections.Generic;
using System.Threading.Tasks;
using masterdata.Models;

namespace masterdata.Interfaces
{
    public interface ICompanyScopeAdapter
    {       
         Task<List<CompanyScope>> GetAllCompanies();
         Task<CompanyScope> InsertNewCompany(CompanyScope company);
         Task<CompanyScope> UpdateCompany(CompanyScope company, int id);
         Task<bool> DeleteCompany(int id);
         Task<CompanyScope> GetCompanyById(int id);
    }
}