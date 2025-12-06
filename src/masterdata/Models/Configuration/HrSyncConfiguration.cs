using System;

namespace masterdata.Models.Configuration
{
    public class HrSyncConfiguration
    {
        public string ApiUrl { get; set; }
        public string AuthenticationType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string validityDate { get; set; }
         public string changedateAttribute { get; set; }
         public string companyCode { get; set; }
        public string percCon { get; set; }
         public string costcenterDummy { get; set; }
        public string gestionali { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
         public string noCostcenter { get; set; }

    }
}