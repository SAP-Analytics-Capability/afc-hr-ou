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
using System.Globalization;

namespace hrsync.Services
{
    public class ExtractionCustomService : IExtractionCustomService
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

        public ExtractionCustomService(
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

        public void MakeUOCustomExtraction(List<HrmasterdataOuList> hrMasterdataOuList, HrmasterdataOuList synclist, DateTime serviceTime)
        {
            Task<HrmasterdataOuList> organizationalUnitsTask;
            HrSchedulerConfiguration dbconfig_custom;
            dbconfig_custom = hrSchedulerConfigurationData.GetHrSchedulerConfigurationCustom();

            serviceTime = DateTime.Now;

            Report currentReport = new Report();
            Logger.LogInformation(string.Format("Extraction CUSTOM hrsync: {0} - Extraction Custom start", DateTime.Now));

            string changedateattribute = string.Empty;
            Report lastReport = ReportData.GetLastReport();
            List<string> notification = new List<string>();
            notification.Add("AFC AWS HrSync CUSTOM Extraction");

            // List<string> notificationInit = new List<string>();
            // notificationInit.Add("AFC AWS HrSync STARTED");
            // notificationInit.Add("Processo CUSTOM acquisizione OU avviato!");
            // mailSender.SendMail(notificationInit);

            if (lastReport != null)
            {
                changedateattribute = lastReport.date_time.ToString("yyyy-MM-dd");
            }
            else
            {
                changedateattribute = DateTime.Today.AddDays(-300).ToString("yyyy-MM-dd");
            }

            Logger.LogInformation($"Extraction CUSTOM hrsync: Last Date about Data Imported (from report table): {changedateattribute}");

            try
            {

                synclist.hrmasterdataou = new List<HrmasterdataOu>();
                string[] flagArgs;
                string flagValiditydate = "";
                string flagChangeddateattribute = "";
                string flagCostcenterDummy = "";
                string flagNoCostcenter = "";
                string flagConsistenti = "";
                string flagEffettivi = "";
                string itemCurrent = "";
                string argChangeddateattribute = "";
                string argValiditydate = "";
                string topology = "";
                const string dummy = "dummy";
                const string insert = "insert";
                const string modify = "modify";
                string current_report_val = "";

                SchedulerArgsOptions schedulerArgs = dbconfig_custom.ToSchedulerArgsOptions(dbconfig_custom);
                if (schedulerArgs.ConfigArgsRequest != null && schedulerArgs.ConfigArgsRequest.Count > 0)
                {
                    foreach (String itemArgs in schedulerArgs.ConfigArgsRequest)
                    {

                        Logger.LogInformation($"Extraction CUSTOM hrsync: processing this configuration {itemArgs}");
                        currentReport.message = "Extraction CUSTOM hrsync";

                        itemCurrent = itemArgs;
                        itemCurrent = itemCurrent.Replace("[", "");
                        itemCurrent = itemCurrent.Replace("]", "");
                        flagArgs = itemCurrent.Split(',');
                        flagValiditydate = flagArgs[0];
                        flagChangeddateattribute = flagArgs[1];
                        flagCostcenterDummy = flagArgs[2];
                        flagNoCostcenter = flagArgs[3];
                        flagConsistenti = flagArgs[4];
                        flagEffettivi = flagArgs[5];

                        if (flagValiditydate.Count(t => t == '-') == 2)
                        {
                            // Parse date-only value with invariant culture.
                            string dateString = flagValiditydate;
                            string format = "yyyy-MM-dd";
                            CultureInfo culture = CultureInfo.CurrentCulture;
                            try
                            {
                                DateTime result = DateTime.ParseExact(dateString, format, culture);
                                argValiditydate = flagValiditydate;
                            }
                            catch (FormatException)
                            {
                                throw new Exception("Extraction CUSTOM hrsync: il parametro data validitydate nel campo config_args_request non ha il formato corretto!");
                            }
                        }
                        else if (flagValiditydate == "" || flagValiditydate == null)
                        {
                            argValiditydate = "";
                        }
                        else
                        {
                            throw new Exception("Extraction CUSTOM hrsync: exception nel parametro validitydate nel campo config_args_request!");
                        }

                        // Logger.LogInformation($"DEBUG: flagChangeddateattribute: [{flagChangeddateattribute}]");
                        bool flagChangeddateattribute_is_number = true;
                        int gg = 0;
                        try
                        {
                            gg = Convert.ToInt32(flagChangeddateattribute);
                        }
                        catch (FormatException)
                        {
                            flagChangeddateattribute_is_number = false;
                        }

                        if (flagChangeddateattribute == "Y")
                        {
                            argChangeddateattribute = changedateattribute;
                        }
                        else if (flagChangeddateattribute.StartsWith("init_month"))
                        {
                            argChangeddateattribute = "";
                            try
                            {
                                int mm = Convert.ToInt32(flagChangeddateattribute.Split(" ").Last());
                                argChangeddateattribute = DateTime.Today.AddMonths(mm).ToString("yyyy-MM") + "-01";
                            }
                            catch (FormatException)
                            {
                                throw new Exception("Extraction CUSTOM hrsync: il parametro changedateattribute nel campo config_args_request non ha il formato 'init_month x' corretto!");
                            }
                        }
                        else if (flagChangeddateattribute_is_number)
                        {
                            argChangeddateattribute = DateTime.Today.AddDays(gg).ToString("yyyy-MM-dd");
                        }
                        else if (flagChangeddateattribute.Count(t => t == '-') == 2)
                        {
                            // Parse date-only value with invariant culture.
                            string dateString = flagChangeddateattribute;
                            string format = "yyyy-MM-dd";
                            CultureInfo culture = CultureInfo.CurrentCulture;
                            try
                            {
                                DateTime result = DateTime.ParseExact(dateString, format, culture);
                                argChangeddateattribute = flagChangeddateattribute;
                            }
                            catch (FormatException)
                            {
                                throw new Exception("Extraction CUSTOM hrsync: il parametro data changedateattribute nel campo config_args_request non ha il formato corretto!");
                            }
                        }
                        else if (flagChangeddateattribute == "" || flagChangeddateattribute == null)
                        {
                            argChangeddateattribute = "";
                        }
                        else
                        {
                            throw new Exception("Extraction CUSTOM hrsync: parametro changeddateattribute nel campo config_args_request non valorizzato correttamente!");
                        }

                        Logger.LogInformation($"Extraction CUSTOM hrsync: Validitydate: {flagValiditydate} -> {argValiditydate}");
                        Logger.LogInformation($"Extraction CUSTOM hrsync: Changeddateattribute: {flagChangeddateattribute} -> {argChangeddateattribute}");
                        Logger.LogInformation($"Extraction CUSTOM hrsync: CostcenterDummy: {flagCostcenterDummy}");
                        Logger.LogInformation($"Extraction CUSTOM hrsync: NoCostcenter: {flagNoCostcenter}");
                        Logger.LogInformation($"Extraction CUSTOM hrsync: Consistenti: {flagConsistenti}");
                        Logger.LogInformation($"Extraction CUSTOM hrsync: Effettivi: {flagEffettivi}");

                        if (flagCostcenterDummy == "Y" || flagCostcenterDummy == "L")
                        {
                            topology = dummy;
                        }
                        else
                        {
                            if (flagNoCostcenter == "Y")
                            {
                                topology = insert;
                            }
                            else
                            {
                                topology = modify;
                            }
                        }

                        Logger.LogInformation($"Extraction CUSTOM hrsync: ou flagged like {topology}");

                        organizationalUnitsTask = retrieveHrMasterDataCustom(CancellationToken.None
                            , argValiditydate
                            , argChangeddateattribute
                            , flagCostcenterDummy
                            , flagNoCostcenter
                            , flagConsistenti
                            , flagEffettivi);
                        organizationalUnitsTask.Wait();

                        if (organizationalUnitsTask.IsCompletedSuccessfully)
                        {
                            if (organizationalUnitsTask.Result != null &&
                                organizationalUnitsTask.Result.hrmasterdataou != null &&
                                organizationalUnitsTask.Result.hrmasterdataou.Count > 0)
                            {
                                foreach (HrmasterdataOu item in organizationalUnitsTask.Result.hrmasterdataou)
                                {
                                    item.Typology = topology;
                                }

                                synclist.hrmasterdataou.AddRange(organizationalUnitsTask.Result.hrmasterdataou);
                                // currentReport.changedFound = "Y";
                                current_report_val = "Y";
                            }
                            else if (organizationalUnitsTask.Result != null &&
                                organizationalUnitsTask.Result.hrmasterdataou != null &&
                                organizationalUnitsTask.Result.hrmasterdataou.Count == 0)
                            {
                                Logger.LogDebug(string.Format("Extraction CUSTOM hrsync: {0} - No change OU (Organizational Unit) has been found.", DateTime.Now));
                                // currentReport.changedFound = "N";
                                current_report_val = "N";
                            }
                            else if (organizationalUnitsTask.Result == null)
                            {
                                Logger.LogWarning(string.Format("Extraction CUSTOM hrsync: {0} - The call to SAP HR for changed UO has been cancelled or it is in a faulty state", DateTime.Now));
                                // currentReport.changedFound = "Faulted or Cancelled";
                                current_report_val = "Faulted or Cancelled";
                                notification.Add("Extraction CUSTOM hrsync: Request for changed OU faulted or cancelled.");
                            }

                        }
                        else if (organizationalUnitsTask.IsFaulted || organizationalUnitsTask.IsCanceled)
                        {
                            Logger.LogWarning(string.Format("Extraction CUSTOM hrsync: {0} - The call to SAP HR for changed UO has been cancelled or it is in a faulty state", DateTime.Now));
                            // currentReport.changedFound = "Faulted or Cancelled";
                            current_report_val = "Faulted or Cancelled";
                            notification.Add("Extraction CUSTOM hrsync: Request for changed OU faulted or cancelled.");
                        }


                        // Logger.LogInformation($"DEBUG: switch current_report_val: [{current_report_val}]");
                        // Logger.LogInformation($"DEBUG: switch topology: [{topology}]");
                        switch (topology)
                        {
                            case string a when a.Contains(dummy):
                                currentReport.dummyFound = current_report_val;
                                break;

                            case string b when b.Contains(modify):
                                currentReport.changedFound = current_report_val;
                                break;

                            case string c when c.Contains(insert):
                                currentReport.noccFound = current_report_val;
                                break;
                        }
                    }
                }

                currentReport.saved = "N";
                currentReport.date_time = serviceTime;

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Extraction CUSTOM hrsync: Unable to get HrSyncData information from base url.");
            }

            if (synclist != null && synclist.hrmasterdataou != null && synclist.hrmasterdataou.Count > 0)
            {
                try
                {
                    List<HrmasterdataOu> oudoublechecklist = new List<HrmasterdataOu>();
                    Report lastUtilReport = ReportData.GetLastUtilReport();
                    Logger.LogInformation("Extraction CUSTOM hrsync: {0} - Last run date: {1}", DateTime.Now, lastUtilReport.date_time);
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

                            Logger.LogInformation(string.Format("Extraction CUSTOM hrsync: {0} - The system imported {1} records", DateTime.Now, oudailylist.Count));
                        }
                        else
                        {
                            Logger.LogWarning(string.Format("Extraction CUSTOM hrsync: {0} - No OU founds in hrmasterdataou for {1}. The process will continue by re-adding old units", DateTime.Now, inputdate));
                            oudoublechecklist.AddRange(synclist.hrmasterdataou);
                        }
                    }
                    else
                    {
                        Logger.LogWarning(string.Format("Extraction CUSTOM hrsync: {0} - No last run date found. The process will continue by re-adding old units", DateTime.Now));
                        oudoublechecklist.AddRange(synclist.hrmasterdataou);
                    }

                    if (oudoublechecklist != null && oudoublechecklist.Count > 0)
                    {
                        HrmasterdataOu oldou = new HrmasterdataOu();
                        bool saved = false;
                        // update insert della ou nella hrmasterdataou
                        saved = hrmasterdataouData.AddIfOrganizationalUnitsByCode(oudoublechecklist, serviceTime);
                        if (saved)
                        {
                            currentReport.saved = "Y";
                            Logger.LogInformation(string.Format("Extraction CUSTOM hrsync: {0} - The system succesfully saved {1} records", DateTime.Now, oudoublechecklist.Count));
                        }
                        else
                        {
                            Logger.LogWarning(string.Format("Extraction CUSTOM hrsync: {0} - No OUs saved or updated in hrmasterdataou.", DateTime.Now));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, string.Format("Extraction CUSTOM hrsync: {0} - Unable to persist organizational unit update.", DateTime.Now));
                }
            }

            ReportData.AddNewReport(currentReport);

            if ((currentReport.changedFound != null && currentReport.changedFound.Contains("or")) ||
                    (currentReport.dummyFound != null && currentReport.dummyFound.Contains("or")) ||
                    (currentReport.noccFound != null && currentReport.noccFound.Contains("or"))
                    )
            {
                // mailSender.SendMail(notification);
            }

            Logger.LogInformation(string.Format("{0} - Extraction CUSTOM hrsync: Extraction Custom is finished", DateTime.Now));

            List<string> notificationEnd = new List<string>();
            notificationEnd.Add("AFC AWS HrSync END");
            notificationEnd.Add("Processo CUSTOM acquisizione OU terminato!");
            mailSender.SendMail(notificationEnd);

            // Verifica flag_sync in hrschedulerconfiguration per avvio FULL sincrona e suo avvio dove sync_post_service == elaborationfullsync

            if (dbconfig_custom != null)
            {
                if (dbconfig_custom.FlagSync == "Y" && dbconfig_custom.SyncPostService == "elaborationmasterdatafullsync")
                {
                    Logger.LogInformation(string.Format("{0} - Full Sync: launched {1}", DateTime.Now, dbconfig_custom.SyncPostService));
                    int res = FullSync.fullSync(dbconfig_custom.FlagSync, dbconfig_custom.SyncPostService);
                }
                else
                {
                    Logger.LogInformation(string.Format("{0} - Full Sync: NOT launched", DateTime.Now));
                }
            }
            Logger.LogInformation(string.Format("{0} - Extraction CUSTOM hrsync exit procedure", DateTime.Now));
        
        }

        private async Task<HrmasterdataOuList> retrieveHrMasterDataCustom(CancellationToken cancellationToken
            , string validitydate
            , string changeddateattribute
            , string costcenterDummy
            , string noCostcenter
            , string consistenti
            , string effettivi)
        {
            HrmasterdataOuList hrMasterdataOuList = null;
            ApiUrlParams apiUrlParams = new ApiUrlParams();

            try
            {
                string baseUrl = string.Format("{0}?", string.Format(hrGlobalConfiguration.Value.ApiUrl));
                baseUrl += apiUrlParams.GetUrlWithParams(
                                                            validitydate,
                                                            changeddateattribute,
                                                            hrGlobalConfiguration.Value.companyCode,
                                                            hrGlobalConfiguration.Value.percCon,
                                                            costcenterDummy,
                                                            hrGlobalConfiguration.Value.gestionali,
                                                            hrGlobalConfiguration.Value.limit,
                                                            hrGlobalConfiguration.Value.offset,
                                                            noCostcenter,
                                                            consistenti,
                                                            effettivi);
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

                        Logger.LogInformation($"Extraction CUSTOM hrsync: Getting OU. Start time: { DateTime.Now}");
                        Logger.LogInformation($"Extraction CUSTOM hrsync: baseUrl: {baseUrl}");
                        using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
                        {
                            Logger.LogInformation($"Extraction CUSTOM hrsync: Getting OU. End time: { DateTime.Now}");
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
                Logger.LogError(ex, string.Format("Extraction CUSTOM hrsync: {0} - Unable to get data from {1}", DateTime.Now, "retrieveHrMasterDataCustom"));
            }
            return hrMasterdataOuList;
        }

    }
}