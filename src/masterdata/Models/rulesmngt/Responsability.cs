using Newtonsoft.Json;

namespace masterdata.Models
{
    public class Responsability
    {
        [JsonProperty(PropertyName = "responsability_code")]
        public string ResponsabilityCode { get; set; }

        [JsonProperty(PropertyName = "new_primo_code")]
        public string NewPrimoCode { get; set; }
    }
}