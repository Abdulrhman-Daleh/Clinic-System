using LoggerMessage;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Log
{
    public class Logger
    {
        private static void InitializeLogSource(string sourceName, string logName)
        {
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, logName);
            }
        }

        public static void LogMessage(LogMessageInformation logMessage)
        {
            InitializeLogSource(logMessage.SourceName, logMessage.LogName);

            try
            {
                EventLog.WriteEntry(logMessage.SourceName, logMessage.ErrorMessage, logMessage.EntryType);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Logger failed: {ex.Message} Application");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    try
                    {
                        EventLog.WriteEntry("Application", $"Logger Failed: {ex}", EventLogEntryType.Error);
                    }
                    catch
                    {
                        return;
                    }
                }

                Logger.LogMessage(new LogMessageInformation($"Logger: {ex.Message}", "Logger", "Application", EventLogEntryType.Error));

            }
        }
    }
}
