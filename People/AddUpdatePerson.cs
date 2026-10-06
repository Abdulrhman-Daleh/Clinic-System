using Business;
using ClinicSystem.Helpers;
using ClinicSystem.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace ClinicSystem
{
    public partial class AddUpdatePerson : FormsDefaultSettings
    {
        private Person _person;
        private int _personId;

        public AddUpdatePerson()
        {
            InitializeComponent();
            lblPersonId.Text = "[Not Set]";
            SetFormTextBasedMode("Add new person", "Add new person", "Add Person", Resources.update_32);
        }

        public AddUpdatePerson(int personId)
        {
            InitializeComponent();
            SetFormTextBasedMode("Update person info", "Update person info", "Update", Resources.update_32);
            _personId = personId;
            PrintPersonId(_personId);
            _person = Person.FindPersonById(_personId);

            if (ValidationHelper.IsEmptyOrNull(_person))
            {
                personalInfoCtrl.Enabled = false;
                return;
            }

            personalInfoCtrl.LoadInfoToPage(_person);

            if (ValidationHelper.IsEmptyOrNull(_person.contactInfo))
            {
                personContactCtrl.Enabled = false;
                return;
            }

            personContactCtrl.LoadContactToPage(_person.contactInfo);
        }

        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText, Image image)
        {
            lblTitle.Text = labelTitle;
            this.Text = pageTitle;
            btnSave.Text = saveButtonText;
            pbModeImage.Image = image;
        }

        private void PrintPersonId(int personId)
        {
            lblPersonId.Text = $"P-{personId}";
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => Close();

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            personalInfoCtrl.RefreshPersonInfo(_person);

            if (ValidationHelper.IsEmptyOrNull(_person))
                return;

            if (_person.Save())
            {
                MessageBox.Show("data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PrintPersonId(_person.PersonId);
                SetFormTextBasedMode("Update person info", "Update person info", "Update", Resources.update_32);
            }
            else
                MessageBox.Show("adding new person failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
