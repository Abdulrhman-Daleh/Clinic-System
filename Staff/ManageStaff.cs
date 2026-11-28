
using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.Staff
{
    public partial class ManageStaff : FormsDefaultSettings
    {
        BindingList<StaffList> StaffList = new BindingList<StaffList>();
        private string FilterName { get; set; }
        public ManageStaff()
        {
            InitializeComponent();
        }
        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvStaff, StaffList, lblRecords, null);
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "Staff no." }, FilterName,
                5, 50);
        }
        private void SetUp()
        {
            cbFilterBy.Items.AddRange(new object[] { "None", "StaffID", "FullName", "Username" });
            cbFilterBy.SelectedIndex = 0;
        }
        private void btnClose_Click(object sender, System.EventArgs e) => Close();
        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FilterName = CommonOperations.GetFilterName(cbFilterBy);
            RefreshFilter(cbFilterBy.SelectedIndex);
        }
        private async void RefreshDgv(int staffId)
        {
            StaffList staffList = await Business.StaffList.FindStaffById(staffId);

            if (ValidationHelper.IsEmptyOrNull(staffList, "Invalid access to staff"))
                return;

            int targetIndex = CommonOperations.FindTargetIndexOfObj(StaffList, "StaffId", staffId);
            CommonOperations.UpdateAfterAdding(StaffList, staffList, targetIndex);
            lblRecords.Text = $"{dgvStaff.Rows.Count} records";
        }
        private void addNewStaffToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            AddUpdateStaff addStaff = new AddUpdateStaff();
            addStaff.SendStaffId += RefreshDgv;
            addStaff.ShowDialog();
        }
        private void updateStaffToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            int staffId = (int)dgvStaff.CurrentRow.Cells["StaffID"].Value;

            AddUpdateStaff updateStaff = new AddUpdateStaff(staffId);
            updateStaff.SendStaffId += RefreshDgv;
            updateStaff.ShowDialog();
        }
        private void viewStaffToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            int staffId = (int)dgvStaff.CurrentRow.Cells["StaffID"].Value;

            ViewStaffInfo viewStaff = new ViewStaffInfo(staffId);
            viewStaff.OnChange += RefreshDgv;
            viewStaff.ShowDialog();
        }
        private void dgvStaff_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvStaff))
                return;

            int staffId = (int)dgvStaff.CurrentRow.Cells["StaffID"].Value;

            ViewStaffInfo viewStaff = new ViewStaffInfo(staffId);
            viewStaff.OnChange += RefreshDgv;
            viewStaff.ShowDialog();
        }
        private async void LoadStaffInfo()
        {
            StaffList = await Business.StaffList.GetStaff();

            if (CommonOperations.LoadDataToDGV(dgvStaff, StaffList, lblRecords))
            {
                dgvStaff.Columns["StaffID"].HeaderText = "Staff no.";
            }

            lblSubTitle.Text = $"Staff -- {Business.StaffList.StaffSummary.StaffCount} staff · {Business.StaffList.StaffSummary.DoctorCount} doctors · {Business.StaffList.StaffSummary.NurseCount} nurses · {Business.StaffList.StaffSummary.Receptionist} front desk";

            RefreshFilter(0);
        }
        private void ManageStaff_Load(object sender, System.EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
            {
                Close();
                return;
            }

            SetUp();
            LoadStaffInfo();
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (FilterName == "StaffID")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);
        }
        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
        private void SetControlEnableStatus(bool status)
        {
            updateStaffToolStripMenuItem.Enabled = status;
            viewStaffToolStripMenuItem.Enabled = status;
        }
        private void cmsStaff_Opening(object sender, CancelEventArgs e)
        {
            SetControlEnableStatus(!ValidationHelper.IsDgvEmptyOrNull(dgvStaff));
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
                PreviousText = value;

                if (CommonOperations.ResetDataGrid(dgvStaff, StaffList, lblRecords, value.ToString()))
                    return;

                List<object> temp = CommonOperations.FilterData(StaffList, FilterName, value);
                CommonOperations.LoadDataToDGV(dgvStaff, temp, lblRecords);
            }
        }
    }
}