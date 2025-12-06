using System;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class Vcs
    {
        [JsonProperty(PropertyName="vcs_id")]
        public Int32 VcsId { get; set;}

        [JsonProperty(PropertyName="vcs_code")]
        public string VcsCode { get; set;}

        [JsonProperty(PropertyName="vcs_description")]
        public string Desc { get; set;}
    }
}