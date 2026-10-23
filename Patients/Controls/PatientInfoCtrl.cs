using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Patients.Controls
{
    public partial class PatientInfoCtrl : UserCtrlDefaultSettings
    {
        public PatientInfoCtrl()
        {
            InitializeComponent();
            CommonOperations.SetUpDefaultControlsValue(new List<Control> { lblBookingfreeze, lblHasUnpaidFees, lblRegistered, lblTotalVisits }, "Not Set");
        }

        public void LoadPatientInfo(Patient patient)
        {

            if (ValidationHelper.IsEmptyOrNull((patient), "patient is not found"))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control> { lblBookingfreeze, lblHasUnpaidFees, lblRegistered, lblTotalVisits }, "Not Set");
                return;
            }

            if (patient.EndOfBookingFreeze == null)
                lblBookingfreeze.Text = $"Not frozen so ({patient.FreezeUnlockDays()}) days";
            else
                lblBookingfreeze.Text = $"Until {patient.EndOfBookingFreeze.Value.ToShortDateString()} ({patient.FreezeUnlockDays()} days)";

            lblRegistered.Text = patient.RegisterDate.ToString();
            lblHasUnpaidFees.Text = patient.ISUnpaidText();
            lblTotalVisits.Text = patient.GetTotalVisits().ToString();
        }


    }
}
