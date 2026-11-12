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

        public static void SeedDefaultData()
        {
            string query = @"USE ClinicDatabase;
SET IDENTITY_INSERT dbo.ContactTypes ON;

IF NOT EXISTS (SELECT 1 FROM dbo.ContactTypes WHERE ContactTypeID = 1)
BEGIN
    INSERT INTO dbo.ContactTypes (ContactTypeID, ContactTypeName)
    VALUES (1, N'Normal');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.ContactTypes WHERE ContactTypeID = 2)
BEGIN
    INSERT INTO dbo.ContactTypes (ContactTypeID, ContactTypeName)
    VALUES (2, N'Emergency');
END;

SET IDENTITY_INSERT dbo.ContactTypes OFF;
SET IDENTITY_INSERT dbo.PolicyStatuses ON;

IF NOT EXISTS (SELECT 1 FROM dbo.PolicyStatuses WHERE PolicyStatusID = 1)
BEGIN
    INSERT INTO dbo.PolicyStatuses (PolicyStatusID, PolicyStatusText)
    VALUES (1, N'Draft');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PolicyStatuses WHERE PolicyStatusID = 2)
BEGIN
    INSERT INTO dbo.PolicyStatuses (PolicyStatusID, PolicyStatusText)
    VALUES (2, N'Awaiting Approval');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PolicyStatuses WHERE PolicyStatusID = 3)
BEGIN
    INSERT INTO dbo.PolicyStatuses (PolicyStatusID, PolicyStatusText)
    VALUES (3, N'Active');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PolicyStatuses WHERE PolicyStatusID = 4)
BEGIN
    INSERT INTO dbo.PolicyStatuses (PolicyStatusID, PolicyStatusText)
    VALUES (4, N'Archived');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PolicyStatuses WHERE PolicyStatusID = 5)
BEGIN
    INSERT INTO dbo.PolicyStatuses (PolicyStatusID, PolicyStatusText)
    VALUES (5, N'Suspended');
END;

SET IDENTITY_INSERT dbo.PolicyStatuses OFF;
SET IDENTITY_INSERT dbo.AppointmentStatuses ON;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 1)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (1, N'Scheduled');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 2)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (2, N'Reminder Sent');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 3)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (3, N'In Room');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 4)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (4, N'Completed');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 5)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (5, N'Cancelled');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 6)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (6, N'No Show');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 7)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (7, N'Rescheduled');
END;

SET IDENTITY_INSERT dbo.AppointmentStatuses OFF;
SET IDENTITY_INSERT dbo.AppointmentTypes ON;
";
            try
            {
                DataAccessHelper.ExecuteNonQuery(query, null);
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation($"{ex.Message}", "AccessString", "Application", EventLogEntryType.Error));
            }
        }

    }
}
