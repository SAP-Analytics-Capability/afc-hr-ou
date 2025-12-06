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

using masterdata.Helpers;
using masterdata.Interfaces.Adapters;
using masterdata.Models.SnowCostCenterResults;
using masterdata.Models.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace masterdata.Helpers.Adapters
{
    public class SnowAdapter : ISnowAdatpter
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private IOptions<SnowAdapterConfiguration> SnowConfiguration;
        private IOptions<ProxyData> ProxyData;
        private readonly IHttpClientFactory ClientFactory;

        public SnowAdapter(
            IConfiguration configuration,
            ILoggerFactory loggerFactory,
            IOptions<SnowAdapterConfiguration> snowconfiguration,
            IOptions<ProxyData> proxydata,
            IHttpClientFactory clientfactory)
        {
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger<SnowAdapter>();
            SnowConfiguration = snowconfiguration;
            ProxyData = proxydata;
            ClientFactory = clientfactory;
        }

        public async Task<RootObject> getSnowCCUOResults(CancellationToken cancellationToken)
        {
            RootObject results = null;
            string baseUrl = string.Empty;
            DateTime dateAssociation = DateTime.Now;
            string associationDate = dateAssociation.ToString("yyyy-MM-dd HH:mm:ss");
            associationDate = associationDate.Replace('/', '-');
            associationDate = associationDate.Replace(":", "%3A");
            associationDate = associationDate.Replace(" ", "'%2C'");
            //associationDate = System.Web.HttpUtility.UrlPathEncode(associationDate);
            SnowConfiguration.Value.SysparmQuery = SnowConfiguration.Value.SysparmQuery.Replace("start_date_association", associationDate);
            
            try
            {
                baseUrl = SnowConfiguration.Value.ApiUrl +
                        "sysparm_query=" + SnowConfiguration.Value.SysparmQuery +
                        "&sysparm_display_value=" + SnowConfiguration.Value.SysparmDisplayValue +
                        "&sysparm_exclude_reference_link=" + SnowConfiguration.Value.SysparmExcludeReferenceLink +
                        "&sysparm_fields=" + SnowConfiguration.Value.SysparmFields;

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
                                    results = JsonConvert.DeserializeObject<RootObject>(json);
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
                throw (ex);
            }
            return results;
        }
    }
}