using System;
using System.Collections.Generic;

using masterdata.Models;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Interfaces
{
    public interface IClientAccessData
    {
        DateTime AddNewAccess(string username, string type);

        List<ClientAccess> RetrieveAccess(string username, string type);
        bool UpdateAccess(string username, string type);

    }
}
