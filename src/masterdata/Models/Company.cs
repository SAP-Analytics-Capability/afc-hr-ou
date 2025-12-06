using Newtonsoft.Json;

namespace masterdata.Models
{
    public class Company
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public Company()
        {
            Code = "company_code";
            Name = "company_name";
        }
    }
}