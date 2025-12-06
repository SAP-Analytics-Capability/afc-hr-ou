using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class PerimetroConsolidamentoData : IPerimetroConsolidamentoData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public PerimetroConsolidamentoData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void AddNewPerimetroConsolidamento(PerimetroConsolidamento pc)
        {
            using (RulesContext context = new RulesContext(databaseConfiguration))
            {
                context.Add(pc);
                context.SaveChanges();

            }
        }

        public List<PerimetroConsolidamento> GetByNewPrimoDescr(string afcNewPrimoDescr)
        {
            List<PerimetroConsolidamento> pclist = new List<PerimetroConsolidamento>();

                try
                {
                    using (var context = new RulesContext(databaseConfiguration))
                    {
                        pclist = (from pc in context.PerimetroConsolidamento
                                     where pc.afcNewPrimoDescr == afcNewPrimoDescr
                                     select pc).ToList<PerimetroConsolidamento>();
                    }
                }
                catch (Exception e)
                {
                    throw new System.ArgumentException("Error in method GetPerimetroConsolidamentobyNewPrimoDescr: " + e.Message);
                }

                return pclist;
            }   

            // public Perimeter GetPerimeterByName(string perimeterName)
            // {
            //     Perimeter perimeter;

            //     try
            //     {
            //         using (var context = new RulesContext(databaseConfiguration))
            //         {
            //             perimeter = (from p in context.Perimeter
            //                          where string.Equals(p.PerimeterName, perimeterName, 
            //                                              StringComparison.OrdinalIgnoreCase)
            //                          select p).SingleOrDefault<Perimeter>();
            //         } 
            //     }
            //     catch (Exception e)
            //     {
            //         throw new System.ArgumentException("Error in method GetPerimetroByName: " + e.Message);
            //     }

            //     return perimeter;
            // }       


        }
    }