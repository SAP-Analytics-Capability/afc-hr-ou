using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IItaGloData
    {

        List<ItaGlo> GetItaGlos();
        ItaGlo GetItaGloByCode(string sapHrGlobalCode);


    }
}