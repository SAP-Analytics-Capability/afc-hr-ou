using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class ActivityList
    {
        [Key]
        [JsonProperty(PropertyName="activity_id")]
        public Int32 ActivityId { get; set;}

        [JsonProperty(PropertyName="activity_name")]
        public string ActivityName { get; set;}

        [JsonProperty(PropertyName="desc")]
        public string Desc { get; set;}
    }
}