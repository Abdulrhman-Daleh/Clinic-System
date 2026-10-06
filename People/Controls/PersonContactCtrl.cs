using Business;
using ClinicSystem.Helpers;
using System;
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

        public delegate void SendContactInfo(string email, string phoneNumber, int contactId, int contactTypeId);
        public event SendContactInfo SendContact;

        public void InvokeEvent()
        {
            this.ValidateChildren(ValidationConstraints.Enabled);

            if (ValidationHelper.UserControlHasErrors(this, errors))
            {
                MessageBox.Show("contact info could not be send to form check required fields.", "Invalid state", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int contactId = lblContactId.Text == "[Not Set]" ? -1 : Convert.ToInt32(lblContactId.Text);
            SendContact?.Invoke(txtEmail.Text.Trim(), txtPhoneNumber.Text.Trim(), contactId, cbContactType.SelectedIndex);
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

        public void LoadContactToPage(PersonContact contact)
        {
            txtEmail.Text = contact.Email;
            txtPhoneNumber.Text = contact.PhoneNumber;
            cbContactType.SelectedIndex = (int)contact.ContactTypeId;
            PrintContactId(contact.ContactId);
        }

        public void PrintContactId(int contactId) => lblContactId.Text = contactId != -1 ? $"C-{contactId}" : "[Not Set]";
    }
}
