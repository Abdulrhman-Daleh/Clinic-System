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
    public class MedicalRecordData
    {
        public static int AddMedicalRecord(MedicalRecordDto recordDto)
        {
            int medicalRecord = -1;

            string query = @"INSERT INTO MedicalRecords (
            PatientID,
            MedicalNumber,
            CreatedDate,
            CurrentMedication,
            CurrentMedicationDuration,
            PastSocialHistory,
            PastSurgeries,
            ConcentToCare,
            SocialStatus,
            Classification,
            Allergies)
           VALUES
            (
           @PatientID,
           (select 'MMR-'+ cast(ISNULL(Max(MedicalRecordID), 0) + 1 as varchar) from MedicalRecords),
           @CreatedDate, 
           @CurrentMedication, 
           @CurrentMedicationDuration, 
           @PastSocialHistory,
           @PastSurgeries, 

           @ConcentToCare,
           @SocialStatus, 
           @Classification, 
           @Allergies);

           select SCOPE_IDENTITY();";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@PatientID", SqlDbType.Int) {Value = recordDto.PatientID },
                new SqlParameter("@CreatedDate", SqlDbType.Date) {Value = recordDto.CreatedDate },
                new SqlParameter("@CurrentMedication", SqlDbType.VarChar) {Value = recordDto.CurrentMedication },
                new SqlParameter("@PastSurgeries", SqlDbType.VarChar) {Value = recordDto.PastSurgeries },
                new SqlParameter("@CurrentMedicationDuration", SqlDbType.Date) {Value = recordDto.CurrentMedicationDuration },
                new SqlParameter("@PastSocialHistory", SqlDbType.VarChar) {Value = recordDto.PastSocialHistory },
                new SqlParameter("@ConcentToCare", SqlDbType.Bit) {Value = recordDto.ConcentToCare },
                new SqlParameter("@SocialStatus", SqlDbType.VarChar, 50) {Value = recordDto.SocialStatus },
                new SqlParameter("@Classification", SqlDbType.VarChar, 50) {Value = recordDto.Classification },
                new SqlParameter("@Allergies", SqlDbType.VarChar, 200) {Value = recordDto.Allergies },
            };

            object obj = DataAccessHelper.ExecuteScalar(query, parameters);

            if (obj != null && int.TryParse(obj.ToString(), out medicalRecord)) { }
            ;

            return medicalRecord;
        }

        public static bool UpdateMedicalRecord(MedicalRecordDto recordDto)
        {
            string query = @"UPDATE MedicalRecords
		SET CurrentMedication = @CurrentMedication,
		CurrentMedicationDuration = @CurrentMedicationDuration,
		PastSocialHistory = @PastSocialHistory,
		PastSurgeries = @PastSurgeries,
		ConcentToCare = @ConcentToCare,
		SocialStatus = @SocialStatus,
		Classification = @Classification,
		Allergies = @Allergies
	WHERE  MedicalRecordID = @MedicalRecordId";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@MedicalRecordId", SqlDbType.Int) {Value = recordDto.MedicalRecordID },
                new SqlParameter("@CurrentMedication", SqlDbType.VarChar) {Value = recordDto.CurrentMedication },
                new SqlParameter("@PastSurgeries", SqlDbType.VarChar) {Value = recordDto.PastSurgeries },
                new SqlParameter("@CurrentMedicationDuration", SqlDbType.Date) {Value = recordDto.CurrentMedicationDuration },
                new SqlParameter("@PastSocialHistory", SqlDbType.VarChar) {Value = recordDto.PastSocialHistory },
                new SqlParameter("@ConcentToCare", SqlDbType.Bit) {Value = recordDto.ConcentToCare },
                new SqlParameter("@SocialStatus", SqlDbType.VarChar, 50) {Value = recordDto.SocialStatus },
                new SqlParameter("@Classification", SqlDbType.VarChar, 50) {Value = recordDto.Classification },
                new SqlParameter("@Allergies", SqlDbType.VarChar, 200) {Value = recordDto.Allergies },
            };

            return DataAccessHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        private static MedicalRecordDto MedicalRecordMapper(SqlDataReader reader)
        {
            try
            {
                return new MedicalRecordDto()
                {
                    MedicalRecordID = (int)reader["MedicalRecordID"],
                    PatientID = (int)reader["PatientID"],
                    MedicalNumber = (string)reader["MedicalNumber"],
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    CurrentMedication = reader["CurrentMedication"] == DBNull.Value
                        ? null : (string)reader["CurrentMedication"],
                    CurrentMedicationDuration = reader["CurrentMedicationDuration"] == DBNull.Value
                        ? default(DateTime) : (DateTime)reader["CurrentMedicationDuration"],
                    PastSocialHistory = reader["PastSocialHistory"] == DBNull.Value
                        ? null : (string)reader["PastSocialHistory"],
                    PastSurgeries = reader["PastSurgeries"] == DBNull.Value
                        ? null : (string)reader["PastSurgeries"],
                    ConcentToCare = (bool)reader["ConcentToCare"],
                    SocialStatus = reader["SocialStatus"] == DBNull.Value
                        ? null : (string)reader["SocialStatus"],
                    Classification = reader["Classification"] == DBNull.Value
                        ? null : (string)reader["Classification"],
                    Allergies = reader["Allergies"] == DBNull.Value
                        ? null : (string)reader["Allergies"]
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(
                    ex.Message,
                    "MedicalRecordData",
                    "Application",
                    EventLogEntryType.Error));

                return null;
            }
        }

        public static MedicalRecordDto FindMedicalRecordByID(int medicalRecordID)
        {
            string query = @"SELECT MedicalRecordID
      ,PatientID
      ,MedicalNumber
      ,CreatedDate
      ,CurrentMedication
      ,CurrentMedicationDuration
      ,PastSocialHistory
      ,PastSurgeries
      ,ConcentToCare
      ,SocialStatus
      ,Classification
      ,Allergies
    FROM MedicalRecords
             WHERE MedicalRecordID = @MedicalRecordID;";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                 new SqlParameter("@MedicalRecordID", SqlDbType.Int)
                 {
                     Value = medicalRecordID
                 }
             };

            return DataAccessHelper.ExecuteOneObject(query, parameters, MedicalRecordMapper);
        }

        private static MedicalRecordListDto MedicalRecordListMapper(SqlDataReader reader)
        {
            try
            {
                return new MedicalRecordListDto()
                {
                    MedicalRecordID = (int)reader["MedicalRecordID"],
                    MedicalNumber = reader["MedicalNumber"] == DBNull.Value
                        ? null : (string)reader["MedicalNumber"],
                    PatientName = (string)reader["PatientName"],
                    Classification = reader["Classification"] == DBNull.Value
                        ? null : (string)reader["Classification"],
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    ConsentToCare = (bool)reader["ConsentToCare"]
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(
                    ex.Message,
                    "MedicalRecordData",
                    "Application",
                    EventLogEntryType.Error));

                return null;
            }
        }

        public static async Task<BindingList<MedicalRecordListDto>> GetMedicalRecords()
        {
            string query = @"
        SELECT
            MR.MedicalRecordID,
            MR.MedicalNumber,
            CONCAT(P.Firstname, ' ', P.Lastname) AS PatientName,
            MR.Classification,
            MR.CreatedDate,
            MR.ConcentToCare AS ConsentToCare
        FROM MedicalRecords MR
        INNER JOIN Patients PA
            ON MR.PatientID = PA.PatientID
        INNER JOIN People P
            ON PA.PersonID = P.PersonID
        ORDER BY MR.MedicalRecordID DESC;";

            return await DataAccessHelper.ExecuteObjectList(
                query,
                new List<SqlParameter>(),
                MedicalRecordListMapper);
        }
    }
}
