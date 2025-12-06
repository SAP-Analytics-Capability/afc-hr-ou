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
using masterdata.Interfaces;
using masterdata.Models.Configuration;

namespace masterdata.Helpers.Adapters
{
    public class CompanyScopeAdapter : ICompanyScopeAdapter
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private readonly IOptions<RulesmngtConfigurations> RulesConfig;
        private readonly IOptions<Client> ClientData;

        public CompanyScopeAdapter(IConfiguration configuration,
                                    ILoggerFactory loggerFactory,
                                    IOptions<RulesmngtConfigurations> rulesconfig,
                                    IOptions<Client> cliendata)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<CompanyScopeAdapter>();
            this.RulesConfig = rulesconfig;
            this.ClientData = cliendata;
        }

        public async Task<List<CompanyScope>> GetAllCompanies()
        {
            List<CompanyScope> results = null;
            string baseUrl = string.Empty;

            try
            {
                baseUrl = $"{RulesConfig.Value.ApiUrl}companyscopes/companies";

                using (HttpClient client = new HttpClient())
                {
                    //string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                results = JsonConvert.DeserializeObject<List<CompanyScope>>(json);
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

        public async Task<CompanyScope> GetCompanyById(int companyId)
        {
            CompanyScope result = null;
            string baseUrl = string.Empty;

            try
            {
                baseUrl = $"{RulesConfig.Value.ApiUrl}companyscopes/{companyId}";

                using (HttpClient client = new HttpClient())
                {
                    //string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<CompanyScope>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get registry information from base url: {0}.", baseUrl));
            }
            return result;
        }

        public async Task<CompanyScope> InsertNewCompany(CompanyScope company)
        {

            string baseUrl = string.Empty;
            //CompanyScope company = null;
            try
            {
                baseUrl = $"{RulesConfig.Value.ApiUrl}companyscopes";


                using (HttpClient client = new HttpClient())
                {
                    //string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                    using (HttpResponseMessage res = await client.PostAsJsonAsync(baseUrl, company))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                company = JsonConvert.DeserializeObject<CompanyScope>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get registry information from base url: {0}", baseUrl));
            }
            return company;
        }

        public async Task<CompanyScope> UpdateCompany(CompanyScope company, int id)
        {

            string baseUrl = string.Empty;
            //bool succesfull = false;
            CompanyScope c = null;
            try
            {
                baseUrl = $"{RulesConfig.Value.ApiUrl}companyscopes/{id}";

                using (HttpClient client = new HttpClient())
                {
                    //string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                    using (HttpResponseMessage res = await client.PutAsJsonAsync(baseUrl, company))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                c = JsonConvert.DeserializeObject<CompanyScope>(json);
                            }

                            //succesfull = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get registry information from base url: {0}", baseUrl));
            }
            return c;
        }

        public async Task<bool> DeleteCompany(int id)
        {

            string baseUrl = string.Empty;
            bool succesfull = false;
            try
            {
                baseUrl = $"{RulesConfig.Value.ApiUrl}companyscopes/{id}";

                using (HttpClient client = new HttpClient())
                {
                    //string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                    using (HttpResponseMessage res = await client.DeleteAsync(baseUrl))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            succesfull = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get registry information from base url: {0}", baseUrl));
            }
            return succesfull;
        }

    }
}