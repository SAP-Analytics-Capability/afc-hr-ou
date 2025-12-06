using System;
using System.Collections.Generic;
using masterdata.Models.rulesmngt;
using masterdata.Helpers;
using Newtonsoft.Json;

namespace masterdata.Models
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

        [JsonProperty(PropertyName = "macro_org_1")]
        public string macroOrg1 { get; set; }

        [JsonProperty(PropertyName = "macro_org_2")]
        public string macroOrg2 { get; set; }

        [JsonProperty(PropertyName = "vcs")]
        public string vcs { get; set; }
        [JsonProperty(PropertyName = "vcs_description")]
        public string vcsDescription { get; set; }

        [JsonProperty(PropertyName = "process_global")]
        public string processGlobal { get; set; }

        [JsonProperty(PropertyName = "process_local")]
        public string processLocal { get; set; }
        [JsonProperty(PropertyName = "process_description")]
        public string processDescription { get; set; }

        [JsonProperty(PropertyName = "organization")]
        public string organization { get; set; }
        [JsonProperty(PropertyName = "organization_description")]
        public string organizationDescription { get; set; }

        [JsonProperty(PropertyName = "new_primo_code")]
        public string NewPrimoCode { get; set; }

        [JsonProperty(PropertyName = "responsability")]
        public string responsability { get; set; }

        [JsonProperty(PropertyName = "primoresp")]
        public string resp { get; set; }

        public BwMasterObject()
        { }

        public BwMasterObject(string type, HrmasterdataOu orgUnit, CompanyRules companyRules, QueryBuilder queryBuilder,string description, string primoResp)
        {

            costCenterCode = type + "000000";
            costCenterDescription = description;
            typeCostCenter = type;
            startDateCostCenter = DateTime.Today.ToString("yyyy-MM-dd");
            endDateCostCenter = DateTime.Today.ToString("yyyy-MM-dd");
            companyAFC = companyRules.SapHrDesc;
            countryAFC = companyRules.Perimeter.PerimeterDesc;
            companyCodeAFC = companyRules.AfcCompanyCode;
            companyAFC = companyRules.SapHrDesc;
            if (!String.IsNullOrEmpty(queryBuilder.MacroOrg1))
                macroOrg1 = queryBuilder.MacroOrg1.ToUpper();
            else
                macroOrg1 = "";
            if (!String.IsNullOrEmpty(queryBuilder.MacroOrg2))
                macroOrg2 = queryBuilder.MacroOrg2.ToUpper();
            else
                macroOrg2 = "";
            if (!String.IsNullOrEmpty(queryBuilder.Vcs))
                vcs = queryBuilder.Vcs;
            else
                vcs = "";
            if (!String.IsNullOrEmpty(queryBuilder.VcsDescription))
                vcsDescription = queryBuilder.VcsDescription;
            else
                vcsDescription = "";
            if (!String.IsNullOrEmpty(queryBuilder.Process))
                processGlobal = queryBuilder.Process;
            else
                processGlobal = "";
            if (!String.IsNullOrEmpty(queryBuilder.ProcessDescription))
                processDescription = queryBuilder.ProcessDescription;
            else
                processDescription = "";
            if (!String.IsNullOrEmpty(queryBuilder.Organization))
                organization = queryBuilder.Organization;
            else
                organization = "";
            if (!String.IsNullOrEmpty(queryBuilder.OrganizationDescription))
                organizationDescription = queryBuilder.OrganizationDescription;
            else
                organizationDescription = "";
            NewPrimoCode = companyRules.NewPrimoCode;
            resp = primoResp;


        }
        public BwMasterObject(string fittizio, HrmasterdataOu orgUnit)
        {

            costCenterCode = "cost_center_code";
            costCenterDescription = "cost_center_description";
            typeCostCenter = "fictitious";
            companyAFC = "no_companyAFC";
            countryAFC = "no_companyAFC";
            companyCodeAFC = "";
            macroOrg1 = "cost_center_macro_org_1";
            macroOrg2 = "cost_center_macro_org_2";
            vcs = "cost_center_vcs";
            processGlobal = "cost_center_global_process";
            processLocal = "cost_center_local_process";
            processDescription = "cost_center_process_description";
            organization = "cost_center_organization";
        }

        public BwMasterObject(string afcCode, string vcS, string macroOrg_1, string macroOrg_2, string country)
        {

            costCenterCode = "cost_center_code";
            costCenterDescription = "Errore filtro CC";
            typeCostCenter = "ERRORE";
            companyAFC = afcCode;
            countryAFC = country;
            companyCodeAFC = "";
            macroOrg1 = macroOrg_1;
            macroOrg2 = macroOrg_2;
            vcs = vcS;
            processGlobal = "cost_center_global_process";
            processLocal = "cost_center_local_process";
            organization = "cost_center_organization";
        }
    }
}