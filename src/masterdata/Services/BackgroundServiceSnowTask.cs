using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using masterdata.Models;
using masterdata.Models.SnowCostCenterResults;
using masterdata.Models.Configuration;
using masterdata.Helpers;
using masterdata.Interfaces;

namespace masterdata.Services
{
    public class BackgroundServiceSnowTask : BackgroundServiceSnow
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private IOptions<SnowAdapterConfiguration> SnowConfiguration;
        private IOptions<ProxyData> ProxyData;
        private IResultAdapter ResultAdapter;
        private readonly IResultData ResultData;
        private readonly IOuCCAssociation OUAssociation;
        private readonly IHrMasterdataOuData HRMasterData;
        private readonly IClientAccessData ClientAccessData;
        private readonly ISchedulerConfigurationData SchedulerConfigurationData;
        private IOptions<SchedulerOptions> Scheduler;
        private IMailSender MailSender;

        public BackgroundServiceSnowTask(IConfiguration configuration,
                                        ILoggerFactory loggerFactory,
                                        IOptions<SnowAdapterConfiguration> snwoconfiguration,
                                        IOptions<ProxyData> proxydata,
                                        IResultAdapter resultAdapter,
                                        IResultData resultdata,
                                        IOuCCAssociation ouassociation,
                                        IHrMasterdataOuData hrmasterdata,
                                        IClientAccessData clientaccessdata,
                                        ISchedulerConfigurationData schedulerConfigurationData,
                                        IOptions<SchedulerOptions> scheduler,
                                        IMailSender mailSender)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<BackgroundServiceSnow>();
            this.SnowConfiguration = snwoconfiguration;
            this.ProxyData = proxydata;
            this.ResultAdapter = resultAdapter;
            this.ResultData = resultdata;
            this.OUAssociation = ouassociation;
            this.HRMasterData = hrmasterdata;
            this.ClientAccessData = clientaccessdata;
            this.SchedulerConfigurationData = schedulerConfigurationData;
            this.Scheduler = scheduler;
            this.MailSender = mailSender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            Logger.LogInformation(string.Format("{0} - Snow Scheduler is starting",DateTime.Now));
            DateTime start = DateTime.Now;

            while (!stoppingToken.IsCancellationRequested)
            {
                RootObject results = null;
                string baseUrl = string.Empty;
                DateTime dateAssociation = DateTime.Now.Date;
                string associationDate = dateAssociation.ToString("yyyy-MM-dd HH:mm:ss");
                associationDate = associationDate.Insert(0, "%27");
                associationDate = associationDate.Insert(associationDate.Length, "%27");
                associationDate = associationDate.Replace('/', '-');
                associationDate = associationDate.Replace(":", "%3A");
                associationDate = associationDate.Replace(" ", "%27%2C%27");
                SnowConfiguration.Value.SysparmQuery = SnowConfiguration.Value.SysparmQuery.Replace("{start_date_association}", associationDate);

                try
                {
                    //lastaccess per associationDate
                    DateTime lastaccess = ClientAccessData.AddNewAccess(SchedulerCommons.SnowScheduler, "OREP");

                    baseUrl = SnowConfiguration.Value.ApiUrl +
                            "sysparm_query=" + SnowConfiguration.Value.SysparmQuery +
                            "&sysparm_display_value=" + SnowConfiguration.Value.SysparmDisplayValue +
                            "&sysparm_exclude_reference_link=" + SnowConfiguration.Value.SysparmExcludeReferenceLink +
                            "&sysparm_fields=" + SnowConfiguration.Value.SysparmFields;
                    // Logger.LogInformation("{0} - Snow Scheduler - sending baseUrl to Snow: {1}", DateTime.Now,baseUrl);

                    using (var httpClientHandler = new HttpClientHandler())
                    {
                        httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                        //httpClientHandler.Proxy = proxy;

                        using (HttpClient client = new HttpClient(httpClientHandler))
                        {
                            var token = Base64Encode(string.Format("{0}:{1}", SnowConfiguration.Value.Username, SnowConfiguration.Value.Password));

                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                            using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    using (HttpContent content = res.Content)
                                    {
                                        Logger.LogInformation("{0} - Snow Scheduler is computing", DateTime.Now);
                                        string json = await content.ReadAsStringAsync();
                                        results = JsonConvert.DeserializeObject<RootObject>(json);

                                        ResultAdapter.InsertResultsByOUCodAndCCCod(results, DateTime.Now);

                                    }
                                }
                            }
                        }
                    }
                    if (!ClientAccessData.UpdateAccess(SchedulerCommons.SnowScheduler, "OREP"))
                    {
                        Logger.LogWarning("Snow Scheduler: Unable to update the client access for the SnowScheduler. Please, check the log.");
                        //Implement send mail method
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, string.Format("{0} - Snow Scheduler: Unable to get registry information from base url: {1}.", DateTime.Now, baseUrl));
                    List<string> notification = new List<string>();
                    notification.Add("AFC AWS Snow Tasks closed Transfer - Snow Scheduler ERROR");
                    notification.Add("Snow Scheduler: Unable to get registry information from base url. . Unable to transfer SNOW Tasks Closed to BW4HANA via DI");
                    MailSender.SendMail(notification);
                }

                List<SchedulerConfiguration> config = SchedulerConfigurationData.GetConfigurationByName(SchedulerCommons.SnowScheduler);
                int delay_minutes = SchedulerCommons.GetInterval(config, SchedulerCommons.SnowScheduler);
                Logger.LogInformation("{0} - Snow Scheduler is in waiting for {1} minutes", DateTime.Now, delay_minutes);
                await Task.Delay(TimeSpan.FromMinutes(delay_minutes));
                Logger.LogInformation("{0} - Snow Scheduler restarted to read configuration [DayOfMonth=0,Interval]", DateTime.Now, delay_minutes);
            }
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
