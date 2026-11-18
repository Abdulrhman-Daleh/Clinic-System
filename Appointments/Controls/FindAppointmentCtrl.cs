using Business;
using ClinicSystem.Helpers;
using System.Threading.Tasks;

namespace ClinicSystem.Appointments.Controls
{
    public partial class FindAppointmentCtrl : UserCtrlDefaultSettings
    {
        public FindAppointmentCtrl()
        {
            InitializeComponent();
            DefaultForm();
        }
        public bool AllowFind
        {
            get => gbFindBy.Enabled;
            set => gbFindBy.Enabled = value;
        }

        public delegate void SendAppointmentInfo(Appointment appointment);
        public event SendAppointmentInfo OnFind;
        private void cbFindBy_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
        private void DefaultForm()
        {
            cbFindBy.Items.Add("AppointmentID");
            cbFindBy.SelectedIndex = 0;
        }
        private void txtFindBy_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            ValidationHelper.HandleInputType(e, true, false);
        }
        private void txtFindBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(txtFindBy.Text))
                errors.SetError(txtFindBy, "Enter an Appointment Id");
            else
                errors.SetError(txtFindBy, null);
        }
        private async Task Find()
        {
            OperationResult result = ValidationHelper.ValidateForm(this, errors);

            if (!result.IsSuccess)
                return;

            int value = int.Parse(txtFindBy.Text.Trim());

            await viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(value);

            if (AllowFind)
                OnFind?.Invoke(viewAppointmentInfoCtrl1.CurrentAppointment);
        }
        private async void BtnFindAppointment_Click(object sender, System.EventArgs e)
        {
            await Find();
        }
        private void AddNewAppointment_Click(object sender, System.EventArgs e)
        {
            BookAppointment bookAppointment = new BookAppointment(AppEnums.cs.EnAppointmentStatuses.Scheduled);
            bookAppointment.OnSave += ReciveAppointmentInfo;
            bookAppointment.ShowDialog();
        }
        private async void ReciveAppointmentInfo(Appointment appointment)
        {
            await viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(appointment.AppointmentID);

            if (AllowFind)
                OnFind?.Invoke(viewAppointmentInfoCtrl1.CurrentAppointment);
        }
        public Appointment CurrentAppointment => viewAppointmentInfoCtrl1.CurrentAppointment;
        public async Task LoadAppointmentInfo(int appointmentId)
        {
            txtFindBy.Text = appointmentId.ToString();
            cbFindBy.Enabled = false;
            AllowFind = false;
            await Find();
        }
    }
}
