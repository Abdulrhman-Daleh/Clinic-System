using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.LabOrderTest.Controls
{
    public partial class ViewLabTestInfoCtrl : UserCtrlDefaultSettings
    {
        public LabTest CurrentLabTest { get; private set; }
        public ViewLabTestInfoCtrl()
        {
            InitializeComponent();
            CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblInstructions, lblNurse, lblPreformed, lblReason, lblRequestedDate, lblResult, lblResultRecorded, lblStatus, lblTestname
            }, "Not Set");
        }

        public void LoadLabTestInfo(int testId)
        {
            CurrentLabTest = LabTest.FindLabTestByID(testId);

            if (ValidationHelper.IsEmptyOrNull(CurrentLabTest, "Test is not present"))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblInstructions, lblNurse, lblPreformed, lblReason, lblRequestedDate, lblResult, lblResultRecorded, lblStatus, lblTestname
            }, "Not Set");
                return;
            }

            lblTestname.Text = CurrentLabTest.TestName;
        }
    }
}
