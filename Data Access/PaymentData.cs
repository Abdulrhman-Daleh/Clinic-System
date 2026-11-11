using Data_Access.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
         new SqlParameter("@AppointmentId", System.Data.SqlDbType.Int) {Value = payment.AppointmentID ?? (object)DBNull.Value},
         new SqlParameter("@PenaltyId", System.Data.SqlDbType.Int) {Value = payment.PenaltyID ?? (object)DBNull.Value},
         new SqlParameter("@PaymentStatusId", System.Data.SqlDbType.Int) {Value = payment.PaymentStatus.PaymentStatusID },
         new SqlParameter("@ProccessById", System.Data.SqlDbType.Int) {Value = payment.ProccessByID },
         new SqlParameter("@PaymentAmount", System.Data.SqlDbType.Decimal) {Value = payment.PaymentAmount }
    };

            object obj = DataAccessHelper.ExecuteScalar(query, parameters);

            if (obj != null && int.TryParse(obj.ToString(), out int id))
                return id;

            return -1;
        }
    }
}
