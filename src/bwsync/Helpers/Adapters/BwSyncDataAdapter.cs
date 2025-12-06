using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using bwsync.Interfaces;
using bwsync.Models;
using bwsync.Models.Configuration;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

using bwsync.Helpers;
using bwsync.Utils;

namespace bwsync.Helpers.Adapters
{
    public class BwSyncDataAdapter : IBwSyncDataAdapter
    {
        private IConfiguration config;
        private IOptions<BwGlobalConfiguration> bwGlobalConfiguration;
        private ILogger logger;

        public BwSyncDataAdapter(ILoggerFactory loggerFactory, IConfiguration config, IOptions<BwGlobalConfiguration> bwGlobalConfiguration)
        {
            this.config = config;
            this.bwGlobalConfiguration = bwGlobalConfiguration;
            this.logger = loggerFactory.CreateLogger<BwSyncDataAdapter>();
        }

        public async Task<BwMasterDataList> GetSapBWData(CancellationToken cancellationToken, string ZVarMacroOrg1, string ZVarMacroOrg2, string ZVarVcs, string companyCode, string ProcessCode, string OrganizationCode)
        {
            BwMasterDataList bwMasterdataList = null;

            try
            {

                string baseUrl = bwGlobalConfiguration.Value.ApiUrlSap + "ZODATA_OR_Q1_CDC_CDP_AWS_V2_SRV/ZODATA_OR_Q1_CDC_CDP_AWS_V2(ZVAR_MACRO_ORG1=param1,ZVAR_MACRO_ORG2=param2,ZVAR_VCS=param3,ZVAR_COMPANY_FILTER=param4,ZVAR_PROCESS_CODE=param5,ZVAR_ORGANIZ_CODE=param6)/Results/?saml2=disabled&$format=json";
                baseUrl = getUrlWithParams(baseUrl, ZVarMacroOrg1, ZVarMacroOrg2, ZVarVcs, companyCode, ProcessCode, OrganizationCode);
                logger.LogInformation("{0} - baseUrl(ApiUrlSap) verso BW: {1}",DateTime.Now, baseUrl);

                cancellationToken.ThrowIfCancellationRequested();

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.Timeout = TimeSpan.FromMinutes(5);
                        string token = EncoderUtility.Base64Encode(bwGlobalConfiguration.Value.UsernameSap + ":" + bwGlobalConfiguration.Value.PasswordSap);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            logger.LogInformation(string.Format("Call end with the following status: {0}", res.StatusCode.ToString()));

                            if (res.IsSuccessStatusCode)
                            {
                                using (HttpContent content = res.Content)
                                {
                                    string json = await content.ReadAsStringAsync();
                                    bwMasterdataList = JsonConvert.DeserializeObject<BwMasterDataList>(json);
                                }
                            }
                        }
                    }
                    
                }
                // if (bwMasterdataList != null)
                //     bwMasterObjectList = getValueConverted(bwMasterdataList,companyCode);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, string.Format("Unable to get BwSyncData information from base url. Error stack: {0}", ex.ToString()));
            }

            return bwMasterdataList;
        }

        public async Task<BwMasterDataList> GetFixedResultForTest(CancellationToken cancellationToken, string target, string useproxy)
        {
            BwMasterDataList bwMasterdataList = null;
            string baseUrl = string.Empty;
            string userpass = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(target) && (target.ToUpper().StartsWith("T") || target.ToUpper().StartsWith("C")))
                {
                    baseUrl = "https://xwq.enelint.global:44321/sap/opu/odata/sap/ZODATA_OR_Q1_CDC_CDP_AWS_SRV/ZODATA_OR_Q1_CDC_CDP_AWS(ZVAR_MACRO_ORG1='BDIS',ZVAR_MACRO_ORG2='ACTGX01',ZVAR_VCS='',ZVAR_COMPANY_FILTER='IT1B')/Results/?saml2=disabled&$format=json";
                    userpass = "ODATA_AWS:Enel$100";

                    logger.LogInformation(string.Format("Access to test test env with {0}", userpass));
                }
                else
                {
                    baseUrl = "https://xwp.enelint.global:44331/sap/opu/odata/sap/ZODATA_OR_Q1_CDC_CDP_AWS_SRV/ZODATA_OR_Q1_CDC_CDP_AWS(ZVAR_MACRO_ORG1='BDIS',ZVAR_MACRO_ORG2='ACTDX05',ZVAR_VCS='',ZVAR_COMPANY_FILTER='DD01')/Results/?saml2=disabled&$format=json";
                    userpass = "USR_ODATA_HR:Enel$100";

                    logger.LogInformation(string.Format("Access to test prod env with {0}", userpass));
                }
                cancellationToken.ThrowIfCancellationRequested();

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    /*if (useproxy.Equals("Y"))
                    {
                        var proxy = new WebProxy()
                        {
                            Address = new Uri("http://proxy-aws.risorse.enel:8080"),
                            UseDefaultCredentials = true
                        };
                        httpClientHandler.Proxy = proxy;
                    }*/

                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.Timeout = TimeSpan.FromMinutes(5);
                        string token = EncoderUtility.Base64Encode(userpass);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {                    
                            if (res.IsSuccessStatusCode)
                            {
                                using (HttpContent content = res.Content)
                                {
                                    string json = await content.ReadAsStringAsync();
                                    bwMasterdataList = JsonConvert.DeserializeObject<BwMasterDataList>(json);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, string.Format("Unable to get BwSyncData information from base url. Error stack: {0}", ex.ToString()));
            }

            return bwMasterdataList;
        }

        private string getUrlWithParams(string apiUrl, string ZVarMacroOrg1, string ZVarMacroOrg2, string ZVarVcs, string companyCode, string ProcessCode, string OrganizationCode)
        {
            ApiUrlParams apiUrlParams = new ApiUrlParams();
            string urlParams = string.Empty;
            if (!string.IsNullOrEmpty(ZVarMacroOrg1))
                urlParams = apiUrl.Replace(apiUrlParams.ZVarMacroOrg1, ZVarMacroOrg1);
            else
                urlParams = apiUrl.Replace(apiUrlParams.ZVarMacroOrg1, "''");

            if (!string.IsNullOrEmpty(ZVarMacroOrg2))
                urlParams = urlParams.Replace(apiUrlParams.ZVarMacroOrg2, ZVarMacroOrg2);
            else
                urlParams = urlParams.Replace(apiUrlParams.ZVarMacroOrg2, "");

            if (!string.IsNullOrEmpty(ZVarVcs))
                urlParams = urlParams.Replace(apiUrlParams.ZVarVcs, ZVarVcs);
            else
                urlParams = urlParams.Replace(apiUrlParams.ZVarVcs, "''");

            if (!string.IsNullOrEmpty(companyCode))
                urlParams = urlParams.Replace(apiUrlParams.ZCompanyCode, companyCode);
            else
                urlParams = urlParams.Replace(apiUrlParams.ZCompanyCode, "''");
            if (!string.IsNullOrEmpty(ProcessCode))
                urlParams = urlParams.Replace(apiUrlParams.ZProcessCode, ProcessCode);
            else
                urlParams = urlParams.Replace(apiUrlParams.ZProcessCode, "''");
            if (!string.IsNullOrEmpty(OrganizationCode))
                urlParams = urlParams.Replace(apiUrlParams.ZOrganizationCode, OrganizationCode);
            else
                urlParams = urlParams.Replace(apiUrlParams.ZOrganizationCode, "''");
            return urlParams;

        }

        private List<BwMasterObject> getValueConverted(BwMasterDataList bwMasterdataList, string companyCode)
        {
            List<BwMasterObject> list = new List<BwMasterObject>();

            foreach (Result result in bwMasterdataList.d.results)
            {
                if (result.YCompCode.Equals(companyCode))
                {
                    BwMasterObject bwMasterObject = new BwMasterObject();

                    bwMasterObject.costCenterCode = result.A4ZFICP12KOSTL;
                    bwMasterObject.costCenterDescription = result.A4ZFICP12LText_CDC;
                    bwMasterObject.startDateCostCenter = result.A0DateTo;
                    bwMasterObject.endDateCostCenter = result.A0DateFrom;
                    bwMasterObject.typeCostCenter = result.A4ZFICP12ZTIPOCC;
                    bwMasterObject.companyCodeAFC = result.YCompCode;
                    bwMasterObject.companyAFC = string.Empty;
                    bwMasterObject.countryAFC = result.A4ZFICP12LAND1;
                    bwMasterObject.macroOrg1 = result.ZMacro1;
                    bwMasterObject.macroOrg2 = result.ZMacro2;
                    bwMasterObject.vcs = result.YVALChain;
                    bwMasterObject.processGlobal = string.Empty;
                    bwMasterObject.processLocal = result.A4ZFICP12ZZPROC;
                    bwMasterObject.organization = result.A4ZFICP12ZZCOD_Org;
                    bwMasterObject.responsability = result.ZABTEI;

                    list.Add(bwMasterObject);
                }
            }

            return list;
        }
    }
}