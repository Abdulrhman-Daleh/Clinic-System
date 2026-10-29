namespace ClinicSystem.Appointments
{
    public partial class ViewAppointment : FormsDefaultSettings
    {
        public ViewAppointment(int appointmentId)
        {
            InitializeComponent();
            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(appointmentId);
            lblAppointmentId.Text = appointmentId.ToString();
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();
    }
}
