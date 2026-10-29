using AppEnums.cs;
using Data_Access.DTOs;
using Data_Access.ListingDTOs;
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

        public static bool NoOverlapWithLastAppointment(AppointmentDto appointmentDto)
        {
            string spName = "NoOverlapWithLastAppointment";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@StaffId", SqlDbType.Int) {Value = appointmentDto.StaffID },
                new SqlParameter("@NewAppointmentDate", SqlDbType.DateTime) {Value = appointmentDto.AppointmentDate },
                new SqlParameter("@NewAppointmentTime", SqlDbType.Time) {Value = appointmentDto.AppointmentTime },
                new SqlParameter("@NewAppointmentEndTime", SqlDbType.Time) {Value = appointmentDto.AppointmentEndTime }
            };

            SqlParameter returnValue = new SqlParameter("@Value", SqlDbType.Int)
            {
                Direction = ParameterDirection.ReturnValue
            };

            DataAccessHelper.ExecuteNonQueryStoredProcedure<int>(spName, parameters, new List<SqlParameter>() { returnValue });

            return Convert.ToInt32(returnValue.Value) == 1;
        }

        private static AppointmentListDto AppointmentListDtoMapper(SqlDataReader reader)
        {
            return new AppointmentListDto()
            {
                AppointmentID = (int)reader["AppointmentID"],
                Time = (string)reader["Time"],
                Patient = (string)reader["Patient"],
                Doctor = (string)reader["Doctor"],
                Type = (string)reader["Type"],
                Status = (string)reader["Status"]
            };
        }

        public static async Task<BindingList<AppointmentListDto>> GetAppointment()
        {
            BindingList<AppointmentListDto> appointments = new BindingList<AppointmentListDto>();

            string query = @"select AppointmentID, TIme, Patient, Doctor, Type, Status From vw_AppointmentsList";

            appointments = await DataAccessHelper.ExecuteObjectList(query, null, AppointmentListDtoMapper);

            return appointments;
        }

        public static AppointmentListDto FindAppointmentListByID(int appointmentId)
        {
            string query = @"select AppointmentID, TIme, Patient, Doctor, Type, Status From vw_AppointmentsList
	        where AppointmentID = @AppointmentId;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AppointmentId", SqlDbType.Int) {Value =  appointmentId}
            };

            return DataAccessHelper.ExecuteOneObject(query, parameters, AppointmentListDtoMapper);
        }

        private static AppointmentDto AppointmentDtoMapper(SqlDataReader reader)
        {
            return new AppointmentDto()
            {
                AppointmentID = (int)reader["AppointmentID"],
                PatientID = (int)reader["PatientID"],
                StaffID = (int)reader["StaffID"],
                AppointmentStatuses = ((EnAppointmentStatuses)reader["AppointmentStatusID"], (string)reader["AppointmentStatusText"]),
                AppointmentTypes = ((EnAppointmentTypes)reader["AppointmentTypeID"], (string)reader["AppointmentTypeText"]),
                AppointmentDate = (DateTime)reader["AppointmentDate"],
                AppointmentTime = reader.GetDateTime(reader.GetOrdinal("AppointmentTime")).TimeOfDay,
                AppointmentEndTime = reader.GetDateTime(reader.GetOrdinal("AppointmentEndTime")).TimeOfDay
            };
        }
        public static AppointmentDto FindAppointmentByID(int appointmentId)
        {
            string query = @"select AppointmentID, PatientID, StaffID, appo.AppointmentStatusID, aStatus.AppointmentStatusText, appo.AppointmentTypeID,
            appoType.AppointmentTypeText, AppointmentDate,
            AppointmentTime, AppointmentEndTime from Appointments as appo inner join AppointmentStatuses as aStatus on 
            appo.AppointmentStatusID = aStatus.AppointmentStatusID inner join AppointmentTypes as appoType on appoType.AppointmentTypeID
            = appo.AppointmentTypeID
            where AppointmentID = 2";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AppointmentId", SqlDbType.Int) {Value =  appointmentId}
            };

            return DataAccessHelper.ExecuteOneObject(query, parameters, AppointmentDtoMapper);
        }


    }
}
