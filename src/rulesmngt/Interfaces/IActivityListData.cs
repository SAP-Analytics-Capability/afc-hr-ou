using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IActivityListData
    {
        void InsertActivityList(ActivityList actList);
         
        List<ActivityList> GetActivityLists();

        ActivityList GetActivityListByName(string activityListName);

        ActivityList GetActivityListByDesc(string activityListDesc);
    }
}