
using AppEnums.cs;
using ClinicSystem.Appointments;
using ClinicSystem.Helpers;
using ClinicSystem.Insurance;
using ClinicSystem.LabOrderTest;
using ClinicSystem.Login;
using ClinicSystem.MedicalRecord;
using ClinicSystem.Medicine;
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
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            ViewPerson viewPerson = new ViewPerson();
            viewPerson.ShowDialog();
        }

        private void addPersonContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            AddPersonContacts addContact = new AddPersonContacts();
            addContact.ShowDialog();
        }

        private void addNewStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            AddUpdateStaff addStaff = new AddUpdateStaff();
            addStaff.ShowDialog();
        }

        private void viewAllPeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            ManagePeople managePeople = new ManagePeople();
            managePeople.ShowDialog();
        }

        private void updateStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            ViewStaffInfo viewStaff = new ViewStaffInfo();
            viewStaff.ShowDialog();
        }

        private void manageStaffToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            ManageStaff manageStaff = new ManageStaff();
            manageStaff.ShowDialog();
        }

        private void registerPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            RegisterPatient patient = new RegisterPatient();
            patient.ShowDialog();
        }

        private void managePatientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            ManagePatient managePatient = new ManagePatient();
            managePatient.ShowDialog();
        }

        private void schedulesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            ManageSchedules schedules = new ManageSchedules();
            schedules.ShowDialog();
        }

        private void policiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            Managepolicies managepolicies = new Managepolicies();
            managepolicies.ShowDialog();
        }

        private void insurancesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            ManageInsurances manageInsurances = new ManageInsurances();
            manageInsurances.ShowDialog();
        }

        private void addMedicalRecordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Doctor))
                return;

            AddUpdateMedicalRecord addMedicalRecord = new AddUpdateMedicalRecord();
            addMedicalRecord.ShowDialog();
        }

        private void manageMedicalRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Doctor))
                return;

            ManageMedicalRecord manageMedical = new ManageMedicalRecord();
            manageMedical.ShowDialog();
        }

        private void addMedicalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Doctor))
                return;

            ViewMedicalRecord viewMRecord = new ViewMedicalRecord();
            viewMRecord.ShowDialog();
        }

        private void bookAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            BookAppointment bookAppointment =
                new BookAppointment(EnAppointmentStatuses.Scheduled);

            bookAppointment.ShowDialog();
        }

        private void manageAppointmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner, EnRole.Doctor))
                return;

            ManageAppointments manageAppointments = new ManageAppointments();
            manageAppointments.ShowDialog();
        }

        private void viewAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(
                EnRole.Receptionist,
                EnRole.Doctor,
                EnRole.Nurse,
                EnRole.Owner))
                return;

            ViewAppointment viewAppointment = new ViewAppointment();
            viewAppointment.ShowDialog();
        }

        private void manageMedicinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            ManageMedicines manageMedicines = new ManageMedicines();
            manageMedicines.ShowDialog();
        }

        private void myLabQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Nurse))
                return;

            ManageMyLabQueue LabQueue = new ManageMyLabQueue();
            LabQueue.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();

            SystemLogin login = new SystemLogin();
            login.ShowDialog();
        }
    }
}
