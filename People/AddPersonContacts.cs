
using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem.People
{
    public partial class AddPersonContacts : FormsDefaultSettings
    {
        private Person Person;
        private int personId;
        public AddPersonContacts()
        {
            InitializeComponent();
            SetFormTextBasedMode("Add Person Contact", "Add Person Contact", "Add Contact");
        }
        public AddPersonContacts(int personId)
        {
            InitializeComponent();
            SetFormTextBasedMode("Add Person Contact", "Add Person Contact", "Add Contact");
            this.personId = personId;
        }
        private async Task SetUpdate(int personId)
        {
            Person = await Person.FindPersonById(personId);

            if (ValidationHelper.IsEmptyOrNull(Person, "Person does not exists"))
                return;

            findPersonCtrl1.AllowFind = false;
            findPersonCtrl1.RecivePersonInfo(Person);
        }
        private void RecivePersonInfo(Person person)
        {
            Person = person;
        }
        private void btnCancel_Click(object sender, System.EventArgs e) => Close();
        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            this.Text = pageTitle;
            btnSave.Text = saveButtonText;
            findPersonCtrl1.OnFind += RecivePersonInfo;
        }
        private void SetPage()
        {
            cbContactType.Items.Add("Normal");
            cbContactType.Items.Add("Emergency");
            cbContactType.SelectedIndex = 0;
            txtPhoneNumber.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }
        private void FillData()
        {
            Person.contactInfo.Email = txtEmail.Text.Trim();
            Person.contactInfo.PhoneNumber = txtPhoneNumber.Text.Trim();
            Person.contactInfo.ContactType = (EnContactTypes)cbContactType.SelectedIndex + 1;
            Person.contactInfo.PersonId = Person.PersonId;
        }
        private bool IsPersonSelected()
        {
            if (ValidationHelper.IsEmptyOrNull(Person, "Person is not selected"))
                return false;

            Person.contactInfo.PersonId = Person.PersonId;
            return true;
        }
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (!IsPersonSelected())
                return;

            FillData();

            if (ValidationHelper.IsEmptyOrNull(Person.contactInfo, "Contact does not exists"))
                return;

            this.ValidateChildren(ValidationConstraints.Enabled);

            OperationResult result = ValidationHelper.ValidateForm(this, errors);

            if (!result.IsSuccess)
            {
                MessageBox.Show(
                    result.Message,
                    result.Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            result = Person.contactInfo.Save();

            MessageBox.Show(
                result.Message,
                result.Title,
                MessageBoxButtons.OK,
                result.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.IsSuccess)
            {
                SetFormTextBasedMode("Update Person Contact", "Update Person Contact", "Update");
                PrintContactId(Person.contactInfo.ContactId);
                CommonOperations.UpdateAfterAdding(Person.Contacts, Person.contactInfo);
            }
        }
        public void PrintContactId(int contactId) => lblContactId.Text = contactId != -1 ? $"C-{contactId}" : "[Not Set]";
        private void txtPhoneNumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((MaskedTextBox)sender, errors);

            if (!ValidationHelper.IsValidPhoneNumber(txtPhoneNumber.Text))
            {
                errors.SetError(txtPhoneNumber, "Invalid phone number length");
                return;
            }

            errors.SetError(txtPhoneNumber, null);
        }
        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.HandleInputType(e, true, false);
        }
        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ValidationHelper.IsEmailValid(txtEmail.Text.Trim()))
            {
                errors.SetError(txtEmail, "Email is not valid");
                return;
            }

            errors.SetError(txtEmail, null);
        }
        private async void AddPersonContacts_Load(object sender, System.EventArgs e)
        {
            SetPage();

            if (personId > 0)
                await SetUpdate(personId);
        }
    }
}
