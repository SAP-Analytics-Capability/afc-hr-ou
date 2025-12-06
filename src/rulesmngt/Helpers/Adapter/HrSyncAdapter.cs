using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rulesmngt.Interfaces.Adapters;
using rulesmngt.Models;
using System.Text;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;
using System.Threading;
using rulesmngt.Helpers;

namespace rulesmngt.Helpers.Adapters
{
    public class HrSyncAdapter : IHrSyncAdapter
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private IOptions<HrSyncConfiguration> HRConfiguration;
        // private readonly IOptions<Client> ClientData;
        // private readonly IHttpClientFactory ClientFactory;

        public HrSyncAdapter(IConfiguration configuration,
                                    ILoggerFactory loggerFactory,
                                    IOptions<HrSyncConfiguration> hrConfiguration
                                    // IOptions<Client> clientdata
                                    // IHttpClientFactory clientfactory
                                    )
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<HrSyncAdapter>();
            this.HRConfiguration = hrConfiguration;
            // this.ClientData = clientdata;
            // this.ClientFactory = clientfactory;
        }

        public async Task<string> reqHrSyncFullSync(CancellationToken cancellationToken, string sync_post_service)
        {
            string baseUrl = string.Empty;
            string results = string.Empty;

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                baseUrl = string.Format(HRConfiguration.Value.ApiUrl + sync_post_service);
                Logger.LogInformation("{0} - baseUrl req hrsync extraction tramite FULL Sync: {1}",DateTime.Now,baseUrl);
                string token = EncoderUtility.Base64Encode(string.Format("{0}:{1}", HRConfiguration.Value.Username, HRConfiguration.Value.Password));

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                

                // using (HttpClient client = ClientFactory.CreateClient())
                using (HttpClient client = new HttpClient(httpClientHandler))
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
                                results = json;
                            }
                        }
                        else
                        {
                            Logger.LogError(res.StatusCode.ToString());
                        }
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
        
    }
}