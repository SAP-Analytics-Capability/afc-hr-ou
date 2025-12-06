using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using masterdata.Models;
using masterdata.Models.rulesmngt;

namespace masterdata.Interfaces
{
    public interface IActivityListAdapter
    {       
         Task<List<ActivityList>> GetActivityLists();

         Task<ActivityList> GetActivityList(string activityCode);
    }
}