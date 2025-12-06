using Newtonsoft.Json;

namespace masterdata.Models
{
    public class CatalogManager
    {
        [JsonProperty(PropertyName = "record_row")]
        public int RecordRow { get; set; }

        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }
    }
}