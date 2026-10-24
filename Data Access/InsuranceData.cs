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
    public class InsuranceData
    {
        public static (int InsuranceId, int PatientId) AddNewInsurance(InsuranceDto insuranceDto, PatientDto patientDto)
        {
            (int InsuranceId, int PatientId) result = (-1, -1);

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@RecordedBy", insuranceDto.RecordedBy),
                    new SqlParameter("@InsuranceTypeId", insuranceDto.InsuranceTypeId),
                    new SqlParameter("@InsuranceStatusId", insuranceDto.InsuranceStatusId),
                    new SqlParameter("@InsuranceProvider", (object)insuranceDto.InsuranceProvider ?? DBNull.Value),
                    new SqlParameter("@PolicyNumber", (object)insuranceDto.PolicyNumber ?? DBNull.Value),
                    new SqlParameter("@CoverageStartDate", insuranceDto.CoverageStartDate),
                    new SqlParameter("@CoverageEndDate", insuranceDto.CoverageEndDate),
                    new SqlParameter("@PatientClaims", (object)insuranceDto.PatientClaims ?? DBNull.Value),
                    new SqlParameter("@PolicyholderPatientId", insuranceDto.PolicyholderPatientId),
                    new SqlParameter("@PersonID", patientDto.PersonId),
                    new SqlParameter("@PolicyID", insuranceDto.PolicyId),
                    new SqlParameter("@Firstname", patientDto.Firstname),
                    new SqlParameter("@LastName", patientDto.Lastname),
                    new SqlParameter("@Gender", patientDto.Gender),
                    new SqlParameter("@PatientID", patientDto.PatientId),
                    new SqlParameter("@IsFrozen", (object)patientDto.IsFrozen ?? DBNull.Value),
                    new SqlParameter("@RegisterDate", patientDto.RegisterDate),
                    new SqlParameter("@EndOfBookingFreeze", (object)patientDto.EndOfBookingFreeze ?? DBNull.Value),
                    new SqlParameter("@HasUnPaidFees", (object)patientDto.HasUnPaidFees ?? DBNull.Value),
                    new SqlParameter("@Relationship", insuranceDto.Relationship)
                };


                List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter("@InsuranceId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    },
                    new SqlParameter("@AddedPatientID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                bool success = DataAccessHelper.ExecuteNonQueryStoredProcedure<InsuranceDto>(
                    "AddNewInsurance", parameters, outputParams);

                if (success)
                {
                    result.InsuranceId = Convert.ToInt32(outputParams[0].Value);
                    result.PatientId = Convert.ToInt32(outputParams[1].Value);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public static bool UpdateInsurance(InsuranceDto insuranceDto)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@InsuranceId", insuranceDto.InsuranceID),
                    new SqlParameter("@PolicyId", insuranceDto.PolicyId),
                    new SqlParameter("@InsuranceTypeId", insuranceDto.InsuranceTypeId),
                    new SqlParameter("@InsuranceStatusId", insuranceDto.InsuranceStatusId),
                    new SqlParameter("@InsuranceProvider", (object)insuranceDto.InsuranceProvider ?? DBNull.Value),
                    new SqlParameter("@CoverageStartDate", insuranceDto.CoverageStartDate),
                    new SqlParameter("@CoverageEndDate", insuranceDto.CoverageEndDate),
                    new SqlParameter("@PatientClaims", (object)insuranceDto.PatientClaims ?? DBNull.Value),
                    new SqlParameter("@Relationship", insuranceDto.Relationship),
                };


                return DataAccessHelper.ExecuteNonQueryStoredProcedure<InsuranceDto>(
                    "UpdateInsurance", parameters, new List<SqlParameter>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static InsuranceDto FindInsuranceByID(int insuranceId)
        {
            try
            {
                string query = @"SELECT InsuranceID, RecordedBy, InsuranceTypeID, InsuranceStatusID, UploadedDate, InsuranceProvider, PolicyNumber, CoverageStartDate, CoverageEndDate,
            PatientClaims, PolicyholderPatientID, PolicyID
                                 FROM Insurance
                                 where InsuranceID = @InsuranceId;";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@InsuranceId", insuranceId)
                };

                return DataAccessHelper.ExecuteOneObject(
                    query, parameters, InsuranceMapper);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static InsuranceDto FindInsuranceByPolicyID(int policyId)
        {
            try
            {
                string query = @"SELECT InsuranceID, RecordedBy, InsuranceTypeID, InsuranceStatusID, UploadedDate, InsuranceProvider, PolicyNumber, CoverageStartDate,
                PatientClaims, PolicyholderPatientID, PolicyID
                                 FROM Insurance
                                 WHERE PolicyId = @PolicyId";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@PolicyId", policyId)
                };

                return DataAccessHelper.ExecuteOneObject(
                    query, parameters, InsuranceMapper);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static InsuranceDto InsuranceMapper(SqlDataReader reader)
        {
            try
            {
                return new InsuranceDto
                {
                    InsuranceID = Convert.ToInt32(reader["InsuranceId"]),
                    PolicyId = Convert.ToInt32(reader["PolicyId"]),
                    RecordedBy = Convert.ToInt32(reader["RecordedBy"]),
                    InsuranceTypeId = Convert.ToInt32(reader["InsuranceTypeId"]),
                    InsuranceStatusId = Convert.ToInt32(reader["InsuranceStatusId"]),
                    UploadedDate = Convert.ToDateTime(reader["UploadedDate"]),
                    InsuranceProvider = reader["InsuranceProvider"] == DBNull.Value
                        ? null : Convert.ToString(reader["InsuranceProvider"]),
                    PolicyNumber = reader["PolicyNumber"] == DBNull.Value
                        ? null : Convert.ToString(reader["PolicyNumber"]),
                    CoverageStartDate = Convert.ToDateTime(reader["CoverageStartDate"]),
                    CoverageEndDate = Convert.ToDateTime(reader["CoverageEndDate"]),
                    PatientClaims = reader["PatientClaims"] == DBNull.Value
                        ? null : Convert.ToString(reader["PatientClaims"]),
                    PolicyholderPatientId = Convert.ToInt32(reader["PolicyholderPatientId"])
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string InsuranceTypeMapper(SqlDataReader reader)
        {
            return (string)reader["InsuranceTypeName"];
        }

        private static string InsuranceStatusMapper(SqlDataReader reader)
        {
            return (string)reader["insuranceStatusText"];
        }

        public static async Task<BindingList<string>> GetInsuranceTypesNames()
        {
            BindingList<string> types = new BindingList<string>();
            string query = @"select InsuranceTypeName from insuranceTypes";

            try
            {
                types = await DataAccessHelper.ExecuteObjectList(query, null, InsuranceTypeMapper);
            }
            catch (Exception ex)
            {

            }

            return types;
        }

        public static async Task<BindingList<string>> GetInsuranceStatusesNames()
        {
            BindingList<string> types = new BindingList<string>();
            string query = @"select insuranceStatusText from insuranceStatuses";

            try
            {
                types = await DataAccessHelper.ExecuteObjectList(query, null, InsuranceStatusMapper);
            }
            catch (Exception ex)
            {

            }

            return types;
        }

        private static InsuranceListDto InsuranceListMapper(SqlDataReader reader)
        {
            try
            {
                return new InsuranceListDto
                {
                    InsuranceID = Convert.ToInt32(reader["InsuranceId"]),
                    InsuranceType = Convert.ToString(reader["InsuranceType"]),
                    HolderName = Convert.ToString(reader["HolderName"]),
                    Members = Convert.ToInt32(reader["Members"]),
                    StatusText = Convert.ToString(reader["StatusText"]),
                    InsuranceProvider = reader["InsuranceProvider"] == DBNull.Value ? null : Convert.ToString(reader["InsuranceProvider"]),
                    PolicyNumber = reader["PolicyNumber"] == DBNull.Value ? null : Convert.ToString(reader["PolicyNumber"]),
                    CoverageStartDate = Convert.ToDateTime(reader["CoverageStart"]),
                    CoverageEndDate = Convert.ToDateTime(reader["CoverageEnd"]),

                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<BindingList<InsuranceListDto>> GetInsurances()
        {
            BindingList<InsuranceListDto> list = new BindingList<InsuranceListDto>();
            string query = @"select * from vw_InsuranceList";

            try
            {
                list = await DataAccessHelper.ExecuteObjectList(query, null, InsuranceListMapper);
            }
            catch (Exception ex)
            {
                throw;
            }

            return list;
        }
    }
}