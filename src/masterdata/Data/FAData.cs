using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using masterdata.Interfaces;
using masterdata.Models;
using masterdata.Models.Configuration;

namespace masterdata.Data
{
    public class FAData : IFAData
    {
        private readonly IOptions<DatabaseConfiguration> DataSource;
        private readonly ILogger Logger;

        public FAData(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerfactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerfactory.CreateLogger<FAData>();
        }

        public FunctionalAck AddNewFA(string caller, DateTime issueTime, string type)
        {
            FunctionalAck result = new FunctionalAck();
            try
            {

                result.caller = caller;
                result.issueTime = issueTime;
                result.expirationDate = issueTime.AddDays(10).Date;
                result.status = FaConstants.uploading;
                result.inputType = type;

                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    context.FunctionalAck.Add(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while addind new FA: {ex.Message}");
            }

            return result;
        }

        public void DeleteFaOnCascade()
        {
            List<FunctionalAck> faData = new List<FunctionalAck>();
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    faData = (from c in context.FunctionalAck
                              select c).ToList<FunctionalAck>();
                    if (faData != null && faData.Count > 0)
                    {
                        foreach (FunctionalAck item in faData)
                        {
                            if (DateTime.Now > item.expirationDate)
                            {
                                context.Remove(item);
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while attempting to delete FA: {ex.Message}");
            }
        }
        public FunctionalAck FaStatus(string faId)
        {
            FunctionalAck fa = new FunctionalAck();
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    fa = (from c in context.FunctionalAck
                          where c.idFa == Convert.ToInt32(faId)
                          select c).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while using CanFaStart: {ex.Message}");
            }
            return fa;
        }

        public void DeleteFA(FunctionalAck fa)
        {
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    context.FunctionalAck.Remove(fa);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while deleting FA: {ex.Message}");
            }
        }

        public void UpdateFaDbStatus(FunctionalAck fa, string type)
        {
            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    switch (type)
                    {
                        case "Pending":
                            fa.status = FaConstants.pending;
                            break;
                        case "Running":
                            fa.status = FaConstants.running;
                            break;
                        case "Ended":
                            fa.status = FaConstants.ended;
                            break;
                        case "Failed":
                            fa.status = FaConstants.Failed;
                            break;
                    }
                    context.Update(fa);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while updating FA status: {ex.Message}");
            }
        }

        public List<CleanBwCC> GetCCS(string faId)
        {
            List<CleanBwCC> result = new List<CleanBwCC>();

            try
            {
                using (MasterDataContext context = new MasterDataContext(DataSource))
                {
                    result = (from c in context.CleanBwCC
                              orderby c.uOrg
                              where c.faId == Convert.ToInt32(faId)
                              select c).ToList<CleanBwCC>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while deleting FA: {ex.Message}");
            }

            return result;
        }
    }
}