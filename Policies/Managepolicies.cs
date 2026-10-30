using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ClinicSystem.Policies
{
    public partial class Managepolicies : FormsDefaultSettings
    {
        BindingList<Policy> Policies = new BindingList<Policy>();

        public Managepolicies()
        {
            InitializeComponent();
        }

        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;

            CommonOperations.HandleSwitchingFilter(
                cbFilterBy, null, txtFilter, dgvPolicies,
                Policies, lblRecords, null);

            ValidationHelper.SetApproperiteMaxLength(
                txtFilter,
                new List<string> { "PolicyID", "OwnerID", "PolicyStatusID" },
                CommonOperations.GetFilterName(cbFilterBy),
                5, 50);
        }

        private void SetUp()
        {
            cbFilterBy.Items.AddRange(new object[]
            {
                "None", "PolicyID", "OwnerID", "PolicyStatusID"
            });

            cbFilterBy.SelectedIndex = 0;
        }

        private void RefreshDgv(Policy policy)
        {
            int targetIndex = -1;

            if (policy == null)
                return;

            if (Policies.Any(x => x.PolicyID == policy.PolicyID))
            {
                targetIndex = Policies.IndexOf(
                    Policies.First(x => x.PolicyID == policy.PolicyID));
            }

            CommonOperations.UpdateAfterAdding(
                Policies, policy, targetIndex);
        }

        private void addNewPolicyToolStripMenuItem_Click(
            object sender, System.EventArgs e)
        {
            AddUpdatePolicy addPolicy = new AddUpdatePolicy();
            addPolicy.SendPolicy += RefreshDgv;
            addPolicy.ShowDialog();
        }

        private void updatePolicyToolStripMenuItem_Click(
            object sender, System.EventArgs e)
        {
            int policyId = (int)dgvPolicies.CurrentRow.Cells["PolicyID"].Value;

            AddUpdatePolicy updatePolicy = new AddUpdatePolicy(policyId);
            updatePolicy.SendPolicy += RefreshDgv;
            updatePolicy.ShowDialog();
        }

        private void viewPolicyToolStripMenuItem_Click(
            object sender, System.EventArgs e)
        {
            int policyId = (int)dgvPolicies.CurrentRow.Cells["PolicyID"].Value;

            ViewPolicyInfo viewPolicy = new ViewPolicyInfo(policyId);
            viewPolicy.OnChange += RefreshDgv;
            viewPolicy.ShowDialog();
        }

        private void dgvPolicies_CellDoubleClick(
            object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (ValidationHelper.IsDgvEmptyOrNull(dgvPolicies))
                return;

            int policyId = (int)dgvPolicies.Rows[e.RowIndex].Cells["PolicyID"].Value;

            ViewPolicyInfo viewPolicy = new ViewPolicyInfo(policyId);
            viewPolicy.ShowDialog();
        }

        private async void LoadPoliciesInfo()
        {
            Policies = await Policy.GetPolicies();

            if (CommonOperations.LoadDataToDGV(
                dgvPolicies, Policies, lblRecords))
            {
                dgvPolicies.Columns["PolicyID"].HeaderText = "Policy no.";
            }

            RefreshFilter(0);
        }

        private void ManagePolcies_Load(object sender, System.EventArgs e)
        {
            SetUp();
            LoadPoliciesInfo();
        }

        private void cbFilterBy_KeyPress(
            object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void SetControlEnableStatus(bool status)
        {
            updatePolicyToolStripMenuItem.Enabled = status;
            viewPolicyToolStripMenuItem.Enabled = status;
        }

        private void cmsPolicies_Opening(
            object sender, CancelEventArgs e)
        {
            SetControlEnableStatus(
                !ValidationHelper.IsDgvEmptyOrNull(dgvPolicies));
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void txtFilter_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);

            ValidationHelper.HandleInputType(e, true, false);
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            object value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(dgvPolicies, Policies, lblRecords, value.ToString()))
                return;

            List<object> temp = CommonOperations.FilterData(Policies, filterName, value);

            CommonOperations.LoadDataToDGV(dgvPolicies, temp, lblRecords);
        }
    }
}