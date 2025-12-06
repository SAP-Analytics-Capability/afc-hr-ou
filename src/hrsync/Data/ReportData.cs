using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using hrsync.Interface;
using hrsync.Models;
using hrsync.Models.Configuration;

namespace hrsync.Data
{
    public class ReportData : IReportData
    {

        private readonly IOptions<DatabaseConfiguration> DataSource;
        private readonly ILogger Logger;

        public ReportData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<HrmasterdataOu>();
        }
        public void AddNewReport(Report r)
        {
            try
            {
                using (HrSyncContext context = new HrSyncContext(DataSource))
                {
                    context.Add(r);
                    context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to add new report.", DateTime.Now));
            }
        }

        public Report GetLastReport()
        {
            Report r = null;
            try
            {
                using (HrSyncContext context = new HrSyncContext(DataSource))
                {
                    r = context.Report.OrderByDescending(u => u.date_time).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get last report.", DateTime.Now));
            }
            return r;
        }

        public Report GetLastUtilReport()
        {
            Report r = null;
            try
            {
                using (HrSyncContext context = new HrSyncContext(DataSource))
                {
                    r = context.Report.Where(data => data.saved == "Y")
                    .OrderByDescending(data => data.date_time).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get last report.", DateTime.Now));
            }
            return r;
        }
    }
}
