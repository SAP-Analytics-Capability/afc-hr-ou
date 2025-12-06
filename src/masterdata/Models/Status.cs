    using Newtonsoft.Json;

    namespace masterdata.Models
    {
        public class Status
        {
            [JsonProperty(PropertyName = "caller")]
            public string Caller { get; set; }
            [JsonProperty(PropertyName = "access_type")]
            public string AccessType { get; set; }
            [JsonProperty(PropertyName = "access_timestamp")]
            public string AccessTimestamp { get; set; }
            [JsonProperty(PropertyName = "exit_timestamp")]
            public string ExitTimestap { get; set; }
            [JsonProperty(PropertyName = "status")]
            public string ProcessStatus { get; set; }
        }
    }