using ClinicSystem.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.People.Controls
{
    public partial class PersonalInfoCtrl : UserCtrlDefaultSettings
    {
        public PersonalInfoCtrl()
        {
            InitializeComponent();
            _SetUpUserCtrl();
        }

        public void ValidateInputValues(object sender, CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((TextBox)sender, Errors);
        }


        private void _SetUpUserCtrl()
        {
            rbMale.Checked = true;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtFirstName.Focus();
        }
    }
}
