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
    public class ExceptionTableAdapter : IExceptionTableAdapter
    {
        private IConfiguration _configuration;
        private ILogger _logger;
        private IOptions<RulesmngtConfigurations> _rulesmngtConfiguration;
        private readonly IOptions<Client> ClientData;
        private readonly IHttpClientFactory ClientFactory;

        public ExceptionTableAdapter(IConfiguration configuration, ILoggerFactory loggerFactory, IOptions<RulesmngtConfigurations> rulesmngtConfiguration,
        IOptions<Client> clientdata, IHttpClientFactory clientfactory)
        {
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<EntityAdapter>();
            _rulesmngtConfiguration = rulesmngtConfiguration;
            this.ClientData = clientdata;
            this.ClientFactory = clientfactory;
        }

        public async Task<List<ExceptionTable>> GetExceptions()
        {
            List<ExceptionTable> results = null;
            string baseUrl = string.Empty;

            //baseUrl = "http://localhost:5001/rulesmngt/v1/exceptiontable";
            baseUrl = _rulesmngtConfiguration.Value.ApiUrl + "exceptiontable";

            try
            {

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
                                results = JsonConvert.DeserializeObject<List<ExceptionTable>>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get registry information from base url.");
            }
            return results;
        }

        public async Task<ExceptionTable> GetExceptionByTypoUo(CancellationToken cancellationToken, string typoUo)
        {
            ExceptionTable results = null;
            string baseUrl = string.Empty;

            try
            {

                baseUrl = _rulesmngtConfiguration.Value.ApiUrl + "exceptiontable/getbytypouo/q?typouo=" + typoUo;

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
                                results = JsonConvert.DeserializeObject<ExceptionTable>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get registry information from base url.");
            }
            return results;
        }

        public async Task<List<ExceptionTable>> GetExceptionByTypoUoGblPrev(CancellationToken cancellationToken, string typoUo, string gblPrev)
        {
            List<ExceptionTable> results = null;
            string baseUrl = string.Empty;

            try
            {
                baseUrl = _rulesmngtConfiguration.Value.ApiUrl + "exceptiontable/getbytypouo/q?typouo=" + typoUo + "&gblprev=" + gblPrev;

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
                                results = JsonConvert.DeserializeObject<List<ExceptionTable>>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get registry information from base url.");
            }
            return results;
        }

    }
}