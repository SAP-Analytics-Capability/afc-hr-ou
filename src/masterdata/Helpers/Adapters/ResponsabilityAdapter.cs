using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using masterdata.Models;
using System.Text;

using masterdata.Helpers;
using masterdata.Interfaces;
using masterdata.Interfaces.Adapters;
using masterdata.Models.rulesmngt;
using masterdata.Models.Configuration;
using System.Threading;

namespace masterdata.Helpers.Adapters
{
    public class ResponsabilityAdapter : IResponsabilityAdapter
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private readonly IOptions<RulesmngtConfigurations> RulesConfig;
        private readonly IOptions<Client> ClientData;

        public ResponsabilityAdapter(IConfiguration configuration,
                                    ILoggerFactory loggerFactory,
                                    IOptions<RulesmngtConfigurations> rulesconfig,
                                    IOptions<Client> cliendata)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<ResponsabilityAdapter>();
            this.RulesConfig = rulesconfig;
            this.ClientData = cliendata;
        }

        public async Task<List<Responsability>> GetResponsabilities()
        {
            List<Responsability> results = null;
            string baseUrl = string.Empty;

            try
            {
                baseUrl = string.Format("{0}{1}", string.Format(RulesConfig.Value.ApiUrl + "rulesmngt/v1/activitylist"));

                using (HttpClient client = new HttpClient())
                {
                    string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                results = JsonConvert.DeserializeObject<List<Responsability>>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get registry information from base url: {0}", baseUrl));
            }
            return results;
        }

        public async Task<List<Responsability>> GetResponsabilityByNewPrimoCode(CancellationToken cancellationToken, string responsability)
        {
            List<Responsability> results = new List<Responsability>();
            string baseUrl = string.Empty;

            try
            {
                baseUrl = string.Format("{0}", string.Format(RulesConfig.Value.ApiUrl + "responsability/getbynewprimocode/q?new_primo="+ responsability));
                //baseUrl ="http://localhost:5006/rulesmngt/v1/responsability/getbynewprimocode/q?new_primo="+ responsability;
                using (HttpClient client = new HttpClient())
                {
                    string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                results = JsonConvert.DeserializeObject<List<Responsability>>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get registry information from base url: {0}.", baseUrl));
            }
            return results;
        }
    }
}