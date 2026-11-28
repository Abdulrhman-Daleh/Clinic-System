using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

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
        private string PreviousText { get; set; } = string.Empty;
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                PerformFilter();
            }
        }
        private void PerformFilter()
        {
            string value = txtFilter.Text.Trim();
            if (PreviousText != value)
            {
                PreviousText = value;

                List<object> temp = CommonOperations.FilterData(payments, FilterName, value);

                if (CommonOperations.ResetDataGrid(dgvPayments, payments, lblRecords, value))
                    return;

                CommonOperations.LoadDataToDGV(dgvPayments, temp, lblRecords);
            }
        }
    }
}
