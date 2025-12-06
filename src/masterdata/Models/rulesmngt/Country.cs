using System;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class Country
    {
        [JsonProperty(PropertyName="country_id")]
        public Int32 CountryId { get; set;}

        [JsonProperty(PropertyName="nation_acronym")]
        public string NationAcronym { get; set;}

        [JsonProperty(PropertyName="nation")]
        public string Nation { get; set;}

        [JsonProperty(PropertyName="enabled")]
        public Int32 Enabled { get; set;}
    }
}