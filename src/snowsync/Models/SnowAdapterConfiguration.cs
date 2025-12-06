namespace snowsync.Models
{
    public class SnowAdapterConfiguration
    {
        public string ApiUrl { get; set;}
        public string AuthenticationType { get; set;}
        public string Username { get; set;}
        public string Password { get; set;}
        public string Limit { get; set;}
        public string offset { get; set;}
        public string SysparmQuery { get; set;}
        public string SysparmDisplayValue { get; set;}
        public string SysparmExcludeReferenceLink { get; set;}
        public string SysparmFields { get; set;}
    }
}