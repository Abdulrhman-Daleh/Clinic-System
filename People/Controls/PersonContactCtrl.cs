using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.People.Controls
{
    public partial class PersonContactCtrl : UserCtrlDefaultSettings
    {
        public PersonContactCtrl()
        {
            InitializeComponent();
            _SetPage();
        }

        private void _SetPage()
        {
            cbContactType.Items.Add("Normal");
            cbContactType.Items.Add("Emergency");
            cbContactType.SelectedIndex = 0;
            txtPhoneNumber.Text = string.Empty;
            txtEmail.Text = string.Empty;
            lblContactId.Text = "[Not Set]";
        }

        private void txtPhoneNumber_Validating(object sender, CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);
        }


        public PersonContact GetContactInfo()
        {
            this.ValidateChildren(ValidationConstraints.Enabled);

            if (ValidationHelper.UserControlHasErrors(this, errors))
            {
                MessageBox.Show("contact info could not be send to form check required fields.", "Invalid state", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }


            return new PersonContact()
            {
                PhoneNumber = txtPhoneNumber.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                ContactTypeId = (EnContactTypes)cbContactType.SelectedIndex + 1
            };
        }


        public void LoadContactToPage(PersonContact contact)
        {
            txtEmail.Text = contact.Email;
            txtPhoneNumber.Text = contact.PhoneNumber;
            lblContactId.Text = contact.ContactId.ToString();
            cbContactType.SelectedIndex = (int)contact.ContactTypeId;
        }
    }
}
