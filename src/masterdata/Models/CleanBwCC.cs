using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace masterdata.Models
{
    public class CleanBwCC
    {
        [Key]
        [JsonIgnore]
        [JsonProperty(PropertyName = "association_cost_center_id")]
        public int associationCostCenterId { get; set; }

        [JsonIgnore]
        [JsonProperty(PropertyName = "organization_unit_id")]
        public int organizationUnitId { get; set; }

        [JsonProperty(PropertyName = "fa_id")]
        public int faId { get; set; }

        [JsonProperty(PropertyName = "u_org")]
        public string uOrg { get; set; }

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

        [JsonProperty(PropertyName = "primoresp")]
        public string primoresp { get; set; }

        [JsonProperty(PropertyName = "issue_time")]
        public DateTime IssueTime { get; set; }



        public CleanBwCC Clone(object obj)
        {
            BwMasterObject cc = (BwMasterObject)obj;
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
            this.primoresp = cc.resp;
            return (CleanBwCC)(object)this;
        }
    }


}