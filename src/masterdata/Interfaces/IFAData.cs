using masterdata.Models;
using System;
using System.Collections.Generic;

namespace masterdata.Interfaces
{
    public interface IFAData
    {
        FunctionalAck AddNewFA(string caller, DateTime issueTime, string type);
        void DeleteFaOnCascade();
        FunctionalAck FaStatus(string faId);
        void UpdateFaDbStatus(FunctionalAck fa, string type);
        void DeleteFA(FunctionalAck fa);
        List<CleanBwCC> GetCCS(string fa);
    }
}