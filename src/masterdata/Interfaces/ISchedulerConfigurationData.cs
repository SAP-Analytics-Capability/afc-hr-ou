using System;
using System.Collections.Generic;

using masterdata.Models;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Interfaces
{
    public interface ISchedulerConfigurationData
    {
        List<SchedulerConfiguration> GetConfiguration();
        List<SchedulerConfiguration> GetConfigurationByName(string username);
        SchedulerConfiguration UpdateActive(string username);
        List<SchedulerConfiguration> GetConfigurationByDay(int day);
    }
}