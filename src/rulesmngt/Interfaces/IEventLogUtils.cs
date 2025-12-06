using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IEventLogUtils
    {
        EventLog writeEventLog(string message, string typo, string operation);
        bool AddEventLog(EventLog eventLog);
         
    }
}