using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            CommonOperations.HandleSwitchingFilter(cbFilterBy, cbPolicyStatus, txtFilter, dgvPolicies, Policies, lblRecords, "Status");

            ValidationHelper.SetApproperiteMaxLength(
                txtFilter,
                new List<string> { "PolicyID", "OwnerID" },
                CommonOperations.GetFilterName(cbFilterBy),
                5, 50);
        }

        private async Task SetUp()
        {
            await LoadPolicyStatuses();

            cbFilterBy.Items.AddRange(new object[]
            {
                "None", "PolicyID", "OwnerID", "Status"
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
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            AddUpdatePolicy addPolicy = new AddUpdatePolicy();
            addPolicy.SendPolicy += RefreshDgv;
            addPolicy.ShowDialog();
        }

        private void updatePolicyToolStripMenuItem_Click(
            object sender, System.EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            int policyId = (int)dgvPolicies.CurrentRow.Cells["PolicyID"].Value;

            AddUpdatePolicy updatePolicy = new AddUpdatePolicy(policyId);
            updatePolicy.SendPolicy += RefreshDgv;
            updatePolicy.ShowDialog();
        }

        private void viewPolicyToolStripMenuItem_Click(
            object sender, System.EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

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

        private async void ManagePolcies_Load(object sender, System.EventArgs e)
        {
            await SetUp();
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
            approveToolStripMenuItem.Enabled = status;
            suspendToolStripMenuItem.Enabled = status;
            archiveToolStripMenuItem.Enabled = status;
        }

        private void cmsPolicies_Opening(
            object sender, CancelEventArgs e)
        {
            bool EmptyDGV = ValidationHelper.IsDgvEmptyOrNull(dgvPolicies);
            SetControlEnableStatus(!EmptyDGV);

            if (!EmptyDGV)
            {
                int policyId = (int)dgvPolicies.CurrentRow.Cells[0].Value;

                Policy policyInfo = Policy.FindPolicyByID(policyId);

                if (ValidationHelper.IsEmptyOrNull(policyInfo))
                {
                    SetControlEnableStatus(false);
                    return;
                }

                if (policyInfo.IsActive() && policyInfo.PolicyStatus.PolicyStatusID == EnPolicyStatuses.Active)
                {
                    approveToolStripMenuItem.Enabled = false;
                    suspendToolStripMenuItem.Enabled = true;
                    archiveToolStripMenuItem.Enabled = true;
                }
                else if (policyInfo.IsActive() && policyInfo.PolicyStatus.PolicyStatusID == EnPolicyStatuses.AwaitingApprovel)
                {
                    approveToolStripMenuItem.Enabled = true;
                    suspendToolStripMenuItem.Enabled = false;
                    archiveToolStripMenuItem.Enabled = true;
                }
                else
                {
                    approveToolStripMenuItem.Enabled = true;
                    suspendToolStripMenuItem.Enabled = false;
                    archiveToolStripMenuItem.Enabled = false;
                }
            }
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

        private void archiveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int policyId = (int)dgvPolicies.CurrentRow.Cells[0].Value;
            if (!Policy.UpdatePolicyStatus(policyId, EnPolicyStatuses.Archived))
            {
                MessageBox.Show("Failed to archive this policy", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Policy got archive successfully", "Archived", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshDgv(Policy.FindPolicyByID(policyId));
        }

        private void suspendToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int policyId = (int)dgvPolicies.CurrentRow.Cells[0].Value;
            if (!Policy.UpdatePolicyStatus(policyId, EnPolicyStatuses.Suspended))
            {
                MessageBox.Show("Failed to suspend this policy", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Policy got suspend successfully", "Archived", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshDgv(Policy.FindPolicyByID(policyId));
        }

        private async Task LoadPolicyStatuses()
        {
            cbPolicyStatus.DataSource =
                await Business.Policy.GetPolicyStatusesNames();

            cbPolicyStatus.SelectedIndex = 0;
        }

        private void cbPolicyStatus_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            List<object> temp = CommonOperations.FilterData(Policies, "Status", cbPolicyStatus.Text);
            CommonOperations.LoadDataToDGV(dgvPolicies, temp, lblRecords);
        }

        private void approveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int policyId = (int)dgvPolicies.CurrentRow.Cells[0].Value;
            if (!Policy.UpdatePolicyStatus(policyId, EnPolicyStatuses.Active))
            {
                MessageBox.Show("Failed to archive this policy", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Policy got archive successfully", "Archived", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshDgv(Policy.FindPolicyByID(policyId));
        }
    }
}