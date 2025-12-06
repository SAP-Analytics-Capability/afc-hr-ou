using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using hrsync.Models;

namespace hrsync.Interface
{
    public interface IHrSyncAdapter
    {
        Task<HrmasterdataOuList> GetSapHRData(CancellationToken cancellationToken,
                                                string validityDate,
                                                string changedateAttribute,
                                                string companyCode,
                                                string percCon,
                                                string noCostcenter,
                                                string costcenterDummy,
                                                string gestionali,
                                                string limit,
                                                string offset);

        Task<HrmasterdataOuList> GetOrganizationalUnit(CancellationToken cancellationToken,
                                                    string oucode);

        Task<string> reqMasterdataSyncFullSync(CancellationToken cancellationToken, string sync_post_service);

    }
}