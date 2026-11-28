using Business;
using ClinicSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.Patients
{
    public partial class ManagePatient : FormsDefaultSettings
    {
        private BindingList<PatientList> Patients { get; set; }
        public ManagePatient()
        {
            InitializeComponent();
        }

        private string FilterName { get; set; }
        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, cbStatus, txtFilter, dgvPatients, Patients, lblRecords, "Status");
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "PatientID" }, FilterName,
                5, 30);
        }
        private void SetUp()
        {
            cbStatus.Items.AddRange(new object[] { "Active", "Not Active" });
            cbStatus.SelectedIndex = 0;

            cbFilterBy.Items.AddRange(new object[] { "None", "PatientID", "FullName", "InsuranceName", "Status" });
        }
        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (FilterName == "PatientID")
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

                if (CommonOperations.ResetDataGrid(dgvPatients, Patients, lblRecords, value.ToString()))
                    return;

                List<object> temp = CommonOperations.FilterData(Patients, FilterName, value);
                CommonOperations.LoadDataToDGV(dgvPatients, temp, lblRecords);
            }
        }
        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CommonOperations.ResetDataGrid(dgvPatients, Patients, lblRecords, cbStatus.Text.ToLower()))
                return;

            object value = cbStatus.Text.Trim();
            List<object> temp = CommonOperations.FilterData(Patients, FilterName, cbStatus.Text);
            CommonOperations.LoadDataToDGV(dgvPatients, temp, lblRecords);
        }
        private async void LoadPatientInfo()
        {
            Patients = await PatientList.GetPatientListingDatas();

            CommonOperations.LoadDataToDGV(dgvPatients, Patients, lblRecords);
            UpdatePatientSummary();
            RefreshFilter(0);
        }
        private void UpdatePatientSummary()
        {
            if (ValidationHelper.IsDgvEmptyOrNull(dgvPatients))
            {
                lblSubTitle.Text = $"Patients — {0} registered · {0} frozen · {0} unpaid";
                return;
            }

            int frozenPatients = 0;
            int unPaidPatients = Patient.GetTotalUnPaidPatients();

            foreach (PatientList patient in Patients)
            {
                if (patient.Status.ToLower() != "active")
                    frozenPatients++;
            }

            lblSubTitle.Text = $"Patients — {Patients.Count} registered · {frozenPatients} frozen · {unPaidPatients} unpaid";
        }
        private void ManagePatient_Load(object sender, System.EventArgs e)
        {
            LoadPatientInfo();
            SetUp();
            UpdatePatientSummary();
        }
        private void btnClose_Click(object sender, EventArgs e) => Close();
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterName = CommonOperations.GetFilterName(cbFilterBy);
            RefreshFilter(cbFilterBy.SelectedIndex);
        }
        private void SetControlEnableStatus(bool status)
        {
            viewPatientToolStripMenuItem.Enabled = status;
        }
        private void cmsPatient_Opening(object sender, CancelEventArgs e)
        {
            SetControlEnableStatus(!ValidationHelper.IsDgvEmptyOrNull(dgvPatients));
        }
        private async void RefreshDgv(Patient p)
        {
            PatientList patient = await PatientList.FindPatientByIdList(p.PatientId);
            int targetIndex = CommonOperations.FindTargetIndexOfObj(Patients, "PatientId", patient.PatientId);
            CommonOperations.UpdateAfterAdding(Patients, patient, targetIndex);
            lblRecords.Text = $"{dgvPatients.Rows.Count} records";
        }
        private void addNewPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterPatient register = new RegisterPatient();
            register.OnRegisterSave += RefreshDgv;
            register.ShowDialog();
        }
        private void viewPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidationHelper.IsDgvEmptyOrNull(dgvPatients))
                return;

            int patientId = (int)dgvPatients.CurrentRow.Cells[0].Value;

            ViewPatient viewPatient = new ViewPatient(patientId);
            viewPatient.ShowDialog();
        }
        private void dgvPatients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ValidationHelper.IsDgvEmptyOrNull(dgvPatients))
                return;

            int patientId = (int)dgvPatients.CurrentRow.Cells[0].Value;

            ViewPatient viewPatient = new ViewPatient(patientId);
            viewPatient.ShowDialog();
        }
    }
}
