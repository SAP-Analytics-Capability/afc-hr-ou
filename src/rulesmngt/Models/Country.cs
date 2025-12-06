using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class Country
    {
        [Key]
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