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
    public class EntityAdapter : IEntityAdapter
    {
        private IConfiguration _configuration;
        private ILogger _logger;
        private IOptions<RulesmngtConfigurations> _rulesmngtConfiguration;       
        private readonly IOptions<Client> ClientData;
        private readonly IHttpClientFactory ClientFactory;

        public EntityAdapter(IConfiguration configuration, ILoggerFactory loggerFactory, IOptions<RulesmngtConfigurations> rulesmngtConfiguration,
        IOptions<Client> clientdata, IHttpClientFactory clientfactory)
        {
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<EntityAdapter>();
            _rulesmngtConfiguration = rulesmngtConfiguration;
            this.ClientData = clientdata;
            this.ClientFactory = clientfactory;
        }

        public async Task<List<Entity>> GetEntities()
        {
            List<Entity> results = null;
            string baseUrl = string.Empty;

            try
            {
                // baseUrl = "https://eneldev.service-now.com/api/now/table/u_ou_case_management?sysparm_query=u_costcenterISNOTEMPTY&sysparm_display_value=true&sysparm_exclude_reference_link=true&sysparm_fields=u_ou_code%2Cu_costcenter";
                 

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
                                results = JsonConvert.DeserializeObject<List<Entity>>(json);
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

        public async Task<Entity> GetEntityByNewPrimo(CancellationToken cancellationToken, string newPrimoCode)
        {
            Entity results = null;
            string baseUrl = string.Empty;

            //entity = System.Web.HttpUtility.UrlPathEncode(newPrimoCode);
            try 
            {   
                baseUrl = _rulesmngtConfiguration.Value.ApiUrl + "entity/getbynewprimocode/q?newprimocode=" + newPrimoCode;
               
                //baseUrl = "http://localhost:6001/rulesmngt/v1/entity/getbyname/q?name=" + entity;
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
                                results = JsonConvert.DeserializeObject<Entity>(json);
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

        public async Task<Entity> GetEntity(CancellationToken cancellationToken, string entity)
        {
            Entity results = null;
            string baseUrl = string.Empty;

            try
            {
                // baseUrl = "https://eneldev.service-now.com/api/now/table/u_ou_case_management?sysparm_query=u_costcenterISNOTEMPTY&sysparm_display_value=true&sysparm_exclude_reference_link=true&sysparm_fields=u_ou_code%2Cu_costcenter";


                using (HttpClient client = new HttpClient())
                {

                    var byteArray = Encoding.ASCII.GetBytes("user:psw");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                results = JsonConvert.DeserializeObject<Entity>(json);
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

        public async Task<string> GetCompanyCode(CancellationToken cancellationToken, string entity)
        {
            string companyCode = string.Empty;
            string baseUrl = string.Empty;

            try 
            {   
                baseUrl = _rulesmngtConfiguration.Value.ApiUrl + "DummyController/q?entity=" + entity;
               
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
                                companyCode = JsonConvert.DeserializeObject(json).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get companyCode from url.");
            }
            return companyCode;
        }
    }
}