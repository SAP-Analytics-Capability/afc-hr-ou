using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IEntityData
    {
        void InsertEntity(Entity entita);

        List<Entity> GetEntities();

        Entity GetEntityByCode(string entityCode);

        Entity GetEntityByName(string entityName);

        List<Entity> GetEntityByGbl(string gbl);
    }
}