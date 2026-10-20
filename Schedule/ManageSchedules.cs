using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClinicSystem.Schedule
{
    public partial class ManageSchedules : FormsDefaultSettings
    {
        private BindingList<Business.Schedule> Schedules;
        public ManageSchedules()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvSchedules, Schedules, lblRecords, null);
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "Staff no." }, CommonOperations.GetFilterName(cbFilterBy),
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

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            object value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(dgvSchedules, Schedules, lblRecords, value.ToString()))
                return;

            List<object> temp = CommonOperations.FilterData(Schedules, filterName, value);
            CommonOperations.LoadDataToDGV(dgvSchedules, temp, lblRecords);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void addNewScheduleToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            AddUpdateSchedule addSchedule = new AddUpdateSchedule();
            addSchedule.ShowDialog();
        }

        private void updateScheduleToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int scheduleId = (int)dgvSchedules.CurrentRow.Cells["ScheduleID"].Value;

            AddUpdateSchedule updateSchedule = new AddUpdateSchedule(scheduleId);
            updateSchedule.ShowDialog();
        }

        private void dgvSchedules_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            int scheduleId = (int)dgvSchedules.CurrentRow.Cells["ScheduleID"].Value;

            AddUpdateSchedule updateSchedule = new AddUpdateSchedule(scheduleId);
            updateSchedule.ShowDialog();
        }
    }
}
