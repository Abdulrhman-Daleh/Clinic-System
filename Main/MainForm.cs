using ClinicSystem.Patients;
using ClinicSystem.People;
using ClinicSystem.People.Controls;
using ClinicSystem.Policies;
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

        private void registerPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterPatient patient = new RegisterPatient();
            patient.ShowDialog();
        }

        private void managePatientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePatient managePatient = new ManagePatient();
            managePatient.ShowDialog();
        }

        private void addPolicyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdatePolicy addPolicy = new AddUpdatePolicy();
            addPolicy.ShowDialog();
        }

        private void viewPolicyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewPolicyInfo viewPolicy = new ViewPolicyInfo();
            viewPolicy.ShowDialog();
        }

        private void managePoliciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Managepolicies policies = new Managepolicies();
            policies.ShowDialog();
        }
    }
}
