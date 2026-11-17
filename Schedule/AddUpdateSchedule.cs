
using Business;
using ClinicSystem.Helpers;
using System;
using System.Windows.Forms;

namespace ClinicSystem.Schedule
{
    public partial class AddUpdateSchedule : FormsDefaultSettings
    {
        Business.Schedule schedule = new Business.Schedule();
        private readonly int ScheduleId = -1;
        public AddUpdateSchedule()
        {
            InitializeComponent();
            schedule = new Business.Schedule();
            SetFormTextBasedMode("Add New Schedule", "Add New Schedule", "Save Schedule");
        }

        public delegate void SendScheduleInfo(Business.Schedule schedule);
        public event SendScheduleInfo OnSave;
        public AddUpdateSchedule(int scheduleId)
        {
            InitializeComponent();
            SetFormTextBasedMode("Update Schedule", "Update Schedule", "Update");
            Load += AddUpdateSchedule_Load;
        }
        private async void AddUpdateSchedule_Load(object sender, EventArgs e)
        {
            schedule = await Business.Schedule.FindScheduleByID(ScheduleId);

            if (ValidationHelper.IsEmptyOrNull(schedule))
            {
                lblScheduleId.Text = "[Not Set]";
                return;
            }

            txtShiftName.Text = schedule.ShiftName;
            dtpStart.Value = DateTime.Today.Add(new TimeSpan(schedule.WorkStartFrom.Hours, schedule.WorkStartFrom.Minutes, schedule.WorkStartFrom.Seconds));

            dtpEnd.Value = DateTime.Today.Add(new TimeSpan(schedule.WorkEndAt.Hours, schedule.WorkEndAt.Minutes, schedule.WorkEndAt.Seconds));

            lblDuration.Text = (schedule.WorkEndAt - schedule.WorkStartFrom).ToString();
        }
        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            this.Text = pageTitle;
            btnAddSchedule.Text = saveButtonText;
            lblScheduleId.Text = "Not Set";
        }
        private void btnCancel_Click(object sender, EventArgs e) => Close();
        private void CollectData()
        {
            schedule.ShiftName = txtShiftName.Text;
            schedule.WorkStartFrom = new TimeSpan(dtpStart.Value.Hour, dtpStart.Value.Minute, dtpStart.Value.Second);
            schedule.WorkEndAt = new TimeSpan(dtpEnd.Value.Hour, dtpEnd.Value.Minute, dtpEnd.Value.Second);
        }
        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            ValidateChildren();

            CollectData();
            lblDuration.Text = (schedule.WorkEndAt - schedule.WorkStartFrom).ToString();

            OperationResult result = ValidationHelper.ValidateForm(this, errors);

            if (!result.IsSuccess)
                return;

            result = schedule.Save();

            MessageBox.Show(
                result.Message,
                result.Title,
                MessageBoxButtons.OK,
                result.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.IsSuccess)
            {
                SetFormTextBasedMode("Update schedule", "Update", "Update");
                lblScheduleId.Text = $"Sch-{schedule.ScheduleId}";
                OnSave?.Invoke(schedule);
            }
        }
        private void txtShiftName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(txtShiftName))
                errors.SetError(txtShiftName, "shift name is required");
            else
                errors.SetError(txtShiftName, null);
        }
        private void dtpStart_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dtpStart.Value >= dtpEnd.Value)
                errors.SetError(dtpStart, "start must be less then end");
            else
                errors.SetError(dtpStart, null);
        }
        private void dtpEnd_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dtpEnd.Value <= dtpStart.Value)
                errors.SetError(dtpEnd, "end must be greater then start");
            else
                errors.SetError(dtpEnd, null);
        }
    }
}
