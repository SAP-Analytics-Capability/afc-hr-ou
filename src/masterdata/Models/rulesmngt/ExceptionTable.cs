using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class ExceptionTable
    {
        [JsonProperty(PropertyName = "exception_id")]
        public Int32 Exception_id { get; set; }

        [JsonProperty(PropertyName = "tipo_uo")]
        public string TipoUo { get; set; }

        [JsonProperty(PropertyName = "value_tipo_uo")]
        public string ValueTipoUo { get; set; }

        [JsonProperty(PropertyName = "gbl_prevalent")]
        public string GblPrevalent { get; set; }

        [JsonProperty(PropertyName = "value_gbl_prevalent")]
        public string ValueGblPrevalent { get; set; }

        [JsonProperty(PropertyName = "npa")]
        public string Npa { get; set; }

        [JsonProperty(PropertyName = "value_npa")]
        public string ValueNpa { get; set; }

        [JsonProperty(PropertyName = "po_object")]
        public string PoObject { get; set; }

        [JsonProperty(PropertyName = "value_po_object")]
        public string ValuePoObject { get; set; }

        [JsonProperty(PropertyName = "afc_macroorg1_1")]
        public string AfcMacroorg1_1 { get; set; }

        [JsonProperty(PropertyName = "value_afc_macroorg1_1")]
        public string ValueAfcMacroorg1_1 { get; set; }
        
        [JsonProperty(PropertyName = "afc_macroorg1_2")]
        public string AfcMacroorg1_2 { get; set; }

        [JsonProperty(PropertyName = "value_afc_macroorg1_2")]
        public string ValueAfcMacroorg1_2 { get; set; }

        [JsonProperty(PropertyName = "active")]
        public string Active { get; set; }

    }

}