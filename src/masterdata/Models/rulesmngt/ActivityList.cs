using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class ActivityList
    {
        [JsonProperty(PropertyName="activity_id")]
        public Int32 ActivityId { get; set;}

        [JsonProperty(PropertyName="activity_name")]
        public string ActivityName { get; set;}

        [JsonProperty(PropertyName="desc")]
        public string Desc { get; set;}
    }
}