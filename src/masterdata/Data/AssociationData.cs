using System;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using masterdata.Interfaces;
using masterdata.Models.Configuration;
using masterdata.Models;

namespace masterdata.Data
{
    public class AssociationData : IAssociationData
    {

        private readonly IOptions<DatabaseConfiguration> DataSource;
        private readonly ILogger Logger;

        public AssociationData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<AssociationData>();
        }

        public TotalCount GetTotalCountByTypeDate(string associationTime, string type)
        {
            DateTime date = Convert.ToDateTime(associationTime);
            TotalCount count = new TotalCount();
            try
            {
                if (type.ToUpper().Equals("CC"))
                {
                    if (date.Hour != 00 || date.Minute != 00 || date.Second != 00)
                    {
                        using (var context = new MasterDataContext(DataSource))
                        {
                            count.totalCount = (from r in context.BwCc
                                                where new DateTime(r.SyncDateTime.Year, r.SyncDateTime.Month, r.SyncDateTime.Day, r.SyncDateTime.Hour, r.SyncDateTime.Minute, r.SyncDateTime.Second) > date

                                                select r).Count();
                        }
                    }
                    else
                    {
                        using (var context = new MasterDataContext(DataSource))
                        {
                            DateTime maxDtuse = (DateTime)(from r in context.HrOu
                                                           where (r.syncDateTime >= date)
                                                           select r.syncDateTime).Max();

                            count.totalCount = (from r in context.BwCc
                                                where (r.SyncDateTime == maxDtuse)
                                                select r).Count();
                        }

                    }

                }

                else
                {
                    
                    if (date.Hour != 00 || date.Minute != 00 || date.Second != 00)
                    {
                        using (var context = new MasterDataContext(DataSource))
                        {
                            count.totalCount = (from r in context.HrOu
                                                where new DateTime(r.syncDateTime.Value.Year, r.syncDateTime.Value.Month, r.syncDateTime.Value.Day, r.syncDateTime.Value.Hour, r.syncDateTime.Value.Minute, r.syncDateTime.Value.Second) > date
                                                select r).Count();
                        }
                    }
                    else
                    {
                        using (var context = new MasterDataContext(DataSource))
                        {
                            DateTime maxDtuse = (DateTime)(from r in context.HrOu
                                                  where (r.syncDateTime >= date)
                                                  select r.syncDateTime).Max();

                            count.totalCount = (from r in context.HrOu
                                                where r.syncDateTime == maxDtuse
/*                                                where (r.syncDateTime >= date)  old version*/
                                                select r).Count();
                        }

                    }
                }

            }

            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in method GetTotalCountByTypeDate.");
            }
            return count;
        }

        // calcolo offset nella bwcc
        public TotalCountOffsetType GetOffsetByTypeDate(string associationTime, string type)
        {
            DateTime date = Convert.ToDateTime(associationTime);
            TotalCountOffsetType offset = new TotalCountOffsetType();
            try
            {
                if (type.ToUpper().Equals("CC"))
                {
                    if (date.Hour != 00 || date.Minute != 00 || date.Second != 00)
                    {
                        using (var context = new MasterDataContext(DataSource))
                        {
                            offset.offSetMax = (from r in context.BwCc
                                                where new DateTime(r.SyncDateTime.Year, r.SyncDateTime.Month, r.SyncDateTime.Day, r.SyncDateTime.Hour, r.SyncDateTime.Minute, r.SyncDateTime.Second) > date
                                                // orderby r.associationCostCenterId descending
                                                select r.associationCostCenterId).DefaultIfEmpty(0).Max();
                            offset.offSetMin = (from r in context.BwCc
                                                  where new DateTime(r.SyncDateTime.Year, r.SyncDateTime.Month, r.SyncDateTime.Day, r.SyncDateTime.Hour, r.SyncDateTime.Minute, r.SyncDateTime.Second) > date
                                                  select r.associationCostCenterId).DefaultIfEmpty(0).Min();
                        }
                    }
                    else
                    {
                        using (var context = new MasterDataContext(DataSource))
                        {
                            DateTime maxDtuse = (DateTime)(from r in context.HrOu
                                                           where (r.syncDateTime >= date)
                                                           select r.syncDateTime).Max();

                            offset.offSetMax = (from r in context.BwCc
                                                where (r.SyncDateTime == maxDtuse)
                                                select r.associationCostCenterId).DefaultIfEmpty(0).Max();
                            offset.offSetMin = (from r in context.BwCc
                                                  where (r.SyncDateTime == maxDtuse)
                                                  select r.associationCostCenterId).DefaultIfEmpty(0).Min();
                        }
                    }
                }
                else // OU
                {
                    if (date.Hour != 00 || date.Minute != 00 || date.Second != 00)
                    {
                        using (var context = new MasterDataContext(DataSource))
                        {
                            offset.offSetMax = (from r in context.HrOu
                                          where new DateTime(r.syncDateTime.Value.Year, r.syncDateTime.Value.Month, r.syncDateTime.Value.Day, r.syncDateTime.Value.Hour, r.syncDateTime.Value.Minute, r.syncDateTime.Value.Second) > date
                                          select r.hrmdou_id).DefaultIfEmpty(0).Max();
                            offset.offSetMin = (from r in context.HrOu
                                                where new DateTime(r.syncDateTime.Value.Year, r.syncDateTime.Value.Month, r.syncDateTime.Value.Day, r.syncDateTime.Value.Hour, r.syncDateTime.Value.Minute, r.syncDateTime.Value.Second) > date
                                                  select r.hrmdou_id).DefaultIfEmpty(0).Min();
                        }
                    }
                    else
                    {
                        using (var context = new MasterDataContext(DataSource))
                        {

                            DateTime maxDtuse = (DateTime)(from r in context.HrOu
                                                           where (r.syncDateTime >= date)
                                                           select r.syncDateTime).Max();

                            offset.offSetMax = (from r in context.HrOu
                                          where (r.syncDateTime == maxDtuse)
                                          select r.hrmdou_id).DefaultIfEmpty(0).Max();
                            offset.offSetMin = (from r in context.HrOu
                                                  where (r.syncDateTime == maxDtuse)
                                                  select r.hrmdou_id).DefaultIfEmpty(0).Min();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in method GetOffSetByTypeDate.");
            }
            return offset;
        }


        public UoLimited GetLimitedUo(string associationTime, string offset, string limit)
        {
            UoLimited results = new UoLimited();
            DateTime date = Convert.ToDateTime(associationTime);

            try
            {
                int inputLimit = Convert.ToInt32(limit != null ? limit : "0");
                int inputOffset = Convert.ToInt32(offset != null ? offset : "0");
                if (date.Hour != 00 || date.Minute != 00 || date.Second != 00)
                {
                    using (var context = new MasterDataContext(DataSource))
                    {
                        results.organization_units = (from r in context.HrOu
                                                      where new DateTime(r.syncDateTime.Value.Year, r.syncDateTime.Value.Month, r.syncDateTime.Value.Day, r.syncDateTime.Value.Hour, r.syncDateTime.Value.Minute, r.syncDateTime.Value.Second) > date && r.hrmdou_id >= inputOffset
                                                      orderby r.hrmdou_id ascending
                                                      select r).Take(inputLimit > 0 ? inputLimit : int.MaxValue)
                                                      .ToList<HrOu>();
                    }
                }
                else
                {
                    using (var context = new MasterDataContext(DataSource))
                    {
                        results.organization_units = (from r in context.HrOu
                                                      where (r.syncDateTime >= date && r.hrmdou_id >= inputOffset)
                                                      orderby r.hrmdou_id ascending
                                                      select r).Take(inputLimit > 0 ? inputLimit : int.MaxValue)
                                                      .ToList<HrOu>();
                    }
                    //     using (var context = new MasterDataContext(DataSource))
                    //     {
                    //         results.organization_units = (from r in context.HrOu
                    //                                       where (r.syncDateTime >= date && r.hrmdou_id >= inputOffset)
                    //                                       select r).Take(inputLimit).ToList<HrOu>();
                    //     }
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in method GetLimitedUo.");
            }
            return results;
        }

        public CcLimited GetLimitedCc(string associationTime, string offset, string limit)
        {
            CcLimited results = new CcLimited();
            DateTime date = Convert.ToDateTime(associationTime);

            try
            {
                int inputLimit = Convert.ToInt32(limit != null ? limit : "0");
                int inputOffset = Convert.ToInt32(offset != null ? offset : "0");
                if (date.Hour != 00 || date.Minute != 00 || date.Second != 00)
                {
                    using (var context = new MasterDataContext(DataSource))
                    {
                        results.cost_centers = (from r in context.BwCc
                                                where new DateTime(r.SyncDateTime.Year, r.SyncDateTime.Month, r.SyncDateTime.Day, r.SyncDateTime.Hour, r.SyncDateTime.Minute, r.SyncDateTime.Second) > date && r.associationCostCenterId >= inputOffset
                                                orderby r.associationCostCenterId ascending
                                                select r).Take(inputLimit > 0 ? inputLimit : int.MaxValue).ToList<BwCC>();
                    }
                }
                else
                {
                    using (var context = new MasterDataContext(DataSource))
                    {
                        results.cost_centers = (from r in context.BwCc
                                                where (r.SyncDateTime >= date && r.associationCostCenterId >= inputOffset)
                                                orderby r.associationCostCenterId ascending
                                                select r).Take(inputLimit > 0 ? inputLimit : int.MaxValue).ToList<BwCC>();
                        //     using (var context = new MasterDataContext(DataSource))
                        //     {
                        //         results.cost_centers = (from r in context.BwCc
                        //                                 where (r.SyncDateTime >= date && r.associationCostCenterId >= inputOffset)
                        //                                 select r).Take(inputLimit).ToList<BwCC>();
                        //     }
                    }
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in method GetLimitedCC.");
            }
            return results;
        }

        public int AddNewResultAssociationOU(HrOu aou)
        {
            int id = 0;
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    context.Add(aou);
                    context.SaveChanges();

                    id = aou.hrmdou_id;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to add new organization unit association");
            }
            return id;
        }

        public void AddNewResultAssociationCC(BwCC acc)
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

        public void AddNewResultAssociationCleanCC(CleanBwCC acc)
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

        public int UpdateAssociationCleanOU(CleanHrOU aou)
        {
            int id = 0;
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    context.Update(aou);
                    context.SaveChanges();

                    id = aou.hrmdou_id;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to add new organization unit association");
            }
            return id;
        }
    }
}