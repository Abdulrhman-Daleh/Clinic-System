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
            if (ValidationHelper.IsEmptyOrNull(person))
            {
                SetUp();
                return;
            }

            lblFirstname.Text = person.Firstname;
            lblLastname.Text = person.Lastname;
            lblGender.Text = person.Gender.ToString();
        }

        public void SetUp()
        {
            CommonOperations.SetUpDefaultControlsValue(new List<Control>() { lblFirstname, lblLastname, lblGender }, "Not Set");
        }
    }
}
