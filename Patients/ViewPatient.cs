using Business;
using ClinicSystem.Helpers;

namespace ClinicSystem.Patients
{
    public partial class ViewPatient : FormsDefaultSettings
    {
        private PatientList patientListInfo = null;
        private Patient patient = null;
        public ViewPatient(int patientId)
        {
            InitializeComponent();
            lblFullname.Text = "[Not Set]";
            patientListInfo = PatientList.FindPatientByIdList(patientId);
            patient = Patient.FindPatientById(patientId);
        }

        private void ViewPatient_Load(object sender, System.EventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(patient) || ValidationHelper.IsEmptyOrNull(patientListInfo))
                return;

            lblFullname.Text = patient.GetFullName();
            viewPersonInfoCtrl1.SetData(patient);
            viewPersonContacts1.LoadUpPersonContact(patient.PersonId);
            patientInfoCtrl1.LoadPatientInfo(patient.RegisterDate, patient.EndOfBookingFreeze, patient.FreezeUnlockDays(), patient.ISUnpaidText(), patientListInfo.Visits);


        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();
    }
}
