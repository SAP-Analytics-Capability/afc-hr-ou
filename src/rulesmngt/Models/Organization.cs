using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class Organization
    {
        [Key]
        [JsonProperty(PropertyName="organization_id")]
        public Int32 OrganizationId { get; set;}

        [JsonProperty(PropertyName="organization_code")]   
        public string OrganizationCode { get; set;}

        [JsonProperty(PropertyName="organization_description")]   
        public string Desc { get; set;}
    }
}