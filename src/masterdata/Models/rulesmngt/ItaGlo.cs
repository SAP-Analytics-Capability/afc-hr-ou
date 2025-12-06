using System;
using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class ItaGlo
    {
        [JsonProperty(PropertyName="ita_glo_id")]
        public Int32 ItaGloId { get; set;}

        [JsonProperty(PropertyName="sap_hr_global_code")]
        public string SapHrGlobalCode { get; set;}

        [JsonProperty(PropertyName="is_enel_italia")]
        public string IsEnelItalia { get; set;}

    }
}