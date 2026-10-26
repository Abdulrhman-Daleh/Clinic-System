using ClinicSystem.Helpers;

namespace ClinicSystem.Insurance
{
    public partial class ViewInsuranceInfo : FormsDefaultSettings
    {
        public ViewInsuranceInfo(int insuranceId)
        {
            InitializeComponent();
            btnEdit.Enabled = false;
            viewInsuranceInfoCtrl1.LoadInsuranceInfo(insuranceId);
            SetForm();
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();
        private void SetForm()
        {
            if (ValidationHelper.IsEmptyOrNull(viewInsuranceInfoCtrl1.LoadedInsurance))
            {
                btnEdit.Enabled = false;
                lblInsuranceId.Text = "Not Set";
                return;
            }

            btnEdit.Enabled = true;
            lblInsuranceId.Text = $"{viewInsuranceInfoCtrl1.LoadedInsurance.InsuranceProvider} — {viewInsuranceInfoCtrl1.LoadedInsurance.PolicyNumber} - {viewInsuranceInfoCtrl1.LoadedInsurance.InsuranceType.InsuranceTypeName}";
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
