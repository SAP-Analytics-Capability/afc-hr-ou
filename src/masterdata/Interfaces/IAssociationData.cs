using System;
using System.Collections.Generic;

using masterdata.Models;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Interfaces
{
    public interface IAssociationData
    {
        TotalCount GetTotalCountByTypeDate(string associationTime, string type);
        TotalCountOffsetType GetOffsetByTypeDate(string associationTime, string type); 
        UoLimited GetLimitedUo(string associationTime, string offset, string limit);
        CcLimited GetLimitedCc(string associationTime, string offset, string limit);
        int AddNewResultAssociationOU(HrOu aou);
        void AddNewResultAssociationCC(BwCC acc);
        int UpdateAssociationCleanOU(CleanHrOU aou);
        void AddNewResultAssociationCleanCC(CleanBwCC acc);
       
    }
}
