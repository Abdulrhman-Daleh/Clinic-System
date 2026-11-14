using Data_Access.DTOs;
using Log;
using LoggerMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Data_Access
{
    public class ReminderData
    {
        private static ReminderDto ReminderMapper(SqlDataReader reader)
        {
            try
            {
                return new ReminderDto()
                {
                    ReminderID = (int)reader["ReminderID"],
                    AppointmentID = (int)reader["AppointmentID"],
                    StaffID = (int)reader["StaffID"],
                    Title = (string)reader["Title"],
                    ReminderDate = (DateTime)reader["ReminderDate"],
                    Subject = reader["Subject"] == DBNull.Value ? string.Empty : (string)reader["Subject"],
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(
                                   ex.Message,
                                   "ReminderData",
                                   "Application",
                                   EventLogEntryType.Error));
                return null;
            }
        }
        public static int AddNewReminder(ReminderDto reminder)
        {
            string query = "sp_AddNewReminder";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = reminder.AppointmentID },
                new SqlParameter("@StaffID", SqlDbType.Int) { Value = reminder.StaffID },
                new SqlParameter("@Title",  SqlDbType.VarChar, 50) { Value = reminder.Title },
                new SqlParameter("@Subject", SqlDbType.VarChar, 50) { Value = reminder.Subject ?? (object)DBNull.Value },
                new SqlParameter("@Message", SqlDbType.VarChar, -1) { Value = reminder.Message }
            };

            List<SqlParameter> outParameters = new List<SqlParameter>()
            {
                new SqlParameter("@ReminderID", System.Data.SqlDbType.Int) {Direction = ParameterDirection.Output}
            };

            bool result = DataAccessHelper.ExecuteNonQueryStoredProcedure<bool>(query, parameters, outParameters);

            return result ? Convert.ToInt32(outParameters[0].Value) : -1;
        }
        public static async Task<BindingList<ReminderDto>> GetAppointmentReminders(int appointmentId)
        {
            string query = "select * from vw_AppointmentReminderList(@AppointmentId)";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                 new SqlParameter("@AppointmentId", SqlDbType.Int) { Value = appointmentId }
            };

            return await DataAccessHelper.ExecuteObjectList(query, parameters, ReminderMapper);
        }
    }
}
