using System;
using System.Collections.Generic;

using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IClientAccessData
    {
        DateTime AddNewAccess(string username, string type);

        ClientAccess RetrieveAccess(string username, string type);
        bool UpdateAccess(string username, string type);
    }
}