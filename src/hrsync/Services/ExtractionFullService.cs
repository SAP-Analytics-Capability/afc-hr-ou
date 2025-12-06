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
    public class ExtractionFullService : IExtractionFullService
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private IOptions<SchedulerOptions> Scheduler;
        private IOptions<ProxyData> ProxyData;
        private IOptions<HrGlobalConfiguration> hrGlobalConfiguration;
        private IReportData ReportData;
        private IHrMasterdataOuData hrmasterdataouData;
        private readonly IHrSchedulerConfigurationData hrSchedulerConfigurationData;
        private IMailSender mailSender;
        private IFullSync FullSync;

        public ExtractionFullService(
            IConfiguration configuration,
            ILoggerFactory loggerFactory,
            IOptions<SchedulerOptions> scheduler,
            IOptions<ProxyData> proxydata,
            IOptions<HrGlobalConfiguration> hrGlobalConfiguration,
            IReportData reportdata,
            IHrMasterdataOuData hrmasterdataouData,
            IHrSchedulerConfigurationData hrSchedulerConfigurationData,
            IMailSender mailSender,
            IFullSync fullSync)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger<BackgroundServiceTask>();
            this.Scheduler = scheduler;
            this.ProxyData = proxydata;
            this.hrGlobalConfiguration = hrGlobalConfiguration;
            this.ReportData = reportdata;
            this.hrmasterdataouData = hrmasterdataouData;
            this.hrSchedulerConfigurationData = hrSchedulerConfigurationData;
            this.mailSender = mailSender;
            this.FullSync = fullSync;
        }

        public void MakeUOFullExtraction(List<HrmasterdataOuList> hrMasterdataOuList, HrmasterdataOuList synclist, DateTime serviceTime)
        {
            Task<HrmasterdataOuList> organizationalUnitsTask;

            Report currentReport = new Report();
            Logger.LogInformation(string.Format("Extraction FULL hrsync: - {0} start", DateTime.Now));

            string changedateattribute = string.Empty;
            Report lastReport = ReportData.GetLastReport();
            List<string> notification = new List<string>();

            // List<string> notificationInit = new List<string>();
            // notificationInit.Add("AFC AWS HrSync STARTED");
            // notificationInit.Add("Processo FULL acquisizione OU avviato!");
            // mailSender.SendMail(notificationInit);

            notification.Add("AFC AWS HrSync FULL Extraction");

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
                        currentReport.changedFound = "Y";
                    }
                    else if (organizationalUnitsTask.Result != null &&
                       organizationalUnitsTask.Result.hrmasterdataou != null &&
                       organizationalUnitsTask.Result.hrmasterdataou.Count == 0)
                    {
                        Logger.LogDebug(string.Format("Extraction FULL hrsync: {0} - No change OU (Organizational Unit) has been found.", DateTime.Now));
                        currentReport.changedFound = "N";
                    }
                    else if (organizationalUnitsTask.Result == null)
                    {
                        Logger.LogWarning(string.Format("Extraction FULL hrsync: {0} - The call to SAP HR for changed UO has been cancelled or it is in a faulty state", DateTime.Now));
                        currentReport.changedFound = "Faulted or Cancelled";
                        notification.Add("Request for changed OU faulted or cancelled.");
                    }

                }
                else if (organizationalUnitsTask.IsFaulted || organizationalUnitsTask.IsCanceled)
                {
                    Logger.LogWarning(string.Format("Extraction FULL hrsync: {0} - The call to SAP HR for changed UO has been cancelled or it is in a faulty state", DateTime.Now));
                    currentReport.changedFound = "Faulted or Cancelled";
                    notification.Add("Request for changed OU faulted or cancelled.");
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
                        currentReport.dummyFound = "Y";

                    }
                    else if (organizationalUnitsTask.Result != null &&
                            organizationalUnitsTask.Result.hrmasterdataou != null &&
                            organizationalUnitsTask.Result.hrmasterdataou.Count == 0)
                    {
                        Logger.LogDebug(string.Format("Extraction FULL hrsync: {0} - No change OU (Organizational Unit) has been found.", DateTime.Now));
                        currentReport.changedFound = "N";
                    }
                    else if (organizationalUnitsTask.Result == null)
                    {
                        Logger.LogWarning(string.Format("Extraction FULL hrsync: {0} - The call to SAP HR for dummy-CC UO has been cancelled or it is in a faulty state", DateTime.Now));
                        currentReport.dummyFound = "Faulted or Cancelled";
                        notification.Add("Request for OU with dummy-CC faulted or cancelled.");
                    }
                }
                else if (organizationalUnitsTask.IsFaulted || organizationalUnitsTask.IsCanceled)
                {
                    Logger.LogWarning(string.Format("Extraction FULL hrsync: {0} - The call to SAP HR for dummy-CC UO has been cancelled or it is in a faulty state", DateTime.Now));
                    currentReport.dummyFound = "Faulted or Cancelled";
                    notification.Add("Request for OU with dummy-CC faulted or cancelled.");
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
                        currentReport.noccFound = "Y";
                    }
                    else if (organizationalUnitsTask.Result != null &&
                        organizationalUnitsTask.Result.hrmasterdataou != null &&
                        organizationalUnitsTask.Result.hrmasterdataou.Count == 0)
                    {
                        Logger.LogDebug(string.Format("Extraction FULL hrsync: {0} - No no-cost-center OU (Organizational Unit) has been found.", DateTime.Now));
                        currentReport.noccFound = "N";
                    }
                    else if (organizationalUnitsTask.Result == null)
                    {
                        Logger.LogWarning(string.Format("Extraction FULL hrsync: {0} - The call to SAP HR for no-cost-center UO has been cancelled or it is in a faulty state", DateTime.Now));
                        currentReport.noccFound = "Faulted or Cancelled";
                        notification.Add("Request for OU with no-CC faulted or cancelled.");
                    }
                }
                else if (organizationalUnitsTask.IsFaulted || organizationalUnitsTask.IsCanceled)
                {
                    Logger.LogWarning(string.Format("Extraction FULL hrsync: {0} - The call to SAP HR for no-cost-center UO has been cancelled or it is in a faulty state", DateTime.Now));
                    currentReport.noccFound = "Faulted or Cancelled";
                    notification.Add("Request for OU with no-CC faulted or cancelled.");
                }
                currentReport.saved = "N";
                currentReport.date_time = serviceTime;
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
                    Logger.LogInformation("Extraction FULL hrsync: {0} - Last run date: {1}", DateTime.Now, lastUtilReport.date_time);
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
                                    if (hrmasterdataouData.RemoveOrganizationalUnit(oldou))
                                    {
                                        sou.SyncDateTime = serviceTime;
                                        hrmasterdataouData.AddNewOrganizationalUnit(sou);
                                    }
                                    currentReport.saved = "Y";
                                }
                            }

                            Logger.LogInformation(string.Format("Extraction FULL hrsync: {0} - The system imported {1} records", DateTime.Now, oudailylist.Count));
                        }
                        else
                        {
                            Logger.LogWarning(string.Format("Extraction FULL hrsync: {0} - No OU founds in hrmasterdataou for {1}. The process will continue by re-adding old units", DateTime.Now, inputdate));
                            oudoublechecklist.AddRange(synclist.hrmasterdataou);
                        }
                    }
                    else
                    {
                        Logger.LogWarning(string.Format("Extraction FULL hrsync: {0} - No last run date found. The process will continue by re-adding old units", DateTime.Now));
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
                            Logger.LogInformation(string.Format("Extraction FULL hrsync: {0} - The system succesfully saved {1} records", DateTime.Now, oudoublechecklist.Count));
                        }
                        else
                        {
                            Logger.LogWarning(string.Format("Extraction FULL hrsync: {0} - No OUs saved or updated in hrmasterdataou.", DateTime.Now));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, string.Format("Extraction FULL hrsync: {0} - Unable to persist organizational unit update.", DateTime.Now));
                }
            }

            ReportData.AddNewReport(currentReport);

            if ((currentReport.changedFound != null && currentReport.changedFound.Contains("or")) || 
                (currentReport.dummyFound != null && currentReport.dummyFound.Contains("or")) || 
                (currentReport.noccFound != null && currentReport.noccFound.Contains("or")))
            {
                // mailSender.SendMail(notification);
            }
            
            List<string> notificationEnd = new List<string>();
            notificationEnd.Add("AFC AWS HrSync END");
            notificationEnd.Add("Processo FULL acquisizione OU terminato!");
            mailSender.SendMail(notificationEnd);

            // Verifica flag_sync in hrschedulerconfiguration per avvio FULL sincrona e suo avvio dove sync_post_service == elaborationfullsync
            HrSchedulerConfiguration dbconfig_full;
            dbconfig_full = hrSchedulerConfigurationData.GetHrSchedulerConfiguration();
            if (dbconfig_full != null)
            {
                if (dbconfig_full.FlagSync == "Y" && dbconfig_full.SyncPostService == "elaborationmasterdatafullsync")
                {
                    Logger.LogInformation(string.Format("{0} - Full Sync: launched {1}", DateTime.Now, dbconfig_full.SyncPostService));
                    int res = FullSync.fullSync(dbconfig_full.FlagSync, dbconfig_full.SyncPostService);
                }
                else
                {
                    Logger.LogInformation(string.Format("{0} - Full Sync: NOT launched", DateTime.Now));
                }
            }
            Logger.LogInformation(string.Format("{0} - Extraction FULL hrsync exit procedure", DateTime.Now));

        }

        private async Task<HrmasterdataOuList> retrieveHrMasterDataChangedDate(CancellationToken cancellationToken, string changeddateattribute)
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
                        Logger.LogInformation($"url req: { baseUrl }");
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
                Logger.LogError(ex, string.Format("Extraction FULL hrsync: {0} - Unable to get data from {1}", DateTime.Now, "retrieveHrMasterDataChangedDate"));
            }
            return hrMasterdataOuList;
        }

        private async Task<HrmasterdataOuList> retrieveHrMasterDataDummy(CancellationToken cancellationToken, string changeddateattribute)
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
                                                            "L",
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
                        Logger.LogInformation($"url req: { baseUrl }");
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
                Logger.LogError(ex, string.Format("Extraction FULL hrsync: {0} - Unable to get data from {1}", DateTime.Now, "retrieveHrMasterDataDummy"));
            }
            return hrMasterdataOuList;
        }

        private async Task<HrmasterdataOuList> retrieveHrMasterDataNoCostCenter(CancellationToken cancellationToken, string changeddateattribute)
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
                        Logger.LogInformation($"url req: { baseUrl }");
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
                Logger.LogError(ex, "Extraction FULL hrsync: {0} - Unable to get data from {1}", DateTime.Now, "retrieveHrMasterDataNoCostCenter");
            }
            return hrMasterdataOuList;
        }

    }
}