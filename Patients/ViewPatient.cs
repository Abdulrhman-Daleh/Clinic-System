using Business;
using ClinicSystem.Helpers;

namespace ClinicSystem.Patients
{
    public partial class ViewPatient : FormsDefaultSettings
    {
        private Patient patient = null;
        public ViewPatient(int patientId)
        {
            InitializeComponent();
            lblFullname.Text = "[Not Set]";
            patient = Patient.FindPatientById(patientId);
        }

        private void ViewPatient_Load(object sender, System.EventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(patient))
                return;

            lblFullname.Text = patient.GetFullName();
            viewPersonInfoCtrl1.SetData(patient);
            viewPersonContacts1.LoadUpPersonContact(patient.PersonId);
            patientInfoCtrl1.LoadPatientInfo(patient);


        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();
    }
}
