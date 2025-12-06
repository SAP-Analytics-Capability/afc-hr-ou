using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace bwsync.Models
{
    public class BwMasterObject
    {
        [JsonProperty(PropertyName = "cost_center_code")]
        public string costCenterCode { get; set; }

        [JsonProperty(PropertyName = "cost_center_description")]
        public string costCenterDescription { get; set; }

        [JsonProperty(PropertyName = "start_date_cost_center")]
        public string startDateCostCenter { get; set; }

        [JsonProperty(PropertyName = "end_date_cost_center")]
        public string endDateCostCenter { get; set; }

        [JsonProperty(PropertyName = "type_date_cost_center")]
        public string typeCostCenter { get; set; }

        [JsonProperty(PropertyName = "company_code_afc")]
        public string companyCodeAFC { get; set; }

        [JsonProperty(PropertyName = "company_afc")]
        public string companyAFC { get; set; }

        [JsonProperty(PropertyName = "country_afc")]
        public string countryAFC { get; set; }

        [JsonProperty(PropertyName = "macroOrg1")]
        public string macroOrg1 { get; set; }

        [JsonProperty(PropertyName = "macroOrg2")]
        public string macroOrg2 { get; set; }

        [JsonProperty(PropertyName = "vcs")]
        public string vcs { get; set; }

        [JsonProperty(PropertyName = "process_global")]
        public string processGlobal { get; set; }

        [JsonProperty(PropertyName = "process_local")]
        public string processLocal { get; set; }

        [JsonProperty(PropertyName = "organization")]
        public string organization { get; set; }

        [JsonProperty(PropertyName = "responsability")]
        public string responsability { get; set; }
    }
}