using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Models;
using masterdata.Models.rulesmngt;

namespace masterdata.Interfaces
{
    public interface IEntityAdapter
    {       
        Task<List<Entity>> GetEntities();

        Task<Entity> GetEntity(CancellationToken cancellationToken, string entity);

        Task<Entity> GetEntityByNewPrimo(CancellationToken cancellationToken, string newPrimoCode);

        Task<string> GetCompanyCode(CancellationToken cancellationToken, string entity);
    }
}