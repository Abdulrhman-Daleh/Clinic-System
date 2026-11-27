using Data_Access.ListingDTOs;
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
    public class PenaltyData
    {
        private static PenaltyListDto PenaltyMapper(SqlDataReader reader)
        {
            try
            {
                return new PenaltyListDto()
                {
                    PenaltyID = (int)reader["PenaltyID"],
                    Patient = (string)reader["Patient"],
                    Price = (decimal)reader["Price"],
                    PenaltyClosedDate = (reader["PenaltyClosedDate"] == DBNull.Value) ? (DateTime?)null : (DateTime)reader["PenaltyClosedDate"],
                    PenaltyDueDate = (DateTime)reader["PenaltyDueDate"],
                    Status = (string)reader["Status"]
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(ex.Message, "PenaltyData", "Application", EventLogEntryType.Error));
                return null;
            }
        }
        public static async Task<BindingList<PenaltyListDto>> GetAllPenalties(int patientId)
        {
            string query = @"select 
				PenaltyID,
				concat(person.Firstname, ' ', person.Lastname) as Patient,
				PenaltyAmount as Price,
				PenaltyDueDate,
				PenaltyClosedDate,
				s.PenaltyStatusText as Status
			from Penalties as p
				inner join Appointments as appo on appo.AppointmentID = p.AppointmentID
				inner join Patients as pat on pat.PatientID = appo.PatientID
				inner join People as person on person.PersonID = pat.PersonID
				inner join PenaltyStatuses as s on s.PenaltyStatusID = p.PenaltyStatusID
				where pat.PatientID = @PatientId";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@PatientId", SqlDbType.Int) {Value = patientId}
            };

            return await DataAccessHelper.ExecuteObjectList(query, parameters, PenaltyMapper);
        }
    }
}
