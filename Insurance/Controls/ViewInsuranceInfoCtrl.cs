using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Insurance.Controls
{
    public partial class ViewInsuranceInfoCtrl : UserCtrlDefaultSettings
    {
        private Business.Insurance insurance = null;
        public ViewInsuranceInfoCtrl()
        {
            InitializeComponent();
            CommonOperations.SetUpDefaultControlsValue(new List<Control>() { lblCoverage, lblPolicy, lblProvider, lblRecordedBy, lblType, lblPatientClaims }, "Not Set");
        }

        public Business.Insurance LoadedInsurance
        {
            get => insurance;
        }

        public void LoadInsuranceInfo(int insuranceId)
        {
            insurance = Business.Insurance.FindInsuranceByID(insuranceId);
            if (ValidationHelper.IsEmptyOrNull(insurance, "insurance is not found"))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>() { lblCoverage, lblPolicy, lblProvider, lblRecordedBy, lblType, lblPatientClaims }, "Not Set");
                return;
            }

            lblProvider.Text = insurance.InsuranceProvider;
            lblCoverage.Text = insurance.CoverageStartDate.ToShortDateString();
            lblPolicy.Text = insurance.PolicyNumber;
            lblType.Text = insurance.InsuranceType.InsuranceTypeName;
            lblRecordedBy.Text = insurance.RecorderStaff.GetFullName();
            lblPatientClaims.Text = insurance.PatientClaims;
        }
    }
}
