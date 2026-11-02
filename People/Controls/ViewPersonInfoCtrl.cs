
using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.People
{
    public partial class ViewPersonInfoCtrl : UserCtrlDefaultSettings
    {
        public ViewPersonInfoCtrl()
        {
            InitializeComponent();
            SetUp();
        }

        public void SetData(Person person)
        {
            lblRegisteredAs.Text = "";

            if (ValidationHelper.IsEmptyOrNull(person))
            {
                SetUp();
                return;
            }

            lblFirstname.Text = person.Firstname;
            lblLastname.Text = person.Lastname;
            lblGender.Text = person.Gender.ToString();
            lblTopGender.Text = $"[{person.GetGenderChar()}] {lblGender.Text}";
            lblFullName.Text = person.GetFullName();

            if (person.IsPersonRegisteredAsPatient().IsSuccess)
                lblRegisteredAs.Text = "[Patient]";

            if (person.IsPersonRegisteredAsStaff().IsSuccess)
                lblRegisteredAs.Text += " [Staff]";
        }

        private void SetUp()
        {
            CommonOperations.SetUpDefaultControlsValue(
                new List<Control>() { lblFirstname, lblLastname, lblGender, lblTopGender },
                "Not Set");
        }
    }
}
