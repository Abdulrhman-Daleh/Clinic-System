using Business;
using ClinicSystem.Helpers;
using System.Windows.Forms;

namespace ClinicSystem
{
    public partial class AddUpdatePerson : FormsDefaultSettings
    {
        private Person _person;
        public AddUpdatePerson()
        {
            InitializeComponent();
            lblPersonId.Text = "[Not Set]";
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => Close();

        private void btnSave_Click(object sender, System.EventArgs e)
        {

            Person tempPerson = personalInfoCtrl.GetPersonInfo();
            PersonContact tempContact = personContactCtrl.GetContactInfo();

            if (tempPerson == null || tempContact == null)
                return;

            _person = tempPerson;
            _person.contactInfo = tempContact;

            if (ValidationHelper.IsValidEmptyOrNull(_person) || ValidationHelper.IsValidEmptyOrNull(_person.contactInfo))
                return;

            _person.AddNewPerson();
            if (_person.PersonId != -1)
            {
                MessageBox.Show("Person added successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblPersonId.Text = _person.PersonId.ToString();
            }
            else
                MessageBox.Show("adding new person failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
