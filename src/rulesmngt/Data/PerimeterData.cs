using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class PerimeterData : IPerimeterData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public PerimeterData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertPerimeter(Perimeter perimetro)
        {
            //TODO
        }
              
        public List<Perimeter> GetPerimeters()
        {
            List<Perimeter> perimeters;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    perimeters = (from p in context.Perimeter select p).ToList<Perimeter>();
                } 
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetPerimetros: " + e.Message);
            }

            return perimeters;
        }

        public Perimeter GetPerimeterById(int perimeterId)
        {
            Perimeter perimeter;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    perimeter = (from p in context.Perimeter
                                 where p.PerimeterId == perimeterId
                                 select p).SingleOrDefault<Perimeter>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetPerimetroById: " + e.Message);
            }

            return perimeter;
        }   

        public Perimeter GetPerimeterByName(string perimeterName)
        {
            Perimeter perimeter;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    perimeter = (from p in context.Perimeter
                                 where string.Equals(p.PerimeterName, perimeterName, 
                                                     StringComparison.OrdinalIgnoreCase)
                                 select p).SingleOrDefault<Perimeter>();
                } 
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetPerimetroByName: " + e.Message);
            }

            return perimeter;
        }       

        public Perimeter GetPerimeterByDesc(string perimeterDesc)
        {
            Perimeter perimeter;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    perimeter = (from p in context.Perimeter
                                 where string.Equals(p.PerimeterDesc, perimeterDesc, 
                                                     StringComparison.OrdinalIgnoreCase)
                                 select p).SingleOrDefault<Perimeter>();
                } 
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetPerimetroByDesc: " + e.Message);
            }

            return perimeter;
        }  
    }
}