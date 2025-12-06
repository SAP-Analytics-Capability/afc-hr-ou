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

using masterdata.Models;
using masterdata.Helpers;
using masterdata.Interfaces;
using masterdata.Interfaces.Adapters;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v2/association")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class AssociationControllerV2 : ControllerBase
    {
        private readonly ILogger Logger;
        private readonly IClientAccessData ClientAccessData;
        private readonly IOptions<Client> ClientData;
        private readonly IClientAuthentication ClientAuthentication;
        private readonly IOuCCAssociation OUCCAssociation;

        private readonly IDistributedCache _Cache;

        public AssociationControllerV2(ILoggerFactory loggerFactory,
                                        IClientAccessData clientaccessdata,
                                        IOptions<Client> clientdata,
                                        IClientAuthentication clientAuthentication,
                                        IOuCCAssociation ouccassociation,
                                        IDistributedCache cache)
        {
            this.Logger = loggerFactory.CreateLogger<AssociationControllerV2>();
            this.ClientAccessData = clientaccessdata;
            this.ClientData = clientdata;
            this.ClientAuthentication = clientAuthentication;
            this.OUCCAssociation = ouccassociation;
            this._Cache = cache;
        }

        [HttpGet("total/q")]
        public ActionResult<int> GetTotalCountCCforOU([FromQuery(Name = "ou_code")] string uOrg)
        {
            ClientAccessData.AddNewAccess(ClientData.Value.Username, "PARTIAL_V2");
            AssociationResultV2 results = new AssociationResultV2();
            TotalCount countCC = new TotalCount();
            string[] listOuCode = uOrg.Split(',');

            try
            {
                string key = uOrg;
                Task<string> task = _Cache.GetStringAsync(uOrg);
                if (string.IsNullOrEmpty(task.Result))
                {
                    results = OUCCAssociation.GetAssociationResultOuCodeV2(listOuCode);

                    if (results.AssociationList != null && results.AssociationList.Count > 0)
                    {
                        _Cache.SetString(uOrg, JsonConvert.SerializeObject(results), new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddSeconds(300) });
                        countCC.totalCount = Convert.ToInt64(results.AssociationList[0].Centers.Count);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    results = JsonConvert.DeserializeObject<AssociationResultV2>(task.Result);
                    countCC.totalCount = Convert.ToInt64(results.AssociationList[0].Centers.Count);
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while getting information about total count of UO/CC.");
                return StatusCode(500, new Error(string.Format("Unable to get associations count number. The endpoint returns with the following exception message: {0}", ex.Message)));

            }
            ClientAccessData.UpdateAccess(ClientData.Value.Username, "PARTIAL_V2");
            return Ok(countCC);
        }

        [HttpGet("cc/q")]
        public ActionResult<CcLimited> GetPaginatedCCforOU([FromQuery(Name = "ou_code")] string uOrg,
        [FromQuery(Name = "pagenumber")] string pagenumber, [FromQuery(Name = "pagesize")] string pagesize)
        {
            ClientAccessData.AddNewAccess(ClientData.Value.Username, "PARTIAL_V2");

            Int32.TryParse(pagenumber, out int pgnumber);
            pgnumber = (pgnumber > 0) ? (pgnumber - 1) : 0;
            // if (pgnumber > 0)
            // {
            //     pgnumber--;
            // }
            // else
            // {
            //     pgnumber = 0;
            // }

            Int32.TryParse(pagesize, out int pgsize);

            AssociationResultV2 results = new AssociationResultV2();
            CcLimited costCenters = new CcLimited();
            costCenters.cost_centers = new List<BwCC>();
            string[] listOuCode = uOrg.Split(',');
            try
            {
                string key = uOrg;
                string i_debug = "X";

                Task<string> task = _Cache.GetStringAsync(uOrg);
              //  if (string.IsNullOrEmpty(task.Result))  
              if (i_debug == "X")
                 {
                    results = OUCCAssociation.GetAssociationResultOuCodeV2(listOuCode);
                    if (results.AssociationList != null && results.AssociationList.Count > 0)
                    {
                        _Cache.SetStringAsync(uOrg, JsonConvert.SerializeObject(results), new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddSeconds(300) });
                        costCenters.cost_centers.AddRange(results.AssociationList[0].Centers.Skip(pgnumber * pgsize).Take(pgsize > 0 ? pgsize : int.MaxValue));
                    }
                    else
                    {
                        return NoContent();
                    }

                }
                else
                {
                    results = JsonConvert.DeserializeObject<AssociationResultV2>(task.Result);
                    // if (results.AssociationList != null && results.AssociationList[0].Centers != null)
                    // {
                    costCenters.cost_centers.AddRange(results.AssociationList[0].Centers.Skip(pgnumber * pgsize).Take(pgsize > 0 ? pgsize : int.MaxValue));
                    //}
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while getting information about CC for UO.");
                return StatusCode(500, new Error(string.Format("Unable to get CC for UO. The endpoint returns with the following exception message: {0}", ex.Message)));

            }
            ClientAccessData.UpdateAccess(ClientData.Value.Username, "PARTIAL_V2");
            return Ok(costCenters);
        }

        [HttpGet("ou/q")]
        public ActionResult<HrOu> GetHrOU([FromQuery(Name = "ou_code")] string uOrg)
        {
            AssociationResultV2 results = new AssociationResultV2();
            results.AssociationList = new List<AssociationV2>();
            HrOu unit = new HrOu();
            string[] listOuCode = uOrg.Split(',');

            try
            {
                ClientAccessData.AddNewAccess(ClientData.Value.Username, "PARTIAL_V2");
                string key = uOrg;

                Task<string> task = _Cache.GetStringAsync(key);
                if (string.IsNullOrEmpty(task.Result))
                {
                    results = OUCCAssociation.GetAssociationResultOuCodeV2(listOuCode);
                    if (results.AssociationList != null && results.AssociationList.Count > 0)
                    {
                        _Cache.SetStringAsync(uOrg, JsonConvert.SerializeObject(results), new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddSeconds(300) });
                        unit = results.AssociationList[0].Unit;
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    results = JsonConvert.DeserializeObject<AssociationResultV2>(task.Result);
                    unit = results.AssociationList[0].Unit;
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while getting information about total count of UO/CC.");
                return StatusCode(500, new Error(string.Format("Unable to get associations count number. The endpoint returns with the following exception message: {0}", ex.Message)));

            }
            ClientAccessData.UpdateAccess(ClientData.Value.Username, "PARTIAL_V2");
            return Ok(unit);
        }
    }
}