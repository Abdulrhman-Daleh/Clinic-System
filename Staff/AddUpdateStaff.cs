using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using Log;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem.Staff
{
    public partial class AddUpdateStaff : FormsDefaultSettings
    {
        Business.Staff staff = new Business.Staff();

        private readonly int StaffId = -1;
        public AddUpdateStaff()
        {
            InitializeComponent();
            SetControls();
            SetFormTextBasedMode("Add new staff", "Add Staff", "Add Staff");
            findPersonCtrl1.OnFind += RecivePersonData;
            findPersonCtrl1.AllowFindButton = false;
            HandleFirstStaff();
            StaffId = -1;
        }
        private void RecivePersonData(Person person)
        {
            if (ValidationHelper.IsEmptyOrNull(person, "person info does not exists"))
            {
                staff.PersonId = -1;
                return;
            }

            staff.Firstname = person.Firstname;
            staff.Lastname = person.Lastname;
            staff.Gender = person.Gender;
            staff.PersonId = person.PersonId;
            staff.contactInfo = person.contactInfo;
        }

        public delegate void SendStaffInfo(int staffId);
        public event SendStaffInfo SendStaffId;
        private void HandleFirstStaff()
        {
            if (Business.Staff.IsFirstStaff())
            {
                findPersonCtrl1.AllowFind = true;
                findPersonCtrl1.AllowFindButton = false
                    ;
                cbRoles.Enabled = false;
                staff.Role = EnRole.Owner;
            }
        }
        public AddUpdateStaff(int staffId)
        {
            InitializeComponent();
            StaffId = staffId;
        }
        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            this.Text = pageTitle;
            btnSave.Text = saveButtonText;
        }
        private void SetControls(int staffId = -1, string staffNumber = null, string userName = "", string password = "")
        {
            txtUsername.Text = userName;
            txtPassword.Text = password;
            SetStaffId(staffId);
            SetStaffNumber(staffNumber);
        }
        private async Task LoadSchedules()
        {
            cbSchedules.DataSource = await Business.Schedule.GetSchedulesNames();
        }
        private async Task LoadRoles()
        {
            cbRoles.DataSource = await Business.Staff.GetRolesNames();
        }
        private void ValidateInputKeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.HandleInputType(e, false, true);
        }
        private void SetStaffId(int staffId)
        {
            lblStaffId.Text = staffId != -1 ? $"STF-{staffId}" : "[Not Set]";
        }
        private void SetStaffNumber(string staffNumber)
        {
            lblStaffNumber.Text = ValidationHelper.IsEmptyOrNull(staffNumber) ? "[Not Set]" : staffNumber;
        }
        private async void AddUpdateStaff_Load(object sender, System.EventArgs e)
        {
            await LoadSchedules();
            await LoadRoles();

            if (StaffId > 0)
            {
                staff = await Business.Staff.FindStaffById(StaffId);

                if (ValidationHelper.IsEmptyOrNull(staff, "Staff is not found"))
                    return;

                SetControls(staff.StaffId, staff.StaffNumber, staff.Username, "Not Allowed To Show");
                SetFormTextBasedMode("Update staff", "Update Staff", "Update");
                findPersonCtrl1.AllowFind = false;
                findPersonCtrl1.RecivePersonInfo(staff);
                txtPassword.Enabled = false;
                cbRoles.SelectedIndex = (int)staff.Role - 1;
                cbSchedules.SelectedIndex = staff.ScheduleId - 1;
            }
        }
        private void btnCancel_Click(object sender, System.EventArgs e) => Close();
        private void CollectData()
        {
            staff.Username = txtUsername.Text;
            staff.ScheduleId = (int)cbSchedules.SelectedIndex + 1;

            if (cbRoles.Enabled)
                staff.Role = ((EnRole)cbRoles.SelectedIndex + 1);

            if (txtPassword.Enabled)
                staff.HashedPassword = PasswordHasher.HashPassword(txtPassword.Text);
        }
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            this.ValidateChildren();

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

            CollectData();

            try
            {
                result = staff.Save();

                MessageBox.Show(
                    result.Message,
                    result.Title,
                    MessageBoxButtons.OK,
                    result.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result.IsSuccess)
                {
                    SetStaffId(staff.StaffId);
                    SetStaffNumber(staff.StaffNumber);
                    SetFormTextBasedMode("update staff", "Update staff", "Update");
                    SendStaffId?.Invoke(staff.StaffId);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "AddUpdateStaff", "Application", System.Diagnostics.EventLogEntryType.Error));
            }
        }
        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            OperationResult result = ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);

            if (!result.IsSuccess)
                return;

            if (Business.Staff.DoesExistsByUsername(txtUsername.Text) && staff.IsAddMode())
            {
                errors.SetError(txtUsername, "User name is already taken");
                return;
            }

            errors.SetError(txtUsername, null);
        }
        private void LockComboFromEditing(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

            if (staff.IsAddMode())
            {
                OperationResult result = ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);
                if (!result.IsSuccess)
                {
                    errors.SetError(txtPassword, result.Message);
                    return;
                }
                else
                    errors.SetError(txtPassword, null);

                if (!ValidationHelper.CheckAcceptablePasswordStrength(txtPassword.Text))
                {
                    errors.SetError(txtPassword, "Password is weak. Rules length >= 10, contains digit, contains punc");
                    return;
                }
            }
        }
    }
}
