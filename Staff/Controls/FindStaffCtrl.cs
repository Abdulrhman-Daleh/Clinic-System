using ClinicSystem.Helpers;
using System;
using System.Windows.Forms;

namespace ClinicSystem.Staff.Controls
{
    public partial class FindStaffCtrl : UserCtrlDefaultSettings
    {
        public Business.Staff staff = new Business.Staff();
        public bool AllowFind
        {
            get => gbFindBy.Enabled;
            set => gbFindBy.Enabled = value;
        }
        public FindStaffCtrl()
        {
            InitializeComponent();

            cbFindBy.Items.Add("StaffID");
            cbFindBy.Items.Add("PersonID");
            cbFindBy.SelectedIndex = 0;

        }


        public delegate void SendStaffInfo(Business.Staff staff);
        public event SendStaffInfo OnFound;

        private void cbFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.HandleInputType(e, true, false);
        }

        private void BtnFindStaff_Click(object sender, EventArgs e)
        {
            Find();
        }

        private void Find()
        {
            if (ValidationHelper.NotValidToSave(this, errors))
                return;

            string searchType = cbFindBy.SelectedItem.ToString();

            int.TryParse(txtFindBy.Text, out int Id);

            switch (searchType)
            {
                case "StaffID":
                    viewStaffInfoCtrl.LoadStaffInfo(Business.Staff.FindStaffById(Id));
                    break;

                case "PersonID":
                    viewStaffInfoCtrl.LoadStaffInfo(Business.Staff.FindStaffByPersonId(Id));
                    break;
            }

            if (AllowFind)
                OnFound?.Invoke(viewStaffInfoCtrl.LoadedStaff);

            staff = viewStaffInfoCtrl.LoadedStaff;
        }

        public void LoadData(Business.Staff st)
        {
            staff = st;
            viewStaffInfoCtrl.LoadStaffInfo(staff);
            txtFindBy.Text = staff.StaffId.ToString();
        }
        private void txtFindBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(txtFindBy.Text))
                errors.SetError(txtFindBy, "no id was provided");
            else
                errors.SetError(txtFindBy, null);
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFindBy.Text = "";
            txtFindBy.Focus();
        }

        private void AddStaff_Click(object sender, EventArgs e)
        {
            AddUpdateStaff addStaff = new AddUpdateStaff();
            addStaff.SendStaffId += ReciveStaffId;
            addStaff.ShowDialog();
        }

        private void ReciveStaffId(int staffId)
        {
            cbFindBy.SelectedIndex = 0;
            txtFindBy.Text = staffId.ToString();

            Find();
        }
    }
}