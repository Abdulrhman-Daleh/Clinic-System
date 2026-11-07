
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
    public class PrescriptionData
    {
        private static PrescriptionDto PrescriptionMapper(SqlDataReader reader)
        {
            try
            {
                return new PrescriptionDto()
                {
                    PrescriptionID = (int)reader["PrescriptionID"],
                    AppointmentID = (int)reader["AppointmentID"],
                    TotalPrice = (decimal)reader["TotalPrice"],
                    DoctorDidSign = (bool)reader["DoctorDidSign"],
                    WriteDate = (DateTime)reader["WriteDate"],
                    ExpirationDate = (DateTime)reader["ExpirationDate"]
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(
                    ex.Message,
                    "PrescriptionData",
                    "Application",
                    EventLogEntryType.Error));

                return null;
            }
        }

        public static int AddNewPrescription(PrescriptionDto prescriptionDto, List<PrescriptionItemDto> items)
        {
            int prescriptionId = -1;

            DataTable dt = new DataTable();
            dt.Columns.Add("medicineid", typeof(int));
            dt.Columns.Add("dose", typeof(byte));
            dt.Columns.Add("frequency", typeof(string));
            dt.Columns.Add("quantity", typeof(byte));
            dt.Columns.Add("duration", typeof(string));

            foreach (var item in items)
                dt.Rows.Add(new object[] { item.MedicineID, item.Dose, item.Frequency, item.Quantity, item.Duration });

            string query = "AddNewPrescription";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@PrescriptionItems", SqlDbType.Structured)
                {
                    Value = dt
                },
                new SqlParameter("@AppointmentID", SqlDbType.Int)
                {
                    Value = prescriptionDto.AppointmentID
                },
                new SqlParameter("@DoctorDidSign", SqlDbType.Bit)
                {
                    Value = prescriptionDto.DoctorDidSign
                },
                new SqlParameter("@TotalPrice", SqlDbType.Decimal)
                {
                    Value = prescriptionDto.TotalPrice
                },
                new SqlParameter("@ExpirationDate", SqlDbType.DateTime)
                {
                    Value = prescriptionDto.ExpirationDate
                }
            };

            List<SqlParameter> outputParameters = new List<SqlParameter>()
            {
                new SqlParameter("@PrescriptionID", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };

            bool result = DataAccessHelper.ExecuteNonQueryStoredProcedure<PrescriptionDto>(query, parameters, outputParameters);


            if (result)
                prescriptionId = Convert.ToInt32(outputParameters[0].Value ?? -1);

            return prescriptionId;
        }

        public static bool UpdatePrescription(PrescriptionDto prescriptionDto)
        {
            string query = @"
                UPDATE Prescriptions SET
                    TotalPrice = @TotalPrice,
                    DoctorDidSign = @DoctorDidSign,
                    ExpirationDate = @ExpirationDate
                WHERE PrescriptionID = @PrescriptionID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@PrescriptionID", SqlDbType.Int)
                {
                    Value = prescriptionDto.PrescriptionID
                },
                new SqlParameter("@TotalPrice", SqlDbType.Decimal)
                {
                    Value = prescriptionDto.TotalPrice
                },
                new SqlParameter("@DoctorDidSign", SqlDbType.Bit)
                {
                    Value = prescriptionDto.DoctorDidSign
                },
                new SqlParameter("@ExpirationDate", SqlDbType.DateTime)
                {
                    Value = prescriptionDto.ExpirationDate
                }
            };

            int affectedRows = DataAccessHelper.ExecuteNonQuery(query, parameters);

            return affectedRows > 0;
        }

        public static PrescriptionDto FindPrescriptionByID(int prescriptionID)
        {
            string query = @"
                SELECT
                    PrescriptionID,
                    AppointmentID,
                    TotalPrice,
                    DoctorDidSign,
                    WriteDate,
                    ExpirationDate
                FROM Prescriptions
                WHERE PrescriptionID = @PrescriptionID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@PrescriptionID", SqlDbType.Int)
                {
                    Value = prescriptionID
                }
            };

            return DataAccessHelper.ExecuteOneObject(
                query,
                parameters,
                PrescriptionMapper);
        }

        public static PrescriptionDto FindPrescriptionByAppointmentID(int appointmentID)
        {
            string query = @"
                SELECT
                    PrescriptionID,
                    AppointmentID,
                    TotalPrice,
                    DoctorDidSign,
                    WriteDate,
                    ExpirationDate
                FROM Prescriptions
                WHERE AppointmentID = @AppointmentID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AppointmentID", SqlDbType.Int)
                {
                    Value = appointmentID
                }
            };

            return DataAccessHelper.ExecuteOneObject(
                query,
                parameters,
                PrescriptionMapper);
        }

        public static async Task<BindingList<PrescriptionDto>> GetPrescriptions()
        {
            string query = @"
                SELECT
                    PrescriptionID,
                    AppointmentID,
                    TotalPrice,
                    DoctorDidSign,
                    WriteDate,
                    ExpirationDate
                FROM Prescriptions";

            return await DataAccessHelper.ExecuteObjectList(
                query,
                null,
                PrescriptionMapper);
        }
    }
}
