using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Insurance
{
    public partial class ViewInsuranceInfo : FormsDefaultSettings
    {
        public ViewInsuranceInfo(int insuranceId)
        {
            InitializeComponent();
            btnEdit.Enabled = false;
            viewInsuranceInfoCtrl1.LoadInsuranceInfo(insuranceId);
            viewPatientsUnderInsurance.LoadUpPatientsInsurance(insuranceId);
            SetForm();
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();
        private void SetForm()
        {
            if (ValidationHelper.IsEmptyOrNull(viewInsuranceInfoCtrl1.LoadedInsurance))
            {
                btnEdit.Enabled = false;
                CommonOperations.SetUpDefaultControlsValue(new List<Control>()
                { lblInsuranceId, lblStatus}, "Not Set");
                return;
            }

            var InsuranceObj = viewInsuranceInfoCtrl1.LoadedInsurance;
            btnEdit.Enabled = true;
            lblStatus.Text = ValidationHelper.Date1BeforeDate2(InsuranceObj.CoverageStartDate, InsuranceObj.CoverageEndDate) ? "Active" : "Not Active";

            lblInsuranceId.Text = $"{InsuranceObj.InsuranceProvider} — {InsuranceObj.PolicyNumber} - {InsuranceObj.InsuranceType.InsuranceTypeName}";
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            AddUpdateInsurance updateInsurance = new AddUpdateInsurance(viewInsuranceInfoCtrl1.LoadedInsurance.InsuranceID);
            updateInsurance.SendInsuranceId += ReciveInsuranceId;
            updateInsurance.ShowDialog();
        }

        public delegate void OnChangeEvent(int insuranceId);
        public event OnChangeEvent OnChange;
        private void ReciveInsuranceId(int insuranceId)
        {
            viewInsuranceInfoCtrl1.LoadInsuranceInfo(insuranceId);
            OnChange?.Invoke(insuranceId);
        }
    }
}
