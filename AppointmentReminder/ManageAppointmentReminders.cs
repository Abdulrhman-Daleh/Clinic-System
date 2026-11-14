using AppEnums.cs;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClinicSystem.AppointmentReminder
{
    public partial class ManageAppointmentReminders : FormsDefaultSettings
    {
        private BindingList<Business.AppointmentReminder> AppointmentReminders { get; set; }
        private int _AppointmetId { get; set; }
        public ManageAppointmentReminders(int appointmentId)
        {
            InitializeComponent();
            _AppointmetId = appointmentId;
        }

        private void RefreshFilter(int index)
        {
            cbFilterBy.SelectedIndex = index;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvReminders, AppointmentReminders, lblRecords, null);
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "ReminderID" }, CommonOperations.GetFilterName(cbFilterBy),
               5, 50);
        }
        private async void LoadInfo(int appointmentId)
        {
            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(appointmentId);
            AppointmentReminders = await Business.AppointmentReminder.GetAppointmentReminders(appointmentId);
            RefreshFilter(0);
        }

        private void cbFilterBy_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void ManageAppointmentReminders_Load(object sender, System.EventArgs e)
        {
            cbFilterBy.Items.AddRange(new object[] { "None", "ReminderID", "Subject" });

            LoadInfo(_AppointmetId);
            RefreshFilter(0);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            string value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(dgvReminders, AppointmentReminders, lblRecords, value.ToString()))
                return;

            List<object> temp = CommonOperations.FilterData(AppointmentReminders, CommonOperations.GetFilterName(cbFilterBy), value);
            CommonOperations.LoadDataToDGV(dgvReminders, temp, lblRecords);
        }

        private void RefreshDgv(Business.AppointmentReminder reminder)
        {
            int targetIndex = CommonOperations.FindTargetIndexOfObj(AppointmentReminders, "ReminderID", reminder.ReminderID);
            CommonOperations.UpdateAfterAdding(AppointmentReminders, reminder, targetIndex);
        }

        private void sendReminderToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner)) return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvReminders)) return;

            int appointmentId = (int)dgvReminders.CurrentRow.Cells["AppointmentID"].Value;
            AddAppointmentReminder addReminder = new AddAppointmentReminder(appointmentId);
            addReminder.OnSave += RefreshDgv;
            addReminder.ShowDialog();
        }
    }
}
