using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using bwsync.Models;
using bwsync.Models.Configuration;
using bwsync.Interfaces;
using MimeKit;
using MailKit.Security;

namespace bwsync.Helpers.Utils
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
                message.From.Add(new MailboxAddress("noreply@enel.com"));
                retieveMailingList("TO").ForEach(to => message.To.Add(new MailboxAddress(to)));
                retieveMailingList("CC").ForEach(cc => message.Cc.Add(new MailboxAddress(cc)));
                if (message.To == null || message.To.Count < 1)
                {
                    message.To.Add(new MailboxAddress("eneltestmail@gmail.com")); // indirizzo email di recupero
                }
                message.Subject = "Maserdata Background Service Notification"; // rendere dinamico l'oggetto (variabile in input?)

                string body = "Salve,<br/>di seguito sono riportati gli errori riscontrati durante l'esecuzione del servizio: <br/><br/>";
                foreach (string notification in content)
                {
                    body += "-" + notification + "<br/>";
                }

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = body;
                bodyBuilder.TextBody = body;

                message.Body = bodyBuilder.ToMessageBody();
                
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.enelint.global", 2526, false);

                    if (client.IsConnected)
                    {

                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        //client.AuthenticationMechanisms.Remove("NTLM");
                        client.AuthenticationMechanisms.Remove("GSSAPI");

                        var ntlm = new SaslMechanismNtlm(Mail.Value.UserCredential, Mail.Value.PwdCredential);
                        client.Authenticate(ntlm);

                        client.Send(message);
                        Logger.LogInformation("sent mail!");
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
                // Logger.LogError(ex, string.Format("Error while sending the email", DateTime.Now));
                Logger.LogWarning(string.Format("{0} - Error [{1}] while sending the email", DateTime.Now, ex.Message));
                return false;
            }
        }


        public List<string> retieveMailingList(string addressType)
        {

            List<string> results = new List<string>();
            try
            {
                using (var context = new BwContext(DataSource))
                {
                    results = (from p in context.Email where (p.address_type == addressType && DateTime.Compare(p.due_date, DateTime.Now) >= 0) select p.mail).ToList<string>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Error retrieving email address type: " + addressType + "", DateTime.Now));
            }
            return results;
        }

        public bool SendMail(string content)
        {
            throw new NotImplementedException();
        }
    }
}
