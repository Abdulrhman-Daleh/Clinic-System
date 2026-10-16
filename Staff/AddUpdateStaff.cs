using AppEnums.cs;
using ClinicSystem.Helpers;
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
        }

        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            this.Text = pageTitle;
            btnSave.Text = saveButtonText;
        }

        private void SetControls(string firstName = "", string lastName = "",
            string userName = "", string password = "", int personId = -1)
        {
            txtFirstName.Focus();
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            txtUsername.Text = userName;
            txtPassword.Text = password;
            SetStaffId(-1);
            SetStaffNumber(null);
            SetFormTextBasedMode("Add new staff", "Add Staff", "Add Staff");
        }

        private async void LoadSchedules()
        {
            cbSchedules.DataSource = await Business.Staff.GetSchedulesNames();
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
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
            cbGender.SelectedIndex = (int)EnGender.Male - 1;
            LoadSchedules();
            LoadRoles();
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => Close();

        private void CollectData()
        {
            staff.Username = txtUsername.Text;
            staff.HashedPassword = txtPassword.Text;
            staff.ScheduleId = (int)cbSchedules.SelectedIndex + 1;
            staff.Role = (EnRole)cbRoles.SelectedIndex + 1;
            staff.Firstname = txtFirstName.Text;
            staff.Lastname = txtLastName.Text;
            staff.Gender = (EnGender)cbGender.SelectedIndex + 1;
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
                }
                else
                    MessageBox.Show("Failed to save", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Username already taken", "Taken", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors))
                return;

            if (Business.Staff.DoesExistsByUsername(txtUsername.Text))
            {
                errors.SetError(txtUsername, "User name is already taken");
                return;
            }

            errors.SetError(txtUsername, null);
        }
    }
}
