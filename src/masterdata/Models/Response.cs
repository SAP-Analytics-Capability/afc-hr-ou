using System.Collections.Generic;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class Response
    {
        [JsonProperty(PropertyName = "message")]
        public string message { get; set; }

    }
}