using System;
using System.Collections.Generic;

using masterdata.Models;

namespace masterdata.Interfaces
{
    public interface IHrMasterdataOuData
    {
        List<HrmasterdataOu> FindDistinct(DateTime lastaccess);

        List<HrmasterdataOu> FindAll();

        List<HrmasterdataOu> FindDistinct(string outcode, DateTime lastaccess);
        
    }
}