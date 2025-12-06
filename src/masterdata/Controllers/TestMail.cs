using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using masterdata.Models;
using masterdata.Interfaces.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using masterdata.Helpers;
using masterdata.Interfaces;
using masterdata.Models.SnowCostCenterResults;
using masterdata.Data;
using System.Threading;
using System.Net;
using System.Reflection;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net.Mail;

namespace masterdata.Controllers
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("masterdata/v1/testmail")]
    public class TestMail : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private readonly IResultData ResultData;
        private readonly IClientAccessData ClientAccessData;

        public TestMail(IConfiguration configuration,
                                        ILoggerFactory loggerFactory,
                                        IClientAuthentication clientAuthentication,
                                        IResultData resultData,
                                        IClientAccessData clientaccessdata)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<SnowAdatpterController>();
            this.ResultData = resultData;
            this.ClientAccessData = clientaccessdata;
        }

        /// <summary>This endpoint triggers the call to Service Now endpoint and it gets the user confirmation about the association proposed by the computation.</summary>
        [HttpGet]
        public ActionResult<List<Result>> SendMail()
        {
            List<Result> snowresult = new List<Result>();
            MailKit.Net.Smtp.SmtpClient smtpClient = new MailKit.Net.Smtp.SmtpClient();
            string msg = string.Empty;
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("noreply@enel.com"));
                message.To.Add(new MailboxAddress("marco.cusimano@everis.com"));
                message.To.Add(new MailboxAddress("gionata.cerasuolo@everis.com"));
                message.Subject = "prova invio mail masterdata";

                message.Body = new TextPart("plain")
                {
                    Text = "Prova invio mail da container docker. <br/><br/> Grazie."
                };
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "Prova invio mail da container docker. <br/><br/> Grazie.";
                bodyBuilder.TextBody = "Prova invio mail da container docker. <br/><br/> Grazie.";

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.enelint.global", 2526, false);

                    if (client.IsConnected)
                    {
                        foreach (string mechanism in client.AuthenticationMechanisms)
                        {
                            Logger.LogInformation(mechanism);
                        }

                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        //client.AuthenticationMechanisms.Remove("NTLM");
                        client.AuthenticationMechanisms.Remove("GSSAPI");

                        var ntlm = new SaslMechanismNtlm("enelint\\zoucc-man", "Z$17eng5tf");
                        client.Authenticate(ntlm);

                        try
                        {
                            client.Send(message);
                            Logger.LogInformation("sent mail!");
                            client.Disconnect(true);
                            return Ok(snowresult);
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError(ex, "An error occurred send ko");
                            return StatusCode(500, "The email was not sent. ");
                        }
                    }
                    else
                    {
                        return StatusCode(500, "unabled to connect. ");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred send ko");
                return BadRequest("Error send mail");
            }
        }

    }
}
