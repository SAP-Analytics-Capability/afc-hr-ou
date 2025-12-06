using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class ProcessLocal
    {
        //[JsonProperty(PropertyName="process_code")]
        public string ProcessCode { get; set;}

        //[JsonProperty(PropertyName="description")]
        public string Desc { get; set;}
    }
}