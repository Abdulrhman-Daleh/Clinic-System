using ClinicSystem.Helpers;

namespace ClinicSystem.Payment
{
    public partial class ManagePayments : FormsDefaultSettings
    {
        public ManagePayments()
        {
            InitializeComponent();
        }

        private void cbFilterBy_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) => ValidationHelper.LockComboBox(e);

        private void SetToolTipControlStatus(bool status)
        {
            viewPaymentToolStripMenuItem.Enabled = status;
        }
        private void cmsPayments_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetToolTipControlStatus(ValidationHelper.IsDgvEmptyOrNull(dgvPayments);
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void ManagePayments_Load(object sender, System.EventArgs e)
        {
            cbFilterBy.Items.AddRange(new object[]
            { "PaymentID", "AppointmentID", });

            cbFilterBy.SelectedIndex = 0;
        }
    }
}
