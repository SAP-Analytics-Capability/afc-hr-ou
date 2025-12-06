using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Models;
using masterdata.Models.rulesmngt;

namespace masterdata.Interfaces
{
    public interface IItaGloAdapter
    {       
        Task<ItaGlo> GetItaGloInfo(CancellationToken cancellationToken, string sapHrCode);

    }
}