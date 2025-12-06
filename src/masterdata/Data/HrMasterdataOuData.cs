using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using masterdata.Interfaces;
using masterdata.Models;
using masterdata.Models.Configuration;

namespace masterdata.Data
{
    public class HrMasterdataOuData : IHrMasterdataOuData
    {
        private readonly IOptions<DatabaseConfiguration> DataSource;
        private readonly ILogger Logger;

        public HrMasterdataOuData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<HrMasterdataOuData>();
        }

        public List<HrmasterdataOu> FindDistinct(DateTime lastaccess)
        {
            List<HrmasterdataOu> results = new List<HrmasterdataOu>();
            try
            {
                using (var context = new MasterDataContext(DataSource))
                {
                    results = (from p in context.HrmasterdataOu
                               where (p.SyncDateTime != null &&
                                     DateTime.Compare(p.SyncDateTime, lastaccess) >= 0)
                               select p).ToList<HrmasterdataOu>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to download latest UO.", DateTime.Now));
            }
            return results;
        }

        public List<HrmasterdataOu> FindAll()
        {
            List<HrmasterdataOu> results = new List<HrmasterdataOu>();
            try
            {
                using (var context = new MasterDataContext(DataSource))
                {
                    results = context.HrmasterdataOu.ToList<HrmasterdataOu>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to download latest UO.", DateTime.Now));
            }
            return results;
        }

        public List<HrmasterdataOu> FindDistinct(string oucode, DateTime lastaccess)
        {
            List<HrmasterdataOu> results = new List<HrmasterdataOu>();
            try
            {
                using (var context = new MasterDataContext(DataSource))
                {
                    results = (from p in context.HrmasterdataOu
                               where (string.Equals(p.UOrg, oucode, StringComparison.InvariantCultureIgnoreCase) &&
                                        p.SyncDateTime != null &&
                                        DateTime.Compare(p.SyncDateTime, lastaccess) >= 0)
                               select p).ToList<HrmasterdataOu>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to download latest UO.", DateTime.Now));
            }

            return results;

        }

    }
}