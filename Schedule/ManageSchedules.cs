using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.Schedule
{
    public partial class ManageSchedules : FormsDefaultSettings
    {
        private BindingList<Business.Schedule> Schedules;
        private string FilterName { get; set; }
        public ManageSchedules()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, System.EventArgs e) => Close();
        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvSchedules, Schedules, lblRecords, null);
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "Staff no." }, FilterName,
                5, 20);
        }
        private void SetUp()
        {
            cbFilterBy.Items.AddRange(new object[] { "None", "ShiftName" });
            cbFilterBy.SelectedIndex = 0;
        }
        private async void LoadScheduleInfo()
        {
            Schedules = await Business.Schedule.GetSchedules();
            CommonOperations.LoadDataToDGV(dgvSchedules, Schedules, lblRecords);
            RefreshFilter(0);
        }
        private void ManageSchedules_Load(object sender, System.EventArgs e)
        {
            SetUp();
            LoadScheduleInfo();
        }
        private void cbFilterBy_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
        private string PreviousText { get; set; } = string.Empty;
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                PerformFilter();
            }
        }
        private void PerformFilter()
        {
            string value = txtFilter.Text.Trim();
            if (PreviousText != value)
            {
                if (CommonOperations.ResetDataGrid(dgvSchedules, Schedules, lblRecords, value.ToString()))
                    return;

                List<object> temp = CommonOperations.FilterData(Schedules, FilterName, value);
                CommonOperations.LoadDataToDGV(dgvSchedules, temp, lblRecords);
            }
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FilterName = CommonOperations.GetFilterName(cbFilterBy);
            RefreshFilter(cbFilterBy.SelectedIndex);
        }
        private void RefreshDgv(Business.Schedule schedule)
        {
            int targetIndex = CommonOperations.FindTargetIndexOfObj(Schedules, "ScheduleId", schedule.ScheduleId);
            CommonOperations.UpdateAfterAdding(Schedules, schedule, targetIndex);
        }
        private void addNewScheduleToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            AddUpdateSchedule addSchedule = new AddUpdateSchedule();
            addSchedule.OnSave += RefreshDgv;
            addSchedule.ShowDialog();
        }
        private void updateScheduleToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int scheduleId = (int)dgvSchedules.CurrentRow.Cells["ScheduleID"].Value;

            AddUpdateSchedule updateSchedule = new AddUpdateSchedule(scheduleId);
            updateSchedule.OnSave += RefreshDgv;
            updateSchedule.ShowDialog();
        }
        private void dgvSchedules_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (ValidationHelper.IsDgvEmptyOrNull(dgvSchedules))
                return;

            int scheduleId = (int)dgvSchedules.CurrentRow.Cells["ScheduleID"].Value;

            AddUpdateSchedule updateSchedule = new AddUpdateSchedule(scheduleId);
            updateSchedule.OnSave += RefreshDgv;
            updateSchedule.ShowDialog();
        }
        private void SetControlEnableStatus(bool status)
        {
            updateScheduleToolStripMenuItem.Enabled = status;
        }
        private void cmsSchedules_Opening(object sender, CancelEventArgs e)
        {
            SetControlEnableStatus(!ValidationHelper.IsDgvEmptyOrNull(dgvSchedules));
        }
    }
}
