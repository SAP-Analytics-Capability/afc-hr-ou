using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class Bpc
    {

        [Key]
        [JsonProperty(PropertyName="bpc_id")]
        public Int32 BpcId { get; set;}

        [JsonProperty(PropertyName="bpc_code")]
        public string bpcCode { get; set;}

        [JsonProperty(PropertyName="description")]
        public string Desc { get; set;}
    }
}