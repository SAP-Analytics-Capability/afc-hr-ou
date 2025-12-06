using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using masterdata.Models;
using masterdata.Helpers;
using masterdata.Interfaces;
using masterdata.Interfaces.Adapters;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v0/association")]
    [ApiExplorerSettings(GroupName = "v0")]
    public class AssociationControllerV0 : ControllerBase
    {
        private readonly ILogger Logger;
        private readonly IClientAccessData ClientAccessData;
        private readonly IOptions<Client> ClientData;
        private readonly IClientAuthentication ClientAuthentication;
        private readonly IOuCCAssociation OUCCAssociation;

        public AssociationControllerV0(ILoggerFactory loggerFactory,
                                        IClientAccessData clientaccessdata,
                                        IOptions<Client> clientdata,
                                        IClientAuthentication clientAuthentication,
                                        IOuCCAssociation ouccassociation)
        {
            this.Logger = loggerFactory.CreateLogger<AssociationControllerV0>();
            this.ClientAccessData = clientaccessdata;
            this.ClientData = clientdata;
            this.ClientAuthentication = clientAuthentication;
            this.OUCCAssociation = ouccassociation;
        }

        [HttpGet]
        //[ApiExplorerSettings(GroupName = "v2")]
        public ActionResult<IEnumerable<AssociationResult>> GetAssociation()
        {
            Logger.LogInformation(string.Format("{0} - A call has been initialized to the association endpoint.", DateTime.Now.ToString()));
            DateTime lastaccess = ClientAccessData.AddNewAccess(ClientData.Value.Username, "FULL");

            try
            {
                AssociationResult associationResult = OUCCAssociation.GetAssociationResult("insert", lastaccess);
                if (associationResult != null)
                {
                    return Ok(associationResult);
                }
                else
                {
                    if (associationResult == null)
                    {
                        associationResult = new AssociationResult();
                    }

                    if (associationResult.AssociationList == null)
                    {
                        associationResult.AssociationList = new List<Association>();
                    }

                    return Ok(associationResult);
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "The endpoint throwns an exception. The endpoint is unable to compute association between Organizational Units and Cost Centers");
                return StatusCode(500, new Error(string.Format("Unable to compute association. The endpoint returns with the following exception message: {0}", ex.Message)));
            }
        }

        [HttpGet("perf")]
        //[ApiExplorerSettings(GroupName = "v2")]
        public ActionResult<IEnumerable<AssociationResultV1>> GetAssociationPerf()
        {
            Logger.LogInformation(string.Format("{0} - A call has been initialized to the association endpoint.", DateTime.Now.ToString()));
            DateTime lastaccess = ClientAccessData.AddNewAccess(ClientData.Value.Username, "FULL");

            try
            {
                AssociationResultV1 ar = new AssociationResultV1();
                List<AssociationV1> aslist = new List<AssociationV1>();

                for (int i = 0; i < 2000; i++)
                {
                    AssociationV1 av2 = new AssociationV1();
                    av2.Unit = AssociationOrganizationUnit.CreateNew(i);
                    av2.Centers = new List<AssociationCostCenter>();
                    for (int j = 0; j < 10; j++)
                    {
                        av2.Centers.Add(AssociationCostCenter.CreateNew(j, i));
                    }
                    aslist.Add(av2);
                    
                }
                ar.AssociationList = aslist;
                return Ok(ar);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "The endpoint throwns an exception. The endpoint is unable to compute association between Organizational Units and Cost Centers");
                return StatusCode(500, new Error(string.Format("Unable to compute association. The endpoint returns with the following exception message: {0}", ex.Message)));
            }
        }
    }
}