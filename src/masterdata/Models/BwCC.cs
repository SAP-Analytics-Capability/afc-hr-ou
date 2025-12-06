using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations; 

namespace masterdata.Models
{
    public class BwCC
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

        public BwCC Clone(object obj)
        {
            BwMasterObject cc = (BwMasterObject)obj;
            this.costCenterCode = cc.costCenterCode;
            this.costCenterDescription = cc.costCenterDescription;
            this.startDateCostCenter = cc.startDateCostCenter;
            this.endDateCostCenter = cc.endDateCostCenter;
            this.typeCostCenter = cc.typeCostCenter;
            this.companyCodeAfc = cc.companyCodeAFC;
            this.companyAfc = cc.companyAFC;
            this.countryAfc = cc.countryAFC;
            this.macroOrg1 = cc.macroOrg1;
            this.macroOrg2 = cc.macroOrg2;
            this.vcs = cc.vcs;
            this.vcsDescription = cc.vcsDescription;
            this.process = cc.processGlobal;
            this.processDescription = cc.processDescription;
            this.organization = cc.organization;
            this.organizationDescription = cc.organizationDescription;
            this.newPrimoCode = cc.NewPrimoCode;
            this.primoresp = cc.resp;
            return (BwCC)(object)this;
        }
    }

    
}