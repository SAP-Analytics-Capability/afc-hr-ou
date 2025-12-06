namespace rulesmngt.Models.Configuration
{
    public class EmailOptions
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderEmail { get; set; }
        public string MailFromEnv { get; set; }
        public string UserCredential {get; set; }
        public string PwdCredential {get; set; }
        
    }
}