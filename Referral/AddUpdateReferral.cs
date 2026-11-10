using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.Referral
{
    public partial class AddUpdateReferral : FormsDefaultSettings
    {

        private Business.Referral referral;

        public AddUpdateReferral(int appointmentId)
        {
            InitializeComponent();
            referral = new Business.Referral();
            lblReferralId.Text = "Not Set";

            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(appointmentId);
            Load += AddUpdateReferral_Load;

            SetFormTextBasedMode(
                "Add Referral",
                "Add Referral",
                "Add");
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
            cbReferralUrgency.DataSource =
                Enum.GetValues(typeof(EnReferralUrgencies));

            cbReferralUrgency.SelectedIndex = 0;
            SetControls();
        }

        private void AddUpdateReferral_Load(object sender, EventArgs e)
        {
            SetPage();
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

            referral.ReferralReason = txtReferralReason.Text.Trim();

            referral.ReferralUrgencies = (
                (EnReferralUrgencies)cbReferralUrgency.SelectedItem,
                cbReferralUrgency.SelectedItem.ToString());

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

        private void btnSave_Click(object sender, EventArgs e)
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

            OperationResult result = referral.Save();

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


