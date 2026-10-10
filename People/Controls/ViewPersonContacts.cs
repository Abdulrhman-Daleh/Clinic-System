namespace ClinicSystem.People.Controls
{
    public partial class ViewPersonContacts : UserCtrlDefaultSettings
    {
        public ViewPersonContacts()
        {
            InitializeComponent();
        }
        public void LoadUpPersonContact(int personId)
        {
            LoadContactData(personId);
        }

        private async void LoadContactData(int personId)
        {
            //  BindingList<PersonContact> contacts = await PersonContact.GetShortContactInfo(personId);
            //
            //  CommonOperations.LoadDataToDGV(dgvContact, contacts, lblRecords);
            //  dgvContact.Columns["ContactTypeID"].Visible = false;
            //  dgvContact.Columns["ContactID"].Visible = false;
        }
    }
}
