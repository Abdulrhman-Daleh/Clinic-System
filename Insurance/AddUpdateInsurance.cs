
using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem.Insurance
{
    public partial class AddUpdateInsurance : FormsDefaultSettings
    {
        private Business.Insurance insurance;

        private readonly int InsuranceId;
        public delegate void SendInsuranceInfo(int insuranceId);
        public event SendInsuranceInfo SendInsuranceId;

        public AddUpdateInsurance()
        {
            InitializeComponent();

            insurance = new Business.Insurance();

            SetControls();

            SetFormTextBasedMode(
                "Add New Insurance", "Add Insurance", "Add Insurance");

            lblInsuranceId.Text = "[Not Set]";

            findPatientCtrl1.SendPatient += RecivePatient;
        }

        private void RecivePatient(Patient patient)
        {
            insurance.PolicyPatientHolder = patient;
        }

        public AddUpdateInsurance(int insuranceId)
        {
            InitializeComponent();


            SetFormTextBasedMode(
                "Update Insurance", "Update Insurance", "Update");

            findPatientCtrl1.AllowedFind = false;
            InsuranceId = insuranceId;

            findPatientCtrl1.LoadPatientInfo(insurance.PolicyholderPatientId);
            SetControls();

            lblInsuranceId.Text = $"Ins-{insuranceId}";
        }

        private void SetUpdateMode()
        {
            SetFormTextBasedMode(
                "Update Insurance", "Update Insurance", "Update");
        }

        private void SetFormTextBasedMode(
            string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            Text = pageTitle;
            btnSave.Text = saveButtonText;
        }

        private async Task SetNeededComboBoxes()
        {
            await LoadInsuranceTypes();
            await LoadInsuranceStatuses();
            await LoadPolicies();
        }

        private async void SetControls()
        {
            await SetNeededComboBoxes();

            if (insurance.IsAddMode())
                return;

            txtProviderName.Text = insurance.InsuranceProvider;
            txtPatientClaims.Text = insurance.PatientClaims;

            dtpCoverageStart.Value = insurance.CoverageStartDate;
            dtpCoverageEnd.Value = insurance.CoverageEndDate;

            cbTypes.SelectedIndex = (int)insurance.InsuranceType - 1;
            cbStatuses.SelectedIndex = (int)insurance.InsuranceStatus - 1;
            txtRelationship.Text = insurance.Relationship;
        }

        private async Task<bool> LoadInsuranceTypes()
        {
            cbTypes.DataSource = await Business.Insurance.GetInsuranceTypesNames();
            return true;
        }

        private async Task<bool> LoadInsuranceStatuses()
        {
            cbStatuses.DataSource = await Business.Insurance.GetInsuranceStatusesNames();
            return true;
        }

        private async Task<bool> LoadPolicies()
        {
            cbPolicies.DisplayMember = "Display";
            cbPolicies.ValueMember = "PolicyID";
            cbPolicies.DataSource = await Policy.GetPoliciesRanges();

            return true;
        }

        private void ValidateRequiredInput(object sender, CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);
        }

        private async Task CollectData()
        {
            insurance.InsuranceProvider = txtProviderName.Text.Trim();
            insurance.PatientClaims = txtPatientClaims.Text.Trim();

            insurance.CoverageStartDate = dtpCoverageStart.Value;
            insurance.CoverageEndDate = dtpCoverageEnd.Value;

            insurance.InsuranceType = (EnInsuranceTypes)cbTypes.SelectedIndex + 1;
            insurance.InsuranceStatus = (EnInsuranceStatuses)cbStatuses.SelectedIndex + 1;

            insurance.UploadedDate = DateTime.Now;
            (int policyId, string display) po = ((int policyId, string display))cbPolicies.SelectedValue;

            insurance.PolicyId = po.policyId;
            insurance.Policy = await Policy.FindPolicyByID(po.policyId);
            insurance.Relationship = txtRelationship.Text;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            ValidateChildren();

            OperationResult result = ValidationHelper.ValidateForm(this, errors);

            if (!result.IsSuccess)
            {
                MessageBox.Show(
                    result.Message,
                    result.Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            await CollectData();

            result = insurance.Save();

            MessageBox.Show(
                result.Message,
                result.Title,
                MessageBoxButtons.OK,
                result.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.IsSuccess)
            {
                lblInsuranceId.Text = $"Ins-{insurance.InsuranceID}";
                findPatientCtrl1.LoadPatientInfo(insurance.PolicyholderPatientId);
                SetUpdateMode();

                findPatientCtrl1.AllowedFind = false;

                SendInsuranceId?.Invoke(insurance.InsuranceID);
            }
        }

        private void dtpCoverageEndDate_Validating(object sender, CancelEventArgs e)
        {
            if (ValidationHelper.Date1BeforeDate2(dtpCoverageEnd.Value.Date, dtpCoverageStart.Value.Date))
            {
                errors.SetError(
                    dtpCoverageEnd,
                    "Coverage end date cannot be earlier than coverage start date.");
            }
            else
                errors.SetError(dtpCoverageEnd, null);
        }

        private void LockComboFromEditing(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void cbTypes_Validating(object sender, CancelEventArgs e)
        {
            if (cbTypes.SelectedIndex == -1)
                errors.SetError(cbTypes, "Select an insurance type");
            else
                errors.SetError(cbTypes, null);
        }

        private void cbStatuses_Validating(object sender, CancelEventArgs e)
        {
            if (cbStatuses.SelectedIndex == -1)
                errors.SetError(cbStatuses, "Select an insurance status");
            else
                errors.SetError(cbStatuses, null);
        }

        private async void AddUpdateInsurance_Load(object sender, EventArgs e)
        {
            insurance = await Business.Insurance.FindInsuranceByID(InsuranceId);

            if (ValidationHelper.IsEmptyOrNull(insurance, "Insurance not found"))
                return;

            insurance.RecorderStaff = Global.CurrentLoggedInStaff;
            insurance.RecordedBy = insurance.RecorderStaff.StaffId;
        }
    }
}
