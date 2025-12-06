using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

using hrsync.Models.Configuration;

namespace hrsync.Models
{
    public class HrSchedulerConfiguration
    {
        [Key]
        public int SchedulerConfigId { get; set; }
        public string DaysDenied { get; set; }
        public string HoursPermitted { get; set; }
        public string MinutePermitted { get; set; }
        public string MinInterval { get; set; }
        public string HrsyncType { get; set; }
        public string HrsyncTypeDesc { get; set; }
        public string ConfigArgsRequest { get; set; }
        public string ConfigArgsRequestDesc { get; set; }
        public string ConfigArgsRequestGuide { get; set; }
        public int Active { get; set; }
        public string FlagSync { get; set; }
        public string SyncPostService { get; set; }

        public SchedulerOptions ToSchedulerOptions(HrSchedulerConfiguration dbConfig)
        {
            SchedulerOptions sConfig = new SchedulerOptions();
            if (dbConfig.DaysDenied != null)
            {
                List<string> daysDenied = new List<string>();

                if (!dbConfig.DaysDenied.Contains(','))
                {
                    daysDenied.Add(dbConfig.DaysDenied);
                }
                else
                {
                    daysDenied = dbConfig.DaysDenied.Split(',').ToList();
                }
                sConfig.DaysDenied = daysDenied;
            }

            if (dbConfig.HoursPermitted != null)
            {
                List<string> hoursPermitted = new List<string>();

                if (!dbConfig.HoursPermitted.Contains(','))
                {
                    hoursPermitted = dbConfig.HoursPermitted.Split(',').ToList();
                }
                else
                {
                    hoursPermitted = dbConfig.HoursPermitted.Split(',').ToList();
                }
                sConfig.HoursPermitted = hoursPermitted;
            }

            sConfig.MinutePermitted = "-1";
            sConfig.Interval = "30";

            if (!string.IsNullOrEmpty(dbConfig.MinutePermitted))
            {
                sConfig.MinutePermitted = dbConfig.MinutePermitted;
            }
            if (!string.IsNullOrEmpty(sConfig.Interval))
            {
                sConfig.Interval = dbConfig.MinInterval;
            }

            sConfig.Active = dbConfig.Active;

            return sConfig;
        }

        public SchedulerArgsOptions ToSchedulerArgsOptions(HrSchedulerConfiguration dbConfig)
        {
            SchedulerArgsOptions sConfig = new SchedulerArgsOptions();
            if (dbConfig.ConfigArgsRequest != null)
            {
                List<string> configArgsRequest = new List<string>();

                if (!dbConfig.ConfigArgsRequest.Contains(';'))
                {
                    configArgsRequest.Add(dbConfig.ConfigArgsRequest);
                }
                else
                {
                    configArgsRequest = dbConfig.ConfigArgsRequest.Split(';').ToList();
                }
                sConfig.ConfigArgsRequest = configArgsRequest;
            }

            return sConfig;
        }

    }

}
