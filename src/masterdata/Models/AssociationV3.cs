using System.Collections.Generic;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class AssociationV3
    {
        [JsonProperty(PropertyName = "association_id")]
        public int AssociationId { get; set; }

        [JsonProperty(PropertyName = "organizational_unit")]
        public CleanHrOU Unit { get; set; }

        [JsonProperty(PropertyName = "cost_centers")]
        public List<CleanBwCC> Centers { get; set; }


        public AssociationV3() { }

    }
}