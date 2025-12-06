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
using rulesmngt.Data;

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

        public List<SchedulerConfiguration> GetConfigurationByName(string username)
        {
            Logger.LogDebug(string.Format("{0} - Retrieving scheduler configuration.", DateTime.Now));

            List<SchedulerConfiguration> config = new List<SchedulerConfiguration>();

            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    config = (from c in context.SchedulerConfiguration
                              where c.SchedulerName.Equals(username)
                              select c).ToList<SchedulerConfiguration>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get scheduler configuration.", DateTime.Now));
                config = null;
            }
            return config;
        }

    }
}