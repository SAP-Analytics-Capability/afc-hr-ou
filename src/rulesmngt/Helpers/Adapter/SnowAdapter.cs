using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using rulesmngt.Helpers;
using rulesmngt.Interfaces.Adapters;
using rulesmngt.Models.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using rulesmngt.Models.SnowTranscodingResult;
using rulesmngt.Models.SnowConsolidationResult;
using rulesmngt.Interfaces;
using rulesmngt.Models;

namespace rulesmngt.Helpers.Adapters
{
    public class SnowAdapter : ISnowAdatpter
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private IOptions<SnowCatalogConfiguration> SnowConfiguration;
        private IOptions<ProxyData> ProxyData;
        private IEventLogUtils EventLogUtils;
        //private readonly IHttpClientFactory ClientFactory;

        public SnowAdapter(
            IConfiguration configuration,
            ILoggerFactory loggerFactory,
            IOptions<SnowCatalogConfiguration> snowconfiguration,
            IOptions<ProxyData> proxydata,
            IEventLogUtils eventLogUtils)
        //IHttpClientFactory clientfactory)
        {
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger<SnowAdapter>();
            SnowConfiguration = snowconfiguration;
            ProxyData = proxydata;
            EventLogUtils = eventLogUtils;
            //ClientFactory = clientfactory;
        }

        public async Task<RootObjectConsolidation> getConsolidationCatalog(CancellationToken cancellationToken)
        {
            RootObjectConsolidation results = null;
            string baseUrl = string.Empty;
            try
            {
                baseUrl = SnowConfiguration.Value.ApiUrl + SnowConfiguration.Value.Catalog;

                cancellationToken.ThrowIfCancellationRequested();

                /*var proxy = new WebProxy()
                {
                    Address = new Uri(ProxyData.Value.Url),
                    UseDefaultCredentials = true
                };

                if (!string.IsNullOrEmpty(ProxyData.Value.Username) && !string.IsNullOrEmpty(ProxyData.Value.Password))
                {
                    proxy.Credentials = new NetworkCredential(ProxyData.Value.Username, ProxyData.Value.Password);
                    proxy.UseDefaultCredentials = false;
                }*/

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    //httpClientHandler.Proxy = proxy;

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        var token = EncoderUtility.Base64Encode(SnowConfiguration.Value.Username, SnowConfiguration.Value.Password);

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            if (res.IsSuccessStatusCode)
                            {
                                using (HttpContent content = res.Content)
                                {
                                    string json = await content.ReadAsStringAsync();
                                    results = JsonConvert.DeserializeObject<RootObjectConsolidation>(json);
                                }
                            }
                            else
                            {
                                Logger.LogError(res.StatusCode.ToString());
                            }

                            if (cancellationToken.IsCancellationRequested)
                            {
                                return results;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get registry information from base url: {0}. Full error message: {1}", baseUrl, ex.ToString()));
                EventLog eventLog = EventLogUtils.writeEventLog(ex.Message, "ERROR", "getConsolidationCatalog");
                EventLogUtils.AddEventLog(eventLog);
                throw (ex);
            }
            return results;
        }

        public async Task<RootObjectTranscoding> getTranscodingActivity(CancellationToken cancellationToken)
        {
            RootObjectTranscoding results = null;
            string baseUrl = string.Empty;
            try
            {
                baseUrl = SnowConfiguration.Value.ApiUrl + SnowConfiguration.Value.Transcoding;

                cancellationToken.ThrowIfCancellationRequested();

                /*var proxy = new WebProxy()
                {
                    Address = new Uri(ProxyData.Value.Url),
                    UseDefaultCredentials = true
                };

                if (!string.IsNullOrEmpty(ProxyData.Value.Username) && !string.IsNullOrEmpty(ProxyData.Value.Password))
                {
                    proxy.Credentials = new NetworkCredential(ProxyData.Value.Username, ProxyData.Value.Password);
                    proxy.UseDefaultCredentials = false;
                }*/

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    //httpClientHandler.Proxy = proxy;

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        var token = EncoderUtility.Base64Encode(SnowConfiguration.Value.Username, SnowConfiguration.Value.Password);

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            if (res.IsSuccessStatusCode)
                            {
                                using (HttpContent content = res.Content)
                                {
                                    string json = await content.ReadAsStringAsync();
                                    results = JsonConvert.DeserializeObject<RootObjectTranscoding>(json);
                                }
                            }
                            else
                            {
                                Logger.LogError(res.StatusCode.ToString());
                            }

                            if (cancellationToken.IsCancellationRequested)
                            {
                                return results;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get registry information from base url: {0}. Full error message: {1}", baseUrl, ex.ToString()));
                EventLog eventLog = EventLogUtils.writeEventLog(ex.Message, "ERROR", "getTranscodingActivity");
                EventLogUtils.AddEventLog(eventLog);
                throw (ex);
            }
            return results;
        }
    }
}