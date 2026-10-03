using ClinicSystem.Helpers;
using System;
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
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
            cbGender.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtFirstName.Focus();
            lblPersonId.Text = "[Not Set]";
        }

        private void LnkUploadPic_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            FileDialog.DefaultExt = ".png";
            FileDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg";

            if (FileDialog.ShowDialog() == DialogResult.No)
                return;
        }
    }
}
