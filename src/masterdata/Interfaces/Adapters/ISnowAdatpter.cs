using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Interfaces.Adapters
{
    public interface ISnowAdatpter
    {
        Task<RootObject> getSnowCCUOResults(CancellationToken cancellationToken);
    }
}