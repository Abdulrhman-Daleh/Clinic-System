using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;

namespace ClinicSystem.Appointments
{
    public partial class ViewAppointment : FormsDefaultSettings
    {
        public ViewAppointment(int appointmentId)
        {
            InitializeComponent();
            findAppointmentCtrl1.LoadAppointmentInfo(appointmentId);
            lblAppointmentId.Text = appointmentId.ToString();
            findAppointmentCtrl1.AllowFind = false;
            SetAppointment(findAppointmentCtrl1.CurrentAppointment);
        }
        public ViewAppointment()
        {
            InitializeComponent();
            findAppointmentCtrl1.OnFind += ReciveAppointmentId;
        }
        private void ReciveAppointmentId(Appointment appointment)
        {
            SetAppointment(appointment);
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void SetAppointment(Appointment appointment)
        {
            if (ValidationHelper.IsEmptyOrNull(appointment))
            {
                lblAppointmentId.Text = "Not Set";
                btnReschedule.Enabled = false;
                return;
            }

            patientInfoCtrl1.LoadPatientInfo(appointment.Patient);
            viewPersonContacts1.LoadUpPersonContact(appointment.Patient.PersonId);
            viewPersonInfoCtrl1.SetData(appointment.Staff);
            lblAppointmentId.Text = appointment.Patient.GetFullName() + " . " + appointment.Staff.Role.ToString() + " . " + appointment.Staff.GetFullName();
            lblStatus.Text = appointment.AppointmentStatuses.ToString();
            btnReschedule.Enabled = true;
            lblAppointmentId.Text = "Appo-" + appointment.AppointmentID.ToString();
            btnReschedule.Enabled = appointment.IsStillActive();
        }

        public delegate void SendAppointmentOnSave(Appointment appointment);
        public event SendAppointmentOnSave OnSave;

        private void btnReschedule_Click(object sender, System.EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Receptionist, EnRole.Owner))
                return;

            BookAppointment rescheduleAppointment = new BookAppointment(findAppointmentCtrl1.CurrentAppointment.AppointmentID, EnAppointmentStatuses.Rescheduled);

            rescheduleAppointment.OnSave += RescheduleAppointment_OnSave;
            rescheduleAppointment.ShowDialog();
        }

        private void RescheduleAppointment_OnSave(Appointment appointment)
        {
            OnSave?.Invoke(appointment);
        }
    }
}
