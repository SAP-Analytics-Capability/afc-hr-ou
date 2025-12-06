using System.Collections.Generic;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class AssociationResultV2
    {
        [JsonProperty(PropertyName = "association_result")]
        public List<AssociationV2> AssociationList { get; set; }

    }
}