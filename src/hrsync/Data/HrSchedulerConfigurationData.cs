using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using hrsync.Interface;
using hrsync.Models;
using hrsync.Models.Configuration;

namespace hrsync.Data
{
    public class HrSchedulerConfigurationData : IHrSchedulerConfigurationData
    {
        private readonly IOptions<DatabaseConfiguration> DataSource;

        private readonly ILogger Logger;

        public HrSchedulerConfigurationData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<HrmasterdataOu>();
        }

        public HrSchedulerConfiguration GetHrSchedulerConfiguration()
        {
            HrSchedulerConfiguration result = null;
            try
            {
                using (var context = new HrSyncContext(DataSource))
                {
                    result = (from r in context.HrSchedulerConfiguration
                              where r.HrsyncType == "FULL"
                              select r).FirstOrDefault<HrSchedulerConfiguration>();
                }
            }
            catch (Exception e)
            {
               Logger.LogError("Error in method GetHrSchedulerconfiguration from db: " + e.Message);
            }

            return result;
        }

        public HrSchedulerConfiguration GetHrSchedulerConfigurationCustom()
        {
            HrSchedulerConfiguration result = null;
            try
            {
                using (var context = new HrSyncContext(DataSource))
                {
                    result = (from r in context.HrSchedulerConfiguration
                              where r.HrsyncType == "CUSTOM"
                              select r).FirstOrDefault<HrSchedulerConfiguration>();
                }
            }
            catch (Exception e)
            {
                Logger.LogError("Error in method GetHrSchedulerconfigurationCustom from db: " + e.Message);
            }

            return result;
        }

    }
}