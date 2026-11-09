
using AppEnums.cs;
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
        private int? _appointmentId;

        public ManageLabTests()
        {
            InitializeComponent();
        }

        public ManageLabTests(int appointmentId)
        {
            InitializeComponent();
            _appointmentId = appointmentId;
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
            if (!ValidationHelper.CheckAccess(EnRole.Doctor, EnRole.Nurse))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvLabTests) || e.RowIndex < 0)
                return;

            int labTestId = (int)dgvLabTests.CurrentRow.Cells["LabID"].Value;
            ViewLabTestInfo viewLabTest = new ViewLabTestInfo(labTestId);
            viewLabTest.ShowDialog();
        }

        private void ManageLabTests_Load(object sender, System.EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Doctor, EnRole.Nurse))
            {
                Close();
                return;
            }

            cbFilterBy.Items.AddRange(new[] { "None", "LabID", "TestName", "NurseName", "TestStatusText" });
            cbFilterBy.SelectedIndex = 0;
            txtFilter.Text = string.Empty;

            if (_appointmentId.HasValue)
                SetLoadedAppointment(_appointmentId.Value);
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
            if (!ValidationHelper.CheckAccess(EnRole.Doctor, EnRole.Nurse))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvLabTests))
                return;

            int labTestId = (int)dgvLabTests.CurrentRow.Cells["LabID"].Value;
            ViewLabTestInfo viewLabTest = new ViewLabTestInfo(labTestId);
            viewLabTest.ShowDialog();
        }

        private void addLabTestToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Doctor))
                return;

            if (findAppointmentCtrl1.CurrentAppointment == null)
                return;

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
