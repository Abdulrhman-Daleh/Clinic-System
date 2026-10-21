using ClinicSystem.Helpers;
using System.Windows.Forms;

namespace ClinicSystem.Staff
{
    public partial class ViewStaffInfo : FormsDefaultSettings
    {

        public Business.Staff ViewedStaff;
        public ViewStaffInfo()
        {
            InitializeComponent();
            findStaffCtrl1.OnFound += ReciveStaffInfo;
            btnEdit.Enabled = false;
            BtnDeactivate.Enabled = false;
        }

        public ViewStaffInfo(int staffId)
        {
            InitializeComponent();
            findStaffCtrl1.AllowFind = false;
            ReciveStaffInfo(Business.Staff.FindStaffById(staffId));
            findStaffCtrl1.LoadData(ViewedStaff);
        }

        public delegate void SendStaffOnChange(int staffId);
        public event SendStaffOnChange OnChange;

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void ReciveStaffInfo(Business.Staff staffInfo)
        {
            ViewedStaff = staffInfo;
            if (ValidationHelper.IsEmptyOrNull(staffInfo))
            {
                BtnDeactivate.Enabled = false;
                btnEdit.Enabled = false;
                viewPersonInfoCtrl1.SetData(null);
                viewPersonContacts1.LoadUpPersonContact(-1);
                return;
            }

            viewPersonInfoCtrl1.SetData(staffInfo);
            viewPersonContacts1.LoadUpPersonContact(staffInfo.PersonId);
            btnEdit.Enabled = true;
            BtnDeactivate.Enabled = staffInfo.IsActive;
        }

        private void BtnDeactivate_Click(object sender, System.EventArgs e)
        {
            if (ViewedStaff.DeactivateStaff())
            {
                MessageBox.Show("Staff got deactivated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnDeactivate.Enabled = false;
                OnChange?.Invoke(ViewedStaff.StaffId);
                return;
            }

            MessageBox.Show("Staff failed to get deactivate", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ReciveStaffId(int staffId)
        {
            OnChange?.Invoke(staffId);
            findStaffCtrl1.LoadData(ViewedStaff);
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            AddUpdateStaff updateStaff = new AddUpdateStaff(ViewedStaff.StaffId);
            updateStaff.SendStaffId += ReciveStaffId;
            updateStaff.ShowDialog();
        }
    }
}
