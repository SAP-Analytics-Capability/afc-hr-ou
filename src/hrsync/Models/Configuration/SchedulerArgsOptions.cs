using System.Collections.Generic;
namespace hrsync.Models.Configuration
{
    public class SchedulerArgsOptions
    {
        public List<string> ConfigArgsRequest { get; set; }
        public string HrsyncType { get; set; }
        public string HrsyncTypeDesc { get; set; }
        public string ConfigArgsRequestDesc { get; set; }

    }
}