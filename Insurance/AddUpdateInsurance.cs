namespace ClinicSystem.Insurance
{
    public partial class AddUpdateInsurance : FormsDefaultSettings
    {

        public AddUpdateInsurance()
        {
            InitializeComponent();
            SetFormTextBasedMode("Add New Insurance", "Add Insurance", "Add Insurance");
        }

        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            this.Text = pageTitle;
            btnSave.Text = saveButtonText;
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => Close();

        private void btnSave_Click(object sender, System.EventArgs e)
        {

        }
    }
}
