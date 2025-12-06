using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class MacroOrg1
    {
        [Key]
        [JsonProperty(PropertyName="macroorg1_id")]
        public Int32 MacroOrg1Id { get; set;}

        [JsonProperty(PropertyName="macroorg1_code")]
        public string MacroOrg1Code { get; set;}

        [JsonProperty(PropertyName="description")]
        public string Desc { get; set;}

    }
}