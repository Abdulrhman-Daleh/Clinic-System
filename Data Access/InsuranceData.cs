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
    public class InsuranceData
    {
        public static (int InsuranceId, int PatientId) AddNewInsurance(InsuranceDto insuranceDto, PatientDto patientDto)
        {
            (int InsuranceId, int PatientId) result = (-1, -1);

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@RecordedBy", insuranceDto.RecordedBy),
                    new SqlParameter("@InsuranceTypeId", insuranceDto.InsuranceType.InsuranceTypeId),
                    new SqlParameter("@InsuranceStatusId", insuranceDto.InsuranceStatus.InsuranceStatusId),
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

            return result;
        }

        public static bool UpdateInsurance(InsuranceDto insuranceDto)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@InsuranceId", insuranceDto.InsuranceID),
                    new SqlParameter("@PolicyId", insuranceDto.PolicyId),
                    new SqlParameter("@InsuranceTypeId", insuranceDto.InsuranceType.InsuranceTypeId),
                    new SqlParameter("@InsuranceStatusId", insuranceDto.InsuranceStatus.InsuranceStatusId),
                    new SqlParameter("@InsuranceProvider", (object)insuranceDto.InsuranceProvider ?? DBNull.Value),
                    new SqlParameter("@CoverageStartDate", insuranceDto.CoverageStartDate),
                    new SqlParameter("@CoverageEndDate", insuranceDto.CoverageEndDate),
                    new SqlParameter("@PatientClaims", (object)insuranceDto.PatientClaims ?? DBNull.Value),
                    new SqlParameter("@Relationship", insuranceDto.Relationship),
                };


            return DataAccessHelper.ExecuteNonQueryStoredProcedure<InsuranceDto>(
                "UpdateInsurance", parameters, new List<SqlParameter>());
        }

        public static InsuranceDto FindInsuranceByID(int insuranceId)
        {
            string query = @"SELECT InsuranceID, RecordedBy, InsuranceTypeID, InStatus.insuranceStatusID, UploadedDate, InsuranceProvider, PolicyNumber, CoverageStartDate, CoverageEndDate,
                PatientClaims, PolicyholderPatientID, PolicyID, InsuranceTypeName, insuranceStatusText
                                 FROM Insurance as I 
                    inner join insuranceTypes as InType on I.InsuranceTypeID = InType.InsurenceTypeID
                    inner join insuranceStatuses as InStatus on I.InsuranceStatusID = InStatus.insuranceStatusID
                                 where InsuranceID = @InsuranceId;";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@InsuranceId", insuranceId)
                };

            return DataAccessHelper.ExecuteOneObject(
                query, parameters, InsuranceMapper);
        }

        public static InsuranceDto FindInsuranceByPolicyID(int policyId)
        {
            string query = @"SELECT InsuranceID, RecordedBy, InsuranceTypeID, InStatus.InsuranceStatusID, UploadedDate, InsuranceProvider, PolicyNumber, CoverageStartDate, CoverageEndDate,
                PatientClaims, PolicyholderPatientID, PolicyID, InsuranceTypeName, insuranceStatusText
                                 FROM Insurance as I 
                    inner join insuranceTypes as InType on I.InsuranceTypeID = InType.InsurenceTypeID
                    inner join insuranceStatuses as InStatus on I.InsuranceStatusID = InStatus.insuranceStatusID
                                 WHERE PolicyId = @PolicyId";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@PolicyId", policyId)
                };

            return DataAccessHelper.ExecuteOneObject(
                query, parameters, InsuranceMapper);
        }

        private static InsuranceDto InsuranceMapper(SqlDataReader reader)
        {
            try
            {
                return new InsuranceDto
                {
                    InsuranceID = (int)reader["InsuranceID"],
                    PolicyId = (int)reader["PolicyID"],
                    RecordedBy = (int)reader["RecordedBy"],
                    InsuranceType = ((EnInsuranceTypes)reader["InsuranceTypeID"], (string)reader["InsuranceTypeName"]),
                    InsuranceStatus = ((EnInsuranceStatuses)reader["InsuranceStatusID"], (string)reader["insuranceStatusText"]),
                    UploadedDate = (DateTime)reader["UploadedDate"],

                    InsuranceProvider = reader["InsuranceProvider"] == DBNull.Value
                 ? null : (string)reader["InsuranceProvider"],

                    PolicyNumber = reader["PolicyNumber"] == DBNull.Value
                 ? null : (string)reader["PolicyNumber"],

                    CoverageStartDate = (DateTime)reader["CoverageStartDate"],
                    CoverageEndDate = (DateTime)reader["CoverageEndDate"],

                    PatientClaims = reader["PatientClaims"] == DBNull.Value
                     ? null : (string)reader["PatientClaims"],

                    PolicyholderPatientId = (int)reader["PolicyholderPatientID"]
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "InsuranceData", "Application", EventLogEntryType.Error));
                return null;
            }
        }

        private static string InsuranceTypeMapper(SqlDataReader reader)
        {
            try
            {
                return (string)reader["InsuranceTypeName"];
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "InsuranceData", "Application", EventLogEntryType.Error));
                return string.Empty;
            }
        }

        private static string InsuranceStatusMapper(SqlDataReader reader)
        {
            try
            {
                return (string)reader["insuranceStatusText"];
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "InsuranceData", "Application", EventLogEntryType.Error));
                return string.Empty;
            }
        }

        public static async Task<BindingList<string>> GetInsuranceTypesNames()
        {
            BindingList<string> types = new BindingList<string>();
            string query = @"select InsuranceTypeName from insuranceTypes";

            types = await DataAccessHelper.ExecuteObjectList(query, null, InsuranceTypeMapper);

            return types;
        }

        public static async Task<BindingList<string>> GetInsuranceStatusesNames()
        {
            BindingList<string> types = new BindingList<string>();
            string query = @"select insuranceStatusText from insuranceStatuses";

            types = await DataAccessHelper.ExecuteObjectList(query, null, InsuranceStatusMapper);

            return types;
        }

        private static InsuranceListDto InsuranceListMapper(SqlDataReader reader)
        {
            try
            {
                return new InsuranceListDto
                {
                    InsuranceID = (int)reader["InsuranceID"],
                    InsuranceType = (string)reader["InsuranceType"],
                    HolderName = (string)reader["HolderName"],
                    Members = (int)reader["Members"],
                    StatusText = (string)reader["StatusText"],

                    InsuranceProvider = reader["InsuranceProvider"] == DBNull.Value
                        ? null : (string)reader["InsuranceProvider"],

                    PolicyNumber = reader["PolicyNumber"] == DBNull.Value
                     ? null : (string)reader["PolicyNumber"],

                    CoverageStartDate = (DateTime)reader["CoverageStart"],
                    CoverageEndDate = (DateTime)reader["CoverageEnd"],

                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "InsuranceData", "Application", EventLogEntryType.Error));
                return null;
            }
        }

        public static async Task<BindingList<InsuranceListDto>> GetInsurances()
        {
            BindingList<InsuranceListDto> list = new BindingList<InsuranceListDto>();
            string query = @"select * from vw_InsuranceList";

            list = await DataAccessHelper.ExecuteObjectList(query, null, InsuranceListMapper);

            return list;
        }

        public static InsuranceListDto FindInsuranceListByID(int insuranceId)
        {
            InsuranceListDto insurance = new InsuranceListDto();
            string query = @"select InsuranceID, PolicyNumber, InsuranceProvider, InsuranceType, HolderName, Members, CoverageStart, CoverageEnd, StatusText from vw_InsuranceList
	        where InsuranceID = @InsuranceId";


            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@InsuranceId", SqlDbType.Int) { Value = insuranceId },
                };

            insurance = DataAccessHelper.ExecuteOneObject(query, parameters, InsuranceListMapper);

            return insurance;
        }

        private static PatientInsuranceInfoDto PatientInsuranceMapper(SqlDataReader reader)
        {
            return new PatientInsuranceInfoDto
            {
                Name = (string)reader["Name"],
                Relationship = (string)reader["Relationship"],
                PatientSince = (DateTime)reader["PatientSince"]
            };
        }

        public static async Task<BindingList<PatientInsuranceInfoDto>> GetPatientsUnderInsurance(int insuranceId)
        {
            BindingList<PatientInsuranceInfoDto> list = new BindingList<PatientInsuranceInfoDto>();

            string query = @"select 
                    	person.Firstname + ' ' + person.Lastname as Name,
                    	PatientRelationship as Relationship,
                    	p.RegisterDate as PatientSince
                    from Patient_Insurance as i inner join Patients as p on i.PatientID = p.PatientID
                    	inner join People as person on person.PersonID = p.PersonID where i.InsuranceID = @InsuranceId";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@InsuranceId", SqlDbType.Int) {Value = insuranceId}
            };

            list = await DataAccessHelper.ExecuteObjectList(query, parameters, PatientInsuranceMapper);

            return list;
        }
    }
}