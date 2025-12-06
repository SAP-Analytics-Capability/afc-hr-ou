using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using snowsync.Models;

namespace snowsync.Interfaces
{
    public interface ISnowAdapterController
    {
        ActionResult<IEnumerable<RootObject>> GetCostCenterAssociation();
    }
}