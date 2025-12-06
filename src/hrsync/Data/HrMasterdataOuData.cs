using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using hrsync.Interface;
using hrsync.Models;
using hrsync.Models.Configuration;

namespace hrsync.Data
{
    public class HrMasterdataOuData : IHrMasterdataOuData
    {
        private readonly IOptions<DatabaseConfiguration> DataSource;

        private readonly ILogger Logger;

        public HrMasterdataOuData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<HrmasterdataOu>();
        }

        public bool AddNewOrganizationalUnit(HrmasterdataOu ou)
        {
            try
            {
                using (HrSyncContext context = new HrSyncContext(DataSource))
                {
                    context.Add(ou);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unabel to add new OU", DateTime.Now));
                return false;
            }
            return true;
        }

        public void AddNewOrganizationalUnit(HrmasterdataOu ou, HrmasterdataOu oldou)
        {
            using (HrSyncContext context = new HrSyncContext(DataSource))
            {
                try
                {
                    context.Remove(oldou);
                    context.Add(ou);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Unable to add new sync OU");
                }
            }
        }

        public bool RemoveOrganizationalUnit(HrmasterdataOu ou)
        {
            try
            {
                using (HrSyncContext context = new HrSyncContext(DataSource))
                {
                    context.Remove(ou);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unabel to add remove  OU", DateTime.Now));
                return false;
            }
            return true;
        }

        public HrmasterdataOu GetOrganizationalUnit(string oucode)
        {
            HrmasterdataOu organizationalunit;

            using (HrSyncContext context = new HrSyncContext(DataSource))
            {
                organizationalunit = context.HrmasterdataOu
                                        .Where(ou => ou.UOrg == oucode)
                                        .Include(ou => ou.UOrg)
                                        .Include(ou => ou.UOrgDesc)
                                        .Include(ou => ou.StartDate)
                                        .Include(ou => ou.EndDate)
                                        .Include(ou => ou.Company)
                                        .Include(ou => ou.CompanyDesc)
                                        .Include(ou => ou.CostCenter)
                                        .Include(ou => ou.PredAttr)
                                        .Include(ou => ou.PredAttrDesc)
                                        .Include(ou => ou.PerimeterDesc)
                                        .Include(ou => ou.BusLineCod1) // to be continued
                                        .Include(ou => ou.BusLineDesc1)
                                        .Include(ou => ou.BusLineCod2)
                                        .Include(ou => ou.BusLineDesc2)
                                        .Include(ou => ou.BusLineCod3)
                                        .Include(ou => ou.BusLineDesc3)
                                        .Include(ou => ou.BusLineCod4)
                                        .Include(ou => ou.BusLineDesc4)
                                        .Include(ou => ou.BusLineCod5)
                                        .Include(ou => ou.BusLineDesc5)
                                        .Include(ou => ou.ServCod1)
                                        .Include(ou => ou.ServDesc1)
                                        .Include(ou => ou.ServCod2)
                                        .Include(ou => ou.ServDesc2)
                                        .Include(ou => ou.ServCod3)
                                        .Include(ou => ou.ServDesc3)
                                        .Include(ou => ou.ServCod4)
                                        .Include(ou => ou.ServDesc4)
                                        .Include(ou => ou.ServCod5)
                                        .Include(ou => ou.ServDesc5)
                                        .Include(ou => ou.StaffCod1)
                                        .Include(ou => ou.StaffDesc1)
                                        .Include(ou => ou.StaffCod2)
                                        .Include(ou => ou.StaffDesc2)
                                        .Include(ou => ou.StaffCod3)
                                        .Include(ou => ou.StaffDesc3)
                                        .Include(ou => ou.StaffCod4)
                                        .Include(ou => ou.StaffDesc4)
                                        .Include(ou => ou.StaffCod5)
                                        .Include(ou => ou.StaffDesc5)
                                        .Include(ou => ou.PredAttrLoc)
                                        .Include(ou => ou.PredAttrDescLoc)
                                        .Include(ou => ou.AttrPrevCodLoc)
                                        .Include(ou => ou.AttrPrevDescLoc)
                                        .Include(ou => ou.PerimeterLoc)
                                        .Include(ou => ou.PerimeterDescLoc)
                                        .Include(ou => ou.BusLineCod1Loc)
                                        .Include(ou => ou.BusLineDesc1Loc)
                                        .Include(ou => ou.BusLineCod2Loc)
                                        .Include(ou => ou.BusLineDesc2Loc)
                                        .Include(ou => ou.BusLineCod3Loc)
                                        .Include(ou => ou.BusLineDesc3Loc)
                                        .Include(ou => ou.BusLineCod4Loc)
                                        .Include(ou => ou.BusLineDesc4Loc)
                                        .Include(ou => ou.BusLineCod5Loc)
                                        .Include(ou => ou.BusLineDesc5Loc)
                                        .Include(ou => ou.ServCod1Loc)
                                        .Include(ou => ou.ServDesc1Loc)
                                        .Include(ou => ou.ServCod2Loc)
                                        .Include(ou => ou.ServDesc2Loc)
                                        .Include(ou => ou.ServCod3Loc)
                                        .Include(ou => ou.ServDesc3Loc)
                                        .Include(ou => ou.ServCod4Loc)
                                        .Include(ou => ou.ServDesc4Loc)
                                        .Include(ou => ou.ServCod5Loc)
                                        .Include(ou => ou.ServDesc5Loc)
                                        .Include(ou => ou.StaffCod1Loc)
                                        .Include(ou => ou.StaffDesc1Loc)
                                        .Include(ou => ou.StaffCod2Loc)
                                        .Include(ou => ou.StaffDesc2Loc)
                                        .Include(ou => ou.StaffCod3Loc)
                                        .Include(ou => ou.StaffDesc3Loc)
                                        .Include(ou => ou.StaffCod4Loc)
                                        .Include(ou => ou.StaffDesc4Loc)
                                        .Include(ou => ou.StaffCod5Loc)
                                        .Include(ou => ou.StaffDesc5Loc)
                                        .Include(ou => ou.uOrgCod)
                                        .SingleOrDefault<HrmasterdataOu>();
            }
            return organizationalunit;
        }

        public List<HrmasterdataOu> FindAllHrMasterDataOus()
        {
            List<HrmasterdataOu> results = null;
            try
            {
                using (var context = new HrSyncContext(DataSource))
                {
                    results = (from r in context.HrmasterdataOu
                               select r).ToList<HrmasterdataOu>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method FindAllHrMasterDataOus from db: " + e.Message);
            }

            return results;
        }

        public List<HrmasterdataOu> GetAllByDate(DateTime date)
        {
            List<HrmasterdataOu> results = null;
            try
            {
                using (var context = new HrSyncContext(DataSource))
                {
                    results = (from r in context.HrmasterdataOu
                               where DateTime.Compare(r.SyncDateTime, date) >= 0
                               select r).ToList<HrmasterdataOu>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get OU by date.", DateTime.Now));
            }

            return results;
        }

        public void DeleteByInternalID(int id)
        {
            try
            {
                using (var context = new HrSyncContext(DataSource))
                {
                    var oldou = from c in context.HrmasterdataOu
                                where c.hrmdou_id == id
                                select c;

                    context.HrmasterdataOu.RemoveRange(oldou);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to delete old OU.", DateTime.Now));
            }

        }

        public void DeleteAllHrMasterDataOus()
        {
            try
            {
                using (var context = new HrSyncContext(DataSource))
                {
                    var all = from c in context.HrmasterdataOu select c;
                    context.HrmasterdataOu.RemoveRange(all);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method DeleteAllHrMasterDataOus : " + e.Message);
            }

        }

        public bool AddIfOrganizationalUnitsByCode(List<HrmasterdataOu> oulist, DateTime initservicetime)
        {
            bool saved = false;
            try
            {

                using (var context = new HrSyncContext(DataSource))
                {
                    foreach (HrmasterdataOu ou in oulist)
                    {
                        HrmasterdataOu oldou = context.HrmasterdataOu
                        .Where(data => data.UOrg == ou.UOrg)
                        .OrderByDescending(data => data.hrmdou_id).FirstOrDefault();
                        if (oldou != null)
                        {
                            if (!ou.Equals(oldou))
                            {
                                ou.SyncDateTime = initservicetime;
                                context.HrmasterdataOu.Remove(oldou);
                                context.HrmasterdataOu.Add(ou);
                                if (!saved)
                                {
                                    saved = true;
                                }

                            }
                            else
                            {    //racquafo modify sync date where ou not change 08-01-2020
                                oldou.SyncDateTime = initservicetime;
                            }
                        }
                        else
                        {
                            ou.SyncDateTime = initservicetime;
                            context.HrmasterdataOu.Add(ou);
                            saved = true;
                        }
                    }
                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Logger.LogError("Error in method HrMasterdataOuData.AddIfOrganizationalUnitsByCode : " + e.Message);
            }
            return saved;

        }

        public List<HrmasterdataOu> GetByDate(DateTime date)
        {
            List<HrmasterdataOu> results = null;
            try
            {
                using (var context = new HrSyncContext(DataSource))
                {
                    results = (from r in context.HrmasterdataOu
                                   //where DateTime.Compare(r.SyncDateTime, date) == 0
                               where (r.SyncDateTime == date)
                               select r).ToList<HrmasterdataOu>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get OU by date.", DateTime.Now));
            }

            return results;
        }

    }
}