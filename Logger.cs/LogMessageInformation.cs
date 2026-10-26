using System.Diagnostics;

namespace LoggerMessage
{
    public class LogMessageInformation
    {
        public string ErrorMessage { get; set; }
        public string SourceName { get; set; }
        public EventLogEntryType EntryType { get; set; }
        public string LogName { get; set; }
        public LogMessageInformation(string errorMessage, string sourceName, string logName, EventLogEntryType entryType)
        {
            ErrorMessage = errorMessage;
            SourceName = sourceName;
            EntryType = entryType;
            LogName = logName;
        }
    }
}
