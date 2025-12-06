using System;

namespace rulesmngt.Models.Configuration
{
    public class SnowCatalogConfiguration
    {
        public string ApiUrl { get; set; }
        public string AuthenticationType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Limit { get; set; }
        public string offset { get; set; }
        public string Catalog { get; set; }
        public string Transcoding { get; set; }
    }
}