using ClinicSystem.People;
using ClinicSystem.People.Controls;
using ClinicSystem.Schedule;
using ClinicSystem.Staff;
using System;
using System.Windows.Forms;

namespace ClinicSystem
{
    public partial class MainForm : FormsDefaultSettings
    {
        public MainForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdatePerson addPerson = new AddUpdatePerson();
            addPerson.ShowDialog();
        }

        private void viewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewPerson viewPerson = new ViewPerson();
            viewPerson.ShowDialog();
        }

        private void addPersonContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPersonContacts addContact = new AddPersonContacts();
            addContact.ShowDialog();
        }

        private void addNewStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateStaff addStaff = new AddUpdateStaff();
            addStaff.ShowDialog();
        }

        private void viewAllPeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePeople managePeople = new ManagePeople();
            managePeople.ShowDialog();
        }

        private void updateStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewStaffInfo viewStaff = new ViewStaffInfo();
            viewStaff.ShowDialog();
        }

        private void addScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateSchedule addSchedule = new AddUpdateSchedule();
            addSchedule.ShowDialog();
        }

        private void manageStaffToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageStaff manageStaff = new ManageStaff();
            manageStaff.ShowDialog();
        }

        private void manageSchedulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageSchedules manageSchedules = new ManageSchedules();
            manageSchedules.ShowDialog();
        }
    }
}
