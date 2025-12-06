using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Globalization;
using masterdata.Models;
using masterdata.Helpers;
using masterdata.Interfaces;
using masterdata.Interfaces.Adapters;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v1/association")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AssociationController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private readonly IClientAccessData ClientAccessData;
        private readonly IOptions<Client> ClientData;
        private readonly IHrSyncAdapter retrieveOUAdapter;
        private readonly IOuCCAssociation ouCCAssociation;
        private readonly IResultData ResultData;
        private readonly IClientAuthentication ClientAuthentication;

        public AssociationController(IConfiguration configuration,
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

        /// <summary>This endpoint returns the result of the association of the Cost Center and Organizational Units.</summary>

        //public ActionResult<IEnumerable<AssociationResult>> GetAssociation([FromQuery(Name = "organizational_code")] int? organizationCode, 
        // [FromQuery(Name = "start_date")] DateTime? startDate, [FromQuery(Name = "end_date")] DateTime? endDate)
        [HttpGet]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<IEnumerable<AssociationResultV1>> GetAssociation()
        {
            Logger.LogInformation(string.Format("{0} - A call has been initialized to the association endpoint.", DateTime.Now.ToString()));

            DateTime lastaccess = ClientAccessData.AddNewAccess(ClientData.Value.Username, "FULL");
            AssociationResultV1 associationresult = new AssociationResultV1();

            try
            {
                associationresult = ouCCAssociation.GetAssociationResultFromService(lastaccess);
                if (associationresult == null)
                {
                    associationresult = new AssociationResultV1();
                    List<AssociationV1> associationList = new List<AssociationV1>();
                    associationresult.AssociationList = associationList;
                }
                else if (associationresult != null && associationresult.AssociationList == null)
                {
                    List<AssociationV1> associationList = new List<AssociationV1>();
                    associationresult.AssociationList = associationList;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "The endpoint throwns an exception. The endpoint is unable to compute association between Organizational Units and Cost Centers");
                return StatusCode(500, new Error(string.Format("Unable to compute association. The endpoint returns with the following exception message: {0}", ex.Message)));
            }

            if (!ClientAccessData.UpdateAccess(ClientData.Value.Username, "FULL"))
            {
                Logger.LogWarning(string.Format("Unable to update the client access for {0}", ClientData.Value.Username));
            }

            if (associationresult == null)
            {
                associationresult = new AssociationResultV1();
            }
            return Ok(associationresult);
        }

        [HttpGet("q")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<IEnumerable<AssociationResult>> GetAssociation([FromQuery(Name = "ou_code")] string oucode)
        {
            Logger.LogInformation(string.Format("{0} - A call has been initialized to the association endpoint.", DateTime.Now));
            DateTime lastaccess = ClientAccessData.AddNewAccess(ClientData.Value.Username, "PARTIAL");
            string[] listOuCode = oucode.Split(',');
            AssociationResult associationResult = new AssociationResult();

            try
            {
                associationResult = ouCCAssociation.GetAssociationResultOuCode(listOuCode, "modify");

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

                    if (oucode.Equals("fake"))
                    {
                        Association afake = new Association();
                        afake.AssociationId = 0;
                        afake.Unit = new HrmasterdataOu("This association has been made for demo purposes.");
                        afake.Centers = new List<BwMasterObject>();

                        associationResult.AssociationList.Add(afake);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "The endpoint throwns an exception. The endpoint is unable to compute association between Organizational Units and Cost Centers");
                return StatusCode(500, new Error(string.Format("Unable to compute association for {0}. The endpoint returns with the following exception message: {1}", oucode, ex.Message)));
            }

            if (!ClientAccessData.UpdateAccess(ClientData.Value.Username, "PARTIAL"))
            {
                Logger.LogWarning(string.Format("Unable to update the client access for {0}", ClientData.Value.Username));
            }

            if (associationResult == null)
            {
                associationResult = new AssociationResult();
            }
            return Ok(associationResult);
        }

    // per paginazione 
    //     [HttpGet("total/q")]
    //     public ActionResult<TotalCount> GetCountByAssociationTime([FromQuery(Name = "date")] string associationTime,
    //                                                                [FromQuery(Name = "type")] string type)
    //     {
    //         TotalCount results = new TotalCount();
    //         DateTime dt;
    //         bool isDateCorrect = DateTime.TryParseExact(associationTime, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
    //         bool isTypeCorrect = type.ToUpper().Equals("OU") || type.ToUpper().Equals("CC");

    //         try
    //         {
    //             if (isDateCorrect && isTypeCorrect)
    //             {
    //                 results = ResultData.GetTotalCountByTypeDate(associationTime, type);
    //                 return Ok(results);
    //             }
    //             else
    //             {
    //                 return BadRequest("Please use date format (yyyy-MM-dd) and as type OU for organization units or CC for cost centers.");
    //             }
    //         }
    //         catch (Exception ex)
    //         {
    //             Logger.LogError(ex, "An error occurred while getting information about total count of UO/CC.");
    //             return StatusCode(500, new Error(string.Format("Unable to get associations count number. The endpoint returns with the following exception message: {0}", ex.Message)));

    //         }
    //     }

    //     [HttpGet("uo/q")]
    //     public ActionResult<AssociationOrganizationUnit> GetUoByOffset([FromQuery(Name = "date")] string associationTime,
    //                                                                [FromQuery(Name = "offset")] string offset,
    //                                                                [FromQuery(Name = "limit")]string limit)
    //     {
    //         UoLimited results = null;
    //         DateTime dt;
    //         bool isDateCorrect = DateTime.TryParseExact(associationTime, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

    //         try
    //         {
    //             if (isDateCorrect)
    //             {
    //                 results = ResultData.GetLimitedUo(associationTime, offset, limit);
    //                 return Ok(results);

    //             }
    //             else
    //             {
    //                     return BadRequest("The input date is wrong, please use format (yyyy-MM-dd)");
    //             }
    //         }
    //         catch (Exception ex)
    //         {
    //             Logger.LogError(ex, "An error occurred while getting information of the association between cost centers and organizational units.");
    //             return StatusCode(500, new Error(string.Format("Unable to get associations count number. The endpoint returns with the following exception message: {0}", ex.Message)));
    //         }
    //     }

    //     [HttpGet("cc/q")]
    //     public ActionResult<AssociationCostCenter> GetCcByOffset([FromQuery(Name = "date")] string associationTime,
    //                                                                [FromQuery(Name = "offset")] string offset,
    //                                                                [FromQuery(Name = "limit")]string limit)
    //     {
    //         CcLimited results = null;
    //         DateTime dt;
    //         bool isDateCorrect = DateTime.TryParseExact(associationTime, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

    //         try
    //         {
    //             if (isDateCorrect)
    //             {
    //                 results = ResultData.GetLimitedCc(associationTime, offset, limit);
    //                 return Ok(results);

    //             }
    //             else
    //             {
    //                     return BadRequest("The input date is wrong, please use format (yyyy-MM-dd)");
    //             }
    //         }
    //         catch (Exception ex)
    //         {
    //             Logger.LogError(ex, "An error occurred while getting information of cost centers.");
    //             return StatusCode(500, new Error(string.Format("Unable to get cost centers. The endpoint returns with the following exception message: {0}", ex.Message)));
    //         }
    //     }
     }
}