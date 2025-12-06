using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;

using Newtonsoft.Json;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using bwsync.Utils;
using bwsync.Models.Sap;
using bwsync.Interfaces;
using bwsync.Models;
using bwsync.Models.Configuration;

namespace bwsync.Helpers.Adapters
{
    public class BwSapDataAdapter: IBwSapDataAdapter
    {
        private IConfiguration config;
        private IOptions<BwGlobalConfiguration> bwGlobalConfiguration;
        private ILogger logger;

        public BwSapDataAdapter(ILoggerFactory loggerFactory, IConfiguration config, IOptions<BwGlobalConfiguration> bwGlobalConfiguration)
        {
            this.config = config;
            this.bwGlobalConfiguration = bwGlobalConfiguration;
            this.logger = loggerFactory.CreateLogger<BwSapDataAdapter>();
        }

        public async Task<BwSapDataList> GetMasterBWData(CancellationToken cancellationToken)
        {
            BwSapDataList bwSapdataList = null;

            try
            {
                string baseUrl = bwGlobalConfiguration.Value.ApiUrlMaster;
                logger.LogInformation("{0} - baseUrl(ApiUrlMaster) verso BW: {1}",DateTime.Now, baseUrl);
                //baseUrl = getUrlWithParams(baseUrl, ZVarMacroOrg1, ZVarMacroOrg2, ZVarVcs);
                cancellationToken.ThrowIfCancellationRequested();

                using (var httpClientHandler = new HttpClientHandler())
                {
                    //bypass https da eliminare
                    //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        //string token = Base64Encode(bwGlobalConfiguration.Value.Username + ":" + bwGlobalConfiguration.Value.Password);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            if (res.IsSuccessStatusCode)
                            {
                                using (HttpContent content = res.Content)
                                {
                                    string json = await content.ReadAsStringAsync();
                                    bwSapdataList = JsonConvert.DeserializeObject<BwSapDataList>(json);
                                }
                            }
                        }
                    }
                }      
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to get GetMasterBWData information from base url.");
            }

            return bwSapdataList;
        }

        private string Base64Encode(string plainText) 
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}