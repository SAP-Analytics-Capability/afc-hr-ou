using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class Bpc
    {

        [JsonProperty(PropertyName="bpc_id")]
        public Int32 BpcId { get; set;}

        [JsonProperty(PropertyName="bpc_code")]
        public string bpcCode { get; set;}

        [JsonProperty(PropertyName="description")]
        public string Desc { get; set;}
    }
}