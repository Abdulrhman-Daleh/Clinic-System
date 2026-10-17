using ClinicSystem.Helpers;
using System.Windows.Forms;

namespace ClinicSystem.Staff.Controls
{
    public partial class FindStaffCtrl : UserCtrlDefaultSettings
    {
        public FindStaffCtrl()
        {
            InitializeComponent();
            cbFindBy.Items.Add("StaffID");
            cbFindBy.Items.Add("PersonID");
        }

        private void cbFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.HandleInputType(e, true, false);
        }
    }
}
