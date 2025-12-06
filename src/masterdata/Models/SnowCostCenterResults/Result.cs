using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace masterdata.Models.SnowCostCenterResults
{
    public class Result
    {
        [Key]
        [JsonProperty(PropertyName = "result_id")]
        public int result_id { get; set; }

        [JsonProperty("sys_id")]
        public string SystemId { get; set; }

        [JsonProperty("u_relationship_start_date")]
        public string RelationshipStartDate { get; set; }

        [JsonProperty("u_code_ou")]
        public string OrganizationalUnitCode { get; set; }

        [JsonProperty("u_code_ou.u_legal_entity")]
        public string LegalEntity { get; set; }

        [JsonProperty("u_costcenter.u_cc_description")]
        public string CostCenterDescription { get; set; }

        [JsonProperty("u_costcenter")]
        public string CostCenterCode { get; set; }

        [JsonProperty("u_costcenter.u_company_code_afc")]
        public string CompanyCode { get; set; }

        [JsonProperty("u_costcenter.u_type")]
        public string CostCenterType { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("u_modify_typology")]
        public string Typology { get; set; }

        [JsonProperty("u_association_time")]
        public DateTime? AssociationTime { get; set; }

        [JsonProperty("sync_date_time")]
        public DateTime SyncDateTime { get; set; }

        [JsonProperty("u_costcenter.u_macroorg1")]
        public string MacroOrg1 { get; set; }

        [JsonProperty("u_costcenter.u_macroorg2")]
        public string MacroOrg2 { get; set; }

        [JsonProperty("u_costcenter.u_vcs")]
        public string Vcs { get; set; }

        [JsonProperty("u_costcenter.u_process_global")]
        public string Process { get; set; }

        [JsonProperty("u_costcenter.u_organization")]
        public string Organization { get; set; }

        [JsonProperty("u_costcenter.u_cc_comp_codeprimo")]
        public string NewPrimoCode { get; set; }

        [JsonProperty("u_code_ou.u_ou_code_10_digits")]
        public string HrOrganizationalUnitCode { get; set; }

        [JsonProperty("u_costcenter.u_company_afc")]
        public string CompanyDescription { get; set; }

        [JsonProperty("u_costcenter.u_country_afc")]
        public string Country { get; set; }

        [JsonProperty("number")]
        public string TaskNumber { get; set; }

        // hr organization unit 10 digit, codice new primo legal entity, descrizione del cost center, country afc, task number snow
    }

    public class RootObject
    {
        public List<Result> result { get; set; }
    }

    public class ResultList
    {
        public List<Result> results { get; set; }
    }
}