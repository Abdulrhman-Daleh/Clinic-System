using Business;
using ClinicSystem.Helpers;
using System;
using System.Windows.Forms;

namespace ClinicSystem.AppointmentReminder
{
    public partial class AddAppointmentReminder : FormsDefaultSettings
    {
        Business.AppointmentReminder Reminder { get; set; }
        public AddAppointmentReminder(int appointmentId)
        {
            InitializeComponent();
            Reminder = new Business.AppointmentReminder();
            LoadCtrl(appointmentId);
        }

        public delegate void SendReminderOnSave(Business.AppointmentReminder reminder);
        public event SendReminderOnSave OnSave;

        private void LoadCtrl(int appointmentId)
        {
            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(appointmentId);

            if (!ValidationHelper.IsEmptyOrNull(Global.CurrentLoggedInStaff))
                viewStaffInfoCtrl1.LoadStaffInfo(Global.CurrentLoggedInStaff);
        }

        private void CollectData()
        {
            Reminder.Appointment = viewAppointmentInfoCtrl1.CurrentAppointment;
            Reminder.AppointmentID = Reminder.Appointment.AppointmentID;

            Reminder.ReminderDate = DateTime.Now;
            Reminder.Subject = txtSubject.Text.Trim();
            Reminder.Title = txtTitle.Text.Trim();
            Reminder.Message = txtMessage.Text.Trim();
        }

        private void BtnCancel_Click(object sender, System.EventArgs e) => Close();

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();

            OperationResult result = ValidationHelper.ValidateForm(this, errors);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CollectData();

            result = Reminder.Save();
            MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, result.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (!result.IsSuccess)
                return;

            Auditor.Log("Added Appointment Reminder", $"Staff {viewStaffInfoCtrl1.LoadedStaff.StaffId} => {viewStaffInfoCtrl1.LoadedStaff.GetFullName()} sended appointment reminder", "AppointmentReminder.Save");
            lblReminderId.Text = $"Rem-{Reminder.ReminderID}";
            btnSave.Enabled = false;
            OnSave?.Invoke(Reminder);
            BtnCancel.Focus();
        }

        private void txtMessage_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox box = (TextBox)sender;

            if (ValidationHelper.IsEmptyOrNull(box.Text))
                errors.SetError(box, "Field is required");
            else
                errors.SetError(box, null);
        }
    }
}
