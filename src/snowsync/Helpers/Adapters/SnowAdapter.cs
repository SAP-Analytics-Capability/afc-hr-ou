using System;
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
using snowsync.Interfaces;
using snowsync.Models;

namespace snowsync.Helpers.Adapters
{
    public class SnowAdapter : ISnowAdatpter
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private IOptions<SnowAdapterConfiguration> SnowConfiguration;

        public SnowAdapter(IConfiguration configuration, ILoggerFactory loggerFactory, IOptions<SnowAdapterConfiguration> snowConfiguration)
        {
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger<SnowAdapter>();
            SnowConfiguration = snowConfiguration;
        }

        public async Task<RootObject> getSnowCCUOResults(CancellationToken cancellationToken)
        {
            RootObject results = null;
            string baseUrl = string.Empty;

            try
            {
                baseUrl = "https://eneldev.service-now.com/api/now/table/u_ou_case_management?sysparm_query=u_costcenterISNOTEMPTY&sysparm_display_value=true&sysparm_exclude_reference_link=true&sysparm_fields=u_ou_code%2Cu_costcenter";
                cancellationToken.ThrowIfCancellationRequested();


                var proxy = new WebProxy()
            	{
                    Address = new Uri("http://proxy-aws.risorse.enel:8080/"),
                    UseDefaultCredentials = true
            	};

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    httpClientHandler.Proxy = proxy;
                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        Logger.LogError("Calling client async-0");
                        string token = Base64Encode("AWS_ws_user:AWS_ws_user");

                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        Logger.LogError("Calling client async");
                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            Logger.LogError("Calling client async-2");
                            if (res.IsSuccessStatusCode)
                            {
                                Logger.LogError("Calling client async-3");
                                using (HttpContent content = res.Content)
                                {
                                    Logger.LogError("Calling client async-4");
                                    string json = await content.ReadAsStringAsync();
                                    results = JsonConvert.DeserializeObject<RootObject>(json);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get registry information from base url: {0}.", baseUrl));
                throw (ex);
            }
            return results;
        }

        private string Base64Encode(string plainText) 
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}