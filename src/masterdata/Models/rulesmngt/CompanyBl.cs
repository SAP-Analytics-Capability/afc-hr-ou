using System;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class CompanyBl
    {
        [JsonProperty(PropertyName="association_id")]
        public string AssociationId { get; set;}

        [JsonProperty(PropertyName="system_company_code")]
        public string SystemCompanyCode { get; set;}

        [JsonProperty(PropertyName="bl")]
        public string Bl { get; set;}
    }
}