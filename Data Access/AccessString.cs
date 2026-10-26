using Log;
using System;
using System.Configuration;
using System.Diagnostics;

namespace Data_Access
{
    public class AccessString
    {
        public static string ConnectionString()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation($"Access String: {ex.Message}", "AccessString", "Application", EventLogEntryType.Error));
            }

            return string.Empty;
        }
    }
}
