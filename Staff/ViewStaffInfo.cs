using ClinicSystem.Helpers;

namespace ClinicSystem.Staff
{
    public partial class ViewStaffInfo : FormsDefaultSettings
    {
        public ViewStaffInfo()
        {
            InitializeComponent();
            findStaffCtrl1.OnFound += ReciveStaffInfo;
            btnEdit.Enabled = false;
            BtnDeactivate.Enabled = false;
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void ReciveStaffInfo(Business.Staff staffInfo)
        {
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
    }
}
