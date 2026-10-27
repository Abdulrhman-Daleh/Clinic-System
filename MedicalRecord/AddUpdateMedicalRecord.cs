using Business;
using ClinicSystem.Helpers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.MedicalRecord
{
    public partial class AddUpdateMedicalRecord : FormsDefaultSettings
    {
        private Business.MedicalRecord medicalRecord;
        public AddUpdateMedicalRecord()
        {
            InitializeComponent();

            medicalRecord = new Business.MedicalRecord();

            SetControls();
            SetFormTextBasedMode(
                "Add new medical record",
                "Add Medical Record",
                "Add Record");

            findPatientCtrl1.SendPatient += RecivePatientData;
        }

        public delegate void SendMedicalRecordInfoHandler(Business.MedicalRecord medicalRecord);
        public event SendMedicalRecordInfoHandler SendMedicalRecordInfo;

        public AddUpdateMedicalRecord(int medicalRecordID)
        {
            InitializeComponent();

            medicalRecord = Business.MedicalRecord.FindMedicalRecordByID(medicalRecordID);

            if (ValidationHelper.IsEmptyOrNull(medicalRecord, "Medical record is not found"))
                return;

            SetControls(medicalRecord.MedicalRecordID);

            SetFormTextBasedMode(
                "Update medical record",
                "Update Medical Record",
                "Update");

            findPatientCtrl1.AllowedFind = false;
            findPatientCtrl1.LoadPatientInfo(medicalRecord.PatientID);
            findPatientCtrl1.SendPatient += RecivePatientData;
        }

        private void RecivePatientData(Patient patient)
        {
            if (ValidationHelper.IsEmptyOrNull(patient))
                return;

            medicalRecord.PatientID = patient.PatientId;
            medicalRecord.Patient = patient;
        }

        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            Text = pageTitle;
            btnSave.Text = saveButtonText;
        }

        private void SetControls(int medicalRecordID = -1)
        {
            SetMedicalRecordID(medicalRecordID);

            if (medicalRecordID == -1)
                return;

            txtCurrentMedication.Text = medicalRecord.CurrentMedication;
            txtPastScoial.Text = medicalRecord.PastSocialHistory;
            txtPastSurgeries.Text = medicalRecord.PastSurgeries;
            chkConsent.Checked = medicalRecord.ConcentToCare;
            cbSocialStatus.Text = medicalRecord.SocialStatus;
            cbClassification.Text = medicalRecord.Classification;
            txtAllergies.Text = medicalRecord.Allergies;

            if (medicalRecord.CurrentMedicationDuration >= dtpMedicationUntil.MinDate &&
                medicalRecord.CurrentMedicationDuration <= dtpMedicationUntil.MaxDate)
            {
                dtpMedicationUntil.Value = medicalRecord.CurrentMedicationDuration;
            }
        }

        private void SetMedicalRecordID(int medicalRecordID)
        {
            lblMedicalRecordID.Text = medicalRecordID != -1
                ? $"MR-{medicalRecordID}"
                : "[Not Set]";
        }

        private void ValidateInputValues(object sender, CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void CollectData()
        {
            medicalRecord.CurrentMedication = txtCurrentMedication.Text.Trim();
            medicalRecord.CurrentMedicationDuration = dtpMedicationUntil.Value;
            medicalRecord.PastSocialHistory = txtPastScoial.Text.Trim();
            medicalRecord.PastSurgeries = txtPastSurgeries.Text.Trim();
            medicalRecord.ConcentToCare = chkConsent.Checked;
            medicalRecord.SocialStatus = cbSocialStatus.Text.Trim();
            medicalRecord.Classification = cbClassification.Text.Trim();
            medicalRecord.Allergies = txtAllergies.Text.Trim();

            if (medicalRecord.MedicalRecordID == -1)
                medicalRecord.CreatedDate = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ValidateChildren();

            if (ValidationHelper.NotValidToSave(this, errors))
            {
                MessageBox.Show(
                    "Form is not valid to save",
                    "Not Valid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (ValidationHelper.IsEmptyOrNull(medicalRecord.Patient))
            {
                MessageBox.Show(
                    "Please select a patient",
                    "Missing Patient",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            CollectData();

            if (medicalRecord.Save())
            {
                MessageBox.Show(
                    "Data saved successfully",
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                SetMedicalRecordID(medicalRecord.MedicalRecordID);

                SetFormTextBasedMode(
                    "Update medical record",
                    "Update Medical Record",
                    "Update");

                findPatientCtrl1.AllowedFind = false;

                SendMedicalRecordInfo?.Invoke(medicalRecord);
            }
            else
            {
                MessageBox.Show(
                    "Failed to save",
                    "Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AddUpdateMedicalRecord_Load(object sender, EventArgs e)
        {
            findPatientCtrl1.AllowAddNew = false;
            cbSocialStatus.Items.Clear();
            cbSocialStatus.Items.AddRange(new object[]
            {
        "Not Specified",
        "Single",
        "Married",
        "Separated",
        "Divorced",
        "Widowed"
            });

            cbClassification.Items.Clear();
            cbClassification.Items.AddRange(new object[]
            {
        "Not Classified",
        "General",
        "Chronic Condition",
        "High Risk",
        "Special Care"
            });

            SetComboBoxValue(cbSocialStatus, medicalRecord.SocialStatus);
            SetComboBoxValue(cbClassification, medicalRecord.Classification);
        }

        private void SetComboBoxValue(ComboBox comboBox, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                comboBox.SelectedIndex = 0;
                return;
            }

            int index = comboBox.FindStringExact(value);

            if (index == -1)
            {
                comboBox.Items.Add(value);
                index = comboBox.Items.Count - 1;
            }

            comboBox.SelectedIndex = index;
        }
    }
}