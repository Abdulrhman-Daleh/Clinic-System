using Business;
using ClinicSystem.Helpers;
using ClinicSystem.Properties;

namespace ClinicSystem.People.Controls
{
    public partial class ViewPerson : FormsDefaultSettings
    {
        private Person person;
        public ViewPerson()
        {
            InitializeComponent();
            findPersonCtrl1.AllowFilter = true;
            ResetEmptyControls();
        }

        private void ResetEmptyControls()
        {
            btnEdit.Enabled = false;
            pbGender.Image = null;
            lblName.Text = "[Not Set]";
            viewPersonContacts1.LoadUpPersonContact(-1);
        }
        private void SetInfo(Person person)
        {

            if (ValidationHelper.IsEmptyOrNull(person, "Person does not exists"))
            {
                ResetEmptyControls();
                return;
            }

            btnEdit.Enabled = true;
            pbGender.Image = this.person.Gender == AppEnums.cs.EnGender.Male ? Resources.male_32 : Resources.female_32;
            viewPersonContacts1.LoadUpPersonContact(person.PersonId);
            lblName.Text = $"Person #{person.PersonId} - {this.person.GetFullName()}";
        }

        public ViewPerson(int personId)
        {
            InitializeComponent();
            person = Person.FindPersonById(personId);
            findPersonCtrl1.AllowFilter = false;
            SetInfo(person);
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            AddUpdatePerson updatePerson = new AddUpdatePerson(person.PersonId);
            updatePerson.ShowDialog();
        }

        private void ViewPerson_Load(object sender, System.EventArgs e)
        {
            findPersonCtrl1.OnFind += RecivePersonObj;
        }

        private void RecivePersonObj(Person person)
        {
            this.person = person;
            SetInfo(person);
        }
    }
}
