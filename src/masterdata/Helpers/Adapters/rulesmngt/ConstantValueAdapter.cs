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
using masterdata.Models.Configuration;
using System.Text;
using System.Threading;
using masterdata.Interfaces;

namespace masterdata.Helpers.Adapters
{
    public class ConstantValueAdapter : IConstantValueAdapter
    {
        private IConfiguration _configuration;
        private ILogger _logger;
        private IOptions<RulesmngtConfigurations> _rulesmngtConfiguration;
        private readonly IOptions<Client> ClientData;
        private readonly IHttpClientFactory ClientFactory;

        public ConstantValueAdapter(IConfiguration configuration, ILoggerFactory loggerFactory, IOptions<RulesmngtConfigurations> rulesmngtConfiguration,
        IOptions<Client> clientdata, IHttpClientFactory clientfactory)
        {
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<EntityAdapter>();
            _rulesmngtConfiguration = rulesmngtConfiguration;
            this.ClientData = clientdata;
            this.ClientFactory = clientfactory;
        }

        public async Task<ConstantValue> GetConstantValue(CancellationToken cancellationToken)
        {
            ConstantValue constant = null;
            string baseUrl = string.Empty;

            try
            {

                //baseUrl = "http://localhost:5006/rulesmngt/v1/constantvalue";
                baseUrl = _rulesmngtConfiguration.Value.ApiUrl + "constantvalue";

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = ClientFactory.CreateClient())
                {
                    var token = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", ClientData.Value.Username, ClientData.Value.Password));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(token));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                constant = JsonConvert.DeserializeObject<ConstantValue>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get registry information from base url.");
            }
            return constant;
        }
    }
}