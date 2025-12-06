using System.Threading;
using System.Threading.Tasks;

using snowsync.Models;

namespace snowsync.Interfaces
{
    public interface ISnowAdatpter
    {
        Task<RootObject> getSnowCCUOResults(CancellationToken cancellationToken);
    }
}