using System.Collections.Generic;
using Newtonsoft.Json;

namespace bwsync.Models
{
    public class Error
    {
        public Error(string message)
        {
            this.Message = message;
        }

        public Error(string message, List<object> items)
        {
            this.Message = message;
            this.Items = items;
        }

        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Items { get; set; }
    }
}