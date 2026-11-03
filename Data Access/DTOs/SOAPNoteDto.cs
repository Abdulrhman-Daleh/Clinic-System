using System;

namespace Data_Access.DTOs
{
    public class SOAPNoteDto
    {
        public int NoteID { get; set; }
        public int WrittenByID { get; set; }
        public int AppointmentID { get; set; }
        public DateTime CreationDate { get; set; }
        public string SubjectiveContent { get; set; }
        public string ObjectiveContent { get; set; }
        public string AssesmentContent { get; set; }
        public string PlanContent { get; set; }
        public bool? IsActive { get; set; }
        public int MedicalRecordID { get; set; }
    }
}
