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
            findPolicyInfo1.LoadPolicyInfo(policyId);
            _policy = Policy.FindPolicyByID(policyId);
            RecivePolicyInfo(_policy);
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
            updatePolicy.SendPolicyId += RecivePolicyId;
            updatePolicy.ShowDialog();
        }

        private void RecivePolicyId(int policyID)
        {
            findPolicyInfo1.LoadPolicyInfo(policyID);
        }
    }
}
