using Business;
using ClinicSystem.Appointments.SOAPNotes;
using ClinicSystem.Helpers;
using ClinicSystem.LabOrderTest;
using ClinicSystem.Medicine.Prescription;
using System.Collections.Generic;
using System.Linq;
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

        private static HashSet<Form> activeForms = new HashSet<Form>();

        private void SetDefaultForm()
        {
            CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblType, lblTypeTitle, lblStatus, lnkSOAPNote, lnkReferral, lnkPrescription, lnkPenalty, lnkPayment, lblPatientId, lnkLabTest, lblDoctor, lblDate, lblFrozen
            }, "Not Set");
        }

        public void LoadUpAppointmentInfo(int appointmentId)
        {
            Appointment = Appointment.FindAppointmentByID(appointmentId);
            if (ValidationHelper.IsEmptyOrNull(Appointment))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>()
                {
                lblType, lblTypeTitle, lblStatus, lnkSOAPNote, lnkReferral, lnkPrescription, lnkPenalty, lnkPayment, lblPatientId, lnkLabTest, lblDoctor, lblDate, lblFrozen
                }, "Not Set");
                return;
            }

            lblPatientId.Text = Appointment.Patient.GetFullName() + " . " + Appointment.Patient.GetGenderChar() + " . since "
                + Appointment.Patient.RegisterDate.ToShortDateString();
            lblDate.Text = Appointment.AppointmentDate.ToShortDateString() + ". . " + Appointment.AppointmentTime.ToString(@"hh\mm") + " - " + Appointment.AppointmentEndTime.ToString(@"hh\mm");
            lblDoctor.Text = Appointment.Staff.GetFullName();
            lblType.Text = Appointment.AppointmentTypes.AppointmentTypeText;
            lblStatus.Text = Appointment.AppointmentStatuses.AppointmentStatusText;
            lblTypeTitle.Text = $"[{Appointment.AppointmentTypes.AppointmentTypeText}]";

            if (Appointment.Patient.IsFrozen ?? false)
                lblFrozen.Text = "Frozen until " + Appointment.Patient.EndOfBookingFreeze;
            else
                lblFrozen.Text = "Not Frozen";

            if (Appointment.Patient.HasUnPaidFees ?? false)
                lblUnpaidfees.Text = " . " + Appointment.Patient.ISUnpaidText();
            else
                lblUnpaidfees.Text = "None";


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

        private void lnkSOAPNote_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewSOAPNote viewNote = new ViewSOAPNote(Appointment.AppointmentID);
            if (activeForms.Where(x => x.Text == viewNote.Text).Count() > 0)
            {
                MessageBox.Show("Form is already open", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                activeForms.Add(viewNote);

            viewNote.ShowDialog();
            activeForms.Remove(viewNote);
        }

        private void lnkPrescription_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewPrescription viewPrescription = new ViewPrescription(Appointment.AppointmentID);
            if (activeForms.Where(x => x.Text == viewPrescription.Text).Count() > 0)
            {
                MessageBox.Show("Form is already open", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                activeForms.Add(viewPrescription);

            viewPrescription.ShowDialog();
            activeForms.Remove(viewPrescription);
        }

        private void lnkLabTest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManageLabTests manageTests = new ManageLabTests(Appointment.AppointmentID);

            if (activeForms.Where(x => x.Text == manageTests.Text).Count() > 0)
            {
                MessageBox.Show("Form is already open", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                activeForms.Add(manageTests);

            manageTests.ShowDialog();
            activeForms.Remove(manageTests);
        }
    }
}
