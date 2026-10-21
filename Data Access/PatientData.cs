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

                List<object> values = DataAccessHelper.ExecuteNonQueryStoredProcedure<int>("AddPatient", parameters, outputParamters);

                if (values.Count > 0)
                {
                    patientId = Convert.ToInt32(values[0]);
                }
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
        public static PatientListingDto FindPatientByIdList(int staffId)
        {
            PatientListingDto patientListing = new PatientListingDto();

            string query = @"select PatientID, Fullname, Gender, RegisterDate, Visits, PatientRelationship, Status from vw_PatientInfo
            where PatientID = @patientId";

            try
            {

                List<SqlParameter> paramters = new List<SqlParameter>()
                {
                    new SqlParameter("@patientId", SqlDbType.Int) {Value = staffId}
                };

                patientListing = DataAccessHelper.ExecuteOneObject(query, paramters, PatientListMapper);
            }
            catch (Exception ex)
            {
                throw;
            }

            return patientListing;
        }
    }
}
