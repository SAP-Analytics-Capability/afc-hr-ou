using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using rulesmngt.Models.SnowConsolidationResult;
using rulesmngt.Models.SnowTranscodingResult;

namespace rulesmngt.Interfaces.Adapters
{
    public interface ISnowAdatpter
    {
        Task<RootObjectConsolidation> getConsolidationCatalog(CancellationToken cancellationToken);
        Task<RootObjectTranscoding> getTranscodingActivity(CancellationToken cancellationToken);
    }
}