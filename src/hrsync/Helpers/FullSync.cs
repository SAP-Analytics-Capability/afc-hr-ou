using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using hrsync.Models;
using hrsync.Interface;

namespace hrsync.Helpers
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
                    // Leggo nel campo SYNC_POST_SERVICE la fase successiva da attivare (elaborationfullsync).
                    // sync_post_service = "elaborationfullsync";  // TO DO
                    if (sync_post_service == "elaborationmasterdatafullsync")
                    {
                        // run /masterdata/v1/elaborationfullsync (queste rest controllano l'eseguibilità in giornata)
                        Task<string> hrsyncTask = HrSyncAdapter.reqMasterdataSyncFullSync(CancellationToken.None, sync_post_service);
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
                Logger.LogError(string.Format("{0} - Unable req masterdata full sync.", DateTime.Now.ToString()), e);
                return 1;
            }

        }
    }
}
