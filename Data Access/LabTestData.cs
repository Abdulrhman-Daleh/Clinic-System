using AppEnums.cs;
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
    public class LabTestData
    {
        private static LabTestDto LabTestMapper(SqlDataReader reader)
        {
            try
            {
                return new LabTestDto()
                {
                    LabID = (int)reader["LabID"],
                    AppointmentID = (int)reader["AppointmentID"],

                    TestStatuses = (
                        (EnTestStatuses)reader["TestStatusID"],
                        (string)reader["TestStatusText"]),

                    NurseID = (int)reader["NurseID"],
                    Reason = reader["Reason"] == DBNull.Value ? null : (string)reader["Reason"],
                    Instructions = reader["Instructions"] == DBNull.Value ? null : (string)reader["Instructions"],
                    TestResult = reader["TestResult"] == DBNull.Value ? null : (string)reader["TestResult"],
                    TestName = (string)reader["TestName"],
                    RequestDate = (DateTime)reader["RequestDate"],
                    PerformDate = reader["PerformDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["PerformDate"],
                    ResultRecordedAt = reader["ResultRecordedAt"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["ResultRecordedAt"]
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(
                    ex.Message,
                    "LabTestData",
                    "Application",
                    EventLogEntryType.Error));

                return null;
            }
        }

        private static string TestStatusMapper(SqlDataReader reader)
        {
            return (string)reader["TestStatusText"];
        }

        public static int AddNewLabTest(LabTestDto labTestDto)
        {
            int labID = -1;

            string query = @"
                INSERT INTO LabTests
                (
                    AppointmentID,
                    TestStatusID,
                    NurseID,
                    Reason,
                    Instructions,
                    TestResult,
                    TestName,
                    RequestDate,
                    PerformDate,
                    ResultRecordedAt
                )
                VALUES
                (
                    @AppointmentID,
                    @TestStatusID,
                    @NurseID,
                    @Reason,
                    @Instructions,
                    @TestResult,
                    @TestName,
                    @RequestDate,
                    @PerformDate,
                    @ResultRecordedAt
                );

                SELECT SCOPE_IDENTITY();";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AppointmentID", SqlDbType.Int)
                {
                    Value = labTestDto.AppointmentID
                },
                new SqlParameter("@TestStatusID", SqlDbType.Int)
                {
                    Value = (int)labTestDto.TestStatuses.TestStatusID
                },
                new SqlParameter("@NurseID", SqlDbType.Int)
                {
                    Value = labTestDto.NurseID
                },
                new SqlParameter("@Reason", SqlDbType.VarChar, -1)
                {
                     Value = string.IsNullOrEmpty(labTestDto.Reason) ? (object)DBNull.Value : labTestDto.Reason
                },
                new SqlParameter("@Instructions", SqlDbType.VarChar, 200)
                {
                     Value = string.IsNullOrEmpty(labTestDto.Instructions) ? (object)DBNull.Value : labTestDto.Instructions
                },
                new SqlParameter("@TestResult", SqlDbType.VarChar, 500)
                {
                    Value = string.IsNullOrEmpty(labTestDto.TestResult) ? (object)DBNull.Value : labTestDto.TestResult
                },
                new SqlParameter("@TestName", SqlDbType.VarChar, 100)
                {
                    Value = labTestDto.TestName
                },
                new SqlParameter("@RequestDate", SqlDbType.DateTime)
                {
                    Value = labTestDto.RequestDate
                },
                new SqlParameter("@PerformDate", SqlDbType.DateTime)
                 {
                     Value = labTestDto.PerformDate == null
                         ? (object)DBNull.Value
                         : labTestDto.PerformDate
                 },
                 new SqlParameter("@ResultRecordedAt", SqlDbType.DateTime)
                 {
                     Value = labTestDto.ResultRecordedAt == null
                         ? (object)DBNull.Value
                         : labTestDto.ResultRecordedAt
                 }
            };

            object result = DataAccessHelper.ExecuteScalar(query, parameters);

            if (result != null && result != DBNull.Value)
                labID = Convert.ToInt32(result);

            return labID;
        }

        public static bool UpdateLabTest(LabTestDto labTestDto)
        {
            string query = @"
                UPDATE LabTests SET
                    TestStatusID = @TestStatusID,
                    NurseID = @NurseID,
                    Reason = @Reason,
                    Instructions = @Instructions,
                    TestResult = @TestResult,
                    TestName = @TestName,
                    PerformDate = @PerformDate,
                    ResultRecordedAt = @ResultRecordedAt
                WHERE LabID = @LabID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@LabID", SqlDbType.Int)
                {
                    Value = labTestDto.LabID
                },
                new SqlParameter("@TestStatusID", SqlDbType.Int)
                {
                    Value = (int)labTestDto.TestStatuses.TestStatusID
                },
                new SqlParameter("@NurseID", SqlDbType.Int)
                {
                    Value = labTestDto.NurseID
                },
                new SqlParameter("@Reason", SqlDbType.VarChar, -1)
                {
                     Value = string.IsNullOrEmpty(labTestDto.Reason) ? (object)DBNull.Value : labTestDto.Reason
                },
                new SqlParameter("@Instructions", SqlDbType.VarChar, 200)
                {
                     Value = string.IsNullOrEmpty(labTestDto.Instructions) ? (object)DBNull.Value : labTestDto.Instructions
                },
                new SqlParameter("@TestResult", SqlDbType.VarChar, 500)
                {
                    Value = string.IsNullOrEmpty(labTestDto.TestResult) ? (object)DBNull.Value : labTestDto.TestResult
                },
                new SqlParameter("@TestName", SqlDbType.VarChar, 100)
                {
                    Value = labTestDto.TestName
                },
                new SqlParameter("@PerformDate", SqlDbType.DateTime)
                {
                    Value = labTestDto.PerformDate == null
                        ? (object)DBNull.Value
                        : labTestDto.PerformDate
                },
                new SqlParameter("@ResultRecordedAt", SqlDbType.DateTime)
                {
                    Value = labTestDto.ResultRecordedAt == null
                        ? (object)DBNull.Value
                        : labTestDto.ResultRecordedAt
                }
            };

            int affectedRows = DataAccessHelper.ExecuteNonQuery(query, parameters);

            return affectedRows > 0;
        }

        public static LabTestDto FindLabTestByID(int labID)
        {
            string query = @"
                SELECT
                    lt.LabID,
                    lt.AppointmentID,
                    lt.TestStatusID,
                    ts.TestStatusText,
                    lt.NurseID,
                    lt.Reason,
                    lt.Instructions,
                    lt.TestResult,
                    lt.TestName,
                    lt.RequestDate,
                    lt.PerformDate,
                    lt.ResultRecordedAt
                FROM LabTests AS lt
                INNER JOIN TestStatuses AS ts
                    ON lt.TestStatusID = ts.TestStatusID
                WHERE lt.LabID = @LabID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@LabID", SqlDbType.Int)
                {
                    Value = labID
                }
            };

            return DataAccessHelper.ExecuteOneObject(
                query,
                parameters,
                LabTestMapper);
        }

        public static async Task<BindingList<LabTestDto>> GetLabTestsByAppointmentID(int appointmentID)
        {
            string query = @"
                SELECT
                    lt.LabID,
                    lt.AppointmentID,
                    lt.TestStatusID,
                    ts.TestStatusText,
                    lt.NurseID,
                    lt.Reason,
                    lt.Instructions,
                    lt.TestResult,
                    lt.TestName,
                    lt.RequestDate,
                    lt.PerformDate,
                    lt.ResultRecordedAt
                FROM LabTests AS lt
                INNER JOIN TestStatuses AS ts
                    ON lt.TestStatusID = ts.TestStatusID
                WHERE lt.AppointmentID = @AppointmentID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AppointmentID", SqlDbType.Int)
                {
                    Value = appointmentID
                }
            };

            return await DataAccessHelper.ExecuteObjectList(
                query,
                parameters,
                LabTestMapper);
        }

        public static async Task<BindingList<LabTestDto>> GetLabTests()
        {
            string query = @"
                SELECT
                    lt.LabID,
                    lt.AppointmentID,
                    lt.TestStatusID,
                    ts.TestStatusText,
                    lt.NurseID,
                    lt.Reason,
                    lt.Instructions,
                    lt.TestResult,
                    lt.TestName,
                    lt.RequestDate,
                    lt.PerformDate,
                    lt.ResultRecordedAt
                FROM LabTests AS lt
                INNER JOIN TestStatuses AS ts
                    ON lt.TestStatusID = ts.TestStatusID";

            return await DataAccessHelper.ExecuteObjectList(
                query,
                null,
                LabTestMapper);
        }

        public static async Task<BindingList<string>> GetTestStatusesNames()
        {
            string query = @"
                SELECT TestStatusText
                FROM TestStatuses
                ORDER BY TestStatusID";

            return await DataAccessHelper.ExecuteObjectList(
                query,
                null,
                TestStatusMapper);
        }
    }
}
