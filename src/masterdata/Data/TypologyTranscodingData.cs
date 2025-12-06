using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using masterdata.Models;
using masterdata.Interfaces;
using masterdata.Models.Configuration;

namespace masterdata.Data
{
    public class TypologyTranscodingData : ITypologyTranscodingData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;
        private readonly ILogger Logger;

        public TypologyTranscodingData(IOptions<DatabaseConfiguration> databaseConfiguration, ILoggerFactory loggerFactory)
        {
            this.databaseConfiguration = databaseConfiguration;
            this.Logger = loggerFactory.CreateLogger<TypologyTranscoding>();
        }
        public List<TypologyTranscoding> GetTypologies()
        {
            List<TypologyTranscoding> typologyTranscodings = null;

            try
            {
                using (MasterDataContext context = new MasterDataContext(databaseConfiguration))
                {
                    typologyTranscodings = (from typologyTranscoding in context.TypologyTranscoding select typologyTranscoding).ToList<TypologyTranscoding>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("Unable to get catalog information: {0}", ex.Message));
            }

            return typologyTranscodings;
        }
    }
}