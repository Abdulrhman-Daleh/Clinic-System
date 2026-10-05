using Business;
using ClinicSystem.Helpers;
using System;
using System.ComponentModel;
using System.IO;
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

        private void _ValidateInputValues(object sender, CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);
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
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            fileDialog.DefaultExt = ".png";
            fileDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg";

            if (fileDialog.ShowDialog() == DialogResult.No)
                return;

            if (File.Exists(fileDialog.FileName))
            {
                pcImage.ImageLocation = fileDialog.FileName;
            }
            else
                MessageBox.Show("Image does not exists on local machine", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public Person GetPersonInfo()
        {
            this.ValidateChildren(ValidationConstraints.Enabled);

            if (ValidationHelper.UserControlHasErrors(this, errors))
            {
                MessageBox.Show("personal info could not be send to form check required fields.", "Invalid state", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }


            return new Person()
            {
                Firstname = txtFirstName.Text.Trim(),
                Lastname = txtLastName.Text.Trim(),
                Gender = cbGender.Text == "Male" ? AppEnums.cs.EnGender.Male : AppEnums.cs.EnGender.Female
            };
        }
    }
}
