using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class Entity
    {
        [Key]
        [JsonProperty(PropertyName="entity_id")]
        public Int32 EntityId { get; set; }

        [JsonProperty(PropertyName="entity_code")]
        public string EntityCode { get; set; }

        [JsonProperty(PropertyName="entity_name")]
        public string EntityName { get; set; }

        [JsonProperty(PropertyName="gbl")]
        public string Gbl { get; set; }
    }
}