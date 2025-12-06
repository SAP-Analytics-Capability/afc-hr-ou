using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class ProcessGlobal
    {
        [JsonProperty(PropertyName="process_code")]
        public string processCode { get; set;}

        [JsonProperty(PropertyName="process_description")]
        public string Desc { get; set;}
    }
}