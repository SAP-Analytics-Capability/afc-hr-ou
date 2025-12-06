using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class EntityData : IEntityData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public EntityData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertEntity(Entity entita)
        {
            throw new NotImplementedException();
        }

        public List<Entity> GetEntities()
        {
            List<Entity> entities;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    entities = (from e in context.Entity select e).ToList<Entity>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetEntityByCode: " + e.Message);
            }

            return entities;
        }

        public Entity GetEntityByCode(string entityCode)
        {
            Entity entity;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    entity = (from e in context.Entity 
                              where string.Equals(e.EntityCode, entityCode,
                                                  StringComparison.OrdinalIgnoreCase) 
                              select e).SingleOrDefault<Entity>();

                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetEntityByCode: " + e.Message);
            }

            return entity;         
        }

        public Entity GetEntityByName(string entityName)
        {
            Entity entity;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    entity = (from e in context.Entity 
                              where string.Equals(e.EntityName, entityName,
                                                  StringComparison.OrdinalIgnoreCase) 
                              select e).SingleOrDefault<Entity>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetEntityByName: " + e.Message);
            }

            return entity;         
        }

        public List<Entity> GetEntityByGbl(string gbl)
        {
            List<Entity> entityList;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    entityList = (from e in context.Entity 
                                  where string.Equals(e.Gbl, gbl,
                                                      StringComparison.OrdinalIgnoreCase) 
                                  select e).ToList<Entity>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetEntityByGbl: " + e.Message);
            }    

            return entityList;   
        }
    }
}