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
    public class ActivityAssociationAdapter : IActivityAssociationAdapter
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private readonly IOptions<RulesmngtConfigurations> RulesConfig;
        private readonly IOptions<Client> ClientData;

        public ActivityAssociationAdapter(IConfiguration configuration,
                                    ILoggerFactory loggerFactory,
                                    IOptions<RulesmngtConfigurations> rulesconfig,
                                    IOptions<Client> cliendata)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<ActivityAssociationAdapter>();
            this.RulesConfig = rulesconfig;
            this.ClientData = cliendata;
        }

        public async Task<List<ActivityAssociation>> GetAllActivityAssociation()
        {
            List<ActivityAssociation> results = null;
            string baseUrl = string.Empty;

            try
            {
                baseUrl = $"{RulesConfig.Value.ApiUrl}activityassociations/activities";

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
                                results = JsonConvert.DeserializeObject<List<ActivityAssociation>>(json);
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

        public async Task<ActivityAssociation> GetSingleActivityAssociation(int activityId)
        {
            ActivityAssociation result = null;
            string baseUrl = string.Empty;

            try
            {
                baseUrl = $"{RulesConfig.Value.ApiUrl}activityassociations/{activityId}";

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
                                result = JsonConvert.DeserializeObject<ActivityAssociation>(json);
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

        public async Task<ActivityAssociation> PostActivityAssociation(ActivityAssociation association)
        {

            string baseUrl = string.Empty;
            ActivityAssociation activity = null;
            try
            {
                baseUrl = $"{RulesConfig.Value.ApiUrl}activityassociations";


                using (HttpClient client = new HttpClient())
                {
                    string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                    using (HttpResponseMessage res = await client.PostAsJsonAsync(baseUrl, association))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                activity = JsonConvert.DeserializeObject<ActivityAssociation>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get registry information from base url: {0}", baseUrl));
            }
            return activity;
        }

        public async Task<ActivityAssociation> PutActivityAssociation(ActivityAssociation association, int id)
        {

            string baseUrl = string.Empty;
            // bool succesfull = false;
            ActivityAssociation aa = null;
            try
            {
                baseUrl = $"{RulesConfig.Value.ApiUrl}activityassociations/{id}";

                using (HttpClient client = new HttpClient())
                {
                    //string token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                    using (HttpResponseMessage res = await client.PutAsJsonAsync(baseUrl, association))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                                using (HttpContent content = res.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                aa = JsonConvert.DeserializeObject<ActivityAssociation>(json);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get registry information from base url: {0}", baseUrl));
            }
            return aa;
        }

        public async Task<bool> DeleteActivityAssociation(int id)
        {

            string baseUrl = string.Empty;
            bool succesfull = false;
            try
            {
                baseUrl = $"{RulesConfig.Value.ApiUrl}activityassociations/{id}";

                using (HttpClient client = new HttpClient())
                {
                    // token = EncoderUtility.Base64Encode(ClientData.Value.Username, ClientData.Value.Password);

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