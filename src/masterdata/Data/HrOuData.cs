using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using masterdata.Interfaces;
using masterdata.Models;
using masterdata.Models.Configuration;

namespace masterdata.Data
{
    public class HrOuData : IHrOuData
    {

        private readonly IOptions<DatabaseConfiguration> DataSource;
        private readonly ILogger Logger;

        public HrOuData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<HrOuData>();;
        }

        public List<HrOu> FindAll()
        {
            List<HrOu> results = new List<HrOu>();
            try
            {
                using (var context = new MasterDataContext(DataSource))
                {

                    results = context.HrOu.ToList<HrOu>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to download UOs.", DateTime.Now));
            }

            return results;

        }

    }
}
