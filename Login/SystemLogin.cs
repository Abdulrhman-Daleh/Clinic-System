using Business;
using ClinicSystem.Helpers;
using ClinicSystem.Staff;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.Login
{
    public partial class SystemLogin : FormsDefaultSettings
    {

        private const string fileName = "C:\\ClinicSystem\\LoginSaves.txt";
        public SystemLogin()
        {
            InitializeComponent();
            SetUpDefaultForm();
        }

        private void SetUpDefaultForm()
        {
            string rememberedUsername = CommonOperations.ReadRememberedUsername(fileName);
            if (!ValidationHelper.IsEmptyOrNull(rememberedUsername))
            {
                txtUsername.Text = rememberedUsername;
                chkRememberMe.Checked = true;
                chkRememberMe.Checked = true;
            }
            else
            {
                txtUsername.Text = string.Empty;
                chkRememberMe.Checked = false;
            }

            txtPassword.Text = "Admin";
            txtUsername.Focus();
            ChkShowPassword.Checked = false;

            txtPassword.UseSystemPasswordChar = true;
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(txtUsername.Text))
            {
                errors.SetError(txtUsername, "Username is required");
                return;
            }

            errors.SetError(txtUsername, null);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(txtPassword.Text))
            {
                errors.SetError(txtPassword, "Password is required");
                return;
            }
            else
                errors.SetError(txtPassword, null);

            if (ValidationHelper.CheckAcceptablePasswordStrength(txtPassword.Text))
            {
                errors.SetError(txtPassword, "Password is weak. Rules length >= 10, contains digit, contains punc");
                return;
            }

            errors.SetError(txtPassword, null);
        }

        private void ChkShowPassword_CheckedChanged(object sender, System.EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkRememberMe.Checked;
        }

        private void BtnSignIn_Click(object sender, System.EventArgs e)
        {
            if (ValidationHelper.NotValidToSave(this, errors))
                return;

            string username = txtUsername.Text.Trim();
            string plainPassword = txtPassword.Text.Trim();

            Business.Staff foundStaff = Business.Staff.FindStaffByUsername(username);

            if (ValidationHelper.IsEmptyOrNull(foundStaff, "Invalid username/password try again..."))
                return;

            if (!PasswordHasher.VerifyPassword(plainPassword, foundStaff.HashedPassword))
            {
                MessageBox.Show("Invalid username/password try again...", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();
            RememberUsername();
            Global.CurrentLoggedInStaff = foundStaff;
            MainForm main = new MainForm();
            main.ShowDialog();
        }

        private void RememberUsername()
        {
            string username = txtUsername.Text.Trim();

            if (chkRememberMe.Checked)
            {
                CommonOperations.SaveUsernameToFile(fileName, username);
                return;
            }

            CommonOperations.SaveUsernameToFile(fileName, "");
        }

        private void SystemLogin_Load(object sender, System.EventArgs e)
        {
            Global.SeedDefaultData();
        }

        private void lnkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddUpdateStaff addStaff = new AddUpdateStaff();
            addStaff.ShowDialog();
        }
    }
}
