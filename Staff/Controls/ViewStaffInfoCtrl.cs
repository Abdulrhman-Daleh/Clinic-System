using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Staff.Controls
{
    public partial class ViewStaffInfoCtrl : UserCtrlDefaultSettings
    {
        public Business.Staff LoadedStaff { get; set; }
        public ViewStaffInfoCtrl()
        {
            InitializeComponent();
            LoadStaffInfo(null);
        }

        public void LoadStaffInfo(Business.Staff staffInfo)
        {
            LoadedStaff = staffInfo;

            if (ValidationHelper.IsEmptyOrNull(staffInfo))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>() {lblUsername, lblStaffNumber, lblSchedule, lblRole,
                lblHired, lblIsActive}, "Not Set");
                return;
            }

            lblHired.Text = staffInfo.HireDate.ToShortDateString();
            lblIsActive.Text = staffInfo.IsActive ? "Active" : "InActive";
            lblRole.Text = staffInfo.RoleInfo.RoleName;
            lblSchedule.Text = staffInfo.ScheduleId.ToString();
            lblStaffNumber.Text = staffInfo.StaffNumber;
            lblUsername.Text = $"{staffInfo.Username}.{staffInfo.RoleInfo.RoleName}";
        }


    }
}
