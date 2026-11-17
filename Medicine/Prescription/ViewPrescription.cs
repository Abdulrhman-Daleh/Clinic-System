using ClinicSystem.Helpers;
using System.Threading.Tasks;

namespace ClinicSystem.Medicine.Prescription
{
    public partial class ViewPrescription : FormsDefaultSettings
    {
        private readonly int AppointmentId;
        public ViewPrescription(int appointmentId)
        {
            InitializeComponent();
            lblPrescriptionId.Text = "Not Set";
            AppointmentId = appointmentId;
        }
        private async Task SetUpCtrl(int appointmentId)
        {
            await viewPrescriptionCtrl1.LoadPrescriptionInfo(appointmentId);
            Business.Prescription Prescription = viewPrescriptionCtrl1.CurrentPrescription;

            if (ValidationHelper.IsEmptyOrNull(Prescription))
                return;

            lblPrescriptionId.Text = $"View Prescription - {Prescription.Appointment.Patient.GetFullName()} . {Prescription.WriteDate.ToShortDateString()}";
            viewMRecordInfo1.LoadMedicalRecordInfo(await Business.MedicalRecord.FindMedicalRecordByPatientID(Prescription.Appointment.PatientID));
            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(Prescription.AppointmentID);
            patientInfoCtrl1.LoadPatientInfo(Prescription.Appointment.Patient);

            btnEdit.Enabled = !Prescription.DoctorDidSign && Prescription.Appointment.IsStillActive();
        }
        private void btnClose_Click(object sender, System.EventArgs e) => Close();
        private async void ViewPrescription_Load(object sender, System.EventArgs e) => await SetUpCtrl(AppointmentId);
    }
}
