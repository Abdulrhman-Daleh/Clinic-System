using Business;
using ClinicSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Policies
{
    public partial class ViewPolicyInfo : FormsDefaultSettings
    {
        private Policy _policy = null;
        private readonly int PolicyId;
        public ViewPolicyInfo()
        {
            InitializeComponent();
            CommonOperations.SetUpDefaultControlsValue(new List<Control>() { lblStatus, lblTitle }, "Not Set");
            findPolicyInfo1.SendPolicy += RecivePolicyInfo;
            btnEdit.Enabled = false;
        }
        public ViewPolicyInfo(int policyId)
        {
            InitializeComponent();
            findPolicyInfo1.AllowedFind = false;
            PolicyId = policyId;

            Load += ViewPolicyInfo_Load;
        }

        private void RecivePolicyInfo(Policy policy)
        {
            if (ValidationHelper.IsEmptyOrNull(policy))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>() { lblStatus, lblTitle }, "Not Set");
                btnEdit.Enabled = false;
                _policy = null;
                return;
            }

            lblStatus.Text = policy.IsActive() ? "[Active]" : "[Not Active]";
            lblTitle.Text = $"Policy - {policy.PolicyID} {policy.ActiveFrom.ToShortDateString()} -> {policy.ActiveTo.ToShortDateString()}";
            _policy = policy;
            btnEdit.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddUpdatePolicy updatePolicy = new AddUpdatePolicy(_policy.PolicyID);
            updatePolicy.SendPolicy += RecivePolicyId;
            updatePolicy.ShowDialog();
        }

        public delegate void SendNotificationOnChange(Policy policy);
        public event SendNotificationOnChange OnChange;

        private async void RecivePolicyId(Policy policy)
        {
            await findPolicyInfo1.LoadPolicyInfo(policy.PolicyID);
            OnChange?.Invoke(_policy);
        }

        private async void ViewPolicyInfo_Load(object sender, EventArgs e)
        {
            await findPolicyInfo1.LoadPolicyInfo(PolicyId);
            _policy = await Policy.FindPolicyByID(PolicyId);
            RecivePolicyInfo(_policy);
        }
    }
}
