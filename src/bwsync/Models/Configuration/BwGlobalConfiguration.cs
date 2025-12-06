namespace bwsync.Models.Configuration
{
    public class BwGlobalConfiguration
    {
        public string ApiUrlSap { get; set; }
        public string AuthenticationTypeSap { get; set; }
        public string UsernameSap { get; set; }
        public string PasswordSap { get; set; }
        public string ApiUrlMaster { get; set; }
        public string AuthenticationTypeMaster { get; set; }
        public string UsernameMaster { get; set; }
        public string PasswordMaster { get; set; }
    }
}