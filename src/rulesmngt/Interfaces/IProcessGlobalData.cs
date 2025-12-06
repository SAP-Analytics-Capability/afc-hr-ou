using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IProcessGlobalData
    {
        void InsertProcessGlobal(ProcessGlobal procGlobal);

        ProcessGlobal GetProcessGlobal(int procCode);

        List<ProcessGlobal> GetProcessGlobals();
    }
}