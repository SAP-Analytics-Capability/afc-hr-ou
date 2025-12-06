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

namespace masterdata.Data
{
    public class ResultData : IResultData
    {

        private readonly IOptions<DatabaseConfiguration> DataSource;
        private readonly ILogger Logger;

        public ResultData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<ResultData>();
        }

        public void AddNewResult(Result r)
        {
            using (MasterDataContext context = new MasterDataContext(DataSource))
            {
                context.Add(r);
                context.SaveChanges();
            }
        }

        public List<Result> GetResultsByOUCodAndCCCod(string OrganizationalUnitCode, string CostCenterCode)
        {
            List<Result> result = new List<Result>(); ;

            try
            {
                using (var context = new MasterDataContext(DataSource))
                {
                    result = (from r in context.Result
                              where (string.Equals(r.OrganizationalUnitCode, OrganizationalUnitCode,
                                                   StringComparison.OrdinalIgnoreCase) &&
                                     string.Equals(r.CostCenterCode, CostCenterCode,
                                                   StringComparison.OrdinalIgnoreCase) &&
                                     r.SyncDateTime != null)
                              orderby r.SyncDateTime descending
                              select r).ToList<Result>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in method GetResultsByOUCodAndCCCod.");
            }
            return result;
        }

        public List<Result> GetResultsByAssociationTime(string associationTime)
        {
            List<Result> results = null;
            DateTime date = Convert.ToDateTime(associationTime);
            try
            {
                using (var context = new MasterDataContext(DataSource))
                {
                    results = (from r in context.Result
                               where (r.AssociationTime >= date)
                               select r).ToList<Result>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in method GetResultsByAssociationTime.");
            }

            return results;
        }

        public List<Result> GetResultsByOUC(string oucode)
        {
            List<Result> results = null;

            try
            {
                using (var context = new MasterDataContext(DataSource))
                {
                    results = (from r in context.Result
                               where string.Equals(r.OrganizationalUnitCode, oucode,
                                                   StringComparison.OrdinalIgnoreCase)
                               select r).ToList<Result>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in method GetResultByOUC.");
            }

            return results;
        }

        public int AddNewResultAssociationOU(AssociationOrganizationUnit aou)
        {
            int id = 0;
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    context.Add(aou);
                    context.SaveChanges();

                    id = aou.OrganizationUnitId;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to add new organization unit association");
            }
            return id;
        }

        public void AddNewResultAssociationCC(AssociationCostCenter acc)
        {
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    context.Add(acc);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to add new cost center association");
            }
        }

        public List<AssociationOrganizationUnit> getLastsDistinctUO(DateTime lastAccess)
        {
            List<AssociationOrganizationUnit> results = null;
            try
            {
                DateTime la = new DateTime(lastAccess.Year, lastAccess.Month, lastAccess.Day, 0, 0, 0);

                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    results = (from r in context.AssociationOrganizationUnit
                               where (DateTime.Compare(r.SyncDateTime, la) >= 0)
                               select r).ToList<AssociationOrganizationUnit>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get associationUO list");
            }
            return results;
        }

        public List<AssociationOrganizationUnit> getLastsDistinctPagedUO(DateTime lastAccess, int offSet, int limit)
        {
            List<AssociationOrganizationUnit> results = null;

            try
            {
                DateTime la = new DateTime(lastAccess.Year, lastAccess.Month, lastAccess.Day, 0, 0, 0);

                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    results = (from r in context.AssociationOrganizationUnit
                               where (DateTime.Compare(r.SyncDateTime, la) >= 0)
                               select r).Skip(offSet).Take(limit).ToList<AssociationOrganizationUnit>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get associationUO list");
            }
            return results;
        }

        public List<AssociationCostCenter> getAssociationCCByUOId(int OrganizationUnitId)
        {
            List<AssociationCostCenter> results = null;

            try
            {
                using (var context = new MasterDataContext(DataSource))
                {
                    results = (from r in context.AssociationCostCenter
                               where r.OrganizationUnitId == OrganizationUnitId
                               select r).ToList<AssociationCostCenter>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in method GetCCByOUId.");
            }

            return results;
        }

        public List<Result> GetSnowResult(DateTime lastAccess)
        {
            List<Result> results = new List<Result>();
            try
            {
                DateTime la = new DateTime(lastAccess.Year, lastAccess.Month, lastAccess.Day, 0, 0, 0);

                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    results = (from r in context.Result
                               where (r.SyncDateTime != null &&
                                        DateTime.Compare(Convert.ToDateTime(r.SyncDateTime.ToString("yyyy-MM-dd")), Convert.ToDateTime(la.ToString("yyyy-MM-dd"))) >= 0)
                               select r).ToList<Result>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to get result from Service Now...");
            }
            return results;
        }

        public void UpdateResult(Result r)
        {
            Logger.LogDebug(string.Format("{0} - Updating Result syncDateTime", DateTime.Now));

            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    context.Update(r);
                    context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to apdate syncDAteTime of Result", DateTime.Now));
            }
        }
    }
}