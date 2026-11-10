using AppEnums.cs;
using Data_Access.DTOs;
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
                INNER JOIN TestsStatuses AS ts
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

        public static LabTestListingDto FindLabTestListByID(int labID)
        {
            string query = @"
        SELECT
            lt.LabID,
            lt.TestName,
            ts.TestStatusText as Status,
            lt.RequestDate as Requested,
            lt.PerformDate as Performed,
            lt.ResultRecordedAt as Resulted,
            lt.TestResult as Result
        FROM LabTests AS lt
        INNER JOIN Staff AS s
            ON lt.NurseID = s.StaffID
        INNER JOIN People AS p
            ON s.PersonID = p.PersonID
        INNER JOIN TestsStatuses AS ts
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
                LabTestListingMapper);
        }

        private static LabTestListingDto LabTestListingMapper(SqlDataReader reader)
        {
            try
            {
                return new LabTestListingDto()
                {
                    LabID = (int)reader["LabID"],
                    TestName = (string)reader["TestName"],
                    NurseName = (string)reader["Nurse"],
                    TestStatusText = (string)reader["Status"],
                    RequestDate = (DateTime)reader["Requested"],
                    PerformDate = reader["Performed"] == DBNull.Value
                        ? (DateTime?)null
                        : (DateTime)reader["Performed"],
                    ResultRecordedAt = reader["Resulted"] == DBNull.Value
                        ? (DateTime?)null
                        : (DateTime)reader["Resulted"],
                    TestResult = reader["Result"] == DBNull.Value
                        ? null
                        : (string)reader["Result"]
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


        public static async Task<BindingList<LabTestListingDto>> GetLabTestsByAppointmentID(
            int appointmentID, int? doctorStaffId, int? nurseStaffId)
        {
            string query = @"
        SELECT  
            lt.LabID,
            lt.TestName,
            CONCAT(p.Firstname, ' ', p.Lastname) AS Nurse,
            ts.TestStatusText as Status,
            lt.RequestDate as Requested,
            lt.PerformDate as Performed,
            lt.ResultRecordedAt as Resulted,
            lt.TestResult as Result
        FROM LabTests AS lt
        INNER JOIN Staff AS s
            ON lt.NurseID = s.StaffID
        INNER JOIN People AS p
            ON s.PersonID = p.PersonID
        INNER JOIN TestsStatuses AS ts
            ON lt.TestStatusID = ts.TestStatusID
        WHERE lt.AppointmentID = @AppointmentID
          AND
          (
              (
                  @DoctorStaffID IS NOT NULL
                  AND EXISTS
                  (
                      SELECT 1
                      FROM Appointments A
                      WHERE A.AppointmentID = lt.AppointmentID
                        AND A.StaffID = @DoctorStaffID
                  )
              )
              OR
              (
                  @NurseStaffID IS NOT NULL
                  AND lt.NurseID = @NurseStaffID
              )
          );";

            List<SqlParameter> parameters = new List<SqlParameter>()
    {
        new SqlParameter("@AppointmentID", SqlDbType.Int)
        {
            Value = appointmentID
        },
        new SqlParameter("@DoctorStaffID", SqlDbType.Int)
        {
            Value = (object)doctorStaffId ?? DBNull.Value
        },
        new SqlParameter("@NurseStaffID", SqlDbType.Int)
        {
            Value = (object)nurseStaffId ?? DBNull.Value
        }
    };

            return await DataAccessHelper.ExecuteObjectList(
                query,
                parameters,
                LabTestListingMapper);
        }


        public static async Task<BindingList<LabTestListingDto>> GetLabTests()
        {
            string query = @"
        SELECT
            lt.LabID,
            lt.TestName,
            ts.TestStatusText as Status,
            lt.RequestDate as Requested,
            lt.PerformDate as Performed,
            lt.ResultRecordedAt as Resulted,
            lt.TestResult as Result
        FROM LabTests AS lt
        INNER JOIN Staff AS s
            ON lt.NurseID = s.StaffID
        INNER JOIN People AS p
            ON s.PersonID = p.PersonID
        INNER JOIN TestsStatuses AS ts
            ON lt.TestStatusID = ts.TestStatusID";

            return await DataAccessHelper.ExecuteObjectList(
                query,
                null,
                LabTestListingMapper);
        }

        private static LabTestQueueListingDto LabTestQueueMapper(SqlDataReader reader)
        {
            try
            {
                return new LabTestQueueListingDto()
                {
                    LabID = (int)reader["LabID"],
                    TestName = (string)reader["TestName"],
                    PatientName = (string)reader["PatientName"],
                    RequestDate = (DateTime)reader["RequestDate"],
                    TestStatusText = (string)reader["TestStatusText"]
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

        public static async Task<BindingList<LabTestQueueListingDto>> GetMyLabQueue(int nurseID)
        {
            string query = @"
        SELECT
            lt.LabID,
            lt.TestName,
            CONCAT(patientPerson.Firstname, ' ', patientPerson.Lastname) AS PatientName,
            lt.RequestDate,
            ts.TestStatusText

        FROM LabTests AS lt

        INNER JOIN Appointments AS a
            ON lt.AppointmentID = a.AppointmentID

        INNER JOIN Patients AS p
            ON a.PatientID = p.PatientID

        INNER JOIN People AS patientPerson
            ON p.PersonID = patientPerson.PersonID

        INNER JOIN TestStatuses AS ts
            ON lt.TestStatusID = ts.TestStatusID

        WHERE
            lt.NurseID = @NurseID
            AND ts.TestStatusText IN ('Requested', 'Performed')

        ORDER BY
            lt.RequestDate ASC,
            lt.LabID ASC";

            List<SqlParameter> parameters = new List<SqlParameter>()
    {
        new SqlParameter("@NurseID", SqlDbType.Int)
        {
            Value = nurseID
        }
    };

            return await DataAccessHelper.ExecuteObjectList(
                query,
                parameters,
                LabTestQueueMapper);
        }
    }
}
