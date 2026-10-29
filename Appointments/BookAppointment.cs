using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System;
using System.Windows.Forms;

namespace ClinicSystem.Appointments
{
    public partial class BookAppointment : FormsDefaultSettings
    {
        private Appointment appointment;
        public BookAppointment(EnAppointmentStatuses appointmentStatus)
        {
            InitializeComponent();
            SetUp();
            appointment = new Appointment();
            appointment.AppointmentStatuses = (appointmentStatus, appointmentStatus.ToString());
            SetFormTextBasedMode("Add Appointment", "Add Appointment", "Add Appointment");
        }

        public delegate void SendAppointmentOnSave(Appointment appointment);
        public event SendAppointmentOnSave OnSave;
        private void SetFormTextBasedMode(
            string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            Text = pageTitle;
            btnSave.Text = saveButtonText;
        }
        private void BtnCancel_Click(object sender, System.EventArgs e) => Close();

        private void SetUp()
        {
            dtpAppointmentDate.MinDate = DateTime.Now;
            dtpAppointmentDate.MaxDate = DateTime.Now.AddMonths(1);
            lblStatus.Text = "Not Set";
            LoadAppointmentTypes();
            findPatientCtrl1.AllowAddNew = false;
            findStaffCtrl1.AllowAddNew = false;
        }

        private async void LoadAppointmentTypes()
        {
            cbType.DataSource = await Appointment.GetAppointmentTypesNames();

            cbType.SelectedIndex = 0;
        }

        private void dtpEnd_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dtpEnd.Value <= dtpStart.Value)
                errors.SetError(dtpEnd, "End time is before start time");
            else
                errors.SetError(dtpEnd, null);

            if ((dtpEnd.Value - dtpStart.Value).TotalHours > 2 || (dtpEnd.Value - dtpStart.Value).TotalMinutes < 60)
                errors.SetError(dtpEnd, "appointment duration is not appropriate must be < 2h and at least 60min");
            else
                errors.SetError(dtpEnd, null);
        }

        private void CollectData()
        {
            appointment.AppointmentTypes = ((EnAppointmentTypes)cbType.SelectedIndex + 1, (string)cbType.SelectedItem.ToString());
            appointment.AppointmentDate = dtpAppointmentDate.Value;
            appointment.AppointmentTime = new TimeSpan(dtpStart.Value.Hour, dtpStart.Value.Minute, dtpStart.Value.Second);
            appointment.AppointmentEndTime = new TimeSpan(dtpEnd.Value.Hour, dtpEnd.Value.Minute, dtpEnd.Value.Second);
        }

        private void BookAppointment_Load(object sender, EventArgs e)
        {
            findPatientCtrl1.SendPatient += RecivePatientInfo;
            findStaffCtrl1.OnFound += ReciveStaffInfo;
        }

        private void ReciveStaffInfo(Business.Staff staff)
        {
            if (!staff.IsRoleApplicableForAppointment())
            {
                errors.SetError(findStaffCtrl1, "Staff is not a doctor nor nurse");
                return;
            }
            else
                errors.SetError(findStaffCtrl1, null);


            appointment.Staff = staff;

            if (ValidationHelper.IsEmptyOrNull(staff))
            {
                appointment.StaffID = -1;
                return;
            }

            appointment.StaffID = staff.StaffId;
        }

        private void RecivePatientInfo(Patient patient)
        {
            appointment.Patient = patient;

            if (ValidationHelper.IsEmptyOrNull(patient))
            {
                appointment.PatientID = -1;
                return;
            }

            appointment.PatientID = patient.PatientId;
        }

        private void Save()
        {
            ValidateChildren();

            if (ValidationHelper.NotValidToSave(this, errors))
            {
                MessageBox.Show("Not Valid to save", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CollectData();

            if (!appointment.Staff.IsActive)
            {
                MessageBox.Show("Staff is not active", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!appointment.NoOverlapWithLastAppointment())
            {
                MessageBox.Show("Staff has an overlap in appointment date", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!appointment.Save())
            {
                MessageBox.Show("Failed to save", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblAppointmentId.Text = "App-" + appointment.AppointmentID;
            SetFormTextBasedMode("Update Appointment", "Update Appointment", "Update");
            findPatientCtrl1.AllowedFind = false;
            findStaffCtrl1.AllowFind = false;
            OnSave?.Invoke(appointment);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
