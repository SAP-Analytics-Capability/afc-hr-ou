using System.Collections.Generic;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class AssociationResultV1
    {
        [JsonProperty(PropertyName = "association_result")]
        public List<AssociationV1> AssociationList { get; set; }

    }
}