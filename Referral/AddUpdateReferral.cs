using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem.Referral
{
    public partial class AddUpdateReferral : FormsDefaultSettings
    {
        private Business.Referral referral;
        private int AppointmentId;
        private EnMode Mode;
        public AddUpdateReferral(int appointmentId, EnMode mode)
        {
            InitializeComponent();
            lblReferralId.Text = "Not Set";

            cbReferralUrgency.DataSource = Enum.GetValues(typeof(EnReferralUrgencies));
            AppointmentId = appointmentId;
            Mode = mode;

            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(appointmentId);
            Load += AddUpdateReferral_Load;
        }

        private async Task SetUpForm(int appointmentId, EnMode mode)
        {
            if (mode == EnMode.Add)
            {
                referral = new Business.Referral();
                SetFormTextBasedMode(
                    "Add Referral",
                    "Add Referral",
                    "Add");
                return;
            }

            SetFormTextBasedMode(
                    "Update Referral",
                    "Update Referral",
                    "Update");

            referral = await Business.Referral.FindReferralByAppointmentID(appointmentId);
            txtReferralReason.Text = referral.ReferralReason;
            txtSpeciality.Text = referral.Speciality;
            cbReferralUrgency.SelectedIndex = (int)referral.ReferralUrgency - 1;
            lblReferralId.Text = $"Ref-{referral.ReferralID}";
            btnSave.Enabled = referral.Appointment.IsStillActive();
            gbReferral.Enabled = referral.Appointment.IsStillActive();
        }

        public delegate void SendReferralInfo(Business.Referral referral);
        public event SendReferralInfo OnSave;

        private void SetFormTextBasedMode(
            string labelTitle,
            string pageTitle,
            string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            Text = pageTitle;
            btnSave.Text = saveButtonText;
        }

        private void SetPage()
        {
            cbReferralUrgency.SelectedIndex = 0;
            SetControls();
        }

        private async void AddUpdateReferral_Load(object sender, EventArgs e)
        {
            SetPage();
            await SetUpForm(AppointmentId, Mode);
        }

        private void SetControls()
        {
            if (ValidationHelper.IsEmptyOrNull(viewAppointmentInfoCtrl1.CurrentAppointment))
                return;

            referral.Appointment = viewAppointmentInfoCtrl1.CurrentAppointment;
            referral.AppointmentID = referral.Appointment.AppointmentID;

            viewSoapNoteCtrl1.LoadSOAPNoteInfoByAppointmentId(referral.AppointmentID);
            viewStaffInfoCtrl1.LoadStaffInfo(referral.Appointment.Staff);
        }

        private void CollectData()
        {
            referral.AppointmentID = viewAppointmentInfoCtrl1.CurrentAppointment.AppointmentID;
            referral.NoteID = viewSoapNoteCtrl1.CurrentNote.NoteID;

            referral.DoctorID = Global.CurrentLoggedInStaff.StaffId;
            referral.Doctor = Global.CurrentLoggedInStaff;

            referral.Note = viewSoapNoteCtrl1.CurrentNote;
            referral.NoteID = referral.Note == null ? -1 : referral.Note.NoteID;

            referral.ReferralReason = txtReferralReason.Text.Trim();

            referral.ReferralUrgency = (EnReferralUrgencies)cbReferralUrgency.SelectedItem;

            referral.Speciality = txtSpeciality.Text.Trim();
        }

        private void txtReferralReason_Validating(
            object sender,
            CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox(
                (TextBox)sender, errors);
        }

        private void txtSpeciality_Validating(
            object sender,
            CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox(
                (TextBox)sender, errors);
        }

        private void cbReferralUrgency_Validating(
            object sender,
            CancelEventArgs e)
        {
            if (cbReferralUrgency.SelectedIndex == -1)
            {
                errors.SetError(
                    cbReferralUrgency,
                    "Select a referral urgency");

                return;
            }

            errors.SetError(cbReferralUrgency, null);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            ValidateChildren();

            OperationResult validationResult =
                ValidationHelper.ValidateForm(this, errors);

            if (!validationResult.IsSuccess)
            {
                MessageBox.Show(
                    validationResult.Message,
                    validationResult.Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            CollectData();

            OperationResult result = await referral.Save();

            MessageBox.Show(
                result.Message,
                result.Title,
                MessageBoxButtons.OK,
                result.IsSuccess
                    ? MessageBoxIcon.Information
                    : MessageBoxIcon.Error);

            if (result.IsSuccess)
            {
                SetFormTextBasedMode(
                    "Update Referral",
                    "Update Referral",
                    "Update");


                viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(referral.AppointmentID);
                OnSave?.Invoke(referral);

                lblReferralId.Text = $"Ref-{referral.ReferralID}";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}


