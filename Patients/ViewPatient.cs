using Business;
using ClinicSystem.Helpers;

namespace ClinicSystem.Patients
{
    public partial class ViewPatient : FormsDefaultSettings
    {
        private Patient patient = null;
        private readonly int PatientId;
        public ViewPatient(int patientId)
        {
            InitializeComponent();
            lblFullname.Text = "[Not Set]";
            PatientId = patientId;
        }
        private async void ViewPatient_Load(object sender, System.EventArgs e)
        {
            patient = await Patient.FindPatientById(PatientId);

            if (ValidationHelper.IsEmptyOrNull(patient))
                return;

            lblFullname.Text = patient.GetFullName();
            viewPersonInfoCtrl1.SetData(patient);
            viewPersonContacts1.LoadUpPersonContact(patient.PersonId);
            patientInfoCtrl1.LoadPatientInfo(patient);
            lblSubTitle.Text = $"{patient.GetGenderChar()} · patient since {patient.RegisterDate.ToShortDateString()}\n {patient.FrozenUntilText()} {patient.UnpaidFeesText()}";
        }
        private void btnClose_Click(object sender, System.EventArgs e) => Close();
    }
}
