using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using rulesmngt.Interfaces;
using rulesmngt.Models.Configuration;
using rulesmngt.Models;

namespace rulesmngt.Data
{
    public class ClientAccessData : IClientAccessData
    {
        private readonly IOptions<DatabaseConfiguration> DataSource;
        private readonly ILogger Logger;

        public ClientAccessData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<ClientAccessData>();
        }

        public DateTime AddNewAccess(string username, string type)
        {
            Logger.LogDebug(string.Format("{0} - Adding new client access", DateTime.Now));

            List<ClientAccess> prevca = new List<ClientAccess>();
            DateTime lastaccess = DateTime.Today;

            ClientAccess ca = new ClientAccess();
            ca.Username = username;
            ca.AccessTimestamp = DateTime.Now;
            ca.AccessType = type;
            ca.Status = "I";

            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    ClientAccess oldca = new ClientAccess();
                    //oldca.Username = username;

                    prevca = (from p in context.ClientAccess
                              where (string.Equals(p.Username, ca.Username, StringComparison.OrdinalIgnoreCase) &&
                                     string.Equals(p.AccessType, ca.AccessType, StringComparison.OrdinalIgnoreCase))
                              select p).ToList<ClientAccess>();

                    if (prevca != null && prevca.Count > 0)
                    {
                        int index = prevca.Count - 1;
                        lastaccess = prevca[index].AccessTimestamp;

                        oldca = prevca[index];
                        context.Remove(oldca);
                        context.SaveChanges();
                    }

                    context.Add(ca);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to save last access for the user.", DateTime.Now));
            }
            return lastaccess;
        }

        public ClientAccess RetrieveAccess(string username, string type)
        {
            Logger.LogDebug(string.Format("{0} - Adding new client access", DateTime.Now));

            // ClientAccess prevca = new ClientAccess();
            // DateTime lastaccess = DateTime.Today;

            // ClientAccess ca = new ClientAccess();
            // ca.Username = username;
            // ca.AccessTimestamp = DateTime.Now;
            // ca.AccessType = type;
            // ca.Status = "I";
            ClientAccess oldca = new ClientAccess();
            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    
                    //oldca.Username = username;

                    oldca = (from p in context.ClientAccess
                                                               where (p.Username.Equals(username) && p.AccessType.Equals(type))
                                                               select p).SingleOrDefault();

                    //lastaccess = Convert.ToDateTime(prevca.ExitTimestamp);

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to save last access for the user.", DateTime.Now));
            }
            return oldca;
        }

        public bool UpdateAccess(string username, string type)
        {
            Logger.LogDebug(string.Format("{0} - Updating client access", DateTime.Now));

            List<ClientAccess> prevca = new List<ClientAccess>();

            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    prevca = (from p in context.ClientAccess
                              where (string.Equals(p.Username, username, StringComparison.OrdinalIgnoreCase) &&
                                     string.Equals(p.Status, "I", StringComparison.OrdinalIgnoreCase) &&
                                     string.Equals(p.AccessType, type, StringComparison.OrdinalIgnoreCase))
                              select p).ToList<ClientAccess>();

                    if (prevca != null && prevca.Count > 0)
                    {
                        int index = prevca.Count - 1;
                        prevca[index].Status = "E";
                        prevca[index].ExitTimestamp = DateTime.Now;

                        context.Update(prevca[index]);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to save last access for the user.", DateTime.Now));
            }
            return (prevca != null && prevca.Count > 0);
        }       
    }
}