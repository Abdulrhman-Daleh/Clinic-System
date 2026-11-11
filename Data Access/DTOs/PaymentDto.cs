using AppEnums.cs;
using System;

namespace Data_Access.DTOs
{
    public class PaymentDto
    {
        public int PaymentID { get; set; }
        public int? AppointmentID { get; set; }
        public int? PenaltyID { get; set; }
        public (EnPaymentStatuses PaymentStatusID, string PaymentStatusText) PaymentStatus { get; set; }
        public int ProccessByID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}