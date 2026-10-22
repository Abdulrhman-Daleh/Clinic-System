using ClinicSystem.Helpers;
using System;
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

        public void LoadPatientInfo(DateTime RegisterDate, DateTime? EndOfBookingFreeze, int FreezeUnlockDays,
            string IsUnPaidText, int TotalVisits)
        {
            if (EndOfBookingFreeze == null)
                lblBookingfreeze.Text = $"Not frozen so ({FreezeUnlockDays}) days";
            else
                lblBookingfreeze.Text = $"Until {EndOfBookingFreeze.Value.ToShortDateString()} ({FreezeUnlockDays} days)";

            lblRegistered.Text = RegisterDate.ToString();
            lblHasUnpaidFees.Text = IsUnPaidText;
            lblTotalVisits.Text = TotalVisits.ToString();
        }


    }
}
