using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.People
{
    public partial class ManagePeople : FormsDefaultSettings
    {
        private List<Person> People { get; set; }
        public ManagePeople()
        {
            InitializeComponent();
        }

        private async void LoadPeopleInfo()
        {
            People = await Person.GetPeople();

            if (CommonOperations.LoadDataToDGV(dgvPeople, People, lblRecords))
            {
                dgvPeople.Columns["PersonID"].HeaderText = "#";
            }
        }

        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, cbGender, txtFilter, dgvPeople, People, lblRecords, "Gender");
        }

        private void SetUp()
        {
            cbGender.Items.AddRange(new object[] { "None", "Male", "Female" });
            cbGender.SelectedIndex = 0;

            cbFilterBy.Items.AddRange(new object[] { "None", "PersonID", "Firstname", "Lastname", "Gender" });
            RefreshFilter(0);
        }
        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void ManagePeople_Load(object sender, System.EventArgs e)
        {
            LoadPeopleInfo();
            SetUp();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);

            if (filterName == "PersonID")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);

        }
    }
}
