using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class Responsability
    {
        [Key]
        [JsonProperty(PropertyName = "responsability_code")]
        public string ResponsabilityCode { get; set; }

        [JsonProperty(PropertyName = "new_primo_code")]
        public string NewPrimoCode { get; set; }
    }
}