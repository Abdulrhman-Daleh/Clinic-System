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

        public delegate void SendContactInfo(string email, string phoneNumber, int contactTypeId);
        public event SendContactInfo SendContact;
        public bool TryToSendContact()
        {
            this.ValidateChildren(ValidationConstraints.Enabled);

            if (ValidationHelper.UserControlHasErrors(this, errors))
            {
                MessageBox.Show("contact info could not be send to form check required fields.", "Invalid state", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SendContact?.Invoke(txtEmail.Text.Trim(), txtPhoneNumber.Text.Trim(), cbContactType.SelectedIndex);
            return true;
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
            ValidationHelper.ValidateRequiredTextBox((MaskedTextBox)sender, errors);

            if (!ValidationHelper.IsValidPhoneNumber(txtPhoneNumber.Text))
            {
                errors.SetError(txtPhoneNumber, "Invalid phone number length");
                return;
            }

            errors.SetError(txtPhoneNumber, null);
        }
        public void LoadContactToPage(PersonContact contact)
        {
            txtEmail.Text = contact.Email;
            txtPhoneNumber.Text = contact.PhoneNumber;
            cbContactType.SelectedIndex = (int)contact.ContactTypeId;
            PrintContactId(contact.ContactId);
        }
        public void PrintContactId(int contactId) => lblContactId.Text = contactId != -1 ? $"C-{contactId}" : "[Not Set]";
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidationHelper.IsEmailValid(txtEmail.Text.Trim()))
            {
                errors.SetError(txtEmail, "Email is not valid");
                return;
            }

            errors.SetError(txtEmail, null);
        }
        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.HandleInputType(e, true, false);
        }
    }
}
