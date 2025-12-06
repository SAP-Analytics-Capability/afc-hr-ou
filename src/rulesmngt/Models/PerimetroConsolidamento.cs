using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class PerimetroConsolidamento
    {
        [Key]
        public long record_id { get; set;}
        [JsonProperty(PropertyName="afc_new_primo_code")]
        public string afcNewPrimoCode { get; set;}
        [JsonProperty(PropertyName="afc_new_primo_descr")]
        public string afcNewPrimoDescr { get; set;}
        [JsonProperty(PropertyName="po_new_primo_code")]
        public string poNewPrimoCode { get; set;}
        [JsonProperty(PropertyName="po_global_code")]
        public string poGlobalCode { get; set;}
        [JsonProperty(PropertyName="po_global_descr")]
        public string poGlobalDescr{ get; set;}
        [JsonProperty(PropertyName="afc_BL_iDIS")]
        public string afcBlIdis{ get; set;}
        [JsonProperty(PropertyName="afc_BL_iGEN")]
        public string afcBlIgen{ get; set;}
        [JsonProperty(PropertyName="afc_BL_iTRD")]
        public string afcBlItrd{ get; set;}
        [JsonProperty(PropertyName="afc_BL_iREN")]
        public string afcBlIren{ get; set;}
        [JsonProperty(PropertyName="afc_BL_iRET")]
        public string afcBlIret{ get; set;}
        [JsonProperty(PropertyName="afc_BL_iREL")]
        public string afcBlIrel{ get; set;}
        [JsonProperty(PropertyName="afc_BL_iSER")]
        public string afcBlIser{ get; set;}
        [JsonProperty(PropertyName="afc_BL_iHOL")]
        public string afcBlIhol{ get; set;}
        [JsonProperty(PropertyName="afc_BL_iNUK")]      
        public string afcBlInuk{ get; set;}
        [JsonProperty(PropertyName="afc_BL_iESO")]
        public string afcBlIeso{ get; set;}
        [JsonProperty(PropertyName="afc_BL_iESL")]
        public string afcBlIesl { get; set; }
        [JsonProperty(PropertyName = "afc_BL_iMOB")]
        public string afcBlImob { get; set; }
        [JsonProperty(PropertyName = "afc_prevalent")]
        public string afcBlPrevalent{ get; set;}
        [JsonProperty(PropertyName="afc_area")]
        public string afcArea{ get; set;} 
        [JsonProperty(PropertyName="afc_perimeter")]      
        public string afcPerimeter{ get; set;}
        [JsonProperty(PropertyName="afc_perimeter_descr")]
        public string afcPerimeterDescr{ get; set;}
        [JsonProperty(PropertyName="afc_country")]
        public string afcCountry{ get; set;}
        [JsonProperty(PropertyName="afc_country_descr")]
        public string afcCountryDescr{ get; set;}
        [JsonProperty(PropertyName="po_country")]
        public string poCountry{ get; set;}
        [JsonProperty(PropertyName="po_country_descr")]
        public string poCountryDescr{ get; set;}
        [JsonProperty(PropertyName="afc_company_code")]
        public string afcCompanyCode{ get; set;}
        [JsonProperty(PropertyName="afc_società_e4e")]
        public string afcSocieta{ get; set;}
      
       
    }
}
