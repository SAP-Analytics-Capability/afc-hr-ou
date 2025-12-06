using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class Vcs
    {
        [Key]
        [JsonProperty(PropertyName="vcs_id")]
        public Int32 VcsId { get; set;}

        [JsonProperty(PropertyName="vcs_code")]
        public string VcsCode { get; set;}

        [JsonProperty(PropertyName="vcs_description")]
        public string Desc { get; set;}
        
    }
}