using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ClinicSystem.LabOrderTest
{
    public partial class ManageLabTests : FormsDefaultSettings
    {
        BindingList<LabTestListing> labTests = new BindingList<LabTestListing>();
        public ManageLabTests()
        {
            InitializeComponent();
        }

        public ManageLabTests(int appointmentId)
        {
            InitializeComponent();
            SetLoadedAppointment(appointmentId);
        }

        private async void SetLoadedAppointment(int appointmentId)
        {
            findAppointmentCtrl1.LoadAppointmentInfo(appointmentId);
            findAppointmentCtrl1.AllowFind = false;
            var appo = findAppointmentCtrl1.CurrentAppointment;

            if (ValidationHelper.IsEmptyOrNull(appo))
                return;

            labTests = await LabTestListing.GetLabTestsByAppointmentID(appointmentId);
            CommonOperations.LoadDataToDGV(dgvLabTests, labTests, lblRecords);
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void cbFilterBy_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) => ValidationHelper.LockComboBox(e);

        private void cmsLabTests_Opening(object sender, System.ComponentModel.CancelEventArgs e)
             => viewLabTestToolStripMenuItem.Enabled = !ValidationHelper.IsDgvEmptyOrNull(dgvLabTests);

        private void dgvLabTests_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            int labTestId = (int)dgvLabTests.CurrentRow.Cells["LabID"].Value;
            ViewLabTestInfo viewLabTest = new ViewLabTestInfo(labTestId);
            viewLabTest.ShowDialog();
        }

        private void ManageLabTests_Load(object sender, System.EventArgs e)
        {
            cbFilterBy.Items.AddRange(new[] { "None", "LabID", "TestName", "NurseName", "TestStatusText" });
            cbFilterBy.SelectedIndex = 0;
            txtFilter.Text = string.Empty;
        }

        private void RefreshFilter(int index)
        {
            cbFilterBy.SelectedIndex = index;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvLabTests, labTests, lblRecords, null);
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "LabID" }, CommonOperations.GetFilterName(cbFilterBy),
              5, 40);
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            object value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(
                dgvLabTests, labTests, lblRecords, value.ToString()))
            {
                return;
            }

            List<object> temp = CommonOperations.FilterData(
                labTests, filterName, value);

            CommonOperations.LoadDataToDGV(dgvLabTests, temp, lblRecords);
        }

        private void viewLabTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int labTestId = (int)dgvLabTests.CurrentRow.Cells["LabID"].Value;
            ViewLabTestInfo viewLabTest = new ViewLabTestInfo(labTestId);
            viewLabTest.ShowDialog();
        }

        private void addLabTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            AddUpdateLabTest addTest = new AddUpdateLabTest(findAppointmentCtrl1.CurrentAppointment.AppointmentID);
            addTest.OnSave += ReciveLabInfo;
            addTest.ShowDialog();
        }

        private void ReciveLabInfo(LabTest labTest)
        {
            int targetIndex = -1;
            LabTestListing labList = LabTestListing.FindLabTestByID(labTest.LabID);

            if (labTests.Any(x => x.LabID == labTest.LabID))
                targetIndex = labTests.IndexOf(labTests.First(x => x.LabID == labTest.LabID));

            CommonOperations.UpdateAfterAdding(labTests, labList, targetIndex);
        }
    }
}
