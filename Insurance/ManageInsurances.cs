
using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.Insurance
{
    public partial class ManageInsurances : FormsDefaultSettings
    {
        private BindingList<InsuranceList> Insurances { get; set; }
        private string FilterName { get; set; }
        public ManageInsurances()
        {
            InitializeComponent();
        }
        private async void LoadInsurancesInfo()
        {
            Insurances = await InsuranceList.GetInsurances();

            if (CommonOperations.LoadDataToDGV(dgvInsurances, Insurances, lblRecords))
            {
                dgvInsurances.Columns["InsuranceID"].HeaderText = "#";
            }

            UpdateInsuranceSummary();
            RefreshFilter(0);
        }

        private void UpdateInsuranceSummary()
        {
            (int InsuranceCount, int ActiveCount, int ExpiredCount) ins = InsuranceList.InsuranceSummary;
            lblSubTitle.Text = $"Insurance — {ins.InsuranceCount} Insurance · {ins.ActiveCount} active · {ins.ExpiredCount} expired";
        }
        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;

            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvInsurances, Insurances, lblRecords, null);

            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "#", "Members" }, FilterName,
                5, 25);
        }
        private void SetUp()
        {
            cbFilterBy.Items.AddRange(new object[]
            {
                "None",
                "#",
                "HolderName",
                "InsuranceProvider",
                "PolicyNumber",
                "Members",
                "StatusText"
            });
        }
        private void ManageInsurances_Load(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
            {
                Close();
                return;
            }

            SetUp();
            LoadInsurancesInfo();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterName = CommonOperations.GetFilterName(cbFilterBy);
            RefreshFilter(cbFilterBy.SelectedIndex);
        }
        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (FilterName == "PolicyNumber")
            {
                ValidationHelper.HandleInputType(e, true, true);
                return;
            }

            if (FilterName == "InsuranceID" ||
                FilterName == "Members")
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
                if (CommonOperations.ResetDataGrid(dgvInsurances, Insurances, lblRecords, value.ToString()))
                    return;

                List<object> temp = CommonOperations.FilterData(Insurances, FilterName, value);
                CommonOperations.LoadDataToDGV(dgvInsurances, temp, lblRecords);
            }
        }
        private void SetControlEnableStatus(bool status)
        {
            updateInsuranceToolStripMenuItem.Enabled = status;
            viewInsuranceToolStripMenuItem.Enabled = status;
        }
        private void cmsInsurances_Opening(object sender, CancelEventArgs e)
        {
            SetControlEnableStatus(
                !ValidationHelper.IsDgvEmptyOrNull(dgvInsurances));
        }
        private void dgvInsurances_CellDoubleClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvInsurances))
                return;

            int insuranceId =
                (int)dgvInsurances.CurrentRow.Cells["InsuranceID"].Value;

            AddUpdateInsurance updateInsurance =
                new AddUpdateInsurance(insuranceId);
            updateInsurance.SendInsuranceId += RefreshDgv;
            updateInsurance.ShowDialog();

        }
        private async void RefreshDgv(int InsuranceId)
        {
            InsuranceList Insurance = await InsuranceList.FindInsurancebyID(InsuranceId);
            int targetIndex = CommonOperations.FindTargetIndexOfObj(Insurances, "InsuranceID", Insurance.InsuranceID);
            CommonOperations.UpdateAfterAdding(Insurances, Insurance, targetIndex);

            lblRecords.Text = $"{dgvInsurances.Rows.Count} records";
        }
        private void addNewInsuranceToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            AddUpdateInsurance addInsurance = new AddUpdateInsurance();
            addInsurance.SendInsuranceId += RefreshDgv;
            addInsurance.ShowDialog();
        }
        private void updateInsuranceToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvInsurances))
                return;

            int insuranceId =
                (int)dgvInsurances.CurrentRow.Cells["InsuranceID"].Value;

            AddUpdateInsurance updateInsurance =
                new AddUpdateInsurance(insuranceId);
            updateInsurance.SendInsuranceId += RefreshDgv;
            updateInsurance.ShowDialog();

        }
        private void viewInsuranceToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvInsurances))
                return;

            int insuranceId =
                (int)dgvInsurances.CurrentRow.Cells["InsuranceID"].Value;


            ViewInsuranceInfo viewInsurance = new ViewInsuranceInfo(insuranceId);
            viewInsurance.OnChange += RefreshDgv;
            viewInsurance.ShowDialog();

        }
        private void btnClose_Click(object sender, EventArgs e) => Close();
    }
}
