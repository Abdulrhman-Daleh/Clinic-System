using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Policies.Controls
{
    public partial class ViewPolicyInfoCtrl : UserCtrlDefaultSettings
    {
        public ViewPolicyInfoCtrl()
        {
            InitializeComponent();
            CommonOperations.SetUpDefaultControlsValue(new List<Control>() { lblActiveFrom, lblActiveTo, lblBookingFreeze, lblLateFees, lblNo_ShowFees, lblSetBy }, "Not Set");
        }

        public void LoadPolicyInfo(Policy policyInfo)
        {
            if (ValidationHelper.IsEmptyOrNull(policyInfo, "Policy is not found"))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>() { lblActiveFrom, lblActiveTo, lblBookingFreeze, lblLateFees, lblNo_ShowFees, lblSetBy }, "Not Set");
                return;
            }

            lblActiveFrom.Text = policyInfo.ActiveFrom.ToShortDateString();
            lblActiveTo.Text = policyInfo.ActiveTo.ToShortDateString();
            lblBookingFreeze.Text = policyInfo.BookingFreezeDaysText();
            lblLateFees.Text = policyInfo.LateFeesPerDayText();
            lblNo_ShowFees.Text = policyInfo.NoShowFeesText();
            lblSetBy.Text = policyInfo.Owner.GetFullName();
        }
    }
}
