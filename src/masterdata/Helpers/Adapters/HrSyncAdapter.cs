using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using masterdata.Interfaces.Adapters;
using masterdata.Models;
using System.Text;
using Microsoft.Extensions.Options;
using masterdata.Models.Configuration;
using System.Threading;

namespace masterdata.Helpers.Adapters
{
    public class HrSyncAdapter : IHrSyncAdapter
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private IOptions<HrSyncConfiguration> HRConfiguration;
        private readonly IOptions<Client> ClientData;
        private readonly IHttpClientFactory ClientFactory;

        public HrSyncAdapter(IConfiguration configuration,
                                    ILoggerFactory loggerFactory,
                                    IOptions<HrSyncConfiguration> hrConfiguration,
                                    IOptions<Client> clientdata,
                                    IHttpClientFactory clientfactory)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<HrSyncAdapter>();
            this.HRConfiguration = hrConfiguration;
            this.ClientData = clientdata;
            this.ClientFactory = clientfactory;
        }

        public async Task<HrmasterdataOuList> GetOrganizationalUnitsLimit(CancellationToken cancellationToken,
                                                                            int limit)
        {
            HrmasterdataOuList results = null;
            string baseUrl = string.Empty;

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                baseUrl = string.Format(HRConfiguration.Value.ApiUrl + "q?limit=" + limit);
                string token = EncoderUtility.Base64Encode(string.Format("{0}:{1}", ClientData.Value.Username, ClientData.Value.Password));

                using (HttpClient client = ClientFactory.CreateClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                    }

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                results = JsonConvert.DeserializeObject<HrmasterdataOuList>(json);
                            }
                        }
                        else
                        {
                            Logger.LogError(res.StatusCode.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get registry information from base url.");
                throw (ex);
            }
            return results;
        }

        public async Task<HrmasterdataOuList> GetOrganizationalUnits(CancellationToken cancellationToken,
                                                                        string validityDate,
                                                                        string changedateAttribute,
                                                                        string companyCode,
                                                                        string percCon,
                                                                        string costcenterDummy,
                                                                        string gestionali,
                                                                        string limit,
                                                                        string offset,
                                                                        string noCostcenter,
                                                                        string changedDate)
        {
            HrmasterdataOuList results = null;
            string baseUrl = string.Empty;

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                baseUrl = string.Format("{0}q?{1}", HRConfiguration.Value.ApiUrl, ApiUrlParams.GetURLWithParams(validityDate, changedateAttribute, companyCode, percCon, costcenterDummy, gestionali, limit, offset, noCostcenter, changedDate));

                //baseUrl = string.Format(baseUrl, this.Configuration["HRSYNC_SERVICE_HOST"],  this.Configuration["HRSYNC_SERVICE_PORT"]);
                string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

                using (HttpClient client = ClientFactory.CreateClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(5);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                    }

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                results = JsonConvert.DeserializeObject<HrmasterdataOuList>(json);
                            }
                        }
                        else
                        {
                            Logger.LogError(res.StatusCode.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get information from base url.");
                throw (ex);
            }
            return results;
        }

        public async Task<HrmasterdataOuList> GetOrganizationalUnit(CancellationToken cancellationToken,
                                                                    string oucode)
        {
            HrmasterdataOuList results = null;
            string baseUrl = string.Empty;

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                baseUrl = string.Format("{0}getbycode/q?ou_code={1}", HRConfiguration.Value.ApiUrl, oucode);
                //baseUrl = string.Format("{0}getbycode/q?ou_code={1}", "http://localhost:5006/hrsync/v1/hrsync/", oucode);
                //baseUrl = string.Format(baseUrl, this.Configuration["HRSYNC_SERVICE_HOST"],  this.Configuration["HRSYNC_SERVICE_PORT"]);

                string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

                using (HttpClient client = ClientFactory.CreateClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(5);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                    }

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                results = JsonConvert.DeserializeObject<HrmasterdataOuList>(json);
                            }
                        }
                        else
                        {
                            Logger.LogError(res.StatusCode.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get information from base url.");
                throw (ex);
            }
            return results;
        }
    }
}