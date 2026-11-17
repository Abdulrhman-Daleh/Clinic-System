using ClinicSystem.Helpers;
using System.Threading.Tasks;

namespace ClinicSystem.LabOrderTest
{
    public partial class ViewLabTestInfo : FormsDefaultSettings
    {
        private readonly int TestId;
        public ViewLabTestInfo(int testId)
        {
            InitializeComponent();
            TestId = testId;
            Load += ViewLabTestInfo_Load;
        }

        private async void ViewLabTestInfo_Load(object sender, System.EventArgs e)
        {
            await SetUpCtrl(TestId);
        }

        private async Task SetUpCtrl(int testId)
        {
            viewLabTestInfoCtrl1.LoadLabTestInfo(testId);

            var test = viewLabTestInfoCtrl1.CurrentLabTest;

            if (ValidationHelper.IsEmptyOrNull(test))
                return;

            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(test.AppointmentID);
            viewMRecordInfo1.LoadMedicalRecordInfo(await Business.MedicalRecord.FindMedicalRecordByPatientID(test.Appointment.PatientID));
            viewPersonInfoCtrl1.SetData(test.Appointment.Patient);
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();
    }
}
