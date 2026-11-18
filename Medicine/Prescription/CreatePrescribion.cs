using Business;
using ClinicSystem.Helpers;
using ClinicSystem.Medicine.PrescriptionItem;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem.Medicine.Prescription
{
    public partial class CreatePrescribion : FormsDefaultSettings
    {
        Business.Prescription prescription { get; set; }
        Business.Appointment Appointment { get; set; }
        private readonly int AppointmentId;
        public CreatePrescribion(int appointmentId)
        {
            InitializeComponent();
            AppointmentId = appointmentId;
            SetFormTextBasedMode("Create new prescription", "Add prescription", "Create");
        }

        public delegate void SendNotifiyOnPrescriptionAdd();
        public event SendNotifiyOnPrescriptionAdd OnSave;
        private async Task SetUpAddMode(int appointmentId)
        {
            prescription = new Business.Prescription();
            Appointment = await Appointment.FindAppointmentByID(appointmentId);

            if (ValidationHelper.IsEmptyOrNull(Appointment, "Appointment is not found"))
                return;

            await viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(appointmentId);

            viewPersonInfoCtrl1.SetData(Appointment.Patient);
            viewStaffInfoCtrl1.LoadStaffInfo(Appointment.Staff);
        }
        private void btnCancel_Click(object sender, System.EventArgs e) => Close();
        private void lnkAddItem_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            AddUpdatePrescriptionItem addItem = new AddUpdatePrescriptionItem();
            addItem.OnSend += ReciveItemInfo;
            addItem.ShowDialog();
        }
        private void ReciveItemInfo(Business.PrescriptionItem item)
        {
            prescription.PrescriptionItems.Add(item);
            prescription.TotalPrice = prescription.CalculateTotalPrice();
            lblTotal.Text = $"{prescription.TotalPrice} $";
        }
        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            Text = pageTitle;
            btnSave.Text = saveButtonText;
        }
        private void CollectData()
        {
            prescription.ExpirationDate = dtpExpirationDate.Value;
            prescription.WriteDate = DateTime.Now;
            prescription.DoctorDidSign = chkDidSign.Checked;
            prescription.AppointmentID = Appointment.AppointmentID;
        }
        private async Task PerformSave()
        {
            CollectData();

            OperationResult result = await prescription.Save();

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetFormTextBasedMode("Update prescription", "Update prescription", "Update");
            lblPrescriptionId.Text = $"per-{prescription.PrescriptionID}";
            lnkAddItem.Enabled = false;
            await viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(prescription.AppointmentID);
            OnSave?.Invoke();
        }
        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            await PerformSave();
        }
        private async void CreatePrescribion_Load(object sender, EventArgs e) => await SetUpAddMode(AppointmentId);
    }
}