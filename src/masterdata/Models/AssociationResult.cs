using System.Collections.Generic;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class AssociationResult
    {
        [JsonProperty(PropertyName = "association_result")]
        public List<Association> AssociationList { get; set; }

    }
}