using System;
using System.Collections.Generic;

using rulesmngt.Models;


namespace rulesmngt.Interfaces
{
    public interface ISchedulerConfigurationData
    {
        List<SchedulerConfiguration> GetConfigurationByName(string username);
    }
}