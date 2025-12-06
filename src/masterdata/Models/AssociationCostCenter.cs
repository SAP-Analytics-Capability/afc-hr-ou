using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using masterdata.Models.rulesmngt;
using masterdata.Helpers;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class AssociationCostCenter
    {
        [Key]
        [JsonProperty(PropertyName = "association_cost_center_id")]
        public int AssociationCostCenterId { get; set; }

        [JsonProperty(PropertyName = "cost_center_id")]
        public string CostCenterId { get; set; }

        [JsonProperty(PropertyName = "organization_unit_id")]
        public int OrganizationUnitId { get; set; }

        [JsonProperty(PropertyName = "cost_center_code")]
        public string CostCenterCode { get; set; }

        [JsonProperty(PropertyName = "cost_center_description")]
        public string CostCenterDescription { get; set; }

        [JsonProperty(PropertyName = "start_date_cost_center")]
        public string StartDateCostCenter { get; set; }

        [JsonProperty(PropertyName = "end_date_cost_center")]
        public string EndDateCostCenter { get; set; }

        [JsonProperty(PropertyName = "type_date_cost_center")]
        public string TypeCostCenter { get; set; }

        [JsonProperty(PropertyName = "company_code_afc")]
        public string CompanyCodeAfc { get; set; }

        [JsonProperty(PropertyName = "company_afc")]
        public string CompanyAfc { get; set; }

        [JsonProperty(PropertyName = "country_afc")]
        public string CountryAfc { get; set; }

        [JsonProperty(PropertyName = "macro_org_1")]
        public string MacroOrg1 { get; set; }

        [JsonProperty(PropertyName = "macro_org_2")]
        public string MacroOrg2 { get; set; }

        [JsonProperty(PropertyName = "vcs")]
        public string Vcs { get; set; }
        [JsonProperty(PropertyName = "vcs_description")]
        public string VcsDescription { get; set; }

        [JsonProperty(PropertyName = "process_global")]

        public string ProcessGlobal { get; set; }


        [JsonProperty(PropertyName = "process_local")]
        public string ProcessLocal { get; set; }
        [JsonProperty(PropertyName = "process_description")]
        public string ProcessDescription { get; set; }

        [JsonProperty(PropertyName = "organization")]
        public string Organization { get; set; }
        [JsonProperty(PropertyName = "organization_description")]
        public string OrganizationDescription { get; set; }

        [JsonProperty(PropertyName = "new_primo_code")]
        public string NewPrimoCode { get; set; }

        [JsonProperty(PropertyName = "primoresp")]
        public string primoresp { get; set; }

        // [JsonProperty(PropertyName = "sync_date_time")]

        // public DateTime syncDateTime { get; set; }


        public AssociationCostCenter Clone(object obj)
        {
            BwMasterObject cc = (BwMasterObject)obj;
            this.CostCenterCode = cc.costCenterCode;
            this.CostCenterDescription = cc.costCenterDescription;
            this.StartDateCostCenter = cc.startDateCostCenter;
            this.EndDateCostCenter = cc.endDateCostCenter;
            this.TypeCostCenter = cc.typeCostCenter;
            this.CompanyCodeAfc = cc.companyCodeAFC;
            this.CompanyAfc = cc.companyAFC;
            this.CountryAfc = cc.countryAFC;
            this.MacroOrg1 = cc.macroOrg1;
            this.MacroOrg2 = cc.macroOrg2;
            this.Vcs = cc.vcs;
            this.VcsDescription = cc.vcsDescription;
            this.ProcessGlobal = cc.processGlobal;
            this.ProcessLocal = cc.processLocal;
            this.ProcessDescription = cc.processDescription;
            this.Organization = cc.organization;
            this.OrganizationDescription = cc.organizationDescription;
            this.NewPrimoCode = cc.NewPrimoCode;
            this.primoresp = cc.resp;
            return (AssociationCostCenter)(object)this;
        }

        public static AssociationCostCenter CreateNew(int id, int ouid)
        {
            AssociationCostCenter cc = new AssociationCostCenter();
            cc.CostCenterId = id.ToString();
            cc.OrganizationUnitId = ouid;
            cc.CostCenterCode = "sampelcode";
            cc.CostCenterDescription = "desc";
            cc.StartDateCostCenter = "9999-01-01";
            cc.EndDateCostCenter = "9999-01-01";
            cc.TypeCostCenter = "R";
            cc.CompanyCodeAfc = "AFCC";
            cc.CompanyAfc = "Code";
            cc.CountryAfc = "IT";
            cc.MacroOrg1 = "BL";
            cc.MacroOrg2 = "GEN";
            cc.Vcs = "VCS";
            cc.ProcessGlobal = "GLOB1AL";
            cc.ProcessLocal = "LOCAL";
            cc.Organization = "ORG";
            return cc;
        }
    }
}