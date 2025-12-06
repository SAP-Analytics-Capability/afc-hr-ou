using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class CompanyScope
    {
        [Key]
        [JsonProperty(PropertyName = "record_id")]
        public Int32 RecordId { get; set; }
        [JsonProperty(PropertyName = "afc_new_primo_code")]
        public string AfcNewPrimoCode { get; set; }

        [JsonProperty(PropertyName = "afc_new_primo_descr")]
        public string AfcNewPrimoDescr { get; set; }

        [JsonProperty(PropertyName = "afc_bl_idis")]
        public string AfcBlIdis { get; set; }

        [JsonProperty(PropertyName = "afc_bl_igen")]
        public string AfcBlIgen { get; set; }

        [JsonProperty(PropertyName = "afc_bl_itrd")]
        public string AfcBlItrd { get; set; }

        [JsonProperty(PropertyName = "afc_bl_iren")]
        public string AfcBlIren { get; set; }

        [JsonProperty(PropertyName = "afc_bl_iret")]
        public string AfcBlIret { get; set; }

        [JsonProperty(PropertyName = "afc_bl_irel")]
        public string AfcBlIrel { get; set; }

        [JsonProperty(PropertyName = "afc_bl_iser")]
        public string AfcBlIser { get; set; }

        [JsonProperty(PropertyName = "afc_bl_ihol")]
        public string AfcBlIhol { get; set; }

        [JsonProperty(PropertyName = "afc_bl_inuk")]
        public string AfcBlInuk { get; set; }

        [JsonProperty(PropertyName = "afc_bl_ieso")]
        public string AfcBlIeso { get; set; }

        [JsonProperty(PropertyName = "afc_bl_iesl")]
        public string AfcBlIesl { get; set; }

        [JsonProperty(PropertyName = "afc_bl_imob")]
        public string AfcBlImob { get; set; }

        [JsonProperty(PropertyName = "afc_bl_prevalent")]
        public string AfcBlPrevalent { get; set; }

        [JsonProperty(PropertyName = "afc_area")]
        public string AfcArea { get; set; }

        [JsonProperty(PropertyName = "afc_perimeter")]
        public string AfcPerimeter { get; set; }

        [JsonProperty(PropertyName = "afc_perimeter_descr")]
        public string AfcPerimeterDescr { get; set; }

        [JsonProperty(PropertyName = "afc_country")]
        public string AfcCountry { get; set; }

        [JsonProperty(PropertyName = "afc_country_descr")]
        public string AfcCountryDescr { get; set; }

        [JsonProperty(PropertyName = "afc_e4e")]
        public string AfcE4e { get; set; }

        [JsonProperty(PropertyName = "po_sap_global_code")]
        public string PoSapGlobalCode { get; set; }

        [JsonProperty(PropertyName = "po_sap_global_descr")]
        public string PoSapGlobalDescr { get; set; }

        [JsonProperty(PropertyName = "po_new_primo_code")]
        public string PoNewPrimoCode { get; set; }

        [JsonProperty(PropertyName = "po_country")]
        public string PoCountry { get; set; }

        [JsonProperty(PropertyName = "po_country_descr")]
        public string PoCountryDescr { get; set; }

        [JsonProperty(PropertyName = "afc_company_code")]
        public string AfcCompanyCode { get; set; }

        [JsonProperty(PropertyName = "flag_consolidation")]
        public string FlagConsolidation { get; set; }
        

        public bool IsValid(CompanyScope cs)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(cs.AfcNewPrimoCode))
                return isValid = false;

            return isValid;

        }
    }

}
