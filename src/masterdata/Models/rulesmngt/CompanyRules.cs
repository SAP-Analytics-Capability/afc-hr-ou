using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class CompanyRules
    {
        [Key]
        [JsonProperty(PropertyName = "company_code")]
        public Int32 CompanyCode { get; set; }

        [JsonProperty(PropertyName = "newprimo_code")]
        public string NewPrimoCode { get; set; }

        [JsonProperty(PropertyName = "sap_hr_code")]
        public string SapHrCode { get; set; }

        [JsonProperty(PropertyName = "newprimo_description")]
        public string NewPrimoDesc { get; set; }

        [JsonProperty(PropertyName = "area")]
        public Area Area { get; set; }

        [JsonProperty(PropertyName = "perimeter")]
        public Perimeter Perimeter { get; set; }

        [JsonProperty(PropertyName = "code_nation")]
        public Country CodeNation { get; set; }

        [JsonProperty(PropertyName = "code_nation_sap")]
        public string CodeNationSap { get; set; }

        [JsonProperty(PropertyName = "e4e")]
        public Int32 E4E { get; set; }

        [JsonProperty(PropertyName = "business_lines")]
        public ICollection<BusinessLines> BusinessLines { get; set; }

        [JsonProperty(PropertyName = "sap_hr_desc")]
        public string SapHrDesc { get; set; }

        [JsonProperty(PropertyName = "afc_company_code")]
        public string AfcCompanyCode { get; set; }

        [JsonProperty(PropertyName = "flag_consolidation")]
        public string FlagConsolidation { get; set; }

    }
}