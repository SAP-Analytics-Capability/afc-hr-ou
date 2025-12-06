using System;
using System.Collections.Generic;

using masterdata.Models;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Interfaces
{
    public interface IResultData
    {
        void AddNewResult(Result r);
        List<Result> GetResultsByOUCodAndCCCod(string OrganizationalUnitCode, string CostCenterCod);
        List<Result> GetResultsByAssociationTime(string associationTime);
        List<Result> GetResultsByOUC(string OrganizationalUnitCode);
        int AddNewResultAssociationOU(AssociationOrganizationUnit aou);

        void AddNewResultAssociationCC(AssociationCostCenter acc);
        List<AssociationOrganizationUnit> getLastsDistinctUO(DateTime lastAccess);

        List<AssociationOrganizationUnit> getLastsDistinctPagedUO(DateTime lastAccess, int offset, int limit);
        List<AssociationCostCenter> getAssociationCCByUOId(int OrganizationUnitId);
        List<Result> GetSnowResult(DateTime lastAccess);
        void UpdateResult(Result r);
    }
}