using Business;
using ClinicSystem.Helpers;
using System;
using System.Windows.Forms;

namespace ClinicSystem.People.Controls
{
    public partial class FindPersonCtrl : UserCtrlDefaultSettings
    {
        public FindPersonCtrl()
        {
            InitializeComponent();
            SetUp();
        }

        public delegate void SendOnFind(Person person);
        public event SendOnFind OnFind;
        private void SetUp()
        {
            txtFindBy.Text = string.Empty;
            txtFindBy.Focus();
            cbFindBy.Items.Add("PersonID");
            cbFindBy.Items.Add("Firstname");
            txtFindBy.MaxLength = 5;
            cbFindBy.SelectedIndex = 0;
            personalInfoCtrl1.Enabled = false;
        }

        private void cbFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFindBy.Text == "PersonID")
            {
                ValidationHelper.HandleInputType(e, true, false);
                return;
            }

            ValidationHelper.HandleInputType(e, false, true);
        }

        private void Find()
        {
            Person person = null;

            if (ValidationHelper.IsEmptyOrNull(txtFindBy.Text, "not allowed to search with empty value"))
            {
                txtFindBy.Text = string.Empty;
                txtFindBy.Focus();
                return;
            }

            switch (cbFindBy.Text)
            {
                case "PersonID":
                    person = Person.FindPersonById(Convert.ToInt32(txtFindBy.Text));
                    txtFindBy.MaxLength = 5;
                    break;

                default:
                    person = Person.FindPersonByFirstname(txtFindBy.Text);
                    txtFindBy.MaxLength = 30;
                    break;
            }
            personalInfoCtrl1.LoadInfoToPage(person);
            OnFind?.Invoke(person);

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Find();
        }

        private void txtFindBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox(txtFindBy, errors);
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidationHelper.ResetDefaultFilter(txtFindBy);
        }

        private void RecivePersonInfo(Person person)
        {
            cbFindBy.SelectedIndex = 0;
            txtFindBy.Text = person.PersonId.ToString();
            personalInfoCtrl1.LoadInfoToPage(person);
            OnFind?.Invoke(person);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddUpdatePerson addPerson = new AddUpdatePerson();
            addPerson.SendPersonInfo += RecivePersonInfo;
            addPerson.ShowDialog();
        }
    }
}
