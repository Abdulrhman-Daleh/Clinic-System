using ClinicSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.MedicalRecord
{
    public partial class ManageMedicalRecord : FormsDefaultSettings
    {
        private BindingList<Business.MedicalRecordList> MedicalRecords { get; set; }

        public ManageMedicalRecord()
        {
            InitializeComponent();
        }

        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, cbConsentToCare, txtFilter, dgvMedicalRecords, MedicalRecords, lblRecords, "ConsentToCare");
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "MedicalRecordID" }, CommonOperations.GetFilterName(cbFilterBy),
                5, 30);
        }

        private void SetUp()
        {
            cbConsentToCare.Items.AddRange(new object[]
            {
        "All",
        "Yes",
        "No"
            });

            cbConsentToCare.SelectedIndex = 0;

            cbFilterBy.Items.AddRange(new object[]
            {
        "None",
        "MedicalRecordID",
        "MedicalNumber",
        "PatientName",
        "Classification",
        "ConsentToCare"
            });

            cbFilterBy.SelectedIndex = 0;
        }

        private async void LoadMedicalRecordInfo()
        {
            MedicalRecords = await Business.MedicalRecordList.GetMedicalRecords();

            CommonOperations.LoadDataToDGV(dgvMedicalRecords, MedicalRecords, lblRecords);

            RefreshFilter(0);
        }

        private void ManageMedicalRecord_Load(object sender, EventArgs e)
        {
            SetUp();
            LoadMedicalRecordInfo();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MedicalRecords == null)
                return;

            CommonOperations.HandleSwitchingFilter(
                cbFilterBy,
                cbConsentToCare,
                txtFilter,
                dgvMedicalRecords,
                MedicalRecords,
                lblRecords,
                "ConsentToCare");

            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void cbConsentToCare_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy).ToLower();

            if (cbConsentToCare.Text == "All")
            {
                CommonOperations.LoadDataToDGV(dgvMedicalRecords, MedicalRecords, lblRecords);
                return;
            }

            if (CommonOperations.ResetDataGrid(dgvMedicalRecords, MedicalRecords, lblRecords, cbConsentToCare.Text.ToLower()))
                return;

            object value = cbConsentToCare.Text.Trim();
            List<object> temp = CommonOperations.FilterData(MedicalRecords, filterName, cbConsentToCare.Text.ToLower() == "yes" ? true : false);
            CommonOperations.LoadDataToDGV(dgvMedicalRecords, temp, lblRecords);
        }

        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);

            if (filterName == "MedicalRecordID")
                ValidationHelper.HandleInputType(e, true, false);
            else if (filterName == "MedicalNumber")
                ValidationHelper.HandleInputType(e, true, true);
            else
                ValidationHelper.HandleInputType(e, true, false);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            object value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(dgvMedicalRecords, MedicalRecords, lblRecords, value.ToString()))
                return;

            List<object> temp = CommonOperations.FilterData(MedicalRecords, filterName, value);
            CommonOperations.LoadDataToDGV(dgvMedicalRecords, temp, lblRecords);
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void SetControlEnableStatus(bool status)
        {
            updateMedicalRecordToolStripMenuItem.Enabled = status;
            viewToolStripMenuItem.Enabled = status;
        }

        private void cmsMedicalRecords_Opening(object sender, CancelEventArgs e)
        {
            SetControlEnableStatus(
                !ValidationHelper.IsDgvEmptyOrNull(dgvMedicalRecords));
        }

        private void RefreshDgv(Business.MedicalRecord record)
        {
            if (record == null)
                return;

            LoadMedicalRecordInfo();
        }

        private void addNewMedicalRecordToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            AddUpdateMedicalRecord addMedicalRecord =
                new AddUpdateMedicalRecord();

            addMedicalRecord.SendMedicalRecordInfo += RefreshDgv;
            addMedicalRecord.ShowDialog();
        }

        private void updateMedicalRecordToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            if (ValidationHelper.IsDgvEmptyOrNull(dgvMedicalRecords))
                return;

            int medicalRecordID = Convert.ToInt32(
                dgvMedicalRecords.CurrentRow.Cells["MedicalRecordID"].Value);

            AddUpdateMedicalRecord updateMedicalRecord =
                new AddUpdateMedicalRecord(medicalRecordID);

            updateMedicalRecord.SendMedicalRecordInfo += RefreshDgv;
            updateMedicalRecord.ShowDialog();
        }

        private void dgvMedicalRecords_CellDoubleClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 ||
                ValidationHelper.IsDgvEmptyOrNull(dgvMedicalRecords))
                return;

            updateMedicalRecordToolStripMenuItem_Click(
                sender, EventArgs.Empty);
        }


    }
}