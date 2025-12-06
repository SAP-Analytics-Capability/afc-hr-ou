using System;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class Entity
    {
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