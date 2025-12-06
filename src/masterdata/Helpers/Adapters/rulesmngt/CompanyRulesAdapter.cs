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
using masterdata.Models.Configuration;
using masterdata.Interfaces;
using System.Web;
using Microsoft.Extensions.Options;
using System.Threading;

namespace masterdata.Helpers.Adapters
{
    public class CompanyRulesAdapter : ICompanyRulesAdapter
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger Logger;
        private readonly IOptions<RulesmngtConfigurations> _rulesmngtConfiguration;
        private IOptions<Client> ClientData;
        private readonly IHttpClientFactory ClientFactory;

        public CompanyRulesAdapter(IConfiguration configuration,
                                    ILoggerFactory loggerFactory,
                                    IOptions<RulesmngtConfigurations> rulesmngtConfiguration,
                                    IOptions<Client> clientdata,
                                    IHttpClientFactory clientfactory)
        {
            this._configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<CompanyRulesAdapter>();
            this._rulesmngtConfiguration = rulesmngtConfiguration;
            this.ClientData = clientdata;
            this.ClientFactory = clientfactory;
        }

        public async Task<List<CompanyRules>> GetCompanies()
        {
            List<CompanyRules> results = null;
            string baseUrl = string.Empty;

            try
            {
                baseUrl = string.Format(_rulesmngtConfiguration.Value.ApiUrl, _configuration["RULESMNGT_SERVICE_HOST"], this._configuration["RULESMNGT_SERVICE_HOST"]) + "companyrules/";

                using (HttpClient client = ClientFactory.CreateClient())
                {
                    var token = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", ClientData.Value.Username, ClientData.Value.Password));
                    //var token = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", RulesConfig.Value.Username, RulesConfig.Value.Password));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(token));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                results = JsonConvert.DeserializeObject<List<CompanyRules>>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get registry information from base url.");
            }
            return results;
        }

        public async Task<CompanyRules> GetCompanyById(string companyRules)
        {
            CompanyRules results = null;
            string baseUrl = string.Empty;

            try
            {
                baseUrl = string.Format(_rulesmngtConfiguration.Value.ApiUrl, _configuration["RULESMNGT_SERVICE_HOST"], this._configuration["RULESMNGT_SERVICE_HOST"]) + "companyrules/";

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
                                results = JsonConvert.DeserializeObject<CompanyRules>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get registry information from base url.");
            }
            return results;
        }

        public async Task<List<CompanyRules>> GetCompanyByDescPerimeter(string companyRulesCode, string companyRulesDesc, CancellationToken cancellationToken)
        {
            List<CompanyRules> results = null;
            string baseUrl = string.Empty;

            companyRulesDesc = System.Web.HttpUtility.UrlPathEncode(companyRulesDesc);
            try
            {
                baseUrl = string.Format("{0}companyscopes/getbydescper/q?sap_desc={1}&afc_desc={2}", string.Format(_rulesmngtConfiguration.Value.ApiUrl), companyRulesCode, companyRulesDesc);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {

                    var token = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _rulesmngtConfiguration.Value.Username, _rulesmngtConfiguration.Value.Password));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(token));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                results = JsonConvert.DeserializeObject<List<CompanyRules>>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get companyRules information from base url.");
            }
            return results;
        }

        public async Task<List<CompanyRules>> GetCompanyBySapHRCode(string companyRulesDesc, CancellationToken cancellationToken)
        {
            List<CompanyRules> results = null;
            string baseUrl = string.Empty;

            companyRulesDesc = System.Web.HttpUtility.UrlPathEncode(companyRulesDesc);
            try
            {

                baseUrl = string.Format("{0}companyscopes/getbydesc/q?company_desc={1}", string.Format(_rulesmngtConfiguration.Value.ApiUrl), companyRulesDesc);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {

                    var token = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _rulesmngtConfiguration.Value.Username, _rulesmngtConfiguration.Value.Password));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(token));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                results = JsonConvert.DeserializeObject<List<CompanyRules>>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get companyRules information from base url.");
            }
            return results;
        }

        public async Task<List<CompanyRules>> GetCompanyBynewPrimoDesc(string newPrimoDesc, CancellationToken cancellationToken)
        {
            List<CompanyRules> results = null;
            string baseUrl = string.Empty;
            newPrimoDesc = System.Web.HttpUtility.UrlPathEncode(newPrimoDesc);

            try
            {
                baseUrl = string.Format("{0}companyscopes/newprimodesc/q?newprimo={1}", string.Format(_rulesmngtConfiguration.Value.ApiUrl), newPrimoDesc);
                cancellationToken.ThrowIfCancellationRequested();
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
                                results = JsonConvert.DeserializeObject<List<CompanyRules>>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get registry information from base url.");
            }
            return results;
        }

        public async Task<CompanyRules> GetCompanyBynewPrimoSapHRCode(string newPrimoSapHRCode)
        {
            CompanyRules results = null;
            string baseUrl = string.Empty;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                results = JsonConvert.DeserializeObject<CompanyRules>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get registry information from base url.");
            }
            return results;
        }
    }
}