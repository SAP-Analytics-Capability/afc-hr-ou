using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using masterdata.Interfaces;
using masterdata.Models;
using masterdata.Models.Configuration;
using masterdata.Models.HrSyncResult;
using Microsoft.EntityFrameworkCore;

namespace masterdata.Helpers
{
    public class DBCleaning : IDBCleaning
    {
        private ILogger Logger;
        private readonly IOptions<DatabaseConfiguration> DataSource;
        public DBCleaning(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerFactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerFactory.CreateLogger<OuCCAssociation>();
        }

        public bool ElaborationDBCleaning()
        {
            Logger.LogInformation("Elaboration DB Cleaning started: " + DateTime.Now);
            bool isClean = false;
            int num = 0;

            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    num = context.Database.ExecuteSqlCommand("select afchr_int_schema.cleaning_function()");
                }

                if (num != 0)
                    isClean = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during DB Cleaning execution", DateTime.Now));
                return false;
            }

            // try
            // {
            //     using (MasterDataContext context = new MasterDataContext(DataSource))
            //     {
            //         bool canSave = true;
            //         if (canSave)
            //         {
            //             //Clean Bwccbck
            //             canSave = CleanBwccBCK(context);
            //         }
            //         if (canSave)
            //         {
            //             //Backup Bwcc
            //             canSave = BackUpBwcc(context);
            //         }
            //         if (canSave)
            //         {
            //             //Clean Hroubck
            //             canSave = CleanHrouBCK(context);
            //         }
            //         if (canSave)
            //         {
            //             //Backup Hrou
            //             canSave = BackUpHrou(context);
            //         }
            //         if (canSave)
            //         {
            //             //Clean Hrmasterdataoubck
            //             canSave = CleanHrmasterdataouBCK(context);
            //         }
            //         if (canSave)
            //         {
            //             //Backup Hrmasterdata
            //             canSave = BackUpHrmasterdataou(context);
            //         }
            //         if (canSave)
            //         {
            //             //Clean Bwcc
            //             canSave = CleanBwcc(context);
            //         }
            //         if (canSave)
            //         {
            //             //Clean Hrou
            //             canSave = CleanHrou(context);
            //         }
            //         if (canSave)
            //         {
            //             context.SaveChanges();
            //             isClean = true;
            //         }
            //         else
            //         {
            //             isClean = false;
            //             Logger.LogError("DB Cleaning is failed transaction Rolled Back");
            //         }
            //     }
            // }
            // catch (Exception ex)
            // {
            //     Logger.LogError(ex, string.Format("ERROR during DB Cleaning execution", DateTime.Now));
            //     return false;
            // }
            Logger.LogInformation("Elaboration DB Cleaning finished:" + DateTime.Now);
            return isClean;
        }

        public bool ElaborationHRCleaning()
        {
            Logger.LogInformation(string.Format("Elaboration DB Cleaning started: {0}", DateTime.Now));

            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    if (CleanHrmasterdataou(context))
                    {
                        context.SaveChanges();
                        Logger.LogInformation(string.Format("Saving cleaning procedure: {0}", DateTime.Now));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during DB Cleaning execution", DateTime.Now));
                return false;
            }
            return true;
        }

        public bool CleanBwccBCK(MasterDataContext context)
        {
            try
            {
                List<BwCCBCK> bwcc = new List<BwCCBCK>();
                bwcc = (from p in context.BwCcBCK select p).ToList<BwCCBCK>();

                if (bwcc != null && bwcc.Count > 0)
                {
                    foreach (BwCCBCK cc in bwcc)
                    {
                        context.Remove(cc);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during bwccBCK delete", DateTime.Now));
                return false;
            }
            return true;
        }

        public bool BackUpBwcc(MasterDataContext context)
        {
            try
            {
                List<BwCC> bwcc = new List<BwCC>();
                bwcc = (from p in context.BwCc select p).ToList<BwCC>();

                if (bwcc != null && bwcc.Count > 0)
                {
                    foreach (BwCC cc in bwcc)
                    {
                        BwCCBCK bck = new BwCCBCK();
                        bck = bck.castBwCCBCK(cc);
                        if (bck.uOrg != null && bck.uOrg != "")
                        {
                            context.BwCcBCK.Add(bck);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during bwcc backup", DateTime.Now));
                return false;
            }
            return true;
        }

        public bool CleanHrouBCK(MasterDataContext context)
        {
            try
            {
                List<HrOuBCK> hrou = new List<HrOuBCK>();
                hrou = (from p in context.HrOuBCK select p).ToList<HrOuBCK>();

                if (hrou != null && hrou.Count > 0)
                {
                    foreach (HrOuBCK bck in hrou)
                    {
                        context.Remove(bck);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during hrouBCK delete", DateTime.Now));
                return false;
            }
            return true;
        }

        public bool BackUpHrou(MasterDataContext context)
        {
            try
            {
                List<HrOu> hrou = new List<HrOu>();
                hrou = (from p in context.HrOu select p).ToList<HrOu>();

                if (hrou != null && hrou.Count > 0)
                {
                    foreach (HrOu ou in hrou)
                    {
                        HrOuBCK bck = new HrOuBCK();
                        bck = bck.castHrOuBCK(ou);
                        if (bck.UOrg != null && bck.UOrg != "")
                        {
                            context.HrOuBCK.Add(bck);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during hrou backup", DateTime.Now));
                return false;
            }
            return true;
        }

        public bool CleanHrmasterdataouBCK(MasterDataContext context)
        {
            try
            {
                List<HrMasterdataOuBCK> hrmasterou = new List<HrMasterdataOuBCK>();
                hrmasterou = (from p in context.HrmasterdataOuBCK select p).ToList<HrMasterdataOuBCK>();

                if (hrmasterou != null && hrmasterou.Count > 0)
                {
                    foreach (HrMasterdataOuBCK bck in hrmasterou)
                    {
                        context.Remove(bck);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during hrmasterdataouBCK delete", DateTime.Now));
                return false;
            }
            return true;
        }

        public bool BackUpHrmasterdataou(MasterDataContext context)
        {
            try
            {
                List<HrmasterdataOu> hrmasterou = new List<HrmasterdataOu>();
                hrmasterou = (from p in context.HrmasterdataOu select p).ToList<HrmasterdataOu>();

                if (hrmasterou != null && hrmasterou.Count > 0)
                {
                    foreach (HrmasterdataOu hrou in hrmasterou)
                    {

                        HrMasterdataOuBCK bck = new HrMasterdataOuBCK();
                        bck = bck.CastHrMasterOu(hrou);
                        if (bck.UOrg != null && bck.UOrg != "")
                        {
                            context.HrmasterdataOuBCK.Add(bck);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during hrmasterdataou backup", DateTime.Now));
                return false;
            }
            return true;
        }

        public bool CleanBwcc(MasterDataContext context)
        {
            try
            {
                List<BwCC> bwcc = new List<BwCC>();
                bwcc = (from p in context.BwCc select p).ToList<BwCC>();

                if (bwcc != null && bwcc.Count > 0)
                {
                    foreach (BwCC cc in bwcc)
                    {
                        context.Remove(cc);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during bwcc delete", DateTime.Now));
                return false;
            }
            return true;
        }

        public bool CleanHrou(MasterDataContext context)
        {
            try
            {
                List<HrOu> hrou = new List<HrOu>();
                hrou = (from p in context.HrOu select p).ToList<HrOu>();

                if (hrou != null && hrou.Count > 0)
                {
                    foreach (HrOu ou in hrou)
                    {
                        context.Remove(ou);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during hrou delete", DateTime.Now));
                return false;
            }
            return true;
        }

        public bool CleanHrmasterdataou(MasterDataContext context)
        {
            try
            {
                List<HrmasterdataOu> hrmasterou = new List<HrmasterdataOu>();
                hrmasterou = (from p in context.HrmasterdataOu select p).ToList<HrmasterdataOu>();

                if (hrmasterou != null && hrmasterou.Count > 0)
                {
                    foreach (HrmasterdataOu hrou in hrmasterou)
                    {
                        context.Remove(hrou);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during hrmasterdaataou delete", DateTime.Now));
                return false;
            }
            return true;
        }
    }
}