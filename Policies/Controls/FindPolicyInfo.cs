using Business;
using ClinicSystem.Helpers;
using System.Windows.Forms;

namespace ClinicSystem.Policies.Controls
{
    public partial class FindPolicyInfo : UserCtrlDefaultSettings
    {
        private Policy policy = new Policy();
        public FindPolicyInfo()
        {
            InitializeComponent();
            DefaultForm();
        }

        public bool AllowedFind
        {
            get => gbFilter.Enabled;
            set => gbFilter.Enabled = value;
        }

        public delegate void SendPolicyInfo(Policy policy);
        public event SendPolicyInfo SendPolicy;

        private void DefaultForm()
        {
            cbFindBy.Items.Add("PolicyID");
            cbFindBy.SelectedIndex = 0;
        }

        private void cbFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.HandleInputType(e, true, false);
        }

        private void txtFindBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(txtFindBy.Text))
                errors.SetError(txtFindBy, "enter a value id");
            else
                errors.SetError(txtFindBy, null);
        }


        public void Find()
        {
            this.ValidateChildren();

            if (ValidationHelper.NotValidToSave(this, errors))
                return;

            int policyId = -1;
            int.TryParse(txtFindBy.Text, out policyId);
            if (policy == null)
                policy = Policy.FindPolicyByID(policyId);

            viewPolicyInfoCtrl1.LoadPolicyInfo(policy);

            if (AllowedFind)
                SendPolicy?.Invoke(policy);

        }

        private void btnFind_Click(object sender, System.EventArgs e)
        {
            Find();
        }


        public void LoadPolicyInfo(int policyId)
        {
            txtFindBy.Text = policyId.ToString();

            Find();
        }

        private void btnAddNew_Click(object sender, System.EventArgs e)
        {
            AddUpdatePolicy addPolicy = new AddUpdatePolicy();
            addPolicy.SendPolicy += RecivePolicyId;
            addPolicy.ShowDialog();
        }

        private void RecivePolicyId(Policy policy)
        {
            txtFindBy.Text = policy.PolicyID.ToString();
            this.policy = policy;
            Find();
        }
    }
}
