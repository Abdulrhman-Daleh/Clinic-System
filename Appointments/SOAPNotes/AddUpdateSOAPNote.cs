using Business;
using ClinicSystem.Helpers;
using System;
using System.Windows.Forms;

namespace ClinicSystem.Appointments.SOAPNotes
{
    public partial class AddUpdateSOAPNote : FormsDefaultSettings
    {
        private Appointment Appointment { get; set; }
        private SOAPNote Note { get; set; }
        public AddUpdateSOAPNote(int appointmentId)
        {
            InitializeComponent();
            lblNoteId.Text = "[Not Set]";
            SetFormTextBasedMode("Add SOAP Note", "Add SOAP Note", "Add Note");
            Appointment = Appointment.FindAppointmentByID(appointmentId);
            Note = new SOAPNote();
            SetUpInfo();
        }

        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            Text = pageTitle;
            btnSave.Text = saveButtonText;
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void SetUpInfo()
        {
            if (ValidationHelper.IsEmptyOrNull(Appointment))
            {
                MessageBox.Show("Appointment is not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            findAppointmentCtrl1.LoadAppointmentInfo(Appointment.AppointmentID);
            findPersonCtrl1.RecivePersonInfo(Appointment.Patient);
            viewMRecordInfo1.LoadMedicalRecordInfo(Business.MedicalRecord.FindMedicalRecordByPatientID(Appointment.PatientID));
            findAppointmentCtrl1.AllowFind = false;
            findPersonCtrl1.AllowFind = false;
        }

        private void CollectData()
        {
            Note.AppointmentID = Appointment.AppointmentID;
            Note.WrittenByID = Global.CurrentLoggedInStaff.StaffId;
            Note.MedicalRecordID = viewMRecordInfo1.CurrentMRecord == null ? -1 : viewMRecordInfo1.CurrentMRecord.MedicalRecordID;
            Note.CreationDate = DateTime.Now;
            Note.ObjectiveContent = ValidationHelper.IsEmptyOrNull(txtObjective.Text.Trim()) ? null : txtObjective.Text.Trim();
            Note.PlanContent = ValidationHelper.IsEmptyOrNull(txtPlan.Text.Trim()) ? null : txtPlan.Text.Trim();
            Note.SubjectiveContent = ValidationHelper.IsEmptyOrNull(txtSubjective.Text.Trim()) ? null : txtSubjective.Text.Trim();
            Note.AssesmentContent = ValidationHelper.IsEmptyOrNull(txtAssessment.Text.Trim()) ? null : txtAssessment.Text.Trim();
            Note.IsActive = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CollectData();
            OperationResult result = Note.Save();

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblNoteId.Text = "[Not Set]";
                return;
            }

            SetFormTextBasedMode("Update SOAP Note", "Update SOAP Note", "Update Note");
            MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblNoteId.Text = $"No-{Note.NoteID}";
            findAppointmentCtrl1.LoadAppointmentInfo(Note.AppointmentID);

        }
    }
}
