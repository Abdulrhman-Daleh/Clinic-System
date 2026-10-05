using System;

namespace ClinicSystem.Staff.Controls
{
    public partial class StaffInfoCtrl : UserCtrlDefaultSettings
    {
        public StaffInfoCtrl()
        {
            InitializeComponent();
            _SetUp();
        }

        private void _SetUp()
        {
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
            cbSchedules.Items.Add("None");
            cbRoles.Items.Add("None");
            dtpHireDate.Value = DateTime.Now;
            lblStaffNumber.Text = "[Not Set]";
            cbSchedules.SelectedIndex = 0;
            cbRoles.SelectedIndex = 0;
            txtUsername.Focus();
        }
    }
}
