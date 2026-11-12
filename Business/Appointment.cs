
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
            if (appointment == null) return null;

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
            if (!Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess)
                return new BindingList<string>();

            return await AppointmentData.GetAppointmentTypesNames();
        }
        private bool AddNewAppointment()
        {
            AppointmentID = AppointmentData.AddNewAppointment(ToDto(this));

            return AppointmentID > 0;
        }
        private Penalty GetPenaltyToAppointment(Policy policy, Appointment appointment)
        {
            Penalty peanlty = new Penalty();
            return peanlty.CreatePenalty(policy, appointment);
        }
        private bool UpdateAppointment(Payment paymentInfo, Policy policy = null)
        {
            return AppointmentData.UpdateAppointment(ToDto(this), Payment.ToDTO(paymentInfo), Penalty.ToDTO(GetPenaltyToAppointment(policy, this))) > 0;
        }
        private OperationResult CheckSaveAccess()
        {
            if (_mode == EnMode.Add)
                return Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner);

            Staff currentStaff = Global.CurrentLoggedInStaff;

            if (currentStaff == null)
            {
                return new OperationResult(
                    "You must be logged in to modify appointments.",
                    "Access Denied",
                    false);
            }

            if (currentStaff.RoleInfo.RoleId == EnRole.Receptionist ||
                currentStaff.RoleInfo.RoleId == EnRole.Owner)
            {
                OperationResult result = Util.CanAccessForm(
                    EnRole.Receptionist,
                    EnRole.Owner);

                if (!result.IsSuccess)
                    return result;

                if (AppointmentStatuses.AppointmentStatusID == EnAppointmentStatuses.Completed)
                {
                    return new OperationResult(
                        "Only the assigned doctor can complete an appointment.",
                        "Access Denied",
                        false);
                }

                return result;
            }

            if (currentStaff.RoleInfo.RoleId != EnRole.Doctor)
            {
                return new OperationResult(
                    "You do not have permission to modify appointments.",
                    "Access Denied",
                    false);
            }

            AppointmentDto existingAppointment =
                AppointmentData.FindAppointmentByID(AppointmentID, Global.CurrentLoggedInStaff.StaffId);

            if (existingAppointment == null)
            {
                return new OperationResult(
                    "Appointment does not exist.",
                    "Not Found",
                    false);
            }

            if (existingAppointment.StaffID != currentStaff.StaffId)
            {
                return new OperationResult(
                    "You are not authorized to modify this appointment.",
                    "Access Denied",
                    false);
            }

            if (existingAppointment.AppointmentStatuses.AppointmentStatusID == EnAppointmentStatuses.Completed ||
                existingAppointment.AppointmentStatuses.AppointmentStatusID == EnAppointmentStatuses.Cancelled ||
                existingAppointment.AppointmentStatuses.AppointmentStatusID == EnAppointmentStatuses.NoShow)
            {
                return new OperationResult(
                    "This appointment can no longer be completed.",
                    "Not Allowed",
                    false);
            }

            // Doctors can only change the appointment status to Completed.
            // They cannot change the patient, assigned staff, type, date, or time.

            if (AppointmentStatuses.AppointmentStatusID != EnAppointmentStatuses.Completed ||
                PatientID != existingAppointment.PatientID ||
                StaffID != existingAppointment.StaffID ||
                AppointmentTypes.AppointmentTypeID != existingAppointment.AppointmentTypes.AppointmentTypeID ||
                AppointmentDate != existingAppointment.AppointmentDate ||
                AppointmentTime != existingAppointment.AppointmentTime ||
                AppointmentEndTime != existingAppointment.AppointmentEndTime)
            {
                return new OperationResult(
                    "Doctors can only complete their assigned appointments.",
                    "Access Denied",
                    false);
            }

            return new OperationResult(
                "Appointment completion access granted.",
                "Access Granted",
                true);
        }
        public OperationResult Save(Payment paymentInfo = null, Policy policy = null)
        {
            OperationResult result = CheckSaveAccess();

            if (!result.IsSuccess)
                return result;

            Staff currentStaff = Global.CurrentLoggedInStaff;

            // Completing an appointment does not require checking
            // schedule availability or staff activation again.

            if (_mode == EnMode.Update &&
                currentStaff.RoleInfo.RoleId == EnRole.Doctor)
            {
                result = IsAppointmentValidToComplete();

                if (policy == null)
                    result = new OperationResult("policy is required to be selected", "Not Valid", false);
            }
            else
            {
                result = IsAppointmentValidToSave();
            }

            if (!result.IsSuccess)
                return result;

            string performedBy = $"StaffID {currentStaff.StaffId}";

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

                    if (UpdateAppointment(paymentInfo, policy))
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
        private OperationResult IsAppointmentValidToComplete()
        {
            if (!Util.IsValidId(AppointmentID) ||
                !Util.IsValidId(PatientID) ||
                !Util.IsValidId(StaffID) ||
                Patient == null ||
                Staff == null)
            {
                return new OperationResult(
                    "Appointment information is invalid.",
                    "Invalid",
                    false);
            }

            return new OperationResult(
                "Appointment information is valid to complete.",
                "Valid",
                true);
        }
        public bool NoOverlapWithLastAppointment()
        {
            return AppointmentData.NoOverlapWithLastAppointment(ToDto(this));
        }
        public static Appointment FindAppointmentByID(int appointmentId)
        {
            if (!Util.CanAccessForm(
                EnRole.Receptionist,
                EnRole.Doctor,
                EnRole.Nurse,
                EnRole.Owner).IsSuccess)
                return null;

            AppointmentDto appointmentDto =
                AppointmentData.FindAppointmentByID(appointmentId, Global.CurrentLoggedInStaff.StaffId);

            if (appointmentDto == null)
                return null;

            // A doctor can only retrieve their own appointments.
            if (Global.CurrentLoggedInStaff.RoleInfo.RoleId == EnRole.Doctor &&
                appointmentDto.StaffID != Global.CurrentLoggedInStaff.StaffId)
                return null;

            return FromDto(appointmentDto);
        }
        private bool CanAccessCurrentAppointment()
        {
            if (Global.CurrentLoggedInStaff == null)
                return false;

            switch (Global.CurrentLoggedInStaff.RoleInfo.RoleId)
            {
                case EnRole.Receptionist:
                case EnRole.Owner:
                case EnRole.Nurse:
                    return true;

                case EnRole.Doctor:
                    return StaffID == Global.CurrentLoggedInStaff.StaffId;

                default:
                    return false;
            }
        }
        public bool HasSOAPNotes() =>
            CanAccessCurrentAppointment() &&
            AppointmentData.HasSOAPNotes(AppointmentID);
        public bool HasPrescription() =>
            CanAccessCurrentAppointment() &&
            AppointmentData.HasPrescription(AppointmentID);
        public bool HasLabTests() =>
            CanAccessCurrentAppointment() &&
            AppointmentData.HasLabTests(AppointmentID);
        public bool HasReferral() =>
            CanAccessCurrentAppointment() &&
            AppointmentData.HasReferral(AppointmentID);
        public bool HasPayment() =>
            CanAccessCurrentAppointment() &&
            AppointmentData.HasPayment(AppointmentID);
        public bool HasPenalty() =>
            CanAccessCurrentAppointment() &&
            AppointmentData.HasPenalty(AppointmentID);
        private OperationResult IsAppointmentValidToSave()
        {
            if (!Util.IsValidId(PatientID) ||
                !Util.IsValidId(StaffID) ||
                Patient == null ||
                Staff == null ||
                Patient.PatientId != PatientID ||
                Staff.StaffId != StaffID)
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
        public bool IsStillActive() =>
            AppointmentStatuses.AppointmentStatusID != EnAppointmentStatuses.Completed &&
            AppointmentStatuses.AppointmentStatusID != EnAppointmentStatuses.Cancelled &&
            AppointmentStatuses.AppointmentStatusID != EnAppointmentStatuses.NoShow;
    }
}
