using AppEnums.cs;
using Business;
using ClinicSystem.AppointmentReminder;
using ClinicSystem.Helpers;
using Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace ClinicSystem.Appointments
{
    public partial class ManageAppointments : FormsDefaultSettings
    {
        private BindingList<AppointmentList> Appointments { get; set; }
        private int PageNumber = 0;
        public ManageAppointments()
        {
            InitializeComponent();
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            string value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(dgvAppointments, Appointments, lblRecords, value.ToString()))
                return;

            List<object> temp = CommonOperations.FilterData(Appointments, filterName, value);
            CommonOperations.LoadDataToDGV(dgvAppointments, temp, lblRecords);
        }
        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
        private void SetUp()
        {
            cbFilterBy.Items.AddRange(new object[] { "None", "AppointmentID", "Patient", "Doctor", "Status", "CalenderDay" });
            cbFilterBy.SelectedIndex = 0;
        }
        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvAppointments, Appointments, lblRecords, null);
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "AppointmentID" }, CommonOperations.GetFilterName(cbFilterBy),
                5, 50);
        }
        private async void LoadAppointments()
        {
            try
            {
                Appointments = await AppointmentList.GetAppointment(PageNumber);
                CommonOperations.LoadDataToDGV(dgvAppointments, Appointments, lblRecords);
                RefreshFilter(0);

                lblSubTitle.Text = $"Appointments -- {AppointmentList.AppointmentSummary.BookingCount} booked · {AppointmentList.AppointmentSummary.CompletedCount} completed · {AppointmentList.AppointmentSummary.CancelledCount} cancelled";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Load appointments", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "Manage Appointment", "Application", EventLogEntryType.Error));
            }
        }
        private void SetNextPreviousButtons(bool status)
        {
            BtnPrevious.Visible = status;
            BtnNext.Visible = status;
        }
        private void ManageAppointments_Load(object sender, EventArgs e)
        {
            SetNextPreviousButtons(Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess);

            SetUp();
            LoadAppointments();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "AppointmentID")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);
        }
        private void btnClose_Click(object sender, EventArgs e) => Close();
        private void bookAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            BookAppointment appointment = new BookAppointment(EnAppointmentStatuses.Scheduled);
            appointment.OnSave += RefreshDgv;
            appointment.ShowDialog();
        }
        private async void RefreshDgv(Appointment appointment)
        {
            AppointmentList appointmentList = await AppointmentList.FindAppointmentListByID(appointment.AppointmentID);

            int targetIndex = CommonOperations.FindTargetIndexOfObj(Appointments, "AppointmentID", appointment.AppointmentID);
            CommonOperations.UpdateAfterAdding(Appointments, appointmentList, targetIndex);

            lblRecords.Text = $"{dgvAppointments.Rows.Count} records";
        }
        private void viewAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Doctor, EnRole.Owner))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvAppointments))
                return;

            int appointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            ViewAppointment viewAppointment = new ViewAppointment(appointmentId);
            viewAppointment.OnSave += RefreshDgv;
            viewAppointment.ShowDialog();
        }
        private void SetControlEnableStatus(bool status)
        {
            rescheduleToolStripMenuItem.Enabled = status;
            viewAppointmentToolStripMenuItem.Enabled = status;
            goToAppointmentToolStripMenuItem.Enabled = status;
            cancelAppointmentToolStripMenuItem.Enabled = status;
            sendReminderToolStripMenuItem.Enabled = status;
            viewAppointmentRemindersToolStripMenuItem.Enabled = status;
        }
        private async void cmsAppointments_Opening(object sender, CancelEventArgs e)
        {
            SetControlEnableStatus(!ValidationHelper.IsDgvEmptyOrNull(dgvAppointments));

            if (ValidationHelper.IsDgvEmptyOrNull(dgvAppointments))
                return;

            int appointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            Appointment app = await Appointment.FindAppointmentByID(appointmentId);

            if (ValidationHelper.IsEmptyOrNull(app, "Appointment is not found"))
                return;

            rescheduleToolStripMenuItem.Enabled = app.IsStillActive();
            goToAppointmentToolStripMenuItem.Enabled = app.IsStillActive();
            sendReminderToolStripMenuItem.Enabled = app.IsStillActive();
            cancelAppointmentToolStripMenuItem.Enabled = app.IsStillActive();
            viewAppointmentRemindersToolStripMenuItem.Enabled = app.DoesHasAppointmentReminders(app.AppointmentID);
        }
        private void dgvAppointments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Doctor, EnRole.Owner))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvAppointments) || e.RowIndex < 0)
                return;

            int appointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            ViewAppointment viewAppointment = new ViewAppointment(appointmentId);
            viewAppointment.ShowDialog();
        }
        private void rescheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvAppointments))
                return;

            int appointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            BookAppointment viewAppointment = new BookAppointment(appointmentId, EnAppointmentStatuses.Rescheduled);
            viewAppointment.OnSave += RefreshDgv;
            viewAppointment.ShowDialog();
        }
        private void goToAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Doctor))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvAppointments))
                return;

            int appointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            PerformAppointment performAppointment = new PerformAppointment(appointmentId);
            performAppointment.OnSave += RefreshDgv;
            performAppointment.ShowDialog();
        }
        private void sendRemainderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            int appointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;
            AddAppointmentReminder addReminder = new AddAppointmentReminder(appointmentId);
            addReminder.ShowDialog();
        }
        private void viewAppointmentRemindersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            int appointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            ManageAppointmentReminders reminders = new ManageAppointmentReminders(appointmentId);
            reminders.ShowDialog();
        }
        private async void cancelAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            int appointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;
            Appointment appointment = await Appointment.FindAppointmentByID(appointmentId);
            OperationResult result = await appointment.CancelAppointment(appointmentId);

            MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, result.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (!result.IsSuccess)
                return;

            RefreshDgv(appointment);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (ValidationHelper.IsDgvEmptyOrNull(dgvAppointments) && PageNumber > 0)
            {
                MessageBox.Show("You have reached the end of appointments", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PageNumber++;
            ManageAppointments_Load(sender, e);
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (PageNumber <= 0 && ValidationHelper.IsDgvEmptyOrNull(dgvAppointments))
            {
                MessageBox.Show("There is no previous page", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PageNumber--;
            ManageAppointments_Load(sender, e);
        }
    }
}
