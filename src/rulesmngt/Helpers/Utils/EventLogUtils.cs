using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Utils
{
    public class EventLogUtils : IEventLogUtils
    {
        private ILogger Logger;
        private readonly IOptions<DatabaseConfiguration> DataSource;
        public EventLogUtils(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerFactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerFactory.CreateLogger<EventLogUtils>();
        }


        public EventLog writeEventLog(string message, string typo, string operation)
        {
            EventLog eventLog = new EventLog();
            eventLog.Message = message;
            eventLog.TypeInfo = typo;
            eventLog.MessageDateTime = DateTime.Now;
            eventLog.Operation = operation;
            return eventLog;
        }
        public bool AddEventLog(EventLog eventLog)
        {
            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    EntityEntry<EventLog> ee = context.Add(eventLog);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to add new EventLog");
                return false;
            }
        }
    }
}