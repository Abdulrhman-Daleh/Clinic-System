using ClinicSystem.Helpers;

namespace ClinicSystem.Medicine.Prescription
{
    public partial class ViewPrescription : FormsDefaultSettings
    {
        public ViewPrescription(int appointmentId)
        {
            InitializeComponent();
            lblPrescriptionId.Text = "Not Set";
            SetUpCtrl(appointmentId);
        }

        private void SetUpCtrl(int appointmentId)
        {
            viewPrescriptionCtrl1.LoadPrescriptionInfo(appointmentId);
            Business.Prescription Prescription = viewPrescriptionCtrl1.CurrentPrescription;

            if (ValidationHelper.IsEmptyOrNull(Prescription))
                return;

            lblPrescriptionId.Text = $"View Prescription - {Prescription.Appointment.Patient.GetFullName()} . {Prescription.WriteDate.ToShortDateString()}";
            viewMRecordInfo1.LoadMedicalRecordInfo(Business.MedicalRecord.FindMedicalRecordByPatientID(Prescription.Appointment.PatientID));
            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(Prescription.AppointmentID);
            patientInfoCtrl1.LoadPatientInfo(Prescription.Appointment.Patient);

            btnEdit.Enabled = !Prescription.DoctorDidSign && Prescription.Appointment.IsStillActive();
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();
    }
}
