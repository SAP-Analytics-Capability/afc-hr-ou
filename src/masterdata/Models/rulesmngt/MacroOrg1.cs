using System;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class MacroOrg1
    {
        [JsonProperty(PropertyName="macroorg1_id")]
        public Int32 MacroOrg1Id { get; set;}

        [JsonProperty(PropertyName="macroorg1_code")]
        public string MacroOrg1Code { get; set;}

        [JsonProperty(PropertyName="description")]
        public string Desc { get; set;}
    }
}