using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Globalization;

using masterdata.Models;
using masterdata.Helpers;
using masterdata.Interfaces;
using masterdata.Interfaces.Adapters;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v2/associations")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class AssociationsControllerV2 : ControllerBase
    {
        private readonly ILogger Logger;
        private readonly IClientAccessData ClientAccessData;
        private readonly IOptions<Client> ClientData;
        private readonly IClientAuthentication ClientAuthentication;
        private readonly IOuCCAssociation OUCCAssociation;
        private readonly IAssociationData AssociationData;

        public AssociationsControllerV2(ILoggerFactory loggerFactory,
                                        IClientAccessData clientaccessdata,
                                        IOptions<Client> clientdata,
                                        IClientAuthentication clientAuthentication,
                                        IOuCCAssociation ouccassociation,
                                        IAssociationData associationData)
        {
            this.Logger = loggerFactory.CreateLogger<AssociationControllerV2>();
            this.ClientAccessData = clientaccessdata;
            this.ClientData = clientdata;
            this.ClientAuthentication = clientAuthentication;
            this.OUCCAssociation = ouccassociation;
            this.AssociationData = associationData;
        }

            [HttpGet("total/q")]
            public ActionResult<TotalCount> GetCountByAssociationTime([FromQuery(Name = "date")] string associationTime,
                                                                       [FromQuery(Name = "type")] string type)
            {
                ClientAccessData.AddNewAccess(ClientData.Value.Username, "FULL_V2");
                TotalCount resultsCount = new TotalCount();
                TotalCountOffsetType resCountOffsetType = new TotalCountOffsetType();
                DateTime dt;
                bool isDateCorrect = DateTime.TryParseExact(associationTime, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                bool isTimestampCorrect = DateTime.TryParseExact(associationTime, "yyyy-MM-dd HH:mm:ss" ,CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                bool isTypeCorrect = type.ToUpper().Equals("OU") || type.ToUpper().Equals("CC");
                string typeres = "";
                string esito = "";
                string esito_full = "";
                DateTime maxDtFull;
                long maxAccessIdFull;

            try
                {
                    if ((isDateCorrect||isTimestampCorrect) && isTypeCorrect)
                    {
                    List<ClientAccess> oldcaResume = new List<ClientAccess>();
                    oldcaResume = ClientAccessData.RetrieveAccess(null, "RESUME_V2");
                    List<ClientAccess> oldcaFull = new List<ClientAccess>();
                    oldcaFull = ClientAccessData.RetrieveAccess("MASTER_SCHEDULER", "FULL_V2");
                    List<ClientAccess> oldcaFullSync = new List<ClientAccess>();
                    oldcaFullSync = ClientAccessData.RetrieveAccess("MASTER_SCHEDULER", "FULL_V2_SYNC");

                    if (oldcaFull.Count() > 0 && oldcaFullSync.Count() > 0)
                    {
                        if (oldcaFullSync[0].AccessTimestamp > oldcaFull[0].AccessTimestamp)
                        {
                            maxDtFull = (DateTime)oldcaFullSync[0].AccessTimestamp;
                            esito_full = oldcaFullSync[0].Status;
                            maxAccessIdFull = oldcaFullSync[0].AccessId;
                        }
                        else
                        {
                            maxDtFull = (DateTime)oldcaFull[0].AccessTimestamp;
                            esito_full = oldcaFull[0].Status;
                            maxAccessIdFull = oldcaFull[0].AccessId;
                        }
                    }
                    else if (oldcaFull.Count() > 0) { maxDtFull = (DateTime)oldcaFull[0].AccessTimestamp; esito_full = oldcaFull[0].Status; maxAccessIdFull = oldcaFull[0].AccessId; }
                    else { maxDtFull = (DateTime)oldcaFullSync[0].AccessTimestamp; esito_full = oldcaFullSync[0].Status; maxAccessIdFull = oldcaFullSync[0].AccessId; }

                    if (oldcaResume[0].AccessTimestamp >= maxDtFull && oldcaResume[0].AccessId > maxAccessIdFull)
                    {
                        typeres = "RESUME";
                        if (oldcaResume[0].Status == "E") { esito = "Success"; } else { esito = "Error"; };
                    }
                    else
                    {
                        typeres = "FULL";
                        if (esito_full == "E") { esito = "Success"; } else { esito = "Error"; };
                    }
                    resultsCount = AssociationData.GetTotalCountByTypeDate(associationTime, type);
                    resCountOffsetType = AssociationData.GetOffsetByTypeDate(associationTime, type);
                    resCountOffsetType.totalCount = resultsCount.totalCount;
                    resCountOffsetType.elabType = typeres;
                    resCountOffsetType.esito = esito;
                    ClientAccessData.UpdateAccess(ClientData.Value.Username, "FULL_V2");
                    return Ok(resCountOffsetType);
                    }
                    else
                    {
                        return BadRequest("Please use date format (yyyy-MM-dd) and as type OU for organization units or CC for cost centers.");
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "An error occurred while getting information about total count of UO/CC.");
                    return StatusCode(500, new Error(string.Format("Unable to get associations count number. The endpoint returns with the following exception message: {0}", ex.Message)));

                }
            }

            [HttpGet("ou/q")]
            public ActionResult<UoLimited> GetUoByOffset([FromQuery(Name = "date")] string associationTime,
                                                                       // INDRA: Superamento offset
                                                                       //[FromQuery(Name = "page")] string page,
                                                                       [FromQuery(Name = "offset")] string offset,
                                                                       [FromQuery(Name = "limit")]string limit)
            {
                ClientAccessData.AddNewAccess(ClientData.Value.Username, "FULL_V2");
                UoLimited results = null;
                DateTime dt;
                bool isDateCorrect = DateTime.TryParseExact(associationTime, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                bool isTimestampCorrect = DateTime.TryParseExact(associationTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

                try
                {
                    if (isDateCorrect||isTimestampCorrect)
                    {
                    // INDRA: Superamento offset
                    //results = AssociationData.GetPagesUo(associationTime, page, limit);
                    results = AssociationData.GetLimitedUo(associationTime, offset, limit);
                    ClientAccessData.UpdateAccess(ClientData.Value.Username, "FULL_V2");
                        return Ok(results);
                    }
                    else
                    {
                        return BadRequest("The input date is wrong, please use format (yyyy-MM-dd)");
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "An error occurred while getting information of the association between cost centers and organizational units.");
                    return StatusCode(500, new Error(string.Format("Unable to get associations count number. The endpoint returns with the following exception message: {0}", ex.Message)));
                }
            }

            [HttpGet("cc/q")]
            public ActionResult<CcLimited> GetCcByOffset([FromQuery(Name = "date")] string associationTime,
                                                                       [FromQuery(Name = "offset")] string offset,
                                                                       [FromQuery(Name = "limit")]string limit)
            {
                ClientAccessData.AddNewAccess(ClientData.Value.Username, "FULL_V2");
                CcLimited results = null;
                DateTime dt;
                bool isDateCorrect = DateTime.TryParseExact(associationTime, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                bool isTimestampCorrect = DateTime.TryParseExact(associationTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

                try
                {
                    if (isDateCorrect||isTimestampCorrect)
                    {
                        results = AssociationData.GetLimitedCc(associationTime, offset, limit);
                        ClientAccessData.UpdateAccess(ClientData.Value.Username, "FULL_V2");
                        return Ok(results);

                    }
                    else
                    {
                        return BadRequest("The input date is wrong, please use format (yyyy-MM-dd)");
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "An error occurred while getting information of cost centers.");
                    return StatusCode(500, new Error(string.Format("Unable to get cost centers. The endpoint returns with the following exception message: {0}", ex.Message)));
                }
            }
    }
}