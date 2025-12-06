using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace rulesmngt.Models.SnowTranscodingResult
{
    public class Result
    {
        public string u_object_abbr { get; set; }
        public string u_afc_process { get; set; }
        public string sys_updated_on { get; set; }
        public string sys_id { get; set; }
        public string sys_updated_by { get; set; }
        public string sys_created_on { get; set; }
        public string u_afc_organization_descr { get; set; }
        public string u_afc_macroorg1_descr { get; set; }
        public string u_attribute { get; set; }
        public string u_bpc_code { get; set; }
        public string sys_created_by { get; set; }
        public string u_object_type { get; set; }
        public string u_perimeter { get; set; }
        public string u_afc_macroorg2 { get; set; }
        public string u_afc_macroorg1 { get; set; }
        public string sys_mod_count { get; set; }
        public string sys_tags { get; set; }
        public string u_object_id { get; set; }
        public string u_afc_organization { get; set; }
        public string u_afc_process_description { get; set; }
        public string u_description { get; set; }
        public string u_afc_vcs_descr { get; set; }
        public string u_not_prevalent_activity { get; set; }
        [JsonProperty(PropertyName = "u_afc_vcs")]
        public string u_afc_vcs { get; set; }
        public string u_afc_macroorg2_descr { get; set; }
        public string u_po_father_code { get; set;}
    }

    public class RootObjectTranscoding
    {
        public List<Result> result { get; set; }
    }

    public class ResultList
    {
        public List<Result> results { get; set; }
    }
}