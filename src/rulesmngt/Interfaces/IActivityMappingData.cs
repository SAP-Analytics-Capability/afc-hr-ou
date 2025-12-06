using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IActivityMappingData
    {
        void InsertActivityMapping(ActivityMapping actMapping);

        List<ActivityMapping> GetActivityMappings();

        ActivityMapping GetActivityMapping(string activityMappingId);

        List<ActivityMapping> GetActivityMappingByCodeEPrev(string objectid, string typeCode);
    }
}