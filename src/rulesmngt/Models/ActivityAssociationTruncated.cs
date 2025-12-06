using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class ActivityAssociationTruncated
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

        [JsonProperty(PropertyName = "typology_object")]
        public string TypologyObject { get; set; }

        public ActivityAssociationTruncated Clone(ActivityAssociation obj)
        {
            
            this.AfcMacroorg1 = obj.AfcMacroorg1;
            this.AfcMacroorg1Desc = obj.AfcMacroorg1Desc;
            this.AfcMacroorg2 = obj.AfcMacroorg2;
            this.AfcMacroorg2Desc = obj.AfcMacroorg2Desc;
            this.ObjectTypeId = obj.ObjectTypeId;
            this.OrOrganization = obj.OrOrganization;
            this.OrOrganizationDesc = obj.OrOrganizationDesc;
            this.OrProcess = obj.OrProcess;
            this.OrProcessDesc = obj.OrProcessDesc;
            this.OrVcs = obj.OrVcs;
            this.OrVcsDesc = obj.OrVcsDesc;
            this.PoAttribute = obj.PoAttribute;
            this.PoBpcCode = obj.PoBpcCode;
            this.PoDescription = obj.PoDescription;
            this.PoNpa = obj.PoNpa;
            this.PoObjectAbbr = obj.PoObjectAbbr;
            this.PoObjectId = obj.PoObjectId;
            this.PoPerimeter = obj.PoPerimeter;
            this.RecordId = obj.RecordId;
            this.TypologyObject = obj.TypologyObject;

            return (ActivityAssociationTruncated)(object)this;

        }
    }
}