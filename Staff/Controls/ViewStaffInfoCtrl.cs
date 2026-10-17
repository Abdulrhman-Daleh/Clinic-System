using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Staff.Controls
{
    public partial class ViewStaffInfoCtrl : UserCtrlDefaultSettings
    {
        public ViewStaffInfoCtrl()
        {
            InitializeComponent();
        }

        public void LoadStaffInfo(Business.Staff staffInfo)
        {
            if (ValidationHelper.IsEmptyOrNull(staffInfo))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>() {lblUsername, lblStaffNumber, lblSchedule, lblRole,
                lblHired, lblIsActive}, "[Not Set]");
                return;
            }
        }
    }
}
