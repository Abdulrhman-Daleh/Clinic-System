using Data_Access.DTOs;
using Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Data_Access
{
    public class AppointmentData
    {
        public static int AddNewAppointment(AppointmentDto appointmentDto)
        {
            int appointmentId = -1;

            string query = @"insert into Appointments
(PatientID, StaffID, AppointmentStatusID, AppointmentTypeID, AppointmentDate, AppointmentTime, AppointmentEndTime)
	values
	(@PatientID, @StaffID, @AppointmentStatusID, @AppointmentTypeID, GETDATE(), @AppointmentTime, @AppointmentEndTime);
	select SCOPE_IDENTITY();";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@PatientID", SqlDbType.Int) {Value = appointmentDto.PatientID },
                new SqlParameter("@StaffID", SqlDbType.Int) {Value = appointmentDto.StaffID },
                new SqlParameter("@AppointmentStatusID", SqlDbType.Int) {Value = appointmentDto.AppointmentStatuses.AppointmentStatusID },
                new SqlParameter("@AppointmentTypeID", SqlDbType.Int) {Value = appointmentDto.AppointmentTypes.AppointmentTypeID },
                new SqlParameter("@AppointmentTime", SqlDbType.Time) {Value = appointmentDto.AppointmentTime },
                new SqlParameter("@AppointmentEndTime", SqlDbType.Time) {Value = appointmentDto.AppointmentEndTime }
            };

            object obj = DataAccessHelper.ExecuteScalar(query, parameters);

            if (obj != null && int.TryParse(obj.ToString(), out appointmentId)) { }
            ;

            return appointmentId;
        }

        public static int UpdateAppointment(AppointmentDto appointmentDto)
        {
            int appointmentId = -1;

            string query = @"Update Appointments
            set AppointmentStatusID = @AppointmentStatusID, AppointmentTypeID = @AppointmentTypeID, AppointmentDate = @AppointmentDate,
            AppointmentTime = @AppointmentTime, AppointmentEndTime = @AppointmentEndTime where AppointmentID = @AppointmentId";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AppointmentId", SqlDbType.Int) {Value = appointmentDto.AppointmentID },
                new SqlParameter("@AppointmentStatusID", SqlDbType.Int) {Value = appointmentDto.AppointmentStatuses.AppointmentStatusID },
                new SqlParameter("@AppointmentTypeID", SqlDbType.Int) {Value = appointmentDto.AppointmentTypes.AppointmentTypeID },
                new SqlParameter("@AppointmentTime", SqlDbType.Time) {Value = appointmentDto.AppointmentTime },
                new SqlParameter("@AppointmentEndTime", SqlDbType.Time) {Value = appointmentDto.AppointmentEndTime },
                new SqlParameter("@AppointmentDate", SqlDbType.Date) {Value = appointmentDto.AppointmentDate }
            };

            return DataAccessHelper.ExecuteNonQuery(query, parameters);
        }

        private static string AppointmentTypeMapper(SqlDataReader reader)
        {
            try
            {
                return (string)reader["AppointmentTypeText"];
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "AppointmentData", "Application", EventLogEntryType.Error));
                return string.Empty;
            }
        }

        public static async Task<BindingList<string>> GetAppointmentTypesNames()
        {
            BindingList<string> types = new BindingList<string>();

            string query = @"select AppointmentTypeText from AppointmentTypes";

            types = await DataAccessHelper.ExecuteObjectList(query, null, AppointmentTypeMapper);

            return types;
        }
    }
}
