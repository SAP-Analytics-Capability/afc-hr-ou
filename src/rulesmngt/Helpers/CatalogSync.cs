using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using rulesmngt.Models;
using rulesmngt.Models.SnowConsolidationResult;
using rulesmngt.Models.SnowTranscodingResult;
using rulesmngt.Interfaces;
using rulesmngt.Interfaces.Adapters;

namespace rulesmngt.Helpers
{
    public class CatalogSync : ICatalogSync
    {
        private readonly ILogger Logger;
        private ISnowAdatpter SnowAdapter;
        private ICatalogUtils CatalogUtils;

        public CatalogSync(ILoggerFactory loggerFactory, ISnowAdatpter snowAdatpter, ICatalogUtils catalogUtils)
        {
            this.Logger = loggerFactory.CreateLogger<CatalogSync>();
            this.SnowAdapter = snowAdatpter;
            this.CatalogUtils = catalogUtils;
        }

        public void Sync()
        {
            try
            {
                Task<RootObjectTranscoding> transcodingTask = SnowAdapter.getTranscodingActivity(CancellationToken.None);
                transcodingTask.Wait();
                if (transcodingTask.IsCompletedSuccessfully && transcodingTask.Result != null)
                {
                    List<ActivityAssociation> newCatalog = CatalogUtils.ConvertToAfcActivityCatalog(transcodingTask.Result);
                    if (CatalogUtils.ElaborationDBCleaningTranscoding() && newCatalog.Count > 0)
                    {
                        CatalogUtils.AddTranscoding(newCatalog);
                    }

                    if (CatalogUtils.isEmptyTranscoding())
                    {
                        CatalogUtils.RollbackTranscoding();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(string.Format("{0} - Unable to read/write the transcoding catalog.", DateTime.Now.ToString()), e);
            }

            try
            {
                Task<RootObjectConsolidation> consolidationTask = SnowAdapter.getConsolidationCatalog(CancellationToken.None);
                consolidationTask.Wait();
                if (consolidationTask.IsCompletedSuccessfully && consolidationTask.Result != null)
                {
                    List<CompanyScope> newCatalog = CatalogUtils.ConvertToAfcConsolidateCatalog(consolidationTask.Result);

                    if (CatalogUtils.ElaborationDBCleaningConsolidating() && newCatalog.Count > 0)
                    {
                        CatalogUtils.AddConsolidated(newCatalog);
                    }

                    if (CatalogUtils.isEmptyConsolidate())
                    {
                        CatalogUtils.RollbackConsolidated();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(string.Format("{0} - Unable to read/write the company catalog.", DateTime.Now.ToString()), e);
            }
        }
    }
}