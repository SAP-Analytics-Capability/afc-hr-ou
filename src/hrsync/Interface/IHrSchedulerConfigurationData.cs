using System;
using System.Collections.Generic;
using hrsync.Models;
using hrsync.Data;

namespace hrsync.Interface
{
    public interface IHrSchedulerConfigurationData
    {
        HrSchedulerConfiguration GetHrSchedulerConfiguration();
        HrSchedulerConfiguration GetHrSchedulerConfigurationCustom();
    }
}