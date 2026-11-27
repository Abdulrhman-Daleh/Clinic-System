using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClinicSystem.Payment
{
    public partial class ManagePayments : FormsDefaultSettings
    {
        BindingList<PaymentList> payments = new BindingList<PaymentList>();
        private string FilterName { get; set; }
        public ManagePayments()
        {
            InitializeComponent();
        }
        private void cbFilterBy_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) => ValidationHelper.LockComboBox(e);
        private void SetToolTipControlStatus(bool status)
        {
            viewPaymentToolStripMenuItem.Enabled = status;
        }
        private void cmsPayments_Opening(object sender, CancelEventArgs e)
        {
            SetToolTipControlStatus(!ValidationHelper.IsDgvEmptyOrNull(dgvPayments));
        }
        private void btnClose_Click(object sender, System.EventArgs e) => Close();
        private async void ManagePayments_Load(object sender, System.EventArgs e)
        {
            cbFilterBy.Items.AddRange(new object[]
            { "None", "PaymentID", "AppointmentID","Patient", "Doctor" });

            payments = await PaymentList.GetPaymentLists();
            cbFilterBy.SelectedIndex = 0;
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FilterName = CommonOperations.GetFilterName(cbFilterBy);
            CommonOperations.ResetDataGrid(dgvPayments, payments, lblRecords, txtFilter.Text.Trim());

            ValidationHelper.SetApproperiteMaxLength(txtFilter, new System.Collections.Generic.List<string>() { "PaymentID", "AppointmentID" }, FilterName, 5, 50);

            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvPayments, payments, lblRecords, null);
        }
        private void txtFilter_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (FilterName == "PaymentID" || FilterName == "AppointmentID")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);
        }
        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            string value = txtFilter.Text.Trim();

            List<object> temp = CommonOperations.FilterData(payments, FilterName, value);

            if (CommonOperations.ResetDataGrid(dgvPayments, payments, lblRecords, value))
                return;

            CommonOperations.LoadDataToDGV(dgvPayments, temp, lblRecords);
        }
    }
}
