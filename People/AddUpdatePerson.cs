using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using ClinicSystem.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClinicSystem
{
    public partial class AddUpdatePerson : FormsDefaultSettings
    {
        private Person _person;

        public delegate void SendPersonInfoOnSave(Person person);
        public event SendPersonInfoOnSave SendPersonInfo;

        public AddUpdatePerson()
        {
            InitializeComponent();
            lblPersonId.Text = "[Not Set]";
            SetFormTextBasedMode("Add new person", "Add new person", "Add Person", Resources.update_32);
            _person = new Person();
            SetControls();
        }

        private void SetControls(int selectedIndex = 0, string firstName = "", string lastName = "", int personId = -1)
        {
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
            txtFirstName.Focus();

            cbGender.SelectedIndex = selectedIndex;
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            PrintPersonId(personId);
        }

        public AddUpdatePerson(int personId)
        {
            InitializeComponent();
            SetFormTextBasedMode("Update person info", "Update person info", "Update", Resources.update_32);
            _person = Person.FindPersonById(personId);

            if (ValidationHelper.IsEmptyOrNull(_person))
                return;

            SetControls((int)_person.Gender - 1, _person.Firstname, _person.Lastname, _person.PersonId);
        }

        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText, Image image)
        {
            lblTitle.Text = labelTitle;
            this.Text = pageTitle;
            btnSave.Text = saveButtonText;
            pbModeImage.Image = image;
        }

        private void ValidateInputValues(object sender, CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);
        }

        private void PrintPersonId(int personId)
        {
            lblPersonId.Text = personId != -1 ? $"P-{personId}" : "[Not Set]";
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => Close();

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(_person, "person is not valid to save"))
                return;

            _person.Firstname = txtFirstName.Text.Trim();
            _person.Lastname = txtLastName.Text.Trim();
            _person.Gender = (EnGender)cbGender.SelectedIndex + 1;

            this.ValidateChildren(ValidationConstraints.Enabled);
            if (ValidationHelper.NotValidToSave(this, errors))
            {
                MessageBox.Show("Form is not valid to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_person.Save())
            {
                MessageBox.Show("data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PrintPersonId(_person.PersonId);
                SendPersonInfo?.Invoke(_person);
                SetFormTextBasedMode("Update person info", "Update person info", "Update", Resources.update_32);
            }
        }

        private void ValidateInputKeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.HandleInputType(e, false, true);
        }

        private void cbGender_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
    }
}
