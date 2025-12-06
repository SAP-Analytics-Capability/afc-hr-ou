using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

using hrsync.Models;
using hrsync.Models.Configuration;
using hrsync.Interface;

using Microsoft.Extensions.Logging;

using System.Net.Mail;
using MimeKit;
using MailKit.Security;

namespace hrsync.Helpers
{
    public class MailSender : IMailSender
    {
        private IOptions<EmailOptions> Mail;
        private readonly ILogger Logger;
        private readonly IOptions<DatabaseConfiguration> DataSource;

        public MailSender(IOptions<DatabaseConfiguration> datasource, IOptions<EmailOptions> mail, ILoggerFactory loggerFactory)
        {
            this.DataSource = datasource;
            this.Mail = mail;
            this.Logger = loggerFactory.CreateLogger<MailSender>();
        }
        public bool SendMail(List<string> content)
        {
            //MailMessage message = new MailMessage();
            var message = new MimeMessage();
            try
            {
                Logger.LogInformation(string.Format("{0} - Hrsync - SenderEmail: {1} ", DateTime.Now.ToString(), Mail.Value.SenderEmail));
                message.From.Add(new MailboxAddress(Mail.Value.SenderEmail));
                retieveMailingList("TO").ForEach(to => message.To.Add(new MailboxAddress(to)));
                retieveMailingList("CC").ForEach(cc => message.Cc.Add(new MailboxAddress(cc)));
                if ((message.To == null || message.To.Count < 1) && (message.Cc == null || message.Cc.Count < 1))
                {
                    Logger.LogWarning(string.Format("{0} - Warning [{1}] while sending the email", DateTime.Now, "Mail Sender: nessuna mail configurata"));
                    return true;
                }

                Logger.LogInformation(string.Format("{0} - Hrsync - MailFromEnv: {1} ", DateTime.Now.ToString(), Mail.Value.MailFromEnv));
                string body = "<br>" + "Notifica da AMBIENTE di [" + Mail.Value.MailFromEnv + "]: <br><br>";
                int i = 0;

                foreach (string notification in content)
                {
                    if (i == 0) { message.Subject = notification; };
                    if (i >= 1) { body += notification + "<br>"; };
                    i++;
                }

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = body;
                bodyBuilder.TextBody = body;

                message.Body = bodyBuilder.ToMessageBody();
                
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    Logger.LogInformation(string.Format("{0} - Hrsync - SmtpServer: {1} ", DateTime.Now.ToString(), Mail.Value.SmtpServer));
                    Logger.LogInformation(string.Format("{0} - Hrsync - SmtpPort: {1} ", DateTime.Now.ToString(), Mail.Value.SmtpPort));
                    client.Connect(Mail.Value.SmtpServer, Mail.Value.SmtpPort, SecureSocketOptions.None);

                    if (client.IsConnected)
                    {
                        Logger.LogInformation(string.Format("{0} - Hrsync - client Mail Connected", DateTime.Now.ToString()));
                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        //client.AuthenticationMechanisms.Remove("NTLM");
                        client.AuthenticationMechanisms.Remove("GSSAPI");

                        Logger.LogInformation(string.Format("{0} - Hrsync - UserCredential: {1} ", DateTime.Now.ToString(), Mail.Value.UserCredential));

                        var ntlm = new SaslMechanismNtlm(Mail.Value.UserCredential, Mail.Value.PwdCredential);
                        client.Authenticate(ntlm);
                        Logger.LogInformation(string.Format("{0} - Hrsync - client Mail Authenticated", DateTime.Now.ToString()));

                        client.Send(message);
                        Logger.LogInformation(string.Format("{0} - Hrsync - MailSender: Mail Sent!", DateTime.Now).ToString());
                        client.Disconnect(true);
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning(string.Format("{0} - Masterdata - MailSender: Error [{1}] while Sending the Mail", DateTime.Now, ex.Message));
                return false;
            }
        }

        public List<string> retieveMailingList(string addressType)
        {

            List<string> results = new List<string>();
            try
            {
                using (var context = new HrSyncContext(DataSource))
                {
                    results = (from p in context.Email where (p.address_type == addressType && DateTime.Compare(p.due_date, DateTime.Now) >= 0) select p.mail).ToList<string>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Error retrieving email address type: " + addressType + "", DateTime.Now));
            }
            return results;
        }

        public bool SendMail(string content)
        {
            throw new NotImplementedException();
        }
    }
}
