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
    public class FullSync : IFullSync
    {
        private readonly ILogger Logger;
        private readonly IHrSyncAdapter HrSyncAdapter;

        public FullSync(ILoggerFactory loggerFactory,
            IHrSyncAdapter hrSyncAdapter)
        {
            this.Logger = loggerFactory.CreateLogger<FullSync>();
            this.HrSyncAdapter = hrSyncAdapter;
        }

        public int fullSync(string flag_sync, string sync_post_service)
        {
            try
            {
                if (flag_sync == "Y")
                {
                    if (sync_post_service == SchedulerCommons.HrsyncFullScheduler || sync_post_service == SchedulerCommons.HrsyncCustomScheduler)
                    {
                        // run 5001/hrsync/v1/extractioncustomsync oppure 5001/hrsync/v1/extractionfullsync  (queste rest controllano l'eseguibilità in giornata)
                        // var hrSyncAdapter = new SchedulerCommons();
                        Task<string> hrsyncTask = HrSyncAdapter.reqHrSyncFullSync(CancellationToken.None, sync_post_service);
                        //hrsyncTask.Wait();
                        //if (hrsyncTask.IsCompleted)
                        //{
                        //    if (hrsyncTask.Result != null)
                        //    {
                        //        return 0;
                        //    }
                        //    else
                        //        return 0;
                        //}
                        //else
                        //{
                        //    return 1;
                        //}
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                Logger.LogError(string.Format("{0} - Unable req hrsyncfull or hrsyncustom.", DateTime.Now.ToString()), e);
                return 1;
            }

        }
    }
}