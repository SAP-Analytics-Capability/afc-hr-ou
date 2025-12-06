using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Models;

namespace masterdata.Interfaces.Adapters
{
    public interface IHrSyncAdapter
    {
        Task<HrmasterdataOuList> GetOrganizationalUnits(CancellationToken cancellationToken,
                                                        string validityDate,
                                                        string changedateAttribute,
                                                        string companyCode,
                                                        string percCon,
                                                        string costcenterDummy,
                                                        string gestionali,
                                                        string limit,
                                                        string offset,
                                                        string noCostcenter,
                                                        string changedDate);
        Task<HrmasterdataOuList> GetOrganizationalUnitsLimit(CancellationToken cancellationToken,
                                                            int limit);

        Task<HrmasterdataOuList> GetOrganizationalUnit(CancellationToken cancellationToken,
                                                        string oucode);
    }
}