using System;
using System.ComponentModel.DataAnnotations;

namespace masterdata.Models
{
    public class SchedulerConfiguration
    {
        [Key]
        public int SchedConfigId { get; set; }
        public int DayOfMonth { get; set; }
        public int TimeOfDay { get; set; }
        public string SchedulerName { get; set; }
        public int IntervalInMinutes { get; set; }
        public int Active { get; set; }
        public string FlagSync { get; set; }
    }
}
