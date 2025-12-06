using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Models;

namespace masterdata.Interfaces
{
    public interface ICatalogManagementAdapter
    {
        Task<List<ActivityAssociation>> GetActivityAssociationCatalog(CancellationToken cancellationToken);
        Task<ActivityAssociation> GetActivityAssociationByID(int id, CancellationToken cancellationToken);
        Task<CatalogManager> AddActivty(ActivityAssociation activity, CancellationToken cancellationToken);
        Task<List<CatalogManager>> AddActivties(List<ActivityAssociation> activity, CancellationToken cancellationToken);
        Task<CatalogManager> UpdateActivity(int id, ActivityAssociation activity, CancellationToken cancellationToken);
        Task<CatalogManager> RemoveActivity(int id, CancellationToken cancellationToken);
        Task<List<CatalogManager>> RemovesActivities(List<int> id, CancellationToken cancellationToken);

        Task<List<CompanyScope>> GetCompanyScopeCatalog(CancellationToken cancellationToken);
        Task<CompanyScope> GetCompanyByID(int id, CancellationToken cancellationToken);
        Task<CatalogManager> AddCompany(CompanyScope company, CancellationToken cancellationToken);
        Task<List<CatalogManager>> AddCompanies(List<CompanyScope> company, CancellationToken cancellationToken);
        Task<CatalogManager> UpdateCompany(int id, CompanyScope company, CancellationToken cancellationToken);
        Task<CatalogManager> RemoveCompany(int id, CancellationToken cancellationToken);
        Task<List<CatalogManager>> RemovesCompanies(List<int> id, CancellationToken cancellationToken);
    }
}