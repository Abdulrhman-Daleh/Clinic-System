using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClinicSystem.Staff
{
    public partial class ManageStaff : FormsDefaultSettings
    {
        BindingList<StaffList> Staff = new BindingList<StaffList>();
        public ManageStaff()
        {
            InitializeComponent();
        }

        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvStaff, Staff, lblRecords, null);
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "Staff no." }, CommonOperations.GetFilterName(cbFilterBy),
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
            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void addNewStaffToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            AddUpdateStaff addStaff = new AddUpdateStaff();
            addStaff.ShowDialog();
        }

        private void updateStaffToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int staffId = (int)dgvStaff.CurrentRow.Cells["StaffID"].Value;

            AddUpdateStaff addStaff = new AddUpdateStaff(staffId);
            addStaff.ShowDialog();
        }

        private void viewStaffToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int staffId = (int)dgvStaff.CurrentRow.Cells["StaffID"].Value;

            ViewStaffInfo viewStaff = new ViewStaffInfo(staffId);
            viewStaff.ShowDialog();
        }

        private void dgvStaff_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            int staffId = (int)dgvStaff.CurrentRow.Cells["StaffID"].Value;

            ViewStaffInfo viewStaff = new ViewStaffInfo(staffId);
            viewStaff.ShowDialog();
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            object value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(dgvStaff, Staff, lblRecords, value.ToString()))
                return;

            List<object> temp = CommonOperations.FilterData(Staff, filterName, value);
            CommonOperations.LoadDataToDGV(dgvStaff, temp, lblRecords);
        }

        private async void LoadStaffInfo()
        {
            Staff = await StaffList.GetStaff();

            if (CommonOperations.LoadDataToDGV(dgvStaff, Staff, lblRecords))
            {
                dgvStaff.Columns["StaffID"].HeaderText = "Staff no.";
            }

            RefreshFilter(0);
        }

        private void ManageStaff_Load(object sender, System.EventArgs e)
        {
            SetUp();
            LoadStaffInfo();
        }

        private void txtFilter_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);

            if (filterName == "StaffID")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);
        }

        private void cbFilterBy_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
    }
}
