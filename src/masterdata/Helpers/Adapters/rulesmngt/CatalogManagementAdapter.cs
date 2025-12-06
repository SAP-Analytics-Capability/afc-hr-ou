using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using masterdata.Interfaces;
using masterdata.Models;
using masterdata.Models.rulesmngt;
using masterdata.Models.Configuration;
using System.Text;

namespace masterdata.Helpers.Adapters
{
    public class CatalogManagementAdapter : ICatalogManagementAdapter
    {
        private readonly IConfiguration Configutation;
        private readonly ILogger Logger;
        private readonly IOptions<RulesmngtConfigurations> RulesConfiguration;
        private IOptions<Client> ClientData;
        private readonly IHttpClientFactory ClientFactory;

        public CatalogManagementAdapter(IConfiguration configuration,
                                            ILoggerFactory loggerfactory,
                                            IOptions<RulesmngtConfigurations> rulesconfiguration,
                                            IOptions<Client> clientdata,
                                            IHttpClientFactory clientfactory)
        {
            this.Configutation = configuration;
            this.Logger = loggerfactory.CreateLogger<CatalogManagementAdapter>();
            this.RulesConfiguration = rulesconfiguration;
            this.ClientData = clientdata;
            this.ClientFactory = clientfactory;
        }

        public async Task<List<ActivityAssociation>> GetActivityAssociationCatalog(CancellationToken cancellationToken)
        {
            List<ActivityAssociation> list = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}activityassociations/activities", RulesConfiguration.Value.ApiUrl);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.GetAsync(baseurl, cancellationToken))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                list = JsonConvert.DeserializeObject<List<ActivityAssociation>>(json);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Catalog Management Adapter throwns an exception.");
            }

            return list;
        }

        public async Task<ActivityAssociation> GetActivityAssociationByID(int id, CancellationToken cancellationToken)
        {
            ActivityAssociation result = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}activityassociations/{1}", RulesConfiguration.Value.ApiUrl, id);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.GetAsync(baseurl, cancellationToken))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<ActivityAssociation>(json);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }

        public async Task<CatalogManager> AddActivty(ActivityAssociation activity, CancellationToken cancellationToken)
        {
            CatalogManager result = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}activityassociations/add/item", RulesConfiguration.Value.ApiUrl);
                //baseurl = string.Format("{0}activityassociations/add/item", "http://localhost:5002/rulesmngt/v1/");

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonActivity = JsonConvert.SerializeObject(activity);
                    HttpContent post = new StringContent(jsonActivity, Encoding.UTF8, "application/json"); // start here
                    using (HttpResponseMessage res = await client.PostAsync(baseurl, post))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<CatalogManager>(json);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }

        public async Task<List<CatalogManager>> AddActivties(List<ActivityAssociation> activity, CancellationToken cancellationToken)
        {
            List<CatalogManager> result = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}activityassociations/add/items", RulesConfiguration.Value.ApiUrl);
                //baseurl = string.Format("{0}activityassociations/add/items", "http://localhost:5002/rulesmngt/v1/");

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonActivity = JsonConvert.SerializeObject(activity);
                    HttpContent post = new StringContent(jsonActivity, Encoding.UTF8, "application/json"); // start here
                    using (HttpResponseMessage res = await client.PostAsync(baseurl, post))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<List<CatalogManager>>(json);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }

        public async Task<CatalogManager> UpdateActivity(int id, ActivityAssociation activity, CancellationToken cancellationToken)
        {
            CatalogManager result = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}activityassociations/update/q?id={1}", RulesConfiguration.Value.ApiUrl, id);
                //baseurl = string.Format("{0}activityassociations/update/q?id={1}", "http://localhost:5002/rulesmngt/v1/", id);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonActivity = JsonConvert.SerializeObject(activity);
                    HttpContent post = new StringContent(jsonActivity, Encoding.UTF8, "application/json"); // start here
                    using (HttpResponseMessage res = await client.PutAsync(baseurl, post))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<CatalogManager>(json);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }

        public async Task<CatalogManager> RemoveActivity(int id, CancellationToken cancellationToken)
        {
            CatalogManager result = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}activityassociations/remove/q?id={1}", RulesConfiguration.Value.ApiUrl, id);
                //baseurl = string.Format("{0}activityassociations/remove/q?id={1}", "http://localhost:5002/rulesmngt/v1/", id);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.DeleteAsync(baseurl, cancellationToken))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<CatalogManager>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }

        public async Task<List<CatalogManager>> RemovesActivities(List<int> ids, CancellationToken cancellationToken)
        {
            List<CatalogManager> result = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}activityassociations/remove", RulesConfiguration.Value.ApiUrl);
                //baseurl = string.Format("{0}activityassociations/remove", "http://localhost:5002/rulesmngt/v1/");

                string jsonIds = JsonConvert.SerializeObject(ids);
                HttpContent post = new StringContent(jsonIds, Encoding.UTF8, "application/json"); // start here
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.PostAsync(baseurl, post))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<List<CatalogManager>>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }

        public async Task<List<CompanyScope>> GetCompanyScopeCatalog(CancellationToken cancellationToken)
        {
            List<CompanyScope> list = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}companyscopes/companies", RulesConfiguration.Value.ApiUrl);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.GetAsync(baseurl, cancellationToken))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                list = JsonConvert.DeserializeObject<List<CompanyScope>>(json);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Catalog Management Adapter throwns an exception.");
            }

            return list;
        }

        public async Task<CompanyScope> GetCompanyByID(int id, CancellationToken cancellationToken)
        {
            CompanyScope result = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}companyscopes/q?id={1}", RulesConfiguration.Value.ApiUrl, id);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.GetAsync(baseurl, cancellationToken))
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
                throw (ex);
            }

            return result;
        }

        public async Task<CatalogManager> AddCompany(CompanyScope company, CancellationToken cancellationToken)
        {
            CatalogManager result = null;
            string baseurl = string.Empty;

            try
            {
                //baseurl = string.Format("{0}companyscopes", "http://localhost:5002/rulesmngt/v1/");
                baseurl = string.Format("{0}companyscopes", RulesConfiguration.Value.ApiUrl);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonCompany = JsonConvert.SerializeObject(company);
                    HttpContent post = new StringContent(jsonCompany, Encoding.UTF8, "application/json"); // start here
                    using (HttpResponseMessage res = await client.PostAsync(baseurl, post))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<CatalogManager>(json);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }

        public async Task<List<CatalogManager>> AddCompanies(List<CompanyScope> companies, CancellationToken cancellationToken)
        {
            List<CatalogManager> result = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}companyscopes/add/items", RulesConfiguration.Value.ApiUrl);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonCompany = JsonConvert.SerializeObject(companies);
                    HttpContent post = new StringContent(jsonCompany, Encoding.UTF8, "application/json"); // start here
                    using (HttpResponseMessage res = await client.PostAsync(baseurl, post))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<List<CatalogManager>>(json);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }

        public async Task<CatalogManager> UpdateCompany(int id, CompanyScope company, CancellationToken cancellationToken)
        {
            CatalogManager result = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}companyscopes/update/{1}", RulesConfiguration.Value.ApiUrl, id);
                //baseurl = string.Format("{0}companyscopes/update/q?id={1}", "http://localhost:5002/rulesmngt/v1/", id);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonActivity = JsonConvert.SerializeObject(company);
                    HttpContent post = new StringContent(jsonActivity, Encoding.UTF8, "application/json"); // start here
                    using (HttpResponseMessage res = await client.PutAsync(baseurl, post))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<CatalogManager>(json);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }

        public async Task<CatalogManager> RemoveCompany(int id, CancellationToken cancellationToken)
        {
            CatalogManager result = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}companyscopes/remove/q?id={1}", RulesConfiguration.Value.ApiUrl, id);
                //baseurl = string.Format("{0}companyscopes/remove/q?id={1}", "http://localhost:5002/rulesmngt/v1/", id);

                cancellationToken.ThrowIfCancellationRequested();
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.DeleteAsync(baseurl, cancellationToken))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<CatalogManager>(json);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }

        public async Task<List<CatalogManager>> RemovesCompanies(List<int> ids, CancellationToken cancellationToken)
        {
            List<CatalogManager> result = null;
            string baseurl = string.Empty;

            try
            {
                baseurl = string.Format("{0}companyscopes/remove", RulesConfiguration.Value.ApiUrl);
                cancellationToken.ThrowIfCancellationRequested();
                string jsonIds = JsonConvert.SerializeObject(ids);
                HttpContent post = new StringContent(jsonIds, Encoding.UTF8, "application/json"); // start here
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage res = await client.PostAsync(baseurl, post))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<List<CatalogManager>>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }
    }
}