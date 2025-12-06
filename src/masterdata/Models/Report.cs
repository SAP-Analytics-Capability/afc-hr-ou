using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class Report
    {
        [Key]
        [JsonProperty(PropertyName="report_id")]
        public Int32 reportId { get; set; }

        [JsonProperty(PropertyName="date_time")]
        public DateTime date_time { get; set; }

        [JsonProperty(PropertyName="message")]
        public string message { get; set; }

        // [JsonProperty(PropertyName="hrmasterdataous")]
        // public List<HrmasterdataOu> hrmasterdataous { get; set; }  
    }
}