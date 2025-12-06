using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IProcessLocalData
    {
        void InsertProcessLocal(ProcessLocal proclocal);

        ProcessLocal GetProcessLocalById(int procId);

        List<ProcessLocal> GetProcessLocals();
    }
}