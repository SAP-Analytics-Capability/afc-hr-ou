using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Models;
using masterdata.Models.rulesmngt;

namespace masterdata.Interfaces
{
    public interface IResponsabilityAdapter
    {       
        Task<List<Responsability>> GetResponsabilities();

        Task<List<Responsability>> GetResponsabilityByNewPrimoCode(CancellationToken cancellationToken, string responsability);

    }
}