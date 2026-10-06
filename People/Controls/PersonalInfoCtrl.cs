using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.People.Controls
{
    public partial class PersonalInfoCtrl : UserCtrlDefaultSettings
    {
        public PersonalInfoCtrl()
        {
            InitializeComponent();
            SetUpUserCtrl();
        }

        public delegate void SendPersonInfo(string firstName, string lastName, EnGender gender);
        public event SendPersonInfo SendInfo;

        public bool TrySendPersonInfo()
        {
            this.ValidateChildren(ValidationConstraints.Enabled);

            if (ValidationHelper.UserControlHasErrors(this, errors))
            {
                MessageBox.Show("person info could not be send to form check required fields.", "Invalid state", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SendInfo?.Invoke(txtFirstName.Text.Trim(), txtLastName.Text.Trim(), (EnGender)cbGender.SelectedIndex + 1);
            return true;
        }

        private void ValidateInputValues(object sender, CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);
        }

        private void SetUpUserCtrl()
        {
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
            cbGender.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtFirstName.Focus();
            lblPersonId.Text = "[Not Set]";
        }

        public void LoadInfoToPage(Person person)
        {
            txtFirstName.Text = person.Firstname;
            txtLastName.Text = person.Lastname;
            cbGender.SelectedIndex = (int)person.Gender - 1;
            PrintPersonId(person.PersonId);
        }

        public void PrintPersonId(int personId) => lblPersonId.Text = personId != -1 ? $"P-{personId}" : "[Not Set]";
    }
}
