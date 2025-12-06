using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using masterdata.Interfaces;
using masterdata.Models.SnowCostCenterResults;
using masterdata.Models.Configuration;
using masterdata.Models;

namespace masterdata.Data
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
                using (MasterDataContext context = new MasterDataContext(DataSource))
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

        public List<ClientAccess> RetrieveAccess(string username, string type)
        {
            Logger.LogDebug(string.Format("{0} - Retriew access client access for the user {1} and type {2}", DateTime.Now,username,type));

            // ClientAccess prevca = new ClientAccess();
            // DateTime lastaccess = DateTime.Today;

            // ClientAccess ca = new ClientAccess();
            // ca.Username = username;
            // ca.AccessTimestamp = DateTime.Now;
            // ca.AccessType = type;
            // ca.Status = "I";

            List<ClientAccess> oldca = new List<ClientAccess>();
            List<ClientAccess> oldcares = new List<ClientAccess>();
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {

                    //oldca.Username = username;

                    if (!(username is null))
                    {
                        oldca = (from p in context.ClientAccess orderby p.AccessTimestamp descending, p.AccessId descending
                                 where (p.Username.Equals(username) && p.AccessType.Equals(type) // &&
                                 // string.Equals(p.Status, "E", StringComparison.OrdinalIgnoreCase)
                                 )
                                 select p).ToList<ClientAccess>();
                    }
                    else
                    {
                        oldca = (from p in context.ClientAccess orderby p.AccessTimestamp descending, p.AccessId descending
                                 where (p.AccessType.Equals(type) // &&
                                 // string.Equals(p.Status, "E", StringComparison.OrdinalIgnoreCase)
                                 )
                                 select p).ToList<ClientAccess>();
                    }

                    if (oldca.Count > 0)
                    { 
                        oldcares.Add(oldca[0]);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable retriew last access for the user {1} and type {2}.", DateTime.Now,username,type));
            }
            return oldcares;
        }

        public bool UpdateAccess(string username, string type)
        {
            Logger.LogDebug(string.Format("{0} - Updating client access", DateTime.Now));

            List<ClientAccess> prevca = new List<ClientAccess>();

            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    if (!(username is null))
                    { 
                    prevca = (from p in context.ClientAccess orderby p.AccessTimestamp ascending
                              where (string.Equals(p.Username, username, StringComparison.OrdinalIgnoreCase) &&
                                     string.Equals(p.Status, "I", StringComparison.OrdinalIgnoreCase) &&
                                     string.Equals(p.AccessType, type, StringComparison.OrdinalIgnoreCase))
                              select p).ToList<ClientAccess>();
                    }
                    else
                    { 
                        prevca = (from p in context.ClientAccess orderby p.AccessTimestamp ascending
                                  where (
                                         string.Equals(p.Status, "I", StringComparison.OrdinalIgnoreCase) &&
                                         string.Equals(p.AccessType, type, StringComparison.OrdinalIgnoreCase))
                                  select p).ToList<ClientAccess>();
                    }

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