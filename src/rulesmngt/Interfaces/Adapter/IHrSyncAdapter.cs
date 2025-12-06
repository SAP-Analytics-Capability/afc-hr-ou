using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rulesmngt.Interfaces.Adapters
{
    public interface IHrSyncAdapter
    {
        Task <string> reqHrSyncFullSync(CancellationToken cancellationToken, string sync_post_service);

    }
}