using System.Collections.Generic;
using System.Threading.Tasks;
using masterdata.Models;

namespace masterdata.Interfaces
{
    public interface IActivityAssociationAdapter
    {       
         Task<List<ActivityAssociation>> GetAllActivityAssociation();
         Task<ActivityAssociation> PostActivityAssociation(ActivityAssociation association);
         Task<ActivityAssociation> PutActivityAssociation(ActivityAssociation association, int id);
         Task<bool> DeleteActivityAssociation(int id);
         Task<ActivityAssociation> GetSingleActivityAssociation(int Activity);
    }
}