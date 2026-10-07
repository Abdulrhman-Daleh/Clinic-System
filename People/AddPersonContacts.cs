using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System.Windows.Forms;

namespace ClinicSystem.People
{
    public partial class AddPersonContacts : FormsDefaultSettings
    {
        private Person Person;
        public AddPersonContacts()
        {
            InitializeComponent();
            SetFormTextBasedMode("Add Person Contact", "Add Person Contact", "Add Contact");
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
            personContactCtrl1.SendContact += ReciveContactInfo;
        }

        private void ReciveContactInfo(string email, string phoneNumber, int contactTypeId)
        {
            Person.contactInfo.Email = email;
            Person.contactInfo.PhoneNumber = phoneNumber;
            Person.contactInfo.ContactTypeId = (EnContactTypes)contactTypeId + 1;
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

            if (!personContactCtrl1.TryToSendContact())
                return;

            if (ValidationHelper.IsEmptyOrNull(Person.contactInfo, "Contact does not exists"))
                return;


            if (Person.contactInfo.Save())
            {
                SetFormTextBasedMode("Update Person Contact", "Update Person Contact", "Update");
                MessageBox.Show("Data Saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblContactId.Text = Person.contactInfo.ContactId.ToString();
                personContactCtrl1.PrintContactId(Person.contactInfo.ContactId);
            }
            else
                MessageBox.Show("Failed to save", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
