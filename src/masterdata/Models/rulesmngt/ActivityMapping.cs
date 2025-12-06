using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class ActivityMapping
    {   
        [JsonProperty(PropertyName="activity_mapping_id")]
        public Int32 ActivityMappingId { get; set;}

        [JsonProperty(PropertyName="object_type_id")]
        public string ObjectType { get; set;}

        [JsonProperty(PropertyName="attribute")]
        public string Attribute { get; set;}

        [JsonProperty(PropertyName="bpc_code")]
        public Bpc BpcCode { get; set;}

        [JsonProperty(PropertyName="not_prevalent")]
        public string NotPrevalent { get; set;}

        [JsonProperty(PropertyName="peo_perimeter")]
        public string PeOPerimeter { get; set;}

        [JsonProperty(PropertyName="macroorg1")]
        public MacroOrg1 MacroOrg1 { get; set;}

        [JsonProperty(PropertyName="macroorg2")]
        public ActivityList MacroOrg2 { get; set;}

        [JsonProperty(PropertyName="process_global")]
        public ProcessGlobal ProcessGlobal { get; set;}

        [JsonProperty(PropertyName="organization")]
        public Organization Organization { get; set;}

        //[JsonProperty(PropertyName="process_local")]
        public string ProcessLocal { get; set;}
        [JsonProperty(PropertyName="process_description")]
        public string ProcessDescription { get; set;}

        [JsonProperty(PropertyName="vcs")]
        public Vcs Vcs { get; set;}
        
        [JsonProperty(PropertyName = "typology_object")]
        public string TypologyObject { get; set; }
    }
}