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
using masterdata.Helpers;

namespace masterdata.Data
{
    public class SchedulerConfigurationData : ISchedulerConfigurationData
    {
        private readonly IOptions<DatabaseConfiguration> DataSource;
        private readonly ILogger Logger;

        public SchedulerConfigurationData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<ClientAccessData>();
        }
        public SchedulerConfiguration UpdateActive(string username)
        {
            Logger.LogDebug(string.Format("{0} - Retrieving scheduler configuration.", DateTime.Now));

            SchedulerConfiguration config = new SchedulerConfiguration();

            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    config = (from c in context.SchedulerConfiguration
                              where c.SchedulerName.Equals(username)
                              select c).FirstOrDefault();
                    if (config.Active == 0)
                    {
                        config.Active = 1;
                        context.Update(config);
                        context.SaveChanges();
                    }
                    else
                    {
                        config.Active = 0;
                        context.Update(config);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get scheduler configuration.", DateTime.Now));
            }
            return config;
        }
        public List<SchedulerConfiguration> GetConfiguration()
        {
            Logger.LogDebug(string.Format("{0} - Retrieving scheduler configuration.", DateTime.Now));

            List<SchedulerConfiguration> config = new List<SchedulerConfiguration>();

            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    config = (from c in context.SchedulerConfiguration
                              select c).ToList<SchedulerConfiguration>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get scheduler configuration.", DateTime.Now));
            }
            return config;
        }
        public List<SchedulerConfiguration> GetConfigurationByName(string username)
        {
            Logger.LogDebug(string.Format("{0} - Retrieving scheduler configuration.", DateTime.Now));

            List<SchedulerConfiguration> config = new List<SchedulerConfiguration>();

            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    config = (from c in context.SchedulerConfiguration
                              where c.SchedulerName.Equals(username)
                              select c).ToList<SchedulerConfiguration>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get scheduler configuration.", DateTime.Now));
            }
            return config;
        }

        public List<SchedulerConfiguration> GetConfigurationByDay(int day)
        {
            Logger.LogDebug(string.Format("{0} - Retrieving scheduler Configuration by DayOfMonth.", DateTime.Now));

            List<SchedulerConfiguration> config = new List<SchedulerConfiguration>();

            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    config = (from c in context.SchedulerConfiguration
                              where c.DayOfMonth.Equals(day) && c.SchedulerName.Equals(SchedulerCommons.MasterScheduler)
                              select c).ToList<SchedulerConfiguration>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get scheduler Configuration by DayOfMonth.", DateTime.Now));
            }
            return config;
        }

    }
}