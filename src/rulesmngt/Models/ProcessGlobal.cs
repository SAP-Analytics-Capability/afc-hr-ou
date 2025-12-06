using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class ProcessGlobal
    {
        [Key]
        [JsonProperty(PropertyName="process_code")]
        public string processCode { get; set;}

        [JsonProperty(PropertyName="process_description")]
        public string Desc { get; set;}
    }
}