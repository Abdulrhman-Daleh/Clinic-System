
using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class Appointment
    {
        private EnMode _mode;

        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int StaffID { get; set; }
        public (EnAppointmentStatuses AppointmentStatusID, string AppointmentStatusText) AppointmentStatuses { get; set; }
        public (EnAppointmentTypes AppointmentTypeID, string AppointmentTypeText) AppointmentTypes { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public TimeSpan AppointmentEndTime { get; set; }
        public Patient Patient { get; set; }
        public Staff Staff { get; set; }

        public Appointment()
        {
            AppointmentID = -1;
            PatientID = -1;
            StaffID = -1;
            AppointmentStatuses = ((EnAppointmentStatuses)EnAppointmentStatuses.Scheduled, "Scheduled");
            AppointmentTypes = ((EnAppointmentTypes)0, "None");
            AppointmentDate = DateTime.Now;
            AppointmentTime = new TimeSpan();
            AppointmentEndTime = new TimeSpan();
            Patient = new Patient();
            Staff = new Staff();
            _mode = EnMode.Add;
        }

        private static AppointmentDto ToDto(Appointment appointment)
        {
            return new AppointmentDto()
            {
                AppointmentID = appointment.AppointmentID,
                PatientID = appointment.PatientID,
                StaffID = appointment.StaffID,
                AppointmentStatuses = (appointment.AppointmentStatuses.AppointmentStatusID, appointment.AppointmentStatuses.AppointmentStatusText),
                AppointmentTypes = (appointment.AppointmentTypes.AppointmentTypeID, appointment.AppointmentTypes.AppointmentTypeText),
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                AppointmentEndTime = appointment.AppointmentEndTime
            };
        }

        private static Appointment FromDto(AppointmentDto appointment)
        {
            if (appointment == null) return null;

            return new Appointment()
            {
                AppointmentID = appointment.AppointmentID,
                PatientID = appointment.PatientID,
                StaffID = appointment.StaffID,
                AppointmentStatuses = appointment.AppointmentStatuses,
                AppointmentTypes = appointment.AppointmentTypes,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                AppointmentEndTime = appointment.AppointmentEndTime,
                _mode = EnMode.Update,
                Staff = Staff.FindStaffById(appointment.StaffID),
                Patient = Patient.FindPatientById(appointment.PatientID)
            };
        }

        public static async Task<BindingList<string>> GetAppointmentTypesNames()
        {
            return await AppointmentData.GetAppointmentTypesNames();
        }

        private bool AddNewAppointment()
        {
            AppointmentID = AppointmentData.AddNewAppointment(ToDto(this));

            return AppointmentID > 0;
        }

        private bool UpdateAppointment()
        {
            return AppointmentData.UpdateAppointment(ToDto(this)) > 0;
        }

        public OperationResult Save()
        {
            OperationResult result = IsAppointmentValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy = Global.CurrentLoggedInStaff == null
                ? "Initial setup"
                : $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case EnMode.Add:

                    if (AddNewAppointment())
                    {
                        _mode = EnMode.Update;

                        Auditor.Log(
                            "Appointment Added",
                            $"Appointment type '{AppointmentTypes.AppointmentTypeText}' for PatientID {PatientID} with StaffID {StaffID} was scheduled on {AppointmentDate:d} from {AppointmentTime:hh\\:mm} to {AppointmentEndTime:hh\\:mm} by {performedBy}.",
                            $"AppointmentID {AppointmentID}");

                        return new OperationResult(
                            "Appointment added successfully.",
                            "Added",
                            true);
                    }

                    return new OperationResult(
                        "Failed to add appointment.",
                        "Failed",
                        false);

                case EnMode.Update:

                    if (UpdateAppointment())
                    {
                        Auditor.Log(
                            "Appointment Updated",
                            $"Appointment type '{AppointmentTypes.AppointmentTypeText}' for PatientID {PatientID} with StaffID {StaffID} was updated to {AppointmentDate:d} from {AppointmentTime:hh\\:mm} to {AppointmentEndTime:hh\\:mm} (Status: {AppointmentStatuses.AppointmentStatusText}) by {performedBy}.",
                            $"AppointmentID {AppointmentID}");

                        return new OperationResult(
                            "Appointment updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update appointment.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid appointment operation mode.",
                "Failed",
                false);
        }

        public bool NoOverlapWithLastAppointment()
        {
            return AppointmentData.NoOverlapWithLastAppointment(ToDto(this));
        }

        public static Appointment FindAppointmentByID(int appointmentId)
        {
            return FromDto(AppointmentData.FindAppointmentByID(appointmentId));
        }

        private OperationResult IsAppointmentValidToSave()
        {
            if (!Util.IsValidId(PatientID) ||
                !Util.IsValidId(StaffID) ||
                Patient == null ||
                Staff == null)
            {
                return new OperationResult(
                    "Patient or staff information is invalid.",
                    "Invalid",
                    false);
            }

            if (!Staff.IsActive)
            {
                return new OperationResult(
                    "Cannot schedule an appointment with inactive staff.",
                    "Invalid",
                    false);
            }

            if (!NoOverlapWithLastAppointment())
            {
                return new OperationResult(
                    "Staff already has an appointment during this time.",
                    "Appointment Conflict",
                    false);
            }

            return new OperationResult(
                "Appointment information is valid to save.",
                "Valid",
                true);
        }

        public bool HasSOAPNotes() => AppointmentData.HasSOAPNotes(AppointmentID);

        public bool HasPrescription() => AppointmentData.HasPrescription(AppointmentID);

        public bool HasLabTests() => AppointmentData.HasLabTests(AppointmentID);

        public bool HasReferral() => AppointmentData.HasReferral(AppointmentID);

        public bool HasPayment() => AppointmentData.HasPayment(AppointmentID);

        public bool HasPenalty() => AppointmentData.HasPenalty(AppointmentID);
    }
}
