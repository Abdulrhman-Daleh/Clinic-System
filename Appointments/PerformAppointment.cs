using AppEnums.cs;
using Business;
using ClinicSystem.Appointments.SOAPNotes;
using ClinicSystem.Helpers;
using ClinicSystem.Insurance;
using ClinicSystem.LabOrderTest;
using ClinicSystem.MedicalRecord;
using ClinicSystem.Medicine.Prescription;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Appointments
{
    public partial class PerformAppointment : FormsDefaultSettings
    {
        private Appointment Appointment { get; set; }
        private Business.MedicalRecord MedicalRecord { get; set; }
        private Business.Insurance Insurance { get; set; }
        public PerformAppointment(int appointmentId)
        {
            InitializeComponent();
            Appointment = Appointment.FindAppointmentByID(appointmentId);
            SetUpForm();
        }

        private void SetUpForm()
        {
            if (ValidationHelper.IsEmptyOrNull(Appointment))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>()
                {
                    lblAppointmentId, lblPatientName, lblName, lblAllergies, lblCurrentMedication, lblInsurance, lblStatus, lblMedicalRecord, lblType
                }, "Not Set");

                lnkViewInsurance.Enabled = false;
                lnkViewMedicalRecord.Enabled = false;
                return;
            }

            MedicalRecord = Business.MedicalRecord.FindMedicalRecordByPatientID(Appointment.PatientID);
            Insurance = Business.Insurance.FindInsuranceByPatientId(Appointment.PatientID);

            lblAppointmentId.Text = Appointment.AppointmentDate.ToShortDateString() + " . " + Appointment.AppointmentTime.ToString(@"hh\mm") + " - " + Appointment.AppointmentEndTime.ToString(@"hh\mm");
            lblName.Text = Appointment.Patient.GetFullName() + " . Dr. " + Appointment.Staff.GetFullName();

            lblType.Text = $"[{Appointment.AppointmentTypes.AppointmentTypeText}]";

            lblStatus.Text = $"[{Appointment.AppointmentStatuses.AppointmentStatusText}]";

            lblPatientName.Text = $"{Appointment.Patient.GetFullName()} . since {Appointment.Patient.RegisterDate.ToShortDateString()} . {Appointment.Patient.GetTotalVisits()} vists";

            lnkSOAPNote.Enabled = !Appointment.HasSOAPNotes();
            lnkPenalty.Enabled = !Appointment.HasPenalty();
            lnkPayment.Enabled = !Appointment.HasPayment();
            lnkPrescribe.Enabled = !Appointment.HasPrescription();
            lnkRefer.Enabled = !Appointment.HasReferral();

            SetUpMedicalRecord();
            SetUpInsurance();
        }

        private void SetUpMedicalRecord()
        {
            if (ValidationHelper.IsEmptyOrNull(MedicalRecord))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>()
                {
                    lblCurrentMedication, lblMedicalRecord, lblAllergies
                }, "Not Set");

                lnkViewMedicalRecord.Enabled = false;
                lnkSOAPNote.Enabled = false;
                return;
            }

            string Allergies = ValidationHelper.IsEmptyOrNull(MedicalRecord.Allergies) ? "None" : MedicalRecord.Allergies;
            string CurrentMedication = ValidationHelper.IsEmptyOrNull(MedicalRecord.CurrentMedication) ? "None" : MedicalRecord.CurrentMedication;

            lblAllergies.Text = $"Allergies - {Allergies}";
            lblCurrentMedication.Text = $"Current medication - {CurrentMedication}";
            lblMedicalRecord.Text = $"Medical record - {MedicalRecord.MedicalNumber} . {Appointment.AppointmentTypes.AppointmentTypeText}";
        }

        private void SetUpInsurance()
        {
            if (ValidationHelper.IsEmptyOrNull(Insurance))
            {
                lblInsurance.Text = "None";
                lnkViewInsurance.Enabled = false;
                return;
            }

            bool IsActive = ValidationHelper.Date1BeforeDate2(Insurance.CoverageStartDate, Insurance.CoverageEndDate);

            lblInsurance.Text = $"Insurance - {Insurance.InsuranceProvider} {Insurance.PolicyNumber} . ";
            if (IsActive)
            {
                lblInsurance.Text += "[Active]";
                lnkViewInsurance.Enabled = true;
            }
            else
            {
                lblInsurance.Text += "[Not Active]";
                lnkViewInsurance.Enabled = false;
            }
        }

        private void lnkViewMedicalRecord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewMedicalRecord medicalRecord = new ViewMedicalRecord(MedicalRecord.MedicalRecordID);
            medicalRecord.ShowDialog();
        }

        private void lnkViewInsurance_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewInsuranceInfo viewInsurance = new ViewInsuranceInfo(Insurance.InsuranceID);
            viewInsurance.ShowDialog();
        }

        private void BtnCancel_Click(object sender, System.EventArgs e) => Close();

        private void lnkSOAPNote_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddUpdateSOAPNote writeSOAP = new AddUpdateSOAPNote(Appointment.AppointmentID);
            writeSOAP.OnSave += ReciveNotifyOnNoteSave;
            writeSOAP.ShowDialog();
        }
        private void ReciveNotifyOnNoteSave() => lnkSOAPNote.Enabled = false;
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Appointment.AppointmentStatuses = (EnAppointmentStatuses.Completed, "Completed");

            OperationResult result = Appointment.Save();

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSave.Enabled = false;
        }

        private void lnkPrescribe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreatePrescribion prescribe = new CreatePrescribion(Appointment.AppointmentID);
            prescribe.OnSave += ReciveNotifyOnPrescriptionAdd;
            prescribe.ShowDialog();
        }

        private void ReciveNotifyOnPrescriptionAdd() => lnkPrescribe.Enabled = false;

        private void lnkOrderTests_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddUpdateLabTest addLabTest = new AddUpdateLabTest(Appointment.AppointmentID);
            addLabTest.ShowDialog();
        }
    }
}
