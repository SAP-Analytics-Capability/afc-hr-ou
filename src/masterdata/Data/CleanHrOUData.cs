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
    public class CleanHrOUData : ICleanHrOU
    {
        private readonly IOptions<DatabaseConfiguration> DataSource;
        private readonly ILogger Logger;

        public CleanHrOUData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<CleanHrOUData>();
        }

        public void AddNewExcelData(List<CleanHrOU> masterExcel)
        {
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    foreach (CleanHrOU item in masterExcel)
                    {
                        context.CleanHrOU.Add(item);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to add excel data to database");
            }
        }

        public void AddNewTxTData(List<string> TxT, int faId)
        {
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    foreach (string item in TxT)
                    {
                        CleanHrOU data = new CleanHrOU();
                        data.UOrg = item;
                        data.faId = faId;
                        context.CleanHrOU.Add(data);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to add excel data to database");
            }
        }

        public List<CleanHrOU> RetrieveDataByFa(int faId)
        {
            List<CleanHrOU> result = new List<CleanHrOU>();
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    result = (from c in context.CleanHrOU
                              where c.faId == faId
                              select c).ToList<CleanHrOU>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Couldn't retrieve data by id: {ex.Message}");
            }
            return result;
        }
    }
}