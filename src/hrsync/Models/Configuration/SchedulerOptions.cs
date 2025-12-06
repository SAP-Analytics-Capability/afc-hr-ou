using System.Collections.Generic;
namespace hrsync.Models.Configuration
{
    public class SchedulerOptions
    {
        public string Interval { get; set; }
        public List<string> DaysDenied { get; set; }
        public List<string> HoursPermitted { get; set; }
        public string MinutePermitted { get; set; }
        public int Active { get; set; }

    }
}
