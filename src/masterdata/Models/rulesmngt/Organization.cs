using System;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class Organization
    {
        [JsonProperty(PropertyName="organization_id")]
        public Int32 OrganizationId { get; set;}

        [JsonProperty(PropertyName="organization_code")]   
        public string OrganizationCode { get; set;}

        [JsonProperty(PropertyName="organization_description")]   
        public string Desc { get; set;}
    }
}