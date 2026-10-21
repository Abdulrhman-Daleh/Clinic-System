using Business;
using ClinicSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, cbStatus, txtFilter, dgvPatients, Patients, lblRecords, "Status");
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "PatientID" }, CommonOperations.GetFilterName(cbFilterBy),
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
            string filterName = CommonOperations.GetFilterName(cbFilterBy);

            if (filterName == "PatientID")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            object value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(dgvPatients, Patients, lblRecords, value.ToString()))
                return;

            List<object> temp = CommonOperations.FilterData(Patients, filterName, value);
            CommonOperations.LoadDataToDGV(dgvPatients, temp, lblRecords);
        }
        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy).ToLower();

            if (CommonOperations.ResetDataGrid(dgvPatients, Patients, lblRecords, cbStatus.Text.ToLower()))
                return;

            object value = cbStatus.Text.Trim();
            List<object> temp = CommonOperations.FilterData(Patients, filterName, cbStatus.Text);
            CommonOperations.LoadDataToDGV(dgvPatients, temp, lblRecords);
        }

        private async void LoadPatientInfo()
        {
            Patients = await PatientList.GetPatientListingDatas();

            CommonOperations.LoadDataToDGV(dgvPatients, Patients, lblRecords);

            RefreshFilter(0);
        }

        private void ManagePatient_Load(object sender, System.EventArgs e)
        {
            LoadPatientInfo();
            SetUp();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }
        private void SetControlEnableStatus(bool status)
        {
            updatePatientToolStripMenuItem.Enabled = status;
            viewPatientToolStripMenuItem.Enabled = status;
            addNewPatientToolStripMenuItem.Enabled = status;
        }

        private void cmsPatient_Opening(object sender, CancelEventArgs e)
        {
            SetControlEnableStatus(!ValidationHelper.IsDgvEmptyOrNull(dgvPatients));
        }

        private void RefreshDgv(int patientId)
        {
            int targetIndex = -1;
            PatientList patient = PatientList.FindPatientByIdList(patientId);

            if (Patients.Any(x => x.PatientId == patient.PatientId))
                targetIndex = Patients.IndexOf(Patients.First(x => x.PatientId == patient.PatientId));

            CommonOperations.UpdateAfterAdding(Patients, patient, targetIndex);
        }

        private void addNewPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterPatient register = new RegisterPatient();
            register.OnRegisterSave += RefreshDgv;
            register.ShowDialog();
        }
    }
}
