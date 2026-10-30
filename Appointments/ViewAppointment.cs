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

            lblAppointmentId.Text = appointment.Patient.GetFullName() + " . " + appointment.Staff.RoleInfo.RoleName + " . " + appointment.Staff.GetFullName();
            lblStatus.Text = appointment.AppointmentStatuses.AppointmentStatusText;
            btnReschedule.Enabled = true;
            lblAppointmentId.Text = "Appo-" + appointment.AppointmentID.ToString();
        }

        private void btnReschedule_Click(object sender, System.EventArgs e)
        {
            BookAppointment rescheduleAppointment = new BookAppointment(findAppointmentCtrl1.CurrentAppointment.AppointmentID, AppEnums.cs.EnAppointmentStatuses.Rescheduled);
            rescheduleAppointment.ShowDialog();
        }
    }
}
