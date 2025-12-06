using System;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class Area
    {
        [JsonProperty(PropertyName="area_id")]
        public Int32 AreaId { get; set;}

        [JsonProperty(PropertyName="area_code")]
        public string AreaCode { get; set;}
    }
}