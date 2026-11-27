using Business.ListingClasses;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.Penaltes
{
    public partial class ManagePenalties : FormsDefaultSettings
    {
        BindingList<PenaltyList> Penalties { get; set; }
        private string FilterName { get; set; }
        public ManagePenalties()
        {
            InitializeComponent();
        }

        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;

            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvPenalties, Penalties, lblRecords, null);

            ValidationHelper.SetApproperiteMaxLength(
                txtFilter,
                new List<string> { "PenaltyID", "AppointmentID" },
                CommonOperations.GetFilterName(cbFilterBy),
                5, 50);

            FilterName = CommonOperations.GetFilterName(cbFilterBy);
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (FilterName == "PenaltyID" || FilterName == "AppointmentID")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            string value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(dgvPenalties, Penalties, lblRecords, value))
                return;

            List<object> temp = CommonOperations.FilterData(Penalties, FilterName, value);

            CommonOperations.LoadDataToDGV(dgvPenalties, temp, lblRecords);
        }
    }
}
