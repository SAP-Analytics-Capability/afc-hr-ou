using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class Area
    {
        [Key]
        [JsonProperty(PropertyName="area_id")]
        public Int32 AreaId { get; set;}

        [JsonProperty(PropertyName="area_code")]
        public string AreaCode { get; set;}
    }
}