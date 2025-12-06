using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace masterdata.Models
{
    public class FunctionalAck
    {
        [Key]
        [JsonProperty(PropertyName = "fa_id")]
        public int idFa { get; set; }

        [JsonProperty(PropertyName = "caller")]

        public string caller { get; set; }

        [JsonProperty(PropertyName = "issue_time")]

        public DateTime issueTime { get; set; }

        [JsonProperty(PropertyName = "expiration_date")]
        public DateTime expirationDate { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string status { get; set; }

        [JsonProperty(PropertyName = "input_type")]
        public string inputType { get; set; }

        public FunctionalAck() { }

        public FunctionalAck(string caller, DateTime issueTime, string inputType, string faStatus)
        {
            this.caller = caller;
            this.issueTime = issueTime;
            this.expirationDate = issueTime.AddDays(10);
            this.inputType = inputType;
            this.status = faStatus;
        }
    }
}