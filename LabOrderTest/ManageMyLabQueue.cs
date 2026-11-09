using Business;
using ClinicSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.LabOrderTest
{
    public partial class ManageMyLabQueue : FormsDefaultSettings
    {
        BindingList<LabTestQueueListing> labTests =
            new BindingList<LabTestQueueListing>();

        public ManageMyLabQueue()
        {
            InitializeComponent();
        }

        private async void ManageMyLabQueue_Load(object sender, EventArgs e)
        {
            cbFilterBy.Items.AddRange(new[]
            {
                "None",
                "LabID",
                "TestName",
                "PatientName",
                "MedicalNumber",
                "TestStatusText"
            });

            cbFilterBy.SelectedIndex = 0;
            txtFilter.Text = string.Empty;

            labTests = await LabTestQueueListing.GetMyLabQueue();

            CommonOperations.LoadDataToDGV(
                dgvLabQueue, labTests, lblRecords);
        }

        private void btnClose_Click(object sender, EventArgs e)
            => Close();

        private void cbFilterBy_KeyPress(
            object sender, KeyPressEventArgs e)
            => ValidationHelper.LockComboBox(e);

        private void cmsLabQueue_Opening(
            object sender, CancelEventArgs e)
            => viewLabTestToolStripMenuItem.Enabled =
                !ValidationHelper.IsDgvEmptyOrNull(dgvLabQueue);

        private void dgvLabQueue_CellDoubleClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvLabQueue.CurrentRow == null)
                return;

            ViewSelectedLabTest();
        }

        private void RefreshFilter(int index)
        {
            cbFilterBy.SelectedIndex = index;

            CommonOperations.HandleSwitchingFilter(
                cbFilterBy, null, txtFilter, dgvLabQueue,
                labTests, lblRecords, null);

            ValidationHelper.SetApproperiteMaxLength(
                txtFilter,
                new List<string> { "LabID" },
                CommonOperations.GetFilterName(cbFilterBy),
                5,
                40);
        }

        private void cbFilterBy_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void txtFilter_TextChanged(
            object sender, EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            object value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(
                dgvLabQueue, labTests, lblRecords, value.ToString()))
            {
                return;
            }

            List<object> temp = CommonOperations.FilterData(
                labTests, filterName, value);

            CommonOperations.LoadDataToDGV(
                dgvLabQueue, temp, lblRecords);
        }

        private void viewLabTestToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            ViewSelectedLabTest();
        }

        private void ViewSelectedLabTest()
        {
            if (ValidationHelper.IsDgvEmptyOrNull(dgvLabQueue))
                return;

            int labTestId =
                (int)dgvLabQueue.CurrentRow.Cells["LabID"].Value;

            ViewLabTestInfo viewLabTest = new ViewLabTestInfo(labTestId);
            viewLabTest.ShowDialog();
        }
    }
}