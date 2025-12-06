using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations; 

namespace masterdata.Models
{
    public class BwCCBCK
    {
     [Key]
        [JsonProperty(PropertyName = "association_cost_center_id")]
        public int associationCostCenterId { get; set; }
        [JsonProperty(PropertyName = "organization_unit_id")]
        public int organizationUnitId { get; set; }
        [JsonProperty(PropertyName = "cost_center_code")]
        public string costCenterCode { get; set; }

        [JsonProperty(PropertyName = "cost_center_description")]
        public string costCenterDescription { get; set; }

        [JsonProperty(PropertyName = "u_org")]
        public string uOrg { get; set; }

        [JsonProperty(PropertyName = "start_date_cost_center")]
        public string startDateCostCenter { get; set; }

        [JsonProperty(PropertyName = "end_date_cost_center")]
        public string endDateCostCenter { get; set; }

        [JsonProperty(PropertyName = "type_date_cost_center")]
        public string typeCostCenter { get; set; }

        [JsonProperty(PropertyName = "company_code_afc")]
        public string companyCodeAfc { get; set; }

        [JsonProperty(PropertyName = "company_afc")]
        public string companyAfc { get; set; }

        [JsonProperty(PropertyName = "country_afc")]
        public string countryAfc { get; set; }

        [JsonProperty(PropertyName = "macro_org_1")]
        public string macroOrg1 { get; set; }

        [JsonProperty(PropertyName = "macro_org_2")]
        public string macroOrg2 { get; set; }

        [JsonProperty(PropertyName = "vcs")]
        public string vcs { get; set; }

        [JsonProperty(PropertyName = "vcs_description")]
        public string vcsDescription { get; set; }

        [JsonProperty(PropertyName = "process")]
        public string process { get; set; }

        [JsonProperty(PropertyName = "process_description")]
        public string processDescription { get; set; }

        [JsonProperty(PropertyName = "organization")]
        public string organization { get; set; }

        [JsonProperty(PropertyName = "organization_description")]
        public string organizationDescription { get; set; }

        [JsonProperty(PropertyName = "new_primo_code")]
        public string newPrimoCode { get; set; }
        
        [JsonProperty(PropertyName = "primoresp")]
        public string primoresp { get; set; }
        [JsonProperty(PropertyName = "sync_date_time")]
        public DateTime SyncDateTime { get; set; }

        public BwCCBCK castBwCCBCK(BwCC bwcc){
            BwCCBCK bck = new BwCCBCK();

            bck.associationCostCenterId = bwcc.associationCostCenterId;
            bck.companyAfc = bwcc.companyAfc;
            bck.companyCodeAfc = bwcc.companyCodeAfc;
            bck.costCenterCode = bwcc.costCenterCode;
            bck.costCenterDescription = bwcc.costCenterDescription;
            bck.countryAfc = bwcc.countryAfc;
            bck.endDateCostCenter = bwcc.endDateCostCenter;
            bck.macroOrg1 = bwcc.macroOrg1;
            bck.macroOrg2 = bwcc.macroOrg2;
            bck.newPrimoCode = bwcc.newPrimoCode;
            bck.organization = bwcc.organization;
            bck.organizationDescription = bwcc.organizationDescription;
            bck.organizationUnitId = bwcc.organizationUnitId;
            bck.primoresp = bwcc.primoresp;
            bck.process = bwcc.process;
            bck.processDescription = bwcc.processDescription;
            bck.startDateCostCenter = bwcc.startDateCostCenter;
            bck.SyncDateTime = bwcc.SyncDateTime;
            bck.typeCostCenter = bwcc.typeCostCenter;
            bck.uOrg = bwcc.uOrg;
            bck.vcs = bwcc.vcs;
            bck.vcsDescription = bwcc.vcsDescription; 
            return bck;
        }
    }
}