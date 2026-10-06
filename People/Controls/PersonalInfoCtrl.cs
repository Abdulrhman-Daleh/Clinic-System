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

        public void RefreshPersonInfo(Person person)
        {
            this.ValidateChildren(ValidationConstraints.Enabled);

            if (ValidationHelper.UserControlHasErrors(this, errors))
            {
                MessageBox.Show("personal info could not be refreshed check required fields.", "Invalid state", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            person.Firstname = txtFirstName.Text.Trim();
            person.Lastname = txtLastName.Text.Trim();
            person.Gender = cbGender.Text == "Male" ? EnGender.Male : EnGender.Female;
        }


        public void LoadInfoToPage(Person person)
        {
            txtFirstName.Text = person.Firstname;
            txtLastName.Text = person.Lastname;
            cbGender.SelectedIndex = (int)person.Gender - 1;
            lblPersonId.Text = $"P-{person.PersonId}";
        }
    }
}
