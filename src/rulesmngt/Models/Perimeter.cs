using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class Perimeter
    {
        [Key]
        [JsonProperty(PropertyName="perimeter_id")]
        public Int32 PerimeterId { get; set;}

        [JsonProperty(PropertyName="perimeter_name")]
        public string PerimeterName { get; set;}

        [JsonProperty(PropertyName="perimeterdesc")]
        public string PerimeterDesc { get; set;}

        [JsonProperty(PropertyName="enabled")]
        public Int32 Enabled { get; set;}
    }
}