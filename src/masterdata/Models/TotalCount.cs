    using System.Collections.Generic;
    using Newtonsoft.Json;

    namespace masterdata.Models
    {
        public class TotalCount
        {
            [JsonProperty(PropertyName = "total_count")]

            public long totalCount { get; set; }
        }

        public class TotalCountOffsetType
        {

        [JsonProperty(PropertyName = "total_count")]
        public long totalCount { get; set; }

        [JsonProperty(PropertyName = "offset_min")]
        public long offSetMin { get; set; }

        [JsonProperty(PropertyName = "offset_max")]
        public long offSetMax { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string elabType { get; set; }

        [JsonProperty(PropertyName = "esito")]
        public string esito { get; set; }
    }

}