using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using hrsync.Helpers;
using hrsync.Interface;
using hrsync.Models;

namespace hrsync.Controllers
{
    [Route("hrsync/v1/hrsync")]
    [ApiController]
    public class HrSyncController : Controller
    {
        private ILogger Logger;
        private IHrSyncAdapter HRsyncAdapter;
        private IReportData ReportData;
        private IMailSender MailSender;

        public HrSyncController(ILoggerFactory loggerFactory, IHrSyncAdapter hrsyncadapter, IReportData reportdata, IMailSender mailsender)
        {
            this.HRsyncAdapter = hrsyncadapter;
            this.Logger = loggerFactory.CreateLogger<HrSyncController>();
            this.ReportData = reportdata;
            this.MailSender = mailsender;
        }

        [HttpGet("q")]
        public IActionResult GetHrSyncData([FromQuery(Name = "validity_date")]string validityDate, [FromQuery(Name = "changedate_attribute")]string changedateAttribute,
                                            [FromQuery(Name = "company_code")]string companyCode, [FromQuery(Name = "perc_con")]string percCon,
                                            [FromQuery(Name = "no_costcenter")]string noCostcenter, [FromQuery(Name = "costcenter_dummy")] string costcenterDummy,
                                            [FromQuery(Name = "gestionali")]string gestionali, [FromQuery(Name = "limit")]string limit, [FromQuery(Name = "offset")]string offset, [FromQuery(Name = "changeddateval")]string changedDateVal)
        {
            try
            {
                if (changedDateVal.Equals("Y"))
                {
                    Report r = ReportData.GetLastReport();
                    if (r != null)
                    {
                        changedateAttribute = r.date_time.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        changedateAttribute = DateTime.Today.AddDays(-10).ToString("yyyy-MM-dd");
                    }
                }

                ApiFilteringChecker.HRApiFilterChecker(validityDate, changedateAttribute, companyCode, percCon, costcenterDummy, gestionali, limit, offset, noCostcenter);

                Task<HrmasterdataOuList> hrSyncTask = HRsyncAdapter.GetSapHRData(CancellationToken.None,
                                                                                    validityDate,
                                                                                    changedateAttribute,
                                                                                    companyCode,
                                                                                    percCon,
                                                                                    costcenterDummy,
                                                                                    gestionali,
                                                                                    limit,
                                                                                    offset,
                                                                                    noCostcenter);
                hrSyncTask.Wait();

                HrmasterdataOuList hrMasterdataOuList = null;
                ActionResult result = null;

                if (hrSyncTask.IsCompletedSuccessfully)
                {
                    hrMasterdataOuList = hrSyncTask.Result;

                    if (hrMasterdataOuList != null)
                    {
                        result = Ok(hrMasterdataOuList);
                        //persistReportCall(DateTime.Now, hrMasterdataOuList);
                    }
                    else
                    {
                        hrMasterdataOuList = new HrmasterdataOuList();
                        result = Ok(hrMasterdataOuList);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while excuting HrSyncData.");
                return StatusCode(500, "Unable to get HR Data");
            }
        }

        [HttpGet("getbycode/q")]
        public IActionResult GetByCode([FromQuery(Name = "ou_code")]string oucode)
        {
            ActionResult result = null;
            try
            {
                Task<HrmasterdataOuList> hrSyncTask = HRsyncAdapter.GetOrganizationalUnit(CancellationToken.None, oucode);
                hrSyncTask.Wait();

                HrmasterdataOuList oudata = null;

                if (hrSyncTask.IsCompletedSuccessfully)
                {
                    oudata = hrSyncTask.Result;

                    if (oudata != null)
                    {
                        result = Ok(oudata);
                    }
                    else
                    {
                        result = StatusCode(404);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get information for the Organizational Unit code {0}", oucode));
                result = StatusCode(500);
            }
            return result;
        }
    /*
        [HttpGet("testmail/q")]
        public bool testmail(){
            bool sended;
            try{
               
               List<string> content = new List<string>();
               content.Add("this is a test mail"); 
               content.Add ("and this is a new line");
               sended = MailSender.SendMail(content);
            }catch (Exception ex){
                sended = false;
                Logger.LogError(ex, string.Format("Unable to send the email", DateTime.Now));
            }
            return sended;
        }
    */
    }
}