using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using bwsync.Models.Sap;

namespace bwsync.Interfaces 
{
    public interface IBwSapDataAdapter
    {
         Task<BwSapDataList> GetMasterBWData(CancellationToken cancellationToken);
    }
}