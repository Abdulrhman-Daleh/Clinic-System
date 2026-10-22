using Data_Access.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data_Access
{
    public class InsuranceData
    {
        public static (int InsuranceId, int PolicyId) AddNewInsurance(InsuranceDto insuranceDto)
        {
            (int InsuranceId, int PolicyId) result = (-1, -1);

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@RecordedBy", insuranceDto.RecordedBy),
                    new SqlParameter("@InsuranceTypeId", insuranceDto.InsuranceTypeId),
                    new SqlParameter("@InsuranceStatusId", insuranceDto.InsuranceStatusId),
                    new SqlParameter("@UploadedDate", insuranceDto.UploadedDate),
                    new SqlParameter("@InsuranceProvider", (object)insuranceDto.InsuranceProvider ?? DBNull.Value),
                    new SqlParameter("@PolicyNumber", (object)insuranceDto.PolicyNumber ?? DBNull.Value),
                    new SqlParameter("@CoverageStartDate", insuranceDto.CoverageStartDate),
                    new SqlParameter("@CoverageEndDate", insuranceDto.CoverageEndDate),
                    new SqlParameter("@PatientClaims", (object)insuranceDto.PatientClaims ?? DBNull.Value),
                    new SqlParameter("@PolicyholderPatientId", insuranceDto.PolicyholderPatientId)
                };

                List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter("@InsuranceId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    },
                    new SqlParameter("@PolicyId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                bool success = DataAccessHelper.ExecuteNonQueryStoredProcedure<InsuranceDto>(
                    "AddNewInsurance", parameters, outputParams);

                if (success)
                {
                    result.InsuranceId = Convert.ToInt32(outputParams[0].Value);
                    result.PolicyId = Convert.ToInt32(outputParams[1].Value);
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
                    new SqlParameter("@InsuranceId", insuranceDto.InsurenceId),
                    new SqlParameter("@PolicyId", insuranceDto.PolicyId),
                    new SqlParameter("@RecordedBy", insuranceDto.RecordedBy),
                    new SqlParameter("@InsuranceTypeId", insuranceDto.InsuranceTypeId),
                    new SqlParameter("@InsuranceStatusId", insuranceDto.InsuranceStatusId),
                    new SqlParameter("@UploadedDate", insuranceDto.UploadedDate),
                    new SqlParameter("@InsuranceProvider", (object)insuranceDto.InsuranceProvider ?? DBNull.Value),
                    new SqlParameter("@PolicyNumber", (object)insuranceDto.PolicyNumber ?? DBNull.Value),
                    new SqlParameter("@CoverageStartDate", insuranceDto.CoverageStartDate),
                    new SqlParameter("@CoverageEndDate", insuranceDto.CoverageEndDate),
                    new SqlParameter("@PatientClaims", (object)insuranceDto.PatientClaims ?? DBNull.Value),
                    new SqlParameter("@PolicyholderPatientId", insuranceDto.PolicyholderPatientId)
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
                string query = @"SELECT *
                                 FROM Insurances
                                 WHERE InsuranceId = @InsuranceId";

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
                string query = @"SELECT *
                                 FROM Insurances
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
                    InsurenceId = Convert.ToInt32(reader["InsuranceId"]),
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
    }
}