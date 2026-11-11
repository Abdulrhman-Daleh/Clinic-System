using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System;
using System.Windows.Forms;

namespace ClinicSystem.Payment
{
    public partial class AddNewPayment : FormsDefaultSettings
    {
        public enum EnAddMode { AddImmediately = 1, ReturnDtaft }
        private EnAddMode _AddMode;
        Business.Payment payment { get; set; }
        public AddNewPayment(int? appointmentId, int? penaltyId, EnAddMode addMode)
        {
            InitializeComponent();
            SetFormTextBasedMode("Add New Payment", "Add Payment", "Add Payment");
            SetUp(appointmentId, penaltyId);
            _AddMode = addMode;
        }

        public delegate void SendPaymentOnSave(Business.Payment paymentInfo);
        public event SendPaymentOnSave OnSave;

        private void SetUp(int? appointmentId, int? penaltyId)
        {
            payment = new Business.Payment();
            payment.AppointmentID = appointmentId;
            payment.PenaltyID = penaltyId;

            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(appointmentId ?? -1);

            if (ValidationHelper.IsEmptyOrNull(viewAppointmentInfoCtrl1.CurrentAppointment))
                return;

            payment.Appointment = viewAppointmentInfoCtrl1.CurrentAppointment;

            viewStaffInfoCtrl1.LoadStaffInfo(payment.Appointment.Staff);
            patientInfoCtrl1.LoadPatientInfo(payment.Appointment.Patient);
        }
        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            Text = pageTitle;
            btnSave.Text = saveButtonText;
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void CollectData()
        {
            payment.PaymentDate = DateTime.Now;
            payment.ProccessByID = Global.CurrentLoggedInStaff.StaffId;
            payment.ProccessBy = Global.CurrentLoggedInStaff;
            payment.PaymentStatus = (EnPaymentStatuses.Pending, "Pending");
            payment.PaymentAmount = Convert.ToDecimal(mskPaymentAmount.Text);
        }

        private void PerformSave()
        {
            ValidateChildren();

            OperationResult result = ValidationHelper.ValidateForm(this, errors);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            result = Util.CanAccessForm(EnRole.Receptionist, EnRole.Doctor);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CollectData();

            if (_AddMode == EnAddMode.ReturnDtaft)
            {
                OnSave?.Invoke(payment);
                this.Close();
                return;
            }

            result = payment.Save();

            MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, result.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (!result.IsSuccess)
                return;

            lblPaymentId.Text = $"pay-{payment.PaymentID}";
            btnSave.Enabled = false;
            mskPaymentAmount.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PerformSave();
        }

        private void mskPaymentAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.HandleInputType(e, true, false);
        }

        private void mskPaymentAmount_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!decimal.TryParse(mskPaymentAmount.Text, out decimal price) || price < 0)
            {
                errors.SetError(mskPaymentAmount, "Enter a valid unit price");
                return;
            }

            errors.SetError(mskPaymentAmount, null);
        }
    }
}
