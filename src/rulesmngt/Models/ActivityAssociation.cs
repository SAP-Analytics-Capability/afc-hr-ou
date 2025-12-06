using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class ActivityAssociation
    {
        [Key]
        [JsonProperty(PropertyName = "record_id")]
        public Int32 RecordId { get; set; }

        [JsonProperty(PropertyName = "po_attribute")]
        public string PoAttribute { get; set; }

        [JsonProperty(PropertyName = "object_type_id")]
        public string ObjectTypeId { get; set; }

        [JsonProperty(PropertyName = "po_object_id")]
        public string PoObjectId { get; set; }

        [JsonProperty(PropertyName = "po_object_abbr")]
        public string PoObjectAbbr { get; set; }

        [JsonProperty(PropertyName = "po_bpc_code")]
        public string PoBpcCode { get; set; }

        [JsonProperty(PropertyName = "po_description")]
        public string PoDescription { get; set; }

        [JsonProperty(PropertyName = "po_npa")]
        public string PoNpa { get; set; }

        [JsonProperty(PropertyName = "po_perimeter")]
        public string PoPerimeter { get; set; }

        [JsonProperty(PropertyName = "afc_macroorg1")]
        public string AfcMacroorg1 { get; set; }

        [JsonProperty(PropertyName = "afc_macroorg1_desc")]
        public string AfcMacroorg1Desc { get; set; }

        [JsonProperty(PropertyName = "afc_macroorg2")]
        public string AfcMacroorg2 { get; set; }

        [JsonProperty(PropertyName = "afc_macroorg2_desc")]
        public string AfcMacroorg2Desc { get; set; }

        [JsonProperty(PropertyName = "or_process")]
        public string OrProcess { get; set; }

        [JsonProperty(PropertyName = "or_process_desc")]
        public string OrProcessDesc { get; set; }

        [JsonProperty(PropertyName = "or_organization")]
        public string OrOrganization { get; set; }

        [JsonProperty(PropertyName = "or_organization_desc")]
        public string OrOrganizationDesc { get; set; }

        [JsonProperty(PropertyName = "or_vcs")]
        public string OrVcs { get; set; }

        [JsonProperty(PropertyName = "or_vcs_desc")]
        public string OrVcsDesc { get; set; }
        //[JsonIgnore]
        [JsonProperty(PropertyName = "bpc_object_id")]
        public string BpcObjectId { get; set; }
        //[JsonIgnore]
        [JsonProperty(PropertyName = "bpc_object_abbr")]
        public string BpcObjectAbbr { get; set; }
        //[JsonIgnore]
        [JsonProperty(PropertyName = "bpc_code")]
        public string BpcCode { get; set; }
        //[JsonIgnore]
        [JsonProperty(PropertyName = "bpc_desc")]
        public string BpcDesc { get; set; }

        [JsonProperty(PropertyName = "typology_object")]
        public string TypologyObject { get; set; }

    }
}