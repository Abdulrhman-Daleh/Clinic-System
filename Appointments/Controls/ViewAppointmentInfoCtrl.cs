using Business;
using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Appointments.Controls
{
    public partial class ViewAppointmentInfoCtrl : UserCtrlDefaultSettings
    {
        public ViewAppointmentInfoCtrl()
        {
            InitializeComponent();
            SetDefaultForm();
        }

        private void SetDefaultForm()
        {
            CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblType, lblTime, lblStatus, lblSOAPNote, lblReferral, lblPrescription, lblPenalty, lblPayment, lblPatientId, lblLapTest, lblDoctor, lblDate
            }, "Not Set");
        }

        public void LoadUpAppointmentInfo(int appointmentId)
        {
            Appointment appointment = Appointment.FindAppointmentByID(appointmentId);
            if (ValidationHelper.IsEmptyOrNull(appointment))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>()
                {
                lblType, lblTime, lblStatus, lblSOAPNote, lblReferral, lblPrescription, lblPenalty, lblPayment, lblPatientId, lblLapTest, lblDoctor, lblDate
                }, "Not Set");
                return;
            }


        }
    }
}
