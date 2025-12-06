using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class ActivityAssociationGroup
    {
        [JsonProperty(PropertyName = "typology_object")]
        public string TypologyObject { get; set; }

        [JsonProperty(PropertyName = "bpc_code")]
        public string BpcCode { get; set; }

        [JsonProperty(PropertyName = "macro_org_1")]
        public string MacroOrg1 { get; set; }

        [JsonProperty(PropertyName = "po_object_abbr")]
        public string PoObjectAbbr { get; set; }
    }
}