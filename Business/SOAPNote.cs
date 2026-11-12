
using AppEnums.cs;
using Data_Access.DTOs;
using Data_Access.ListingDTOs;
using System;

namespace Business
{
    public class SOAPNote
    {
        private EnMode _mode;

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
        public Staff WrittenBy { get; set; }
        public Appointment Appointment { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public SOAPNote()
        {
            NoteID = -1;
            WrittenByID = -1;
            AppointmentID = -1;
            MedicalRecordID = -1;
            CreationDate = DateTime.Now;
            _mode = EnMode.Add;
        }
        private SOAPNote(int noteID, int writtenById, int appointmentId, int medicalRecordId)
        {
            NoteID = noteID;
            WrittenBy = Staff.FindStaffById(writtenById);
            Appointment = Appointment.FindAppointmentByID(appointmentId);
            MedicalRecord = MedicalRecord.FindMedicalRecordByID(medicalRecordId);
            _mode = EnMode.Update;
        }
        public static SOAPNote FromDto(SOAPNoteDto noteDto)
        {
            if (noteDto == null)
                return null;

            return new SOAPNote(noteDto.NoteID, noteDto.WrittenByID, noteDto.AppointmentID, noteDto.MedicalRecordID)
            {
                NoteID = noteDto.NoteID,
                WrittenByID = noteDto.WrittenByID,
                AppointmentID = noteDto.AppointmentID,
                CreationDate = noteDto.CreationDate,
                SubjectiveContent = noteDto.SubjectiveContent,
                ObjectiveContent = noteDto.ObjectiveContent,
                AssesmentContent = noteDto.AssesmentContent,
                PlanContent = noteDto.PlanContent,
                IsActive = noteDto.IsActive,
                MedicalRecordID = noteDto.MedicalRecordID
            };
        }
        public static SOAPNoteDto ToDto(SOAPNote note)
        {
            if (note == null)
                return null;

            return new SOAPNoteDto()
            {
                NoteID = note.NoteID,
                WrittenByID = note.WrittenByID,
                AppointmentID = note.AppointmentID,
                CreationDate = note.CreationDate,
                SubjectiveContent = note.SubjectiveContent,
                ObjectiveContent = note.ObjectiveContent,
                AssesmentContent = note.AssesmentContent,
                PlanContent = note.PlanContent,
                IsActive = note.IsActive,
                MedicalRecordID = note.MedicalRecordID
            };
        }
        private static OperationResult CheckDoctorAccess()
        {
            return Util.CanAccessForm(EnRole.Doctor);
        }
        private bool AddNewSOAPNote()
        {
            NoteID = SOAPNoteData.AddNewSOAPNote(ToDto(this));

            return NoteID > 0;
        }
        private bool UpdateSOAPNote()
        {
            return SOAPNoteData.UpdateSOAPNote(ToDto(this));
        }
        public OperationResult Save()
        {
            OperationResult result = CheckDoctorAccess();

            if (!result.IsSuccess)
                return result;

            if (Global.CurrentLoggedInStaff == null)
                return new OperationResult("No user is logged in.", "Not Logged", false);


            if (_mode == EnMode.Add)
                WrittenByID = Global.CurrentLoggedInStaff.StaffId;

            string performedBy = $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            result = IsNoteValidToSave();

            if (!result.IsSuccess)
            {
                return new OperationResult(
                    "Failed to save SOAP note missing some data.",
                    "Failed",
                    false);
            }

            switch (_mode)
            {
                case EnMode.Add:

                    if (AddNewSOAPNote())
                    {
                        _mode = EnMode.Update;

                        Auditor.Log(
                            "SOAP Note Added",
                            $"SOAP note for AppointmentID {AppointmentID} and MedicalRecordID {MedicalRecordID} was created by {performedBy}.",
                            $"NoteID {NoteID}");

                        return new OperationResult(
                            "SOAP note added successfully.",
                            "Added",
                            true);
                    }

                    return new OperationResult(
                        "Failed to add SOAP note.",
                        "Failed",
                        false);

                case EnMode.Update:

                    if (UpdateSOAPNote())
                    {
                        Auditor.Log(
                            "SOAP Note Updated",
                            $"SOAP note for AppointmentID {AppointmentID} and MedicalRecordID {MedicalRecordID} was updated by {performedBy}.",
                            $"NoteID {NoteID}");

                        return new OperationResult(
                            "SOAP note updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update SOAP note.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid SOAP note operation mode.",
                "Failed",
                false);
        }
        public static SOAPNote FindSOAPNoteByID(int noteID)
        {
            if (!CheckDoctorAccess().IsSuccess)
                return null;

            return FromDto(SOAPNoteData.FindSOAPNoteByID(noteID));
        }
        public static SOAPNote FindSOAPNoteByAppointmentID(int appointmentID)
        {
            if (!CheckDoctorAccess().IsSuccess)
                return null;

            return FromDto(SOAPNoteData.FindSOAPNoteByAppointmentID(appointmentID));
        }
        private OperationResult IsNoteValidToSave()
        {
            if (Util.IsValidId(WrittenByID) &&
                Util.IsValidId(AppointmentID) &&
                Util.IsValidId(MedicalRecordID) &&
                IsActive != null)
            {
                return new OperationResult(
                    "Note is valid",
                    "Valid",
                    true);
            }

            return new OperationResult(
                "Note is missing some data",
                "Missing Data",
                false);
        }
    }
}
