using Business;
using ClinicSystem.Helpers;
using System.Windows.Forms;

namespace ClinicSystem.Patients
{
    public partial class RegisterPatient : FormsDefaultSettings
    {
        Patient patient = new Patient();
        public RegisterPatient()
        {
            InitializeComponent();
            SetFormTextBasedMode("Register New Patient", "Register patient", "Register Patient");
            lblPatientId.Text = "[Not Set]";
            findPersonCtrl1.OnFind += RecivePersonData;
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => Close();

        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            this.Text = pageTitle;
            btnRegister.Text = saveButtonText;
        }

        public delegate void RegisterPatientData(int patientId);
        public event RegisterPatientData OnRegisterSave;

        private void RecivePersonData(Person person)
        {
            if (ValidationHelper.IsEmptyOrNull(person, "person info does not exists"))
                return;

            patient.Firstname = person.Firstname;
            patient.Lastname = person.Lastname;
            patient.Gender = person.Gender;
            patient.PersonId = person.PersonId;
            patient.contactInfo = person.contactInfo;
        }

        private void btnRegister_Click(object sender, System.EventArgs e)
        {
            if (!patient.AddNewPatient())
            {
                MessageBox.Show("Failed to save", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Data saved succeesfullu", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblPatientId.Text = $"pat-{patient.PatientId}";
            OnRegisterSave?.Invoke(patient.PatientId);
            this.Close();
        }
    }
}
