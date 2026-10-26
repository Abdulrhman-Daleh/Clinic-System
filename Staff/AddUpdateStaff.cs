using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using Log;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.Staff
{
    public partial class AddUpdateStaff : FormsDefaultSettings
    {
        Business.Staff staff = new Business.Staff();
        public AddUpdateStaff()
        {
            InitializeComponent();
            SetControls();
            SetFormTextBasedMode("Add new staff", "Add Staff", "Add Staff");
            findPersonCtrl1.OnFind += RecivePersonData;
        }
        private void RecivePersonData(Person person)
        {
            if (ValidationHelper.IsEmptyOrNull(person, "person info does not exists"))
                return;

            staff.Firstname = person.Firstname;
            staff.Lastname = person.Lastname;
            staff.Gender = person.Gender;
            staff.PersonId = person.PersonId;
            staff.contactInfo = person.contactInfo;
        }

        public delegate void SendStaffInfo(int staffId);
        public event SendStaffInfo SendStaffId;

        public AddUpdateStaff(int staffId)
        {
            InitializeComponent();
            staff = Business.Staff.FindStaffById(staffId);

            if (ValidationHelper.IsEmptyOrNull(staff, "Staff is not found"))
                return;

            SetControls(staff.StaffId, staff.StaffNumber, staff.Username, "Not Allowed To Show");
            SetFormTextBasedMode("Update staff", "Update Staff", "Update");
            findPersonCtrl1.AllowFilter = false;
            findPersonCtrl1.RecivePersonInfo(staff);
            txtPassword.Enabled = false;
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

        private async void LoadSchedules()
        {
            cbSchedules.DataSource = await Business.Schedule.GetSchedulesNames();
        }

        private async void LoadRoles()
        {
            cbRoles.DataSource = await Business.Staff.GetRolesNames();
        }

        private void ValidateInputValues(object sender, CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);
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

        private void AddUpdateStaff_Load(object sender, System.EventArgs e)
        {
            LoadSchedules();
            LoadRoles();
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => Close();

        private void CollectData()
        {
            staff.Username = txtUsername.Text;
            staff.ScheduleId = (int)cbSchedules.SelectedIndex + 1;
            staff.RoleInfo = ((EnRole)cbRoles.SelectedIndex + 1, cbRoles.SelectedItem.ToString());

            if (txtPassword.Enabled)
                staff.HashedPassword = PasswordHasher.HashPassword(txtPassword.Text);
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            this.ValidateChildren();

            if (ValidationHelper.NotValidToSave(this, errors))
            {
                MessageBox.Show("Form is not valid to save", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CollectData();

            try
            {
                if (staff.Save())
                {
                    MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetStaffId(staff.StaffId);
                    SetStaffNumber(staff.StaffNumber);
                    SetFormTextBasedMode("update staff", "Update staff", "Update");
                    SendStaffId?.Invoke(staff.StaffId);
                }
                else
                    MessageBox.Show("Failed to save", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "AddUpdateStaff", "Application", System.Diagnostics.EventLogEntryType.Error));
            }
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors))
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
    }
}
