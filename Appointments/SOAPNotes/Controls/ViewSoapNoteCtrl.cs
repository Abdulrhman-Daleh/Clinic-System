using Business;
using ClinicSystem.Helpers;

namespace ClinicSystem.Appointments.SOAPNotes.Controls
{
    public partial class ViewSoapNoteCtrl : UserCtrlDefaultSettings
    {
        private SOAPNote note = null;
        public SOAPNote CurrentNote { get => note; }
        public ViewSoapNoteCtrl()
        {
            InitializeComponent();
        }

        public void LoadSOAPNoteInfoByAppointmentId(int appointmentId)
        {
            note = SOAPNote.FindSOAPNoteByAppointmentID(appointmentId);

            if (ValidationHelper.IsEmptyOrNull(note, "Note is not available"))
            {
                CommonOperations.SetUpDefaultControlsValue(new System.Collections.Generic.List<System.Windows.Forms.Control>()
                {
                    txtAssessment, txtObjective, txtPlan, txtSubjective, txtWrittenBy
                }, "Not Set");
                return;
            }

            txtWrittenBy.Text += $" {note.WrittenBy.GetFullName()} . {note.CreationDate.ToShortDateString()}";
            txtSubjective.Text = ValidationHelper.IsEmptyOrNull(note.SubjectiveContent) ? "None" : note.SubjectiveContent;
            txtObjective.Text = ValidationHelper.IsEmptyOrNull(note.ObjectiveContent) ? "None" : note.ObjectiveContent;
            txtPlan.Text = ValidationHelper.IsEmptyOrNull(note.PlanContent) ? "None" : note.PlanContent;
            txtAssessment.Text = ValidationHelper.IsEmptyOrNull(note.AssesmentContent) ? "None" : note.AssesmentContent;
        }
    }
}
