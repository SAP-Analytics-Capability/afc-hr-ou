using System.Collections.Generic;
using rulesmngt.Models;
using rulesmngt.Models.SnowConsolidationResult;
using rulesmngt.Models.SnowTranscodingResult;

namespace rulesmngt.Interfaces
{
    public interface ICatalogUtils
    {
        List<CompanyScope> ConvertToAfcConsolidateCatalog(RootObjectConsolidation snowCatalog);
        List<ActivityAssociation> ConvertToAfcActivityCatalog(RootObjectTranscoding snowCatalogTranscoding);
        bool ElaborationDBCleaningTranscoding();
        bool ElaborationDBCleaningConsolidating();
        bool AddTranscoding(List<ActivityAssociation> activities);
        bool AddConsolidated(List<CompanyScope> companies);
        bool isEmptyTranscoding();
        bool isEmptyConsolidate();
        bool RollbackConsolidated();
        bool RollbackTranscoding();
    }
}