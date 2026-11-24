using AppEnums.cs;
using Data_Access.DTOs;
using Data_Access.ListingDTOs;
using Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data_Access
{
    public class PaymentData
    {
        public static int AddNewPayment(PaymentDto payment)
        {
            string query = @"insert into Payments (AppointmentID, PenaltyID, PaymentStatusID, ProccessByID, PaymentDate, PaymentAmount)
            values (@AppointmentId, @PenaltyId, @PaymentStatusId, @ProccessById, GetDate(), @PaymentAmount);
            select SCOPE_IDENTITY();";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@AppointmentId", SqlDbType.Int) {Value = payment.AppointmentID ?? (object)DBNull.Value},
                new SqlParameter("@PenaltyId", SqlDbType.Int) {Value = payment.PenaltyID ?? (object)DBNull.Value},
                new SqlParameter("@PaymentStatusId", SqlDbType.Int) {Value = (int)payment.PaymentStatus },
                new SqlParameter("@ProccessById", SqlDbType.Int) {Value = payment.ProccessByID },
                new SqlParameter("@PaymentAmount", SqlDbType.Decimal) {Value = payment.PaymentAmount }
            };

            object obj = DataAccessHelper.ExecuteScalar(query, parameters);

            if (obj != null && int.TryParse(obj.ToString(), out int id))
                return id;

            return -1;
        }
        private static PaymentListDto PaymentListMapper(SqlDataReader reader)
        {
            try
            {
                return new PaymentListDto()
                {
                    PaymentID = (int)reader["PaymentID"],
                    Patient = (string)reader["Patient"],
                    Doctor = (string)reader["Doctor"],
                    AppointmentID = (int)reader["AppointmentID"],
                    Amount = (decimal)reader["PaymentAmount"],
                    ProceedBy = (string)reader["ProceedBy"],
                    TotalPayments = (int)reader["TotalPayments"],
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "PaymentData", "Application", System.Diagnostics.EventLogEntryType.Error));
                return null;
            }
        }
        public static async Task<BindingList<PaymentListDto>> GetAllPayments()
        {
            string query = @"
            select PaymentID,
                   concat(patientPerson.Firstname, ' ', patientPerson.Lastname) as Patient,
                   concat(doctorPerson.Firstname, ' ', doctorPerson.Lastname) as Doctor,
                   appo.AppointmentID,
                   pay.PaymentAmount,
                   concat(RecordBy.Firstname, ' ', RecordBy.Lastname) as ProceedBy,
                   count(*) over (partition by pay.AppointmentID) as TotalPayments
            From Payments as pay
            inner join Appointments as appo on appo.AppointmentID = pay.AppointmentID
            inner join Patients as p on p.PatientID = appo.PatientID
            inner join Staff as s on s.StaffID = appo.StaffID
            inner join People as patientPerson on patientPerson.PersonID = p.PersonID
            inner join People as doctorPerson on doctorPerson.PersonID = s.PersonID
            inner join People as RecordBy on RecordBy.PersonID = pay.ProccessByID";

            return await DataAccessHelper.ExecuteObjectList(query, null, PaymentListMapper);
        }
        private static PaymentDto PaymentMapper(SqlDataReader reader)
        {
            try
            {
                return new PaymentDto()
                {
                    PaymentID = (int)reader["PaymentID"],
                    AppointmentID = reader["AppointmentID"] == DBNull.Value ? null : (int?)reader["AppointmentID"],
                    PaymentAmount = (decimal)reader["PaymentAmount"],
                    ProccessByID = (int)reader["ProccessByID"],
                    PaymentDate = (DateTime)reader["PaymentDate"],
                    PenaltyID = reader["PenaltyID"] == DBNull.Value ? null : (int?)reader["PenaltyID"],
                    PaymentStatus = (EnPaymentStatuses)reader["PaymentStatus"],
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "PaymentData", "Application", System.Diagnostics.EventLogEntryType.Error));
                return null;
            }

        }
        public static async Task<PaymentDto> FindPaymentByID(int paymentID)
        {
            string query = @"";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@PaymentId", SqlDbType.Int) { Value = paymentID }
            };

            return await DataAccessHelper.ExecuteOneObject(query, parameters, PaymentMapper);
        }

    }
}
