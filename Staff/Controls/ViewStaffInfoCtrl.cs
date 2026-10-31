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
                CommonOperations.SetUpDefaultControlsValue(new List<Control>() { lblFullName, lblHired, lblRoleTitle, lblUsername, lblStaffNumber, lblShift, lblRole, lblStatus }, "Not Set");
                return;
            }

            lblFullName.Text = staffInfo.GetFullName();
            lblStatus.Text = staffInfo.IsActive ? "Active" : "InActive";
            lblRole.Text = staffInfo.RoleInfo.RoleName;
            lblShift.Text = staffInfo.Schedule.ShiftName;
            lblStaffNumber.Text = staffInfo.StaffNumber;
            lblUsername.Text = $"{staffInfo.Username}";
            lblHired.Text = staffInfo.HireDate.ToShortDateString();
            lblRoleTitle.Text = $"[{staffInfo.RoleInfo.RoleName}]";
        }


    }
}
