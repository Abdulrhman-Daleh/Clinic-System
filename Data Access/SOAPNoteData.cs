using Data_Access.DTOs;
using Log;
using LoggerMessage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Data_Access.ListingDTOs
{
    public class SOAPNoteData
    {
        private static SOAPNoteDto SOAPNoteMapper(SqlDataReader reader)
        {
            try
            {
                return new SOAPNoteDto()
                {
                    NoteID = (int)reader["NoteID"],
                    WrittenByID = (int)reader["WrittenByID"],
                    AppointmentID = (int)reader["AppointmentID"],
                    CreationDate = (DateTime)reader["CreationDate"],
                    SubjectiveContent = reader["SubjectiveContent"] == DBNull.Value ? null : (string)reader["SubjectiveContent"],
                    ObjectiveContent = reader["ObjectiveContent"] == DBNull.Value ? null : (string)reader["ObjectiveContent"],
                    AssesmentContent = reader["AssesmentContent"] == DBNull.Value ? null : (string)reader["AssesmentContent"],
                    PlanContent = reader["PlanContent"] == DBNull.Value ? null : (string)reader["PlanContent"],
                    IsActive = reader["IsActive"] == DBNull.Value ? (bool?)null : (bool)reader["IsActive"],
                    MedicalRecordID = (int)reader["MedicalRecordID"]
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(ex.Message, "SOAPNoteData", "Application", EventLogEntryType.Error));
                return null;
            }
        }

        public static int AddNewSOAPNote(SOAPNoteDto noteDto)
        {
            int noteId = -1;

            string query = @"
                INSERT INTO SOAPNotes
                (
                    WrittenByID,
                    AppointmentID,
                    CreationDate,
                    SubjectiveContent,
                    ObjectiveContent,
                    AssesmentContent,
                    PlanContent,
                    IsActive,
                    MedicalRecordID
                )
                VALUES
                (
                    @WrittenByID,
                    @AppointmentID,
                    @CreationDate,
                    @SubjectiveContent,
                    @ObjectiveContent,
                    @AssesmentContent,
                    @PlanContent,
                    @IsActive,
                    @MedicalRecordID
                );

                SELECT SCOPE_IDENTITY();";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@WrittenByID", SqlDbType.Int) { Value = noteDto.WrittenByID },
                new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = noteDto.AppointmentID },
                new SqlParameter("@CreationDate", SqlDbType.DateTime) { Value = noteDto.CreationDate },
                new SqlParameter("@SubjectiveContent", SqlDbType.VarChar, -1) { Value = (object)noteDto.SubjectiveContent ?? DBNull.Value },
                new SqlParameter("@ObjectiveContent", SqlDbType.VarChar, -1) { Value = (object)noteDto.ObjectiveContent ?? DBNull.Value },
                new SqlParameter("@AssesmentContent", SqlDbType.VarChar, -1) { Value = (object)noteDto.AssesmentContent ?? DBNull.Value },
                new SqlParameter("@PlanContent", SqlDbType.VarChar, -1) { Value = (object)noteDto.PlanContent ?? DBNull.Value },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = (object)noteDto.IsActive ?? DBNull.Value },
                new SqlParameter("@MedicalRecordID", SqlDbType.Int) { Value = noteDto.MedicalRecordID }
            };

            object result = DataAccessHelper.ExecuteScalar(query, parameters);

            if (result != null && int.TryParse(result.ToString(), out noteId)) { }
                ;

            return noteId;
        }

        public static bool UpdateSOAPNote(SOAPNoteDto noteDto)
        {
            string query = @"
                UPDATE SOAPNotes SET
                    WrittenByID = @WrittenByID,
                    AppointmentID = @AppointmentID,
                    CreationDate = @CreationDate,
                    SubjectiveContent = @SubjectiveContent,
                    ObjectiveContent = @ObjectiveContent,
                    AssesmentContent = @AssesmentContent,
                    PlanContent = @PlanContent,
                    IsActive = @IsActive,
                    MedicalRecordID = @MedicalRecordID
                WHERE NoteID = @NoteID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@NoteID", SqlDbType.Int) { Value = noteDto.NoteID },
                new SqlParameter("@WrittenByID", SqlDbType.Int) { Value = noteDto.WrittenByID },
                new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = noteDto.AppointmentID },
                new SqlParameter("@CreationDate", SqlDbType.DateTime) { Value = noteDto.CreationDate },
                new SqlParameter("@SubjectiveContent", SqlDbType.VarChar, -1) { Value = (object)noteDto.SubjectiveContent ?? DBNull.Value },
                new SqlParameter("@ObjectiveContent", SqlDbType.VarChar, -1) { Value = (object)noteDto.ObjectiveContent ?? DBNull.Value },
                new SqlParameter("@AssesmentContent", SqlDbType.VarChar, -1) { Value = (object)noteDto.AssesmentContent ?? DBNull.Value },
                new SqlParameter("@PlanContent", SqlDbType.VarChar, -1) { Value = (object)noteDto.PlanContent ?? DBNull.Value },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = (object)noteDto.IsActive ?? DBNull.Value },
                new SqlParameter("@MedicalRecordID", SqlDbType.Int) { Value = noteDto.MedicalRecordID }
            };

            int affectedRows = DataAccessHelper.ExecuteNonQuery(query, parameters);

            return affectedRows > 0;
        }

        public static SOAPNoteDto FindSOAPNoteByID(int noteId)
        {
            string query = @"
                SELECT *
                FROM SOAPNotes
                WHERE NoteID = @NoteID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@NoteID", SqlDbType.Int) { Value = noteId }
            };

            return DataAccessHelper.ExecuteOneObject(query, parameters, SOAPNoteMapper);
        }

        public static SOAPNoteDto FindSOAPNoteByAppointmentID(int appointmentId)
        {
            string query = @"
                SELECT *
                FROM SOAPNotes
                WHERE AppointmentID = @AppointmentID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = appointmentId }
            };

            return DataAccessHelper.ExecuteOneObject(query, parameters, SOAPNoteMapper);
        }
    }
}