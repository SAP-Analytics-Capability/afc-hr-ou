using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hrsync.Models
{
    public class Report
    {
        [Key]
        public Int32 reportId { get; set; }
        public DateTime date_time { get; set; }
        public string message { get; set; }
        public string changedFound { get; set; } // Y or N or KO
        public string dummyFound { get; set; } // Y or N or KO
        public string noccFound { get; set; } // Y or N or KO
        public string saved { get; set; } // Y or N

        // [JsonProperty(PropertyName="hrmasterdataous")]
        // public List<HrmasterdataOu> hrmasterdataous { get; set; }  
    }
}