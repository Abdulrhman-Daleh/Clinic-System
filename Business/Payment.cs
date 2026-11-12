using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;

namespace Business
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int? AppointmentID { get; set; }
        public int? PenaltyID { get; set; }
        public (EnPaymentStatuses PaymentStatusID, string PaymentStatusText) PaymentStatus { get; set; }
        public int ProccessByID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public Appointment Appointment { get; set; }
        public Staff ProccessBy { get; set; }
        public static PaymentDto ToDTO(Payment payment)
        {
            if (payment == null)
                return new PaymentDto();

            return new PaymentDto()
            {
                AppointmentID = payment.AppointmentID,
                PenaltyID = payment.PenaltyID,
                PaymentStatus = payment.PaymentStatus,
                ProccessByID = payment.ProccessByID,
                PaymentDate = payment.PaymentDate,
                PaymentAmount = payment.PaymentAmount,
            };
        }
        public Payment()
        {
            PaymentID = -1;
            AppointmentID = null;
            PenaltyID = null;
            PaymentStatus = ((EnPaymentStatuses)0, "None");
            ProccessBy = null;
            ProccessByID = -1;
            PaymentAmount = 0;
            PaymentDate = DateTime.MinValue;
        }
        private bool AddNewPayment()
        {
            PaymentID = PaymentData.AddNewPayment(ToDTO(this));

            return PaymentID > 0;
        }
        public OperationResult Save()
        {
            OperationResult result = IsPaymentValidtoSave();

            if (!result.IsSuccess)
                return result;

            result = Util.CanAccessForm(EnRole.Receptionist, EnRole.Doctor);

            if (!result.IsSuccess)
                return result;

            if (AddNewPayment())
            {

                Auditor.Log(
                    "Payment Added",
                    $"Payment '{PaymentID}' for AppointmentID {AppointmentID} (Status: {PaymentStatus.PaymentStatusText}) was added by {ProccessBy.GetFullName()}.",
                    $"LabID {PaymentID}");

                return new OperationResult(
                    "Payment was added successfully.",
                    "Added",
                    true);
            }

            return new OperationResult(
                        "Failed to add payment.",
                        "Failed",
                        false);
        }
        private OperationResult IsPaymentValidtoSave()
        {
            if (!Util.IsValidId(this.AppointmentID ?? -1) && !Util.IsValidId(this.PenaltyID ?? -1))
                return new OperationResult("Payment is not valid", "Not Valid", false);

            if (!Util.IsValidId(ProccessByID) || !(PaymentAmount > 0) || Appointment == null || ProccessBy == null)
                return new OperationResult("Payment is not valid to save", "Not Valid", false);

            return new OperationResult("Payment is valid", "Valid", true);

        }
    }
}
