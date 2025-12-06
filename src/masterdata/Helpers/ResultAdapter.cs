using System;
using System.Collections.Generic;
// using System.Linq;
using System.Threading;
// using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Logging;

using masterdata.Interfaces;
//using masterdata.Interfaces.Adapters;
using masterdata.Models;
//using masterdata.Models.rulesmngt;
//using masterdata.Helpers.Adapters;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Helpers
{
    public class ResultAdapter : IResultAdapter
    {
        private IResultData ResultData;
        private IEntityAdapter _EntityData;

        private CancellationTokenSource _Token = new CancellationTokenSource();

        public ResultAdapter(IResultData resultData,
                             IEntityAdapter entityData)
        {
            this.ResultData = resultData;
            this._EntityData = entityData;
        }

        public void InsertResultsByOUCodAndCCCod(RootObject results)
        {
            foreach (Result r in results.result)
            {
                List<Result> resultInDb = ResultData.GetResultsByOUCodAndCCCod(r.OrganizationalUnitCode, r.CostCenterCode);
                if (resultInDb == null)
                {
                    ResultData.AddNewResult(r);
                }
            }
        }

        public void InsertResultsByOUCodAndCCCod(RootObject results, DateTime syncdatetime)
        {
            CancellationToken token = _Token.Token;
            foreach (Result r in results.result)
            {
                if(string.IsNullOrEmpty(r.CompanyCode))
                {
                    r.CompanyCode = _EntityData.GetCompanyCode(token,r.LegalEntity).Result;
                }
                List<Result> resultInDb = ResultData.GetResultsByOUCodAndCCCod(r.OrganizationalUnitCode, r.CostCenterCode);
                if (resultInDb == null || resultInDb.Count == 0)
                {
                    r.SyncDateTime = syncdatetime;
                    ResultData.AddNewResult(r);
                }
                else
                {
                    bool hasnewstate = false;
                    Result dbres = resultInDb[0];
                     if (!dbres.State.Equals(r.State))
                         {
                             hasnewstate = true;
                         }
                         else
                         {
                             hasnewstate = false;
                             r.result_id = dbres.result_id;
                             r.SyncDateTime = syncdatetime;
                             ResultData.UpdateResult(r);
                         }
                    // foreach (Result dbres in resultInDb)
                    // {
                    //     if (!dbres.State.Equals(r.State))
                    //     {
                    //         hasnewstate = true;
                    //     }
                    //     else
                    //     {
                    //         hasnewstate = false;
                    //         dbres.AssociationTime = r.AssociationTime;
                    //         dbres.SyncDateTime= syncdatetime;
                    //         ResultData.UpdateResult(dbres);
                    //         break;
                    //     }
                    // }

                    if (hasnewstate)
                    {
                        r.SyncDateTime = syncdatetime;
                        ResultData.AddNewResult(r);
                    }
                }
            }
        }
    }
}