using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Models;

namespace masterdata.Interfaces.Adapters
{
    public interface IBwSyncAdapter
    {   
        Task<List<BwMasterObject>> GetBwMasterDatas(CancellationToken cancellationToken, string ZVarMacroOrg1, string ZVarMacroOrg2, string ZVarVcs, string companyCode, string Process, string Organization);
    }
}