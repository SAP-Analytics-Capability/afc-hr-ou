using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Models;
using masterdata.Models.rulesmngt;

namespace masterdata.Interfaces
{
    public interface IActivityMappingAdapter
    {       
        Task<List<ActivityMapping>> GetActivityMappings(CancellationToken cancellationToken);

        Task<ActivityMapping> GetActivityMapping(CancellationToken cancellationToken,string activityMappingId);

        Task<List<ActivityMapping>> GetActivityMappingByCodeEPrev(CancellationToken cancellationToken, string objectid, string typeCode);
        Task<List<ActivityAssociationGroup>> GetActivityAssociationGroup(CancellationToken cancellationToken, string objectid, string typeCode);
    }
}