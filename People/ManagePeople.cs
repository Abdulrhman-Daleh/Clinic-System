using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using ClinicSystem.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ClinicSystem.People
{
    public partial class ManagePeople : FormsDefaultSettings
    {
        private BindingList<Person> People { get; set; }
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

            RefreshFilter(0);
        }
        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, cbGender, txtFilter, dgvPeople, People, lblRecords, "Gender");
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "PersonID" }, CommonOperations.GetFilterName(cbFilterBy),
                5, 10);
        }
        private void SetUp()
        {
            cbGender.Items.AddRange(new object[] { "None", "Male", "Female" });
            cbGender.SelectedIndex = 0;

            cbFilterBy.Items.AddRange(new object[] { "None", "PersonID", "Firstname", "Lastname", "Gender" });
        }
        private void btnClose_Click(object sender, EventArgs e) => Close();
        private void ManagePeople_Load(object sender, EventArgs e)
        {
            LoadPeopleInfo();
            SetUp();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
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
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            object value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(dgvPeople, People, lblRecords, value.ToString()))
                return;

            List<object> temp = CommonOperations.FilterData(People, filterName, value);
            CommonOperations.LoadDataToDGV(dgvPeople, temp, lblRecords);
        }
        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy).ToLower();

            if (CommonOperations.ResetDataGrid(dgvPeople, People, lblRecords, cbGender.Text.ToLower()))
                return;

            object value = cbGender.Text.Trim();
            List<object> temp = CommonOperations.FilterData(People, filterName, value.Equals("Male") ? EnGender.Male : EnGender.Female);
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
            int personId = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;

            ViewPerson viewPerson = new ViewPerson(personId);
            viewPerson.OnEdit += RefreshDgv;
            viewPerson.ShowDialog();
        }
        private void RefreshDgv(Person person)
        {
            int targetIndex = -1;

            if (People.Any(x => x.PersonId == person.PersonId))
                targetIndex = People.IndexOf(People.First(x => x.PersonId == person.PersonId));

            CommonOperations.UpdateAfterAdding(People, person, targetIndex);
        }
        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdatePerson addPerson = new AddUpdatePerson();
            addPerson.SendPersonInfo += RefreshDgv;
            addPerson.ShowDialog();
        }
        private void updatePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personId = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;

            AddUpdatePerson updatePerson = new AddUpdatePerson(personId);
            updatePerson.SendPersonInfo += RefreshDgv;
            updatePerson.ShowDialog();
        }
        private void viewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personId = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;

            ViewPerson viewPerson = new ViewPerson(personId);
            viewPerson.OnEdit += RefreshDgv;
            viewPerson.ShowDialog();
        }
        private void addPersonContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPersonContacts addContact = new AddPersonContacts();
            addContact.ShowDialog();
        }
    }
}
