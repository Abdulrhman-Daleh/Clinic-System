using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem.Payment.Controls
{
    public partial class ViewPaymentCtrl : UserCtrlDefaultSettings
    {
        private Business.Payment payment;
        public Business.Payment CurrentPayment { get { return payment; } }
        public ViewPaymentCtrl()
        {
            InitializeComponent();
            CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblAmount, lblAppliedTo, lblPaymentDate ,lblProcessedBy, lblStatus
            }, "Not Set");
        }

        public async Task LoadPaymentInfo(int paymentId)
        {
            payment = await Business.Payment.FindPaymentByID(paymentId);

            if (ValidationHelper.IsEmptyOrNull(payment, "Payment is not found"))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblAmount, lblAppliedTo, lblPaymentDate ,lblProcessedBy, lblStatus
            }, "Not Set");
                return;
            }

            lblAmount.Text = $"{payment.PaymentAmount} $";
            lblAppliedTo.Text = payment.GetAppliedTo();
            lblPaymentDate.Text = payment.PaymentDate.ToShortDateString();
            lblProcessedBy.Text = payment.ProccessBy.GetFullName();
            lblStatus.Text = payment.PaymentStatus.ToString();
        }
    }
}
