using Business;
using ClinicSystem.Helpers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.Policies
{
    public partial class AddUpdatePolicy : FormsDefaultSettings
    {
        Business.Policy policy = new Business.Policy();
        public AddUpdatePolicy()
        {
            InitializeComponent();
            SetControls();

            SetFormTextBasedMode(
                "Add new policy",
                "Add Policy",
                "Add Policy",
                true, true, true);

            findStaffCtrl1.OnFound += _ReciveStaffInfo;
        }

        private void _ReciveStaffInfo(Business.Staff owner)
        {
            if (ValidationHelper.IsEmptyOrNull(owner))
            {
                policy.Owner = owner;
                policy.OwnerID = -1;
                return;
            }

            policy.Owner = owner;
            policy.OwnerID = owner.StaffId;
        }

        public delegate void SendPolicyInfo(Policy policy);
        public event SendPolicyInfo SendPolicy;

        public AddUpdatePolicy(int policyID)
        {
            InitializeComponent();

            policy = Business.Policy.FindPolicyByID(policyID);

            if (ValidationHelper.IsEmptyOrNull(policy, "Policy does not exist"))
                return;

            SetControls(
                policy.PolicyID,
                policy.PolicyStatusID,
                policy.ActiveFrom,
                policy.ActiveTo,
                policy.NoShowFreezeDays,
                policy.LateFeesPerDay,
                policy.NoShowFee);

            SetFormTextBasedMode(
                "Update policy",
                "Update Policy",
                "Update",
                policy.NoShowFee.HasValue,
                policy.LateFeesPerDay.HasValue,
                policy.NoShowFreezeDays.HasValue);

            findStaffCtrl1.AllowFind = false;
            findStaffCtrl1.LoadData(policy.Owner);
        }

        private void SetFormTextBasedMode(
            string labelTitle,
            string pageTitle,
            string saveButtonText,
            bool noShowFees,
            bool lateFeesPerDay,
            bool freezeOnBooking)
        {
            lblTitle.Text = labelTitle;
            this.Text = pageTitle;
            btnSave.Text = saveButtonText;

            chk_No_ShowFees.Checked = noShowFees;
            chk_FeesPerDay.Checked = lateFeesPerDay;
            chkFreezeDays.Checked = freezeOnBooking;
        }

        private void SetControls(
            int policyID = -1,
            int policyStatusID = -1,
            DateTime? activeFrom = null,
            DateTime? activeTo = null,
            short? noShowFreezeDays = null,
            decimal? lateFeesPerDay = null,
            decimal? noShowFee = null)
        {
            LoadPolicyStatuses();
            SetPolicyId(policyID);

            dtpActiveFrom.Value = activeFrom ?? DateTime.Today;
            dtpActiveTo.Value = activeTo ?? DateTime.Today.AddYears(1);

            nudNoShowFreezeDays.Value = noShowFreezeDays.HasValue
                ? Math.Min(nudNoShowFreezeDays.Value + noShowFreezeDays.Value -
                    nudNoShowFreezeDays.Value, nudNoShowFreezeDays.Maximum)
                : nudNoShowFreezeDays.Minimum;

            nudFeesPerDay.Value = lateFeesPerDay.HasValue
                ? Math.Min(lateFeesPerDay.Value, nudFeesPerDay.Maximum)
                : nudFeesPerDay.Minimum;

            nudNoShowFee.Value = noShowFee.HasValue
                ? Math.Min(noShowFee.Value, nudNoShowFee.Maximum)
                : nudNoShowFee.Minimum;

            if (cbPolicyStatus.Items.Count > 0)
                cbPolicyStatus.SelectedIndex = policyStatusID - 1;
        }

        private void SetPolicyId(int policyID)
        {
            lblPolicyId.Text = policyID != -1
                ? $"POL-{policyID}"
                : "[Not Set]";
        }

        private async void LoadPolicyStatuses()
        {
            cbPolicyStatus.DataSource =
                await Business.Policy.GetPolicyStatusesNames();

            if (policy.PolicyStatusID > 0)
                cbPolicyStatus.SelectedIndex = policy.PolicyStatusID - 1;
            else
                cbPolicyStatus.SelectedIndex = 0;
        }

        private void AddUpdatePolicy_Load(object sender, EventArgs e)
        {
            UpdatePolicyFieldsVisibility();
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void CollectData()
        {
            policy.PolicyStatusID = cbPolicyStatus.SelectedIndex + 1;
            policy.ActiveFrom = dtpActiveFrom.Value;
            policy.ActiveTo = dtpActiveTo.Value;

            policy.NoShowFreezeDays = chkFreezeDays.Checked
                ? (short)nudNoShowFreezeDays.Value
                : (short?)null;

            policy.LateFeesPerDay = chk_FeesPerDay.Checked
                ? nudFeesPerDay.Value
                : (decimal?)null;

            policy.NoShowFee = chk_No_ShowFees.Checked
                ? nudNoShowFee.Value
                : (decimal?)null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ValidateChildren();

            if (ValidationHelper.NotValidToSave(this, errors))
            {
                MessageBox.Show(
                    "Form is not valid to save",
                    "Not Valid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            CollectData();
            if (ValidationHelper.IsEmptyOrNull(policy.Owner, "No owner was selected"))
                return;

            if (!policy.Owner.IsOwner())
            {
                MessageBox.Show("Selected staff is no an owner", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (policy.Save())
            {
                MessageBox.Show(
                    "Data saved successfully",
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                SetPolicyId(policy.PolicyID);

                SetFormTextBasedMode(
                    "Update policy",
                    "Update Policy",
                    "Update",
                    policy.NoShowFee.HasValue,
                    policy.LateFeesPerDay.HasValue,
                    policy.NoShowFreezeDays.HasValue);

                SendPolicy?.Invoke(policy);
            }
            else
            {
                MessageBox.Show(
                    "Failed to save",
                    "Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dtpActiveTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpActiveTo.Value <= dtpActiveFrom.Value)
            {
                errors.SetError(
                    dtpActiveTo,
                    "Active To must be later than Active From");
            }
            else
            {
                errors.SetError(dtpActiveTo, null);
            }
        }

        private void chk_No_ShowFees_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePolicyFieldsVisibility();
        }

        private void chk_FeesPerDay_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePolicyFieldsVisibility();
        }

        private void chkFreezeDays_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePolicyFieldsVisibility();
        }

        private void UpdatePolicyFieldsVisibility()
        {
            nudNoShowFee.Visible = chk_No_ShowFees.Checked;
            nudFeesPerDay.Visible = chk_FeesPerDay.Checked;
            nudNoShowFreezeDays.Visible = chkFreezeDays.Checked;
        }

        private void nudNoShowFreezeDays_Validating(object sender, EventArgs e)
        {
            if (chkFreezeDays.Checked && nudNoShowFreezeDays.Value <= 0)
            {
                errors.SetError(nudNoShowFreezeDays, "Freeze days must be greater than 0");
            }
            else
                errors.SetError(nudNoShowFreezeDays, null);
        }

        private void nudFeesPerDay_Validating_1(object sender, CancelEventArgs e)
        {
            if (chk_FeesPerDay.Checked && nudFeesPerDay.Value <= 0)
            {
                errors.SetError(nudFeesPerDay, "Late fees per day must be greater than 0");
            }
            else
                errors.SetError(nudFeesPerDay, null);
        }

        private void nudNoShowFee_Validating_1(object sender, CancelEventArgs e)
        {
            if (chk_No_ShowFees.Checked && nudNoShowFee.Value <= 0)
            {
                errors.SetError(nudNoShowFee, "No-show fee must be greater than 0");
            }
            else
                errors.SetError(nudNoShowFee, null);
        }

        private void cbPolicyStatus_Validating(object sender, CancelEventArgs e)
        {
            if (cbPolicyStatus.SelectedIndex == -1)
            {
                errors.SetError(cbPolicyStatus,
                    "Please select a policy status");
                return;
            }
            else
                errors.SetError(cbPolicyStatus, null);
        }
    }
}