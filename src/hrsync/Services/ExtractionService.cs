using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using hrsync.Interface;
using hrsync.Models;
using hrsync.Models.Configuration;
using System.Text;
using hrsync.Helpers;
using hrsync.Data;
using System.Linq;

namespace hrsync.Services
{
    public class ExtractionService : IExtractionService
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private IOptions<SchedulerOptions> Scheduler;
        private IOptions<ProxyData> ProxyData;
        private IOptions<HrGlobalConfiguration> hrGlobalConfiguration;
        private IReportData ReportData;
        private IHrMasterdataOuData hrmasterdataouData;
        private readonly IHrSchedulerConfigurationData hrSchedulerConfigurationData;

        public ExtractionService(
            IConfiguration configuration,
            ILoggerFactory loggerFactory,
            IOptions<SchedulerOptions> scheduler,
            IOptions<ProxyData> proxydata,
            IOptions<HrGlobalConfiguration> hrGlobalConfiguration,
            IReportData reportdata,
            IHrMasterdataOuData hrmasterdataouData,
            IHrSchedulerConfigurationData hrSchedulerConfigurationData)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<BackgroundServiceTask>();
            this.Scheduler = scheduler;
            this.ProxyData = proxydata;
            this.hrGlobalConfiguration = hrGlobalConfiguration;
            this.ReportData = reportdata;
            this.hrmasterdataouData = hrmasterdataouData;
            this.hrSchedulerConfigurationData = hrSchedulerConfigurationData;
        }

//         // public void  MakeUOExtraction
//         // (List<HrmasterdataOuList> hrMasterdataOuList, HrmasterdataOuList synclist, Task<HrmasterdataOuList> organizationalUnitsTask, 
//         // string[] servicemessages = new string[3])
//         // {
//         //     List<HrmasterdataOuList> hrMasterdataOuList = new List<HrmasterdataOuList>();
//         //     HrmasterdataOuList synclist = new HrmasterdataOuList();
//         //     Task<HrmasterdataOuList> organizationalUnitsTask;
//         //     //DateTime initservicetime = DateTime.Now;
//         //     string[] servicemessages = new string[3];

//         //     //Report currentReport = new Report();

//         //     Logger.LogInformation("Extraction is starting");

//         //     stoppingToken.Register(() =>
//         //         Logger.LogDebug($" Scheduler is stopping"));

//         //     while (!stoppingToken.IsCancellationRequested)
//         //     {
//         //         DateTime serviceTime = DateTime.Now;
//         //         bool canStart = false;

//         //         HrSchedulerConfiguration dbconfig = hrSchedulerConfigurationData.GetHrSchedulerConfiguration();
//         //         if (dbconfig != null)
//         //         {
//         //             SchedulerOptions schedulerconfig = dbconfig.ToSchedulerOptions(dbconfig);
//         //             canStart = CanStart(schedulerconfig, serviceTime);
//         //         }
//         //         if (canStart)
//         //         {
//         ì
//         //             int ts = Convert.ToInt32(serviceTime.AddMinutes(Convert.ToInt32(dbconfig.MinInterval)).Subtract(DateTime.Now).TotalMinutes);
//         //             //await Task.Delay(TimeSpan.FromMinutes(Convert.ToInt32(Scheduler.Value.Interval)), stoppingToken);
//         //             await Task.Delay(TimeSpan.FromMinutes(ts), stoppingToken);

//         //         }

//         //     }
//         // }

//         //serviceTime = DateTime.Now;
        public void MakeUOExtraction(List<HrmasterdataOuList> hrMasterdataOuList, HrmasterdataOuList synclist, DateTime serviceTime)
        {
            Task<HrmasterdataOuList> organizationalUnitsTask;

            Report currentReport = new Report();
            Logger.LogInformation(string.Format("scheduler can start"));

            string changedateattribute = string.Empty;
            Report lastReport = ReportData.GetLastReport();

            if (lastReport != null)
            {
                changedateattribute = lastReport.date_time.ToString("yyyy-MM-dd");
            }
            else
            {
                changedateattribute = DateTime.Today.AddDays(-300).ToString("yyyy-MM-dd");
            }

            try
            {
                synclist.hrmasterdataou = new List<HrmasterdataOu>();
                organizationalUnitsTask = retrieveHrMasterDataChangedDate(CancellationToken.None, changedateattribute);
                organizationalUnitsTask.Wait();

                if (organizationalUnitsTask.IsCompletedSuccessfully)
                {
                    if (organizationalUnitsTask.Result != null &&
                       organizationalUnitsTask.Result.hrmasterdataou != null &&
                       organizationalUnitsTask.Result.hrmasterdataou.Count > 0)
                    {
                        foreach (HrmasterdataOu item in organizationalUnitsTask.Result.hrmasterdataou)
                        {
                            item.Typology = "modify";
                        }

                        synclist.hrmasterdataou.AddRange(organizationalUnitsTask.Result.hrmasterdataou);
                        //servicemessages[0] = "Changed UO found";   
                        currentReport.changedFound = "Y";
                    }
                    else
                    {
                        Logger.LogDebug(string.Format("{0} - No change OU (Organizational Unit) has been found.", DateTime.Now));
                        //servicemessages[0] = "No changed UO found";
                        currentReport.changedFound = "N";
                    }
                }
                else if (organizationalUnitsTask.IsFaulted || organizationalUnitsTask.IsCanceled)
                {
                    Logger.LogWarning(string.Format("{0} - The call to SAP HR for changed UO has been cancelled or it is in a faulty state", DateTime.Now));
                    //servicemessages[0] = "Changed UO faulted or cancelled";
                    currentReport.changedFound = "Faulted or Cancelled";
                }

                organizationalUnitsTask = retrieveHrMasterDataDummy(CancellationToken.None, changedateattribute);
                organizationalUnitsTask.Wait();

                if (organizationalUnitsTask.IsCompletedSuccessfully)
                {
                    if (organizationalUnitsTask.Result != null &&
                        organizationalUnitsTask.Result.hrmasterdataou != null &&
                        organizationalUnitsTask.Result.hrmasterdataou.Count > 0)
                    {
                        foreach (HrmasterdataOu item in organizationalUnitsTask.Result.hrmasterdataou)
                        {
                            item.Typology = "dummy";
                        }
                        synclist.hrmasterdataou.AddRange(organizationalUnitsTask.Result.hrmasterdataou);
                        //servicemessages[1] = "Dummy-CC UO found";
                        currentReport.dummyFound = "Y";

                    }
                    else
                    {
                        Logger.LogDebug(string.Format("{0} - No dummy CC OU (Organizational Unit) has been found.", DateTime.Now));
                        //servicemessages[1] = "No dummy-CC changed UO found";
                        currentReport.dummyFound = "N";
                    }
                }
                else if (organizationalUnitsTask.IsFaulted || organizationalUnitsTask.IsCanceled)
                {
                    Logger.LogWarning(string.Format("{0} - The call to SAP HR for dummy-CC UO has been cancelled or it is in a faulty state", DateTime.Now));
                    //servicemessages[1] = "Dummy-CC UO faulted or cancelled";
                    currentReport.dummyFound = "Faulted or Cancelled";
                }
                organizationalUnitsTask = retrieveHrMasterDataNoCostCenter(CancellationToken.None, changedateattribute);
                organizationalUnitsTask.Wait();


                if (organizationalUnitsTask.IsCompletedSuccessfully)
                {
                    if (organizationalUnitsTask.Result != null &&
                        organizationalUnitsTask.Result.hrmasterdataou != null &&
                        organizationalUnitsTask.Result.hrmasterdataou.Count > 0)
                    {
                        foreach (HrmasterdataOu item in organizationalUnitsTask.Result.hrmasterdataou)
                        {
                            item.Typology = "insert";
                        }

                        synclist.hrmasterdataou.AddRange(organizationalUnitsTask.Result.hrmasterdataou);
                        //servicemessages[2] = "No-CC UO found";
                        currentReport.noccFound = "Y";
                    }
                    else
                    {
                        Logger.LogDebug(string.Format("{0} - No no-cost-center OU (Organizational Unit) has been found.", DateTime.Now));
                        //servicemessages[2] = "No No-CC UO found";
                        currentReport.noccFound = "N";
                    }
                }
                else if (organizationalUnitsTask.IsFaulted || organizationalUnitsTask.IsCanceled)
                {
                    Logger.LogWarning(string.Format("{0} - The call to SAP HR for no-cost-center UO has been cancelled or it is in a faulty state", DateTime.Now));
                    //servicemessages[2] = "No-CC UO faulted or cancelled";
                    currentReport.noccFound = "Faulted or Cancelled";
                }
                currentReport.saved = "N";
                currentReport.date_time = serviceTime;
                //ReportData.AddNewReport(currentReport);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get HrSyncData information from base url.");
            }

            if (synclist != null && synclist.hrmasterdataou != null && synclist.hrmasterdataou.Count > 0)
            {
                try
                {
                    List<HrmasterdataOu> oudoublechecklist = new List<HrmasterdataOu>();
                    Report lastUtilReport = ReportData.GetLastUtilReport();
                    if (lastUtilReport != null)
                    {
                        DateTime inputdate = lastUtilReport.date_time;
                        List<HrmasterdataOu> oudailylist = hrmasterdataouData.GetByDate(inputdate);
                        if (oudailylist != null && oudailylist.Count > 0)
                        {
                            foreach (HrmasterdataOu sou in synclist.hrmasterdataou)
                            {
                                HrmasterdataOu oldou = null;
                                foreach (HrmasterdataOu dou in oudailylist)
                                {
                                    if (dou != null && dou.UOrg == sou.UOrg)
                                    {
                                        oldou = dou;
                                    }

                                    if (oldou != null)
                                    {
                                        break;
                                    }
                                }
                                if (oldou == null)
                                {
                                    sou.SyncDateTime = serviceTime;
                                    oudoublechecklist.Add(sou);
                                }
                                else
                                {
                                    //HrmasterdataOu hroldou = (HrmasterdataOu)oldou;
                                    if (!sou.Equals(oldou))
                                    {
                                        sou.SyncDateTime = serviceTime;
                                        hrmasterdataouData.AddNewOrganizationalUnit(sou, oldou);
                                        currentReport.saved = "Y";
                                    }
                                }
                            }
                        }
                        else
                        {
                            Logger.LogWarning(string.Format("{0} - Unable to get OU. The process will continue by re-adding old units", DateTime.Now));
                            oudoublechecklist.AddRange(synclist.hrmasterdataou);
                        }

                    }
                    else
                    {
                        Logger.LogWarning(string.Format("{0} - Unable to get OU. The process will continue by re-adding old units", DateTime.Now));
                        oudoublechecklist.AddRange(synclist.hrmasterdataou);

                    }

                    if (oudoublechecklist != null && oudoublechecklist.Count > 0)
                    {
                        HrmasterdataOu oldou = new HrmasterdataOu();
                        bool saved = false;
                        saved = hrmasterdataouData.AddIfOrganizationalUnitsByCode(oudoublechecklist, serviceTime);
                        if (saved)
                        {
                            currentReport.saved = "Y";
                        }

                    }

                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, string.Format("{0} - Unable to persist organizational unit update.", DateTime.Now));
                }

            }
            ReportData.AddNewReport(currentReport);
            //Commento per commit

            Logger.LogDebug("Scheduler is stopping.");
        }

        public async Task<HrmasterdataOuList> retrieveHrMasterDataChangedDate(CancellationToken cancellationToken, string changeddateattribute)
        {
            HrmasterdataOuList hrMasterdataOuList = null;
            ApiUrlParams apiUrlParams = new ApiUrlParams();

            try
            {
                string baseUrl = string.Format("{0}?", string.Format(hrGlobalConfiguration.Value.ApiUrl));
                baseUrl += apiUrlParams.GetUrlWithParams(hrGlobalConfiguration.Value.validityDate,
                                                            changeddateattribute,
                                                            hrGlobalConfiguration.Value.companyCode,
                                                            hrGlobalConfiguration.Value.percCon,
                                                            "N",
                                                            //hrGlobalConfiguration.Value.costcenterDummy,
                                                            hrGlobalConfiguration.Value.gestionali,
                                                            hrGlobalConfiguration.Value.limit,
                                                            hrGlobalConfiguration.Value.offset,
                                                            hrGlobalConfiguration.Value.noCostcenter,
                                                            "Y",
                                                            "Y");
                cancellationToken.ThrowIfCancellationRequested();

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        client.Timeout = TimeSpan.FromMinutes(15);
                        string token = EncoderUtility.Base64Encode(string.Format("{0}:{1}", hrGlobalConfiguration.Value.Username, hrGlobalConfiguration.Value.Password));

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                        Logger.LogInformation($"Getting changed OU. Start time: { DateTime.Now}");
                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            Logger.LogInformation($"Getting changed OU. End time: { DateTime.Now}");
                            if (res.IsSuccessStatusCode)
                            {
                                using (HttpContent content = res.Content)
                                {
                                    if (res.StatusCode == HttpStatusCode.OK)
                                    {
                                        string json = await content.ReadAsStringAsync();
                                        json = json.Replace("hrmasterdata-ou", "hrmasterdataou");
                                        hrMasterdataOuList = JsonConvert.DeserializeObject<HrmasterdataOuList>(json);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{0} - Unable to get data from {1}", DateTime.Now, "retrieveHrMasterDataChangedDate");
            }
            return hrMasterdataOuList;
        }

        public async Task<HrmasterdataOuList> retrieveHrMasterDataDummy(CancellationToken cancellationToken, string changeddateattribute)
        {
            HrmasterdataOuList hrMasterdataOuList = null;
            ApiUrlParams apiUrlParams = new ApiUrlParams();

            try
            {
                string baseUrl = string.Format("{0}?", string.Format(hrGlobalConfiguration.Value.ApiUrl));
                baseUrl += apiUrlParams.GetUrlWithParams(hrGlobalConfiguration.Value.validityDate,
                                                            //changeddateattribute,
                                                            hrGlobalConfiguration.Value.changedateAttribute,
                                                            hrGlobalConfiguration.Value.companyCode,
                                                            hrGlobalConfiguration.Value.percCon,
                                                            "Y",
                                                            hrGlobalConfiguration.Value.gestionali,
                                                            hrGlobalConfiguration.Value.limit,
                                                            hrGlobalConfiguration.Value.offset,
                                                            "N",
                                                            "Y");
                cancellationToken.ThrowIfCancellationRequested();

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        client.Timeout = TimeSpan.FromMinutes(15);
                        string token = EncoderUtility.Base64Encode(string.Format("{0}:{1}", hrGlobalConfiguration.Value.Username, hrGlobalConfiguration.Value.Password));

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                        Logger.LogInformation($"Getting Dummy OU. Start time: { DateTime.Now}");
                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            Logger.LogInformation($"Getting Dummy OU. End time: { DateTime.Now}");
                            if (res.IsSuccessStatusCode)
                            {
                                using (HttpContent content = res.Content)
                                {
                                    if (res.StatusCode == HttpStatusCode.OK)
                                    {
                                        string json = await content.ReadAsStringAsync();
                                        json = json.Replace("hrmasterdata-ou", "hrmasterdataou");
                                        hrMasterdataOuList = JsonConvert.DeserializeObject<HrmasterdataOuList>(json);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{0} - Unable to get data from {1}", DateTime.Now, "retrieveHrMasterDataDummy");
            }
            return hrMasterdataOuList;
        }

        public async Task<HrmasterdataOuList> retrieveHrMasterDataNoCostCenter(CancellationToken cancellationToken, string changeddateattribute)
        {
            HrmasterdataOuList hrMasterdataOuList = null;
            ApiUrlParams apiUrlParams = new ApiUrlParams();

            try
            {
                string baseUrl = string.Format("{0}?", string.Format(hrGlobalConfiguration.Value.ApiUrl));
                baseUrl += apiUrlParams.GetUrlWithParams(hrGlobalConfiguration.Value.validityDate,
                                                            //changeddateattribute,
                                                            hrGlobalConfiguration.Value.changedateAttribute,
                                                            hrGlobalConfiguration.Value.companyCode,
                                                            hrGlobalConfiguration.Value.percCon,
                                                            "N",
                                                            hrGlobalConfiguration.Value.gestionali,
                                                            hrGlobalConfiguration.Value.limit,
                                                            hrGlobalConfiguration.Value.offset,
                                                            "Y",
                                                            "Y");
                cancellationToken.ThrowIfCancellationRequested();

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        client.Timeout = TimeSpan.FromMinutes(15);
                        string token = EncoderUtility.Base64Encode(string.Format("{0}:{1}", hrGlobalConfiguration.Value.Username, hrGlobalConfiguration.Value.Password));

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

                        Logger.LogInformation($"Getting No-CC OU. Start time: { DateTime.Now}");
                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            Logger.LogInformation($"Getting No-CC OU. End time: { DateTime.Now}");
                            if (res.IsSuccessStatusCode)
                            {
                                using (HttpContent content = res.Content)
                                {
                                    if (res.StatusCode == HttpStatusCode.OK)
                                    {
                                        string json = await content.ReadAsStringAsync();
                                        json = json.Replace("hrmasterdata-ou", "hrmasterdataou");
                                        hrMasterdataOuList = JsonConvert.DeserializeObject<HrmasterdataOuList>(json);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{0} - Unable to get data from {1}", DateTime.Now, "retrieveHrMasterDataNoCostCenter");
            }
            return hrMasterdataOuList;
        }

    }
}