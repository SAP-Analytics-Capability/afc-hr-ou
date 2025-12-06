using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using masterdata.Models;
using masterdata.Interfaces;
using masterdata.Interfaces.Adapters;

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v1/resume")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ResumeMassiveController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private readonly IClientAccessData ClientAccessData;
        private readonly IOptions<Client> ClientData;
        private readonly IHrSyncAdapter retrieveOUAdapter;
        private readonly IOuCCAssociation ouCCAssociation;
        private readonly IResultData ResultData;
        private readonly IClientAuthentication ClientAuthentication;

        public ResumeMassiveController(IConfiguration configuration,
                                        ILoggerFactory loggerFactory,
                                        IClientAccessData clientaccessdata,
                                        IOptions<Client> clientdata,
                                        IHrSyncAdapter retrieveOUAdapter,
                                        IOuCCAssociation ouCCAssociation,
                                        IClientAuthentication clientAuthentication,
                                        IResultData resultData)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<AssociationController>();
            this.ClientAccessData = clientaccessdata;
            this.ClientData = clientdata;

            this.retrieveOUAdapter = retrieveOUAdapter;
            this.ouCCAssociation = ouCCAssociation;
            this.ClientAuthentication = clientAuthentication;
            this.ResultData = resultData;
        }

        [HttpPost]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<ActionResult> Resume([FromBody] string[] ids)
        {
            Logger.LogInformation(string.Format("{0} - A call has been initialized to the resume endpoint.", DateTime.Now.ToString()));
            ClientAccessData.AddNewAccess(ClientData.Value.Username, "RESUME_V2");
            AssociationResultV2 results = new AssociationResultV2();

            try
            {
                Task.Run(() => ouCCAssociation.ComputeAssociationResume(ids, "", DateTime.Now));
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("Unable to run process {0}", ex));
                return StatusCode(500);
            }
            //results = ouCCAssociation.GetAssociationResultOuCodeV2(ids);

            return Ok("Elaboration is in progress...");
        }
    }
}