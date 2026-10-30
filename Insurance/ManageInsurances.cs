using Business;
using ClinicSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ClinicSystem.Insurance
{
    public partial class ManageInsurances : FormsDefaultSettings
    {
        private BindingList<InsuranceList> Insurances { get; set; }

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

            RefreshFilter(0);
        }

        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;

            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvInsurances, Insurances, lblRecords, null);

            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "#", "Members" }, CommonOperations.GetFilterName(cbFilterBy),
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
            SetUp();
            LoadInsurancesInfo();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);

            if (filterName == "PolicyNumber")
            {
                ValidationHelper.HandleInputType(e, true, true);
                return;
            }

            if (filterName == "#" ||
                filterName == "Members")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            object value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(
                dgvInsurances, Insurances, lblRecords, value.ToString()))
            {
                return;
            }

            if (filterName == "#")
                filterName = "InsuranceID";

            List<object> temp = CommonOperations.FilterData(
                Insurances, filterName, value);

            CommonOperations.LoadDataToDGV(dgvInsurances, temp, lblRecords);
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
            if (ValidationHelper.IsDgvEmptyOrNull(dgvInsurances))
                return;

            int insuranceId =
                (int)dgvInsurances.CurrentRow.Cells["InsuranceID"].Value;

            AddUpdateInsurance updateInsurance =
                new AddUpdateInsurance(insuranceId);
            updateInsurance.SendInsuranceId += RefreshDgv;
            updateInsurance.ShowDialog();

        }

        private void RefreshDgv(int InsuranceId)
        {
            int targetIndex = -1;
            InsuranceList Insurance = InsuranceList.FindInsurancebyID(InsuranceId);

            if (Insurances.Any(x => x.InsuranceID == Insurance.InsuranceID))
                targetIndex = Insurances.IndexOf(Insurances.First(x => x.InsuranceID == Insurance.InsuranceID));

            CommonOperations.UpdateAfterAdding(Insurances, Insurance, targetIndex);
        }

        private void addNewInsuranceToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            AddUpdateInsurance addInsurance = new AddUpdateInsurance();
            addInsurance.SendInsuranceId += RefreshDgv;
            addInsurance.ShowDialog();
        }

        private void updateInsuranceToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
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