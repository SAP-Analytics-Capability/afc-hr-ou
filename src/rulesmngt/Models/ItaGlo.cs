using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class ItaGlo
    {
        [Key]
        [JsonProperty(PropertyName="ita_glo_id")]
        public Int32 ItaGloId { get; set;}

        [JsonProperty(PropertyName="sap_hr_global_code")]
        public string SapHrGlobalCode { get; set;}

        [JsonProperty(PropertyName="is_enel_italia")]
        public string IsEnelItalia { get; set;}

    }
}