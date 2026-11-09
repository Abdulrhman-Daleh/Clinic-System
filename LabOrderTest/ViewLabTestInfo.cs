using ClinicSystem.Helpers;

namespace ClinicSystem.LabOrderTest
{
    public partial class ViewLabTestInfo : FormsDefaultSettings
    {
        public ViewLabTestInfo(int testId)
        {
            InitializeComponent();
            SetUpCtrl(testId);
        }

        private void SetUpCtrl(int testId)
        {
            viewLabTestInfoCtrl1.LoadLabTestInfo(testId);

            var test = viewLabTestInfoCtrl1.CurrentLabTest;

            if (ValidationHelper.IsEmptyOrNull(test))
                return;

            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(test.AppointmentID);
            viewMRecordInfo1.LoadMedicalRecordInfo(Business.MedicalRecord.FindMedicalRecordByPatientID(test.Appointment.PatientID));
            viewPersonInfoCtrl1.SetData(test.Appointment.Patient);
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();
    }
}
