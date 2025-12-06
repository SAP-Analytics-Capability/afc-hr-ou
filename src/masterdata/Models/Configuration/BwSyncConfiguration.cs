using System;

namespace masterdata.Models.Configuration
{
    public class BwSyncConfiguration
    {
        public string ApiUrl { get; set; }
        public string AuthenticationType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SamlDis { get; set; }
        public string Limit { get; set; }
        public string offset { get; set; }

    }
}