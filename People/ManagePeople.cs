using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using ClinicSystem.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.People
{
    public partial class ManagePeople : FormsDefaultSettings
    {
        private BindingList<Person> People { get; set; }
        private string FilterName { get; set; }
        public ManagePeople()
        {
            InitializeComponent();
        }
        private async void LoadPeopleInfo()
        {
            People = await Person.GetPeople();

            if (CommonOperations.LoadDataToDGV(dgvPeople, People, lblRecords))
            {
                dgvPeople.Columns["PersonId"].HeaderText = "#";
            }

            lblSubTitle.Text = $"People -- {Person.PeopleSummary.PeopleCount} people · {Person.PeopleSummary.PatientCount} patients · {Person.PeopleSummary.StaffCount} staff";

            RefreshFilter(0);
        }
        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, cbGender, txtFilter, dgvPeople, People, lblRecords, "Gender");
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "PersonId" }, FilterName,
                5, 10);
        }
        private void SetUp()
        {
            cbGender.Items.AddRange(new object[] { "Both", "Male", "Female" });
            cbGender.SelectedIndex = 0;

            cbFilterBy.Items.AddRange(new object[] { "None", "PersonId", "Firstname", "Lastname", "Gender" });
        }
        private void btnClose_Click(object sender, EventArgs e) => Close();
        private void ManagePeople_Load(object sender, EventArgs e)
        {
            LoadPeopleInfo();
            SetUp();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterName = CommonOperations.GetFilterName(cbFilterBy);
            RefreshFilter(cbFilterBy.SelectedIndex);
        }
        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (FilterName == "PersonId")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);

        }
        private string PreviousText { get; set; } = string.Empty;
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                PerformFilter();
            }
        }
        private void PerformFilter()
        {
            string value = txtFilter.Text.Trim();
            if (PreviousText != value)
            {
                PreviousText = value;

                if (CommonOperations.ResetDataGrid(dgvPeople, People, lblRecords, value.ToString()))
                    return;

                List<object> temp = CommonOperations.FilterData(People, FilterName, value);
                CommonOperations.LoadDataToDGV(dgvPeople, temp, lblRecords);
            }
        }
        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGender.Text == "Both")
            {
                CommonOperations.LoadDataToDGV(dgvPeople, People, lblRecords);
                return;
            }

            if (CommonOperations.ResetDataGrid(dgvPeople, People, lblRecords, cbGender.Text.ToLower()))
                return;

            object value = cbGender.Text.Trim();
            List<object> temp = CommonOperations.FilterData(People, FilterName, value.Equals("Male") ? nameof(EnGender.Male) : nameof(EnGender.Female));
            CommonOperations.LoadDataToDGV(dgvPeople, temp, lblRecords);
        }
        private void SetControlEnableStatus(bool status)
        {
            updatePersonToolStripMenuItem.Enabled = status;
            viewPersonToolStripMenuItem.Enabled = status;
            addPersonContactToolStripMenuItem.Enabled = status;
        }
        private void cmsPeople_Opening(object sender, CancelEventArgs e)
        {
            SetControlEnableStatus(!ValidationHelper.IsDgvEmptyOrNull(dgvPeople));
        }
        private void dgvPeople_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ValidationHelper.IsDgvEmptyOrNull(dgvPeople))
                return;

            int PersonId = (int)dgvPeople.CurrentRow.Cells["PersonId"].Value;

            ViewPerson viewPerson = new ViewPerson(PersonId);
            viewPerson.OnEdit += RefreshDgv;
            viewPerson.ShowDialog();
        }
        private void RefreshDgv(Person person)
        {
            int targetIndex = CommonOperations.FindTargetIndexOfObj(People, "PersonId", person.PersonId);
            CommonOperations.UpdateAfterAdding(People, person, targetIndex);
        }
        private void updatePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            int PersonId = (int)dgvPeople.CurrentRow.Cells["PersonId"].Value;

            AddUpdatePerson updatePerson = new AddUpdatePerson(PersonId);
            updatePerson.SendPersonInfo += RefreshDgv;
            updatePerson.ShowDialog();
        }
        private void viewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            int PersonId = (int)dgvPeople.CurrentRow.Cells["PersonId"].Value;

            ViewPerson viewPerson = new ViewPerson(PersonId);
            viewPerson.OnEdit += RefreshDgv;
            viewPerson.ShowDialog();
        }
        private void addPersonContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            int PersonId = (int)dgvPeople.CurrentRow.Cells["PersonId"].Value;

            AddPersonContacts addContact = new AddPersonContacts(PersonId);
            addContact.ShowDialog();
        }
    }
}
