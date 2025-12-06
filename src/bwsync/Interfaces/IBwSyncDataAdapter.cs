using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using bwsync.Models;

namespace bwsync.Interfaces 
{
    public interface IBwSyncDataAdapter
    {
        Task<BwMasterDataList> GetSapBWData(CancellationToken cancellationToken, string ZVarMacroOrg1, string ZVarMacroOrg2, string ZVarVcs, string companyCode, string ProcessCode, string OrganizationCode);

        Task<BwMasterDataList> GetFixedResultForTest(CancellationToken cancellationToken, string target, string useproxy);
    }
}