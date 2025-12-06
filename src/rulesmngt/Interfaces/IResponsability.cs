using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IResponsability
    {
        void InsertResponsability(Responsability responsability);

        List<Responsability> GetResponsabilityByNewPrimo(string newPrimoCode);

        List<Responsability> GetResponsability();
    }
}