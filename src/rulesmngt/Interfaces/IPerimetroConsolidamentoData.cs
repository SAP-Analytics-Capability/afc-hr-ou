using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IPerimetroConsolidamentoData
    {
        void AddNewPerimetroConsolidamento(PerimetroConsolidamento pc);
        List<PerimetroConsolidamento> GetByNewPrimoDescr(string afcNewPrimoDescr);
    }
}