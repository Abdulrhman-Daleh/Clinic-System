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

        private Appointment Appointment { get; set; }
        public Appointment CurrentAppointment { get => Appointment; }

        private void SetDefaultForm()
        {
            CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblType, lblTime, lblStatus, lnkSOAPNote, lnkReferral, lnkPrescription, lnkPenalty, lnkPayment, lblPatientId, lnkLabTest, lblDoctor, lblDate, lblFrozen
            }, "Not Set");
        }

        public void LoadUpAppointmentInfo(int appointmentId)
        {
            Appointment = Appointment.FindAppointmentByID(appointmentId);
            if (ValidationHelper.IsEmptyOrNull(Appointment))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>()
                {
                lblType, lblTime, lblStatus, lnkSOAPNote, lnkReferral, lnkPrescription, lnkPenalty, lnkPayment, lblPatientId, lnkLabTest, lblDoctor, lblDate, lblFrozen
                }, "Not Set");
                return;
            }

            lblPatientId.Text = Appointment.Patient.GetFullName() + " . " + Appointment.Patient.GetGenderChar() + " . since "
                + Appointment.Patient.RegisterDate.ToShortDateString();
            lblDate.Text = Appointment.AppointmentDate.ToShortDateString();
            lblTime.Text = Appointment.AppointmentTime.ToString(@"hh\mm") + " - " + Appointment.AppointmentEndTime.ToString(@"hh\mm");
            lblDoctor.Text = Appointment.Staff.GetFullName();
            lblType.Text = Appointment.AppointmentTypes.AppointmentTypeText;
            lblStatus.Text = Appointment.AppointmentStatuses.AppointmentStatusText;

            if (Appointment.Patient.IsFrozen ?? false)
                lblFrozen.Text = "Frozen until " + Appointment.Patient.EndOfBookingFreeze;

            if (Appointment.Patient.HasUnPaidFees ?? false)
                lblFrozen.Text = " . " + Appointment.Patient.ISUnpaidText();

            if (Appointment.HasSOAPNotes())
                SetLinkValues(lnkSOAPNote, "View SOAP Note", true);
            else
                SetLinkValues(lnkSOAPNote, "Not written", false);

            if (Appointment.HasPenalty())
                SetLinkValues(lnkPenalty, "View Penalty", true);
            else
                SetLinkValues(lnkPenalty, "None", false);

            if (Appointment.HasLabTests())
                SetLinkValues(lnkLabTest, "View Lab Tests", true);
            else
                SetLinkValues(lnkLabTest, "None", false);

            if (Appointment.HasReferral())
                SetLinkValues(lnkReferral, "View Referral", true);
            else
                SetLinkValues(lnkReferral, "None", false);

            if (Appointment.HasPrescription())
                SetLinkValues(lnkPrescription, "View Prescription", true);
            else
                SetLinkValues(lnkPrescription, "None", false);

            if (Appointment.HasPayment())
                SetLinkValues(lnkPayment, "View Payment", true);
            else
                SetLinkValues(lnkPayment, "None", false);
        }

        private void SetLinkValues(LinkLabel link, string linkValue, bool status)
        {
            link.Enabled = status;
            link.Text = linkValue;
        }
    }
}
