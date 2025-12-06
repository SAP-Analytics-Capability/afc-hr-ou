using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class BusinessLines
    {
        [Key]
        [JsonProperty(PropertyName="business_lines_id")]
        public Int32 BusinessLinesId { get; set;}

        [JsonProperty(PropertyName="business_lines_name")]
        public string BusinessLinesName { get; set;}

        [JsonProperty(PropertyName="Description")]
        public string Desc { get; set;}

        [JsonProperty(PropertyName="prevalent")]
        public Int32 Prevalent { get; set;}

        [JsonProperty(PropertyName="enabled")]
        public Int32 Enabled { get; set;} 

        [JsonProperty(PropertyName="company_rules")]
        public CompanyRules CompanyRules {get; set;}  
    }
}