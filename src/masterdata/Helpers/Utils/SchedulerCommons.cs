using System;
using System.Collections.Generic;
using masterdata.Models;

using Microsoft.Extensions.Logging;
using masterdata.Services;

namespace masterdata.Helpers
{
    public class SchedulerCommons
    {
        public const string MasterScheduler = "MASTER_SCHEDULER";
        public const string SnowScheduler = "SNOW_SCHEDULER";
        public const string OldScheduler = "SCHEDULER";
        public static bool SchedulerCanStart(List<SchedulerConfiguration> config, int safemode)
        {
            ILoggerFactory _Factory = new LoggerFactory();
            ILogger Logger = _Factory.CreateLogger<BackgroundServiceMasterTask>();

            bool canstart = (config != null && config.Count > 0);
            if (canstart)
            {
                int today = DateTime.Now.Day;
                int hour = DateTime.Now.Hour;
                // int minute = DateTime.Now.Minute;
                // int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int dayZero = -1;
                SchedulerConfiguration dayZeroConfig = new SchedulerConfiguration();

                foreach (SchedulerConfiguration sc in config)
                {
                    if ((canstart = (today == sc.DayOfMonth && hour == sc.TimeOfDay && sc.Active == 1 //&& minute == 0
                        ))) {
                        Logger.LogInformation("{0} - Scheduler MASTERDATA can start because DayOfMonth and Hour scheduled are Now and Active = 1", DateTime.Now);
                        break;
                    }

                    if (sc.DayOfMonth == 0)
                    {
                        dayZeroConfig = sc;
                        dayZero = sc.DayOfMonth;
                        break;
                    }
                }

                if (dayZeroConfig.DayOfMonth == dayZero && DateTime.Now.Hour == dayZeroConfig.TimeOfDay && dayZeroConfig.Active == 1 //&& DateTime.Now.Minute == 0
                    ) {
                    Logger.LogInformation("{0} - Scheduler MASTERDATA can start because DayOfMonth = 0 and Hour scheduled is Now and Active = 1", DateTime.Now);
                    canstart = true;
                }
            }
            else
            {
                Logger.LogInformation("{0} - Scheduler MASTERDATA can't start because is not scheduled in hrscheduledconfiguration", DateTime.Now);
            }
            //return (safemode == 1 ? true : canstart);
            return canstart;
        }
        public static int GetInterval(List<SchedulerConfiguration> config, string schedulerName)
        {
            int interval = 0;
            if (config != null && config.Count == 1 && config[0].DayOfMonth == 0 && config[0].SchedulerName == schedulerName)
            {
                return config[0].IntervalInMinutes;
            }
            else if (config != null && config.Count > 0)
            {
                foreach (SchedulerConfiguration sc in config)
                {
                    if (sc.DayOfMonth == DateTime.Now.Day && sc.SchedulerName == schedulerName)
                    {
                        return sc.IntervalInMinutes;
                    }
                }
                return config[0].IntervalInMinutes;
            }
            else
            {
                switch (schedulerName)
                {
                    case MasterScheduler:
                        interval = 239;
                        break;
                    case SnowScheduler:
                        interval = 30;
                        break;
                    case OldScheduler:
                        interval = 100;
                        break;
                }
                return interval;
            }

        }
    }
}
