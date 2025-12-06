using System;
using System.Collections.Generic;
using System.Linq;
using rulesmngt.Models;
using rulesmngt.Interfaces;
using rulesmngt.Interfaces.Adapters;
using System.Threading;
using System.Threading.Tasks;

namespace rulesmngt.Helpers
{
    public class SchedulerCommons
    {
        public const string CatalogScheduler = "CATALOG_SCHEDULER";
        public const string HrsyncFullScheduler = "extractionfullsync";
        public const string HrsyncCustomScheduler = "extractioncustomsync";

        public static bool SchedulerCanStart(List<SchedulerConfiguration> config)
        {
            bool canstart = (config != null && config.Count > 0);
            if (canstart)
            {
                //Per la schedulazione dell'aggiornamento dei cataloghi è sorta l'esigenza di schedularla in base al giorno della settimana
                //  cioè lun, mar, mer.. etc. Dunque il campo dayOfMonth rappresenta in realtà solo in qesto caso 
                //   il giorno della settimana. Quindi da 0 (domenica) a 6 (sabato). 

                //int today = DateTime.Now.Day;
                int hour = DateTime.Now.Hour;
                int today_week = DateTime.Now.DayOfWeek.GetHashCode();
                
                // int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                // SchedulerConfiguration dayZeroConfig = new SchedulerConfiguration();

                foreach (SchedulerConfiguration sc in config)
                {
                    if ((canstart = (today_week == sc.DayOfMonth && hour == sc.TimeOfDay && sc.Active == 1)))
                        break;

                    // if (sc.DayOfMonth == 0)
                    //    dayZeroConfig = sc;
                }

                //if (DateTime.Now.Day == daysInMonth && DateTime.Now.Hour == dayZeroConfig.TimeOfDay && DateTime.Now.Minute == 0)
                //    canstart = true;
            }
            return canstart;
        }

        public static int GetInterval(List<SchedulerConfiguration> config, string schedulerName)
        {
            int interval = 0;
            if (config != null && config.Count > 0)
            {
                return interval = config[0].IntervalInMinutes;
            }
            else
            {
                interval = 30;
                return interval;
            }
        }

        public static int GetIntervalByDay(List<SchedulerConfiguration> config, string schedulerName)
        {
            int default_interval = 30;
            int today_week = DateTime.Now.DayOfWeek.GetHashCode();
            if (config != null && config.Count > 0)
            {
                foreach (SchedulerConfiguration sc in config)
                {
                    if (sc.SchedulerName == schedulerName && sc.DayOfMonth == today_week)
                    {
                        return sc.IntervalInMinutes;
                    }
                }
                return default_interval;
            }
            else
            {
                return default_interval;
            }
        }

    }
}
