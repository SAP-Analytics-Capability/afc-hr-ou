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
using masterdata.Models.rulesmngt;
using masterdata.Interfaces;
using Microsoft.Extensions.Options;
using masterdata.Models.Configuration;
using System.Threading;

namespace masterdata.Helpers.Adapters
{
    public class ItaGloAdapter : IItaGloAdapter
    {
        private IConfiguration _configuration;
        private ILogger _logger;
        private IOptions<RulesmngtConfigurations> _rulesmngtConfiguration;
        private readonly IOptions<Client> ClientData;
        private readonly IHttpClientFactory ClientFactory;

        public ItaGloAdapter(IConfiguration configuration, ILoggerFactory loggerFactory, IOptions<RulesmngtConfigurations> rulesmngtConfiguration,
        IOptions<Client> clientdata, IHttpClientFactory clientfactory)
        {
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<EntityAdapter>();
            _rulesmngtConfiguration = rulesmngtConfiguration;
            this.ClientData = clientdata;
            this.ClientFactory = clientfactory;
        }

        public async Task<ItaGlo> GetItaGloInfo(CancellationToken cancellationToken, string sapHrCode)
        {
            ItaGlo itaGlo = null;
            string baseUrl = string.Empty;

            try
            {
                baseUrl = string.Format("{0}itaglo/getitaglo/q?saphrcode={1}", string.Format(_rulesmngtConfiguration.Value.ApiUrl), sapHrCode);

               //baseUrl = "http://localhost:5006/rulesmngt/v1/itaglo/getitaglo/q?saphrcode=" + sapHrCode;

                using (HttpClient client = new HttpClient())
                {

                    var token = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _rulesmngtConfiguration.Value.Username, _rulesmngtConfiguration.Value.Password));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(token));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                itaGlo = JsonConvert.DeserializeObject<ItaGlo>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get registry information from base url.");
            }
            return itaGlo;
        }
    }
}