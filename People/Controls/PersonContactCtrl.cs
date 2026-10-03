using ClinicSystem.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.People.Controls
{
    public partial class PersonContactCtrl : UserCtrlDefaultSettings
    {
        public PersonContactCtrl()
        {
            InitializeComponent();
            _SetPage();
        }

        private void _SetPage()
        {
            cbContactType.Items.Add("Normal");
            cbContactType.Items.Add("Emergency");
            cbContactType.SelectedIndex = 0;
            txtPhoneNumber.Text = string.Empty;
            txtEmail.Text = string.Empty;
            lblContactId.Text = "[Not Set]";
        }
        private void txtPhoneNumber_Validating(object sender, CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);
        }
    }
}
