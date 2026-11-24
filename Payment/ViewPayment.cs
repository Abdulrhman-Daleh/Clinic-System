using ClinicSystem.Helpers;

namespace ClinicSystem.Payment
{
    public partial class ViewPayment : FormsDefaultSettings
    {
        private readonly int paymentId;
        public ViewPayment(int paymentId)
        {
            InitializeComponent();
            this.paymentId = paymentId;
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private async void ViewPayment_Load(object sender, System.EventArgs e)
        {
            await viewPaymentCtrl1.LoadPaymentInfo(paymentId);

            Business.Payment payment = viewPaymentCtrl1.CurrentPayment;

            if (ValidationHelper.IsEmptyOrNull(payment, "Payment is no longer available"))
                return;

            await viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(payment.AppointmentID ?? -1);

            if (ValidationHelper.IsEmptyOrNull(viewAppointmentInfoCtrl1.CurrentAppointment, "appointment is no longer available"))
                return;

            viewStaffInfoCtrl1.LoadStaffInfo(viewAppointmentInfoCtrl1.CurrentAppointment.Staff);
            lblSubTitle.Text = $"Payment #{payment.PaymentID} · Appt #{payment.AppointmentID} · {payment.PaymentDate.ToShortDateString()}";
        }
    }
}
