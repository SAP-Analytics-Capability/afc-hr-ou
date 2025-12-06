using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using masterdata.Interfaces;
using masterdata.Models.SnowCostCenterResults;
using masterdata.Models.Configuration;
using masterdata.Models;
using System.Data;


namespace masterdata.Data
{
    public class SequenceData : ISequenceData
    {
        private readonly IOptions<DatabaseConfiguration> DataSource;
        private readonly ILogger Logger;

        public SequenceData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<SequenceData>();
        }

        public int GetSequence()
        {
            int seq = 0;
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    using (var command = context.Database.GetDbConnection().CreateCommand())
                    {
                        string query = string.Format("select nextval ('{0}.fictcode_seq') as value", DataSource.Value.Schema);
                        command.CommandText = query;
                        context.Database.OpenConnection();
                        using (var result = command.ExecuteReader())
                        {
                            if (result.Read())
                            {
                               seq = Convert.ToInt32(result["value"]);
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }                        
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get fict_sequence_number"));
            }
            return seq;
        }
    }
}