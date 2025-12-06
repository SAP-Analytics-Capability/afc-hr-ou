using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;

namespace rulesmngt.Data
{
    public class ProcessGlobalData : IProcessGlobalData
    {

        public void InsertProcessGlobal(ProcessGlobal procGlobal)
        {
            //TODO
        }

        public ProcessGlobal GetProcessGlobalById(Int32 procCode)
        {
            
            return null;
        }        
        public List<ProcessGlobal> GetProcessGlobals()
        {
            //TODO
            return null;
        }

        public ProcessGlobal GetProcessGlobal(int procCode)
        {
            throw new NotImplementedException();
        }
    }
}