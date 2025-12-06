using Newtonsoft.Json;

namespace masterdata.Models.rulesmngt
{
    public class BcpCode
    {
        [JsonProperty(PropertyName="bpc_code")]
        public string BpcCode { get; set;}

        [JsonProperty(PropertyName="description")]
        public string Desc { get; set;}
    }
}