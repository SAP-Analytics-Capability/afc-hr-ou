using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using masterdata.Models;

namespace masterdata.Interfaces
{
    public interface IOuCCAssociation
    {
        AssociationResult GetAssociationResult(string associationType, DateTime lastaccess);
        AssociationResult GetAssociationResultOuCode(string[] listOucode, string associationType);
        AssociationResultV1 GetAssociationResultFromService(DateTime lastaccess);
        void ComputeAssociation(string associationType, DateTime lastaccess);
        AssociationResultV2 GetAssociationResultOuCodeV2(string[] listOucode);
        void ComputeAssociationV2(string associationType, DateTime lastaccess);
        AssociationResult getCCForCleaner(HrmasterdataOuList orgUnitList);
        List<HrmasterdataOu> getHrMasterDataOuCode(string oucode);
        AssociationResult getCCForUO(HrmasterdataOuList orgUnitList, string associationType, bool isCleaner);
        void ComputeAssociationResume(string[] ids, string associationType, DateTime lastaccess);

    }
}