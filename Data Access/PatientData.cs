using Data_Access.DTOs;
using Data_Access.ListingDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data_Access
{
    public class PatientData
    {
        private static PatientDto PatientMapper(SqlDataReader reader)
        {
            return new PatientDto()
            {
                PatientId = (int)reader["PatientID"],
                PersonId = (int)reader["PersonID"],
                RegisterDate = (DateTime)reader["RegisterDate"],
                IsFrozen = (reader["IsFrozen"] == DBNull.Value) ? (bool?)null : (bool)reader["IsFrozen"],
                EndOfBookingFreeze = (reader["EndOfBookingFreeze"] == DBNull.Value) ? (DateTime?)null : (DateTime)reader["EndOfBookingFreeze"],
                HasUnPaidFees = (reader["HasUnPaidFees"] == DBNull.Value) ? (bool?)null : (bool)reader["HasUnPaidFees"],
                Firstname = (string)reader["Firstname"],
                Lastname = (string)reader["Lastname"],
                Gender = Convert.ToChar(reader["Gender"])
            };
        }
        private static PatientListingDto PatientListMapper(SqlDataReader reader)
        {
            return new PatientListingDto()
            {
                PatientId = (int)reader["PatientID"],
                FullName = (string)reader["FullName"],
                Gender = Convert.ToChar(reader["Gender"]),
                RegisterDate = (DateTime)reader["RegisterDate"],
                Visits = (int)reader["Visits"],
                InsuranceName = (reader["PatientRelationship"] == DBNull.Value) ? "No Insurance" : (string)reader["PatientRelationship"],
                Status = (string)reader["Status"]
            };
        }
        public static int AddNewPatient(PatientDto patientDto)
        {
            int patientId = -1;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@PersonId", SqlDbType.Int) {Value = patientDto.PersonId},
                    new SqlParameter("@Firstname", SqlDbType.VarChar, 40) {Value = patientDto.Firstname},
                    new SqlParameter("@Lastname", SqlDbType.VarChar, 40) {Value = patientDto.Lastname},
                    new SqlParameter("@Gender", SqlDbType.VarChar, 1) {Value = patientDto.Gender},
                };

                List<SqlParameter> outputParamters = new List<SqlParameter>()
                {
                    new SqlParameter("@PatientId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                bool result = DataAccessHelper.ExecuteNonQueryStoredProcedure<int>("AddPatient", parameters, outputParamters);

                if (result)
                    patientId = Convert.ToInt32(outputParamters[0].Value ?? -1);
            }
            catch (Exception ex)
            {
                throw;
            }

            return patientId;
        }
        public static async Task<BindingList<PatientListingDto>> GetPatientListingDatas()
        {
            BindingList<PatientListingDto> list = new BindingList<PatientListingDto>();

            string query = @"select * from vw_PatientInfo";

            try
            {
                list = await DataAccessHelper.ExecuteObjectList<PatientListingDto>(query, null, PatientListMapper);
            }
            catch (Exception ex)
            {
                throw;
            }

            return list;
        }
        public static PatientListingDto FindPatientByIdList(int patientId)
        {
            PatientListingDto patientListing = new PatientListingDto();

            string query = @"select PatientID, Fullname, Gender, RegisterDate, Visits, PatientRelationship, Status from vw_PatientInfo
            where PatientID = @patientId";

            try
            {

                List<SqlParameter> paramters = new List<SqlParameter>()
                {
                    new SqlParameter("@patientId", SqlDbType.Int) {Value = patientId}
                };

                patientListing = DataAccessHelper.ExecuteOneObject(query, paramters, PatientListMapper);
            }
            catch (Exception ex)
            {
                throw;
            }

            return patientListing;
        }
        public static PatientDto FindPatientById(int patientId)
        {
            PatientDto patient = new PatientDto();


            string query = @"select person.Firstname, person.Lastname, person.Gender, PatientID, p.PersonID, IsFrozen, RegisterDate, EndOfBookingFreeze, HasUnPaidFees from Patients as p
	inner join People as person on person.PersonID = p.PersonID
	where PatientID = @patientId";

            try
            {
                List<SqlParameter> paramters = new List<SqlParameter>()
                {
                    new SqlParameter("@patientId", SqlDbType.Int) {Value = patientId}
                };

                patient = DataAccessHelper.ExecuteOneObject(query, paramters, PatientMapper);
            }
            catch (Exception ex)
            {
                throw;
            }

            return patient;
        }
        public static int GetTotalVisits(int patientId)
        {
            int totalVisits = 0;
            string query = @"select 
        	count(p.PatientID) as TotalVisits
        from Patients as p
        	inner join Appointments as app on app.PatientID = p.PatientID
        	where p.PatientID = @patientId;";

            try
            {
                List<SqlParameter> paramters = new List<SqlParameter>()
                {
                    new SqlParameter("@patientId", SqlDbType.Int) { Value = patientId}
                };

                object obj = DataAccessHelper.ExecuteScalar(query, paramters);

                if (obj != null && int.TryParse(obj.ToString(), out totalVisits)) { }
                ;
            }
            catch (Exception ex)
            {
                throw;
            }

            return totalVisits;
        }
    }
}
