using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using masterdata.Interfaces.Adapters;
using masterdata.Models;
using masterdata.Models.Configuration;
using masterdata.Helpers;

namespace masterdata.Helpers.Adapters
{
    public class BwSyncAdapter : IBwSyncAdapter
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private IOptions<BwSyncConfiguration> BWConfig;
        private IOptions<Client> ClientData;
        private readonly IHttpClientFactory ClientFactory;

        public BwSyncAdapter(IConfiguration configuration,
                                ILoggerFactory loggerFactory,
                                IOptions<BwSyncConfiguration> bwSyncConfiguration,
                                IOptions<Client> clientdata,
                                IHttpClientFactory clientfactory)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<BwSyncAdapter>();
            this.BWConfig = bwSyncConfiguration;
            this.ClientData = clientdata;
            this.ClientFactory = clientfactory;
        }

        public async Task<List<BwMasterObject>> GetBwMasterDatas(CancellationToken cancellationToken, string ZVarMacroOrg1, string ZVarMacroOrg2, string ZVarVcs, string companyCode, string Process, string Organization)
        {
            BwMasterDataList result = new BwMasterDataList();
            List<BwMasterObject> resultsConverter = null;

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                string baseUrl = BWConfig.Value.ApiUrl + "ZVAR_MACRO_ORG1='param1'&ZVAR_MACRO_ORG2='param2'&ZVAR_VCS='param3'&COMPANY='" + companyCode + "'&ZVAR_PROCESS_CODE='" + Process + "'&ZVAR_ORGANIZ_CODE='" + Organization+"'";
                //string baseUrl = "http://localhost:5008/bwsync/v1/bwsync/q?ZVAR_MACRO_ORG1='param1'&ZVAR_MACRO_ORG2='param2'&ZVAR_VCS='param3'&COMPANY='" + companyCode + "'&ZVAR_PROCESS_CODE='" + Process + "'&ZVAR_ORGANIZ_CODE='" + Organization+"'";
                
                baseUrl = ApiUrlParams.GetURLWithParams(baseUrl, ZVarMacroOrg1, ZVarMacroOrg2, ZVarVcs);

                using (HttpClient client = ClientFactory.CreateClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(5);
                    string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                    }

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<BwMasterDataList>(json);
                            }
                        }
                        else
                        {
                            Logger.LogError(res.StatusCode.ToString());
                        }
                    }
                }
                if (result != null)
                    resultsConverter = getValueConverted(result, companyCode);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get information from base url.", DateTime.Now));
            }
            return resultsConverter;
        }
        private List<BwMasterObject> getValueConverted(BwMasterDataList bwMasterdataList, string companyCode)
        {
            List<BwMasterObject> list = new List<BwMasterObject>();

            if (bwMasterdataList.d != null)
            {
                foreach (ResultBw result in bwMasterdataList.d.results)
                {
                    if (result.YCompCode.Equals(companyCode))
                    {
                        BwMasterObject bwMasterObject = new BwMasterObject();

                        bwMasterObject.costCenterCode = result.A4ZFICP12KOSTL;
                        bwMasterObject.costCenterDescription = result.A4ZFICP12LText_CDC;
                        bwMasterObject.endDateCostCenter = result.A0DateTo;
                        bwMasterObject.startDateCostCenter = result.A0DateFrom;
                        bwMasterObject.typeCostCenter = result.A4ZFICP12ZTIPOCC;
                        bwMasterObject.companyCodeAFC = result.YCompCode;
                        bwMasterObject.companyAFC = string.Empty;
                        bwMasterObject.countryAFC = result.A4ZFICP12LAND1;
                        bwMasterObject.macroOrg1 = result.ZMacro1;
                        bwMasterObject.macroOrg2 = result.ZMacro2;
                        bwMasterObject.vcs = result.YVALChain;
                        bwMasterObject.vcsDescription = string.Empty;
                        bwMasterObject.processGlobal = result.A4ZFICP12ZZPROC;
                        bwMasterObject.processLocal = string.Empty;
                        bwMasterObject.processDescription = string.Empty;
                        bwMasterObject.organization = result.A4ZFICP12ZZCOD_Org;
                        bwMasterObject.organizationDescription = string.Empty;
                        bwMasterObject.responsability = result.ZABTEI;
                        bwMasterObject.resp = result.YPRIMResp;
                        

                        list.Add(bwMasterObject);
                    }
                }
            }

            return list;
        }
    }
}