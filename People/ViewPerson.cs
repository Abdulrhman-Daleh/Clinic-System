using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using ClinicSystem.Properties;

namespace ClinicSystem.People.Controls
{
    public partial class ViewPerson : FormsDefaultSettings
    {
        private Person person;
        private readonly int personId;
        public ViewPerson()
        {
            InitializeComponent();
            findPersonCtrl1.AllowFind = true;
            ResetEmptyControls();
        }

        public delegate void delEditedInfo(Person person);
        public event delEditedInfo OnEdit;
        private void ResetEmptyControls()
        {
            btnEdit.Enabled = false;
            pbGender.Image = null;
            lblName.Text = "[Not Set]";
            viewPersonContacts1.LoadUpPersonContact(-1);
        }
        private void SetInfo(Person per)
        {
            person = per;

            if (ValidationHelper.IsEmptyOrNull(person))
            {
                ResetEmptyControls();
                return;
            }

            findPersonCtrl1.AllowFind = false;
            btnEdit.Enabled = true;
            pbGender.Image = person.Gender == EnGender.Male ? Resources.male_32 : Resources.female_32;
            viewPersonContacts1.LoadUpPersonContact(person.PersonId);
            lblName.Text = $"Person #{person.PersonId} - {this.person.GetFullName()}";
            findPersonCtrl1.RecivePersonInfo(person);
        }

        public ViewPerson(int personId)
        {
            InitializeComponent();
            this.personId = personId;
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            if (!Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess && Global.CurrentLoggedInStaff.PersonId != person.PersonId)
                return;

            AddUpdatePerson updatePerson = new AddUpdatePerson(person.PersonId);
            updatePerson.SendPersonInfo += RecivePersonObj;
            updatePerson.ShowDialog();
        }

        private async void ViewPerson_Load(object sender, System.EventArgs e)
        {
            findPersonCtrl1.OnFind += RecivePersonObj;

            if (personId > 0)
                SetInfo(await Person.FindPersonById(personId));
        }

        private void RecivePersonObj(Person person)
        {
            this.person = person;
            SetInfo(person);
            OnEdit?.Invoke(person);
        }

        private void BtnReset_Click(object sender, System.EventArgs e)
        {
            ResetEmptyControls();
            findPersonCtrl1.RecivePersonInfo(null);
            findPersonCtrl1.AllowFind = true;
        }
    }
}
