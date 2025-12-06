using System;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class Perimeter
    {
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