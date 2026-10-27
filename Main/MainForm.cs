using AppEnums.cs;
using ClinicSystem.Insurance;
using ClinicSystem.Login;
using ClinicSystem.MedicalRecord;
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

        private void manageStaffToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageStaff manageStaff = new ManageStaff();
            manageStaff.ShowDialog();
        }

        private void registerPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterPatient patient = new RegisterPatient(EnAddMode.AddImmediately);
            patient.ShowDialog();
        }

        private void managePatientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePatient managePatient = new ManagePatient();
            managePatient.ShowDialog();
        }

        private void schedulesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageSchedules schedules = new ManageSchedules();
            schedules.ShowDialog();
        }

        private void policiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Managepolicies managepolicies = new Managepolicies();
            managepolicies.ShowDialog();
        }

        private void insurancesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageInsurances manageInsurances = new ManageInsurances();
            manageInsurances.ShowDialog();
        }

        private void addMedicalRecordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddUpdateMedicalRecord addMedicalRecord = new AddUpdateMedicalRecord();
            addMedicalRecord.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();

            SystemLogin login = new SystemLogin();
            login.ShowDialog();
        }
    }
}
