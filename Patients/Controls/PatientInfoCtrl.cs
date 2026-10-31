using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Patients.Controls
{
    public partial class PatientInfoCtrl : UserCtrlDefaultSettings
    {
        public PatientInfoCtrl()
        {
            InitializeComponent();
            CommonOperations.SetUpDefaultControlsValue(new List<Control> { lblFrozen, lblMedicalRecord, lblRegistered, lblTotalVisits, lblUnpaidFees }, "Not Set");
        }

        public void LoadPatientInfo(Patient patient)
        {

            if (ValidationHelper.IsEmptyOrNull((patient), "patient is not found"))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control> { lblFrozen, lblMedicalRecord, lblRegistered, lblTotalVisits, lblUnpaidFees }, "Not Set");
                return;
            }

            if (patient.EndOfBookingFreeze == null)
                lblFrozen.Text = $"Not frozen so ({patient.FreezeUnlockDays()}) days";
            else
                lblFrozen.Text = $"Until {patient.EndOfBookingFreeze.Value.ToShortDateString()} ({patient.FreezeUnlockDays()} days)";

            lblRegistered.Text = patient.RegisterDate.ToString();
            lblUnpaidFees.Text = patient.ISUnpaidText();
            lblTotalVisits.Text = patient.GetTotalVisits().ToString();
            SetMedicalRecordInfo(patient);
        }

        private void SetMedicalRecordInfo(Patient patient)
        {
            Business.MedicalRecord medicalRecord = Business.MedicalRecord.FindMedicalRecordByPatientID(patient.PatientId);

            if (ValidationHelper.IsEmptyOrNull(medicalRecord))
            {
                lblMedicalRecord.Text = "Does not have medical record";
                return;
            }

            lblMedicalRecord.Text = medicalRecord.MedicalNumber + " " + medicalRecord.SocialStatus;
        }


    }
}
