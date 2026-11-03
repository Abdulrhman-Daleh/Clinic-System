using Business;
using ClinicSystem.Helpers;

namespace ClinicSystem.Appointments.SOAPNotes
{
    public partial class ViewSOAPNote : FormsDefaultSettings
    {
        public SOAPNote CurrentNote { get => viewSoapNoteCtrl1.CurrentNote; }
        public ViewSOAPNote(int appointmentId)
        {
            InitializeComponent();
            viewSoapNoteCtrl1.LoadSOAPNoteInfoByAppointmentId(appointmentId);
            LoadSOAPNoteInfo(appointmentId);
        }

        public void LoadSOAPNoteInfo(int appointmentId)
        {
            if (ValidationHelper.IsEmptyOrNull(viewSoapNoteCtrl1.CurrentNote))
            {
                lblNoteId.Text = "[Not Set]";
                return;
            }

            lblNoteId.Text = $"SOAP Note - {CurrentNote.WrittenBy.GetFullName()} . {CurrentNote.CreationDate.ToShortDateString()}";
            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(appointmentId);
            viewPersonInfoCtrl1.SetData(CurrentNote.WrittenBy);
            viewMRecordInfo1.LoadMedicalRecordInfo(CurrentNote.MedicalRecord);
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => Close();
    }
}
