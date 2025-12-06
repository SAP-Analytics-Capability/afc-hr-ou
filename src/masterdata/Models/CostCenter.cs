using System;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class CostCenter
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

        public CostCenter()
        {
            costCenterCode = "cost_center_code";
            costCenterDescription = "cost_center_description";
            typeCostCenter = "cost_center_type";
            companyAFC = "company";
            countryAFC = "country";
            macroOrg1 = "cost_center_macro_org_1";
            macroOrg2 = "cost_center_macro_org_2";
            vcs = "cost_center_vcs";
            processGlobal = "cost_center_global_process";
            processLocal = "cost_center_local_process";
            organization = "cost_center_organization";
        }

        public CostCenter(string fittizio)
        {

            costCenterCode = "cost_center_code";
            costCenterDescription = "cost_center_description";
            typeCostCenter = "cost_center_type";
            companyAFC = "company";
            countryAFC = "country";
            macroOrg1 = "cost_center_macro_org_1";
            macroOrg2 = "cost_center_macro_org_2";
            vcs = "cost_center_vcs";
            processGlobal = "cost_center_global_process";
            processLocal = "cost_center_local_process";
            organization = "cost_center_organization";
        }
    }
}