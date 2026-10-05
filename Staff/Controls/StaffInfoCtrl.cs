using ClinicSystem.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

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
            lblStaffNumber.Text = "[Not Set]";
            cbSchedules.SelectedIndex = 0;
            cbRoles.SelectedIndex = 0;
            txtUsername.Focus();
        }

        public void ValidateInputValues(object sender, CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);
        }

    }
}
