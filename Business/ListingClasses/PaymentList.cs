using Data_Access;
using Data_Access.ListingDTOs;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class PaymentList
    {
        public int PaymentID { get; set; }
        public string Patient { get; set; }
        public string Doctor { get; set; }
        public int AppointmentID { get; set; }
        public decimal Amount { get; set; }
        public string ProceedBy { get; set; }
        public int TotalPayments { get; set; }

        private static PaymentList FromDto(PaymentListDto paymentDto)
        {
            if (paymentDto == null) { return null; }

            return new PaymentList()
            {
                PaymentID = paymentDto.PaymentID,
                Patient = paymentDto.Patient,
                Doctor = paymentDto.Doctor,
                AppointmentID = paymentDto.AppointmentID,
                Amount = paymentDto.Amount,
                ProceedBy = paymentDto.ProceedBy,
                TotalPayments = paymentDto.TotalPayments
            };
        }

        public static async Task<BindingList<PaymentList>> GetPaymentLists()
        {
            BindingList<PaymentList> payments = new BindingList<PaymentList>();

            foreach (var dto in await PaymentData.GetAllPayments())
                payments.Add(FromDto(dto));

            return payments;
        }
    }
}
