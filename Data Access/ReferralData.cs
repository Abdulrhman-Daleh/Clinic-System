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
    public class ReferralData
    {
        private static ReferralDto ReferralMapper(SqlDataReader reader)
        {
            try
            {
                return new ReferralDto()
                {
                    ReferralID = (int)reader["ReferralID"],
                    DoctorID = (int)reader["DoctorID"],
                    AppointmentID = (int)reader["AppointmentID"],
                    NoteID = (int)reader["NoteID"],
                    ReferralReason = (string)reader["ReferralReason"],
                    ReferralDate = (DateTime)reader["ReferralDate"],
                    ReferralUrgencies = (
                        (AppEnums.cs.EnReferralUrgencies)(int)reader["ReferralUrgencyID"],
                        (string)reader["ReferralUrgencyText"]
                    ),
                    Speciality = (string)reader["Speciality"]
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(
                    ex.Message,
                    "ReferralData",
                    "Application",
                    EventLogEntryType.Error));

                return null;
            }
        }

        public static int AddNewReferral(ReferralDto referralDto)
        {
            int referralId = -1;

            string query = @"
                INSERT INTO Referrals
                (
                    DoctorID,
                    AppointmentID,
                    NoteID,
                    ReferralReason,
                    ReferralDate,
                    ReferralUrgencyID,
                    Speciality
                )
                VALUES
                (
                    @DoctorID,
                    @AppointmentID,
                    @NoteID,
                    @ReferralReason,
                    @ReferralDate,
                    @ReferralUrgencyID,
                    @Speciality
                );

                SELECT SCOPE_IDENTITY();";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@DoctorID", SqlDbType.Int)
                    { Value = referralDto.DoctorID },

                new SqlParameter("@AppointmentID", SqlDbType.Int)
                    { Value = referralDto.AppointmentID },

                new SqlParameter("@NoteID", SqlDbType.Int)
                    { Value = referralDto.NoteID },

                new SqlParameter("@ReferralReason", SqlDbType.VarChar)
                    { Value = referralDto.ReferralReason },

                new SqlParameter("@ReferralDate", SqlDbType.DateTime)
                    { Value = referralDto.ReferralDate },

                new SqlParameter("@ReferralUrgencyID", SqlDbType.Int)
                    { Value = (int)referralDto.ReferralUrgencies.ReferralUrgencyID },

                new SqlParameter("@Speciality", SqlDbType.VarChar)
                    { Value = referralDto.Speciality }
            };

            object result = DataAccessHelper.ExecuteScalar(query, parameters);

            if (result != null && int.TryParse(result.ToString(), out referralId))
                return referralId;

            return -1;
        }

        public static bool UpdateReferral(ReferralDto referralDto)
        {
            string query = @"
                UPDATE Referrals
                SET
                    DoctorID = @DoctorID,
                    AppointmentID = @AppointmentID,
                    NoteID = @NoteID,
                    ReferralReason = @ReferralReason,
                    ReferralDate = @ReferralDate,
                    ReferralUrgencyID = @ReferralUrgencyID,
                    Speciality = @Speciality
                WHERE ReferralID = @ReferralID;

                SELECT @@ROWCOUNT;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ReferralID", SqlDbType.Int)
                    { Value = referralDto.ReferralID },

                new SqlParameter("@DoctorID", SqlDbType.Int)
                    { Value = referralDto.DoctorID },

                new SqlParameter("@AppointmentID", SqlDbType.Int)
                    { Value = referralDto.AppointmentID },

                new SqlParameter("@NoteID", SqlDbType.Int)
                    { Value = referralDto.NoteID },

                new SqlParameter("@ReferralReason", SqlDbType.VarChar)
                    { Value = referralDto.ReferralReason },

                new SqlParameter("@ReferralDate", SqlDbType.DateTime)
                    { Value = referralDto.ReferralDate },

                new SqlParameter("@ReferralUrgencyID", SqlDbType.Int)
                    { Value = (int)referralDto.ReferralUrgencies.ReferralUrgencyID },

                new SqlParameter("@Speciality", SqlDbType.VarChar)
                    { Value = referralDto.Speciality }
            };

            object result = DataAccessHelper.ExecuteScalar(query, parameters);

            return result != null && Convert.ToInt32(result) > 0;
        }

        public static ReferralDto FindReferralByID(int referralID)
        {
            string query = @"
                SELECT
                    r.ReferralID,
                    r.DoctorID,
                    r.AppointmentID,
                    r.NoteID,
                    r.ReferralReason,
                    r.ReferralDate,
                    r.ReferralUrgencyID,
                    ru.ReferralUrgencyText,
                    r.Speciality
                FROM Referrals AS r
                INNER JOIN ReferralUrgencies AS ru
                    ON r.ReferralUrgencyID = ru.ReferralUrgencyID
                WHERE r.ReferralID = @ReferralID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ReferralID", SqlDbType.Int)
                    { Value = referralID }
            };

            return DataAccessHelper.ExecuteOneObject(
                query, parameters, ReferralMapper);
        }

        public static async Task<BindingList<ReferralDto>> GetReferralsByAppointmentID(int appointmentID)
        {
            string query = @"
                SELECT
                    r.ReferralID,
                    r.DoctorID,
                    r.AppointmentID,
                    r.NoteID,
                    r.ReferralReason,
                    r.ReferralDate,
                    r.ReferralUrgencyID,
                    ru.ReferralUrgencyText,
                    r.Speciality
                FROM Referrals AS r
                INNER JOIN ReferralUrgencies AS ru
                    ON r.ReferralUrgencyID = ru.ReferralUrgencyID
                WHERE r.AppointmentID = @AppointmentID
                ORDER BY r.ReferralDate DESC";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AppointmentID", SqlDbType.Int)
                    { Value = appointmentID }
            };

            return await DataAccessHelper.ExecuteObjectList<ReferralDto>(
                query, parameters, ReferralMapper);
        }

        public static async Task<BindingList<ReferralDto>> GetReferrals()
        {
            string query = @"
                SELECT
                    r.ReferralID,
                    r.DoctorID,
                    r.AppointmentID,
                    r.NoteID,
                    r.ReferralReason,
                    r.ReferralDate,
                    r.ReferralUrgencyID,
                    ru.ReferralUrgencyText,
                    r.Speciality
                FROM Referrals AS r
                INNER JOIN ReferralUrgencies AS ru
                    ON r.ReferralUrgencyID = ru.ReferralUrgencyID
                ORDER BY r.ReferralDate DESC";

            return await DataAccessHelper.ExecuteObjectList<ReferralDto>(
                query, null, ReferralMapper);
        }
    }
}