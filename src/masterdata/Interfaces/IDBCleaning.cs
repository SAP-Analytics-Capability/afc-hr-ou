using masterdata.Models;

namespace masterdata.Interfaces
{
    public interface IDBCleaning
    {
        bool ElaborationDBCleaning();
        bool ElaborationHRCleaning();
        bool CleanBwccBCK(MasterDataContext context);
        bool BackUpBwcc(MasterDataContext context);
        bool CleanHrouBCK(MasterDataContext context);
        bool BackUpHrou(MasterDataContext context);
        bool CleanHrmasterdataouBCK(MasterDataContext context);
        bool BackUpHrmasterdataou(MasterDataContext context);
        bool CleanBwcc(MasterDataContext context);
        bool CleanHrou(MasterDataContext context);
        bool CleanHrmasterdataou(MasterDataContext context);
    }
}