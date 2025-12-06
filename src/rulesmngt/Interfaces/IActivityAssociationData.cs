using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IActivityAssociationData
    {
        List<ActivityAssociation> GetActivityAssociations();

        List<ActivityAssociation> GetActivityAssociationsByBPC(string description);

        List<ActivityAssociation> GetActivityAssociationsByCodeEPrev(string objectid, string typeCode);
        ActivityAssociation AddActivityAssociation(ActivityAssociation aa);
        List<ActivityAssociation> GetAllActivities();
        ActivityAssociation GetActivityById(int recordId);
        ActivityAssociation UpdateActivity(ActivityAssociation a);
        bool DeleteActivity(ActivityAssociation oldactivity);
    }
}