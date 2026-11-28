
using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.LabOrderTest
{
    public partial class ManageLabTests : FormsDefaultSettings
    {
        BindingList<LabTestListing> labTests = new BindingList<LabTestListing>();
        private readonly int? _appointmentId;
        private string FilterName { get; set; }
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
            await findAppointmentCtrl1.LoadAppointmentInfo(appointmentId);
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
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "LabID" }, FilterName,
              5, 40);
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FilterName = CommonOperations.GetFilterName(cbFilterBy);
            RefreshFilter(cbFilterBy.SelectedIndex);
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

                if (CommonOperations.ResetDataGrid(dgvLabTests, labTests, lblRecords, value.ToString()))
                    return;

                List<object> temp = CommonOperations.FilterData(labTests, FilterName, value);
                CommonOperations.LoadDataToDGV(dgvLabTests, temp, lblRecords);
            }
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
            addTest.OnSave += RefreshDGV;
            addTest.ShowDialog();
        }
        private async void RefreshDGV(LabTest labTest)
        {
            LabTestListing labList = await LabTestListing.FindLabTestByID(labTest.LabID);
            int targetIndex = CommonOperations.FindTargetIndexOfObj(labTests, "LabID", labList.LabID);
            CommonOperations.UpdateAfterAdding(labTests, labList, targetIndex);

            lblRecords.Text = $"{dgvLabTests.Rows.Count} records";

        }
    }
}
