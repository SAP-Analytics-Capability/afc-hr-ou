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
using hrsync.Interface;
using hrsync.Models;
using hrsync.Models.Configuration;
using System.Text;
using Microsoft.Extensions.Logging;

namespace hrsync.Helpers
{
    public class HrSyncAdapter : IHrSyncAdapter
    {
        private IConfiguration config;
        private IOptions<HrGlobalConfiguration> hrGlobalConfiguration;
        private IOptions<MasterdataSyncConfiguration> masterdataSyncConfiguration;
        private ILogger logger;

        public HrSyncAdapter(ILoggerFactory loggerFactory, IConfiguration config, IOptions<HrGlobalConfiguration> hrGlobalConfiguration
            , IOptions<MasterdataSyncConfiguration> masterdataSyncConfiguration)
        {
            this.config = config;
            this.hrGlobalConfiguration = hrGlobalConfiguration;
            this.masterdataSyncConfiguration = masterdataSyncConfiguration;
            this.logger = loggerFactory.CreateLogger<HrSyncAdapter>();
        }

        public async Task<HrmasterdataOuList> GetSapHRData(CancellationToken cancellationToken, string validityDate, string changedateAttribute, string companyCode, string percCon,
                                                                        string costcenterDummy, string gestionali, string limit, string offset, string noCostcenter)
        {
            HrmasterdataOuList hrMasterdataOuList = null;

            try
            {
                string baseUrl = string.Format("{0}?", string.Format(hrGlobalConfiguration.Value.ApiUrl));
                baseUrl += getUrlWithParams(validityDate, changedateAttribute, companyCode, percCon, costcenterDummy, gestionali, limit, offset, noCostcenter);
                cancellationToken.ThrowIfCancellationRequested();

                using (var httpClientHandler = new HttpClientHandler())
                {
                    //bypass https da eliminare
                    
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        client.Timeout = TimeSpan.FromMinutes(5);
                        string token = EncoderUtility.Base64Encode(hrGlobalConfiguration.Value.Username + ":" + hrGlobalConfiguration.Value.Password);

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            if (res.IsSuccessStatusCode)
                            {
                                using (HttpContent content = res.Content)
                                {
                                    string json = await content.ReadAsStringAsync();
                                    json = json.Replace("hrmasterdata-ou", "hrmasterdataou");
                                    hrMasterdataOuList = JsonConvert.DeserializeObject<HrmasterdataOuList>(json);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to get HrSyncData information from base url.");
            }

            return hrMasterdataOuList;
        }

        public async Task<HrmasterdataOuList> GetOrganizationalUnit(CancellationToken cancellationToken, string oucode)
        {
            HrmasterdataOuList hrMasterdataOuList = null;

            try
            {
                //string baseUrl = string.Format("{0}/{1}", string.Format(hrGlobalConfiguration.Value.ApiUrl, hrGlobalConfiguration.Value.Endpoint, hrGlobalConfiguration.Value.Port, hrGlobalConfiguration.Value.Params, oucode));
                string baseUrl = string.Format("{0}/{1}",hrGlobalConfiguration.Value.ApiUrl, oucode);

                cancellationToken.ThrowIfCancellationRequested();

                using (var httpClientHandler = new HttpClientHandler())
                {
                    //bypass https da eliminare
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        client.Timeout = TimeSpan.FromMinutes(5);
                        string token = EncoderUtility.Base64Encode(hrGlobalConfiguration.Value.Username + ":" + hrGlobalConfiguration.Value.Password);

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            if (res.IsSuccessStatusCode)
                            {
                                using (HttpContent content = res.Content)
                                {
                                    string json = await content.ReadAsStringAsync();
                                    json = json.Replace("hrmasterdata-ou", "hrmasterdataou");
                                    hrMasterdataOuList = JsonConvert.DeserializeObject<HrmasterdataOuList>(json);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to get HrSyncData information from base url.");
            }

            return hrMasterdataOuList;
        }

        private string getUrlWithParams(string validityDate, string changedateAttribute, string companyCode, string percCon,
                                            string costcenterDummy, string gestionali, string limit, string offset, string noCostcenter)
        {
            ApiUrlParams apiUrlParams = new ApiUrlParams();
            string urlParams = apiUrlParams.ValidityDate + validityDate
                                + apiUrlParams.ChangedateAttribute + changedateAttribute
                                + apiUrlParams.CompanyCode + companyCode
                                + apiUrlParams.PercCon + percCon
                                + apiUrlParams.CostcenterDummy + costcenterDummy
                                + apiUrlParams.Gestionali + gestionali
                                + apiUrlParams.Limit + limit
                                + apiUrlParams.Offset + offset
                                + apiUrlParams.NoCostcenter + noCostcenter;
            return urlParams;
        }

        private string getUrlWithParams(string validityDate, string changedateAttribute, string companyCode, string percCon,
                                            string costcenterDummy, string gestionali, string limit, string offset, string noCostcenter, string effettivi)
        {
            ApiUrlParams apiUrlParams = new ApiUrlParams();
            string urlParams = apiUrlParams.ValidityDate + validityDate
                                + apiUrlParams.ChangedateAttribute + changedateAttribute
                                + apiUrlParams.CompanyCode + companyCode
                                + apiUrlParams.PercCon + percCon
                                + apiUrlParams.CostcenterDummy + costcenterDummy
                                + apiUrlParams.Gestionali + gestionali
                                + apiUrlParams.Limit + limit
                                + apiUrlParams.Offset + offset
                                + apiUrlParams.NoCostcenter + noCostcenter 
                                + apiUrlParams.Effettivi + effettivi;
                                //+ "&ou_consistenti=" + effettivi;
            return urlParams;
        }

        public async Task<string> reqMasterdataSyncFullSync(CancellationToken cancellationToken, string sync_post_service)
        {
            try
            {
                string baseUrl = string.Format("{0}{1}", masterdataSyncConfiguration.Value.ApiUrl, sync_post_service);
                logger.LogInformation("{0} - url req: {1} ", DateTime.Now, baseUrl);

                cancellationToken.ThrowIfCancellationRequested();

                using (var httpClientHandler = new HttpClientHandler())
                {
                    //bypass https da eliminare
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        client.Timeout = TimeSpan.FromMinutes(5);
                        string token = EncoderUtility.Base64Encode(masterdataSyncConfiguration.Value.Username + ":" + masterdataSyncConfiguration.Value.Password);

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            return res.IsSuccessStatusCode.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to launch masterdata full sync from base url.");
            }

            return "false";

        }
    }
}
