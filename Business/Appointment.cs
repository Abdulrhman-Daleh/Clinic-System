
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
        public EnAppointmentStatuses AppointmentStatuses { get; set; }
        public EnAppointmentTypes AppointmentTypes { get; set; }
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
            AppointmentStatuses = 0;
            AppointmentTypes = 0;
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
                AppointmentStatuses = appointment.AppointmentStatuses,
                AppointmentTypes = appointment.AppointmentTypes,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                AppointmentEndTime = appointment.AppointmentEndTime
            };
        }
        private static async Task<Appointment> FromDto(AppointmentDto appointmentDto)
        {
            if (appointmentDto == null) return null;

            Appointment appointment = new Appointment()
            {
                AppointmentID = appointmentDto.AppointmentID,
                PatientID = appointmentDto.PatientID,
                StaffID = appointmentDto.StaffID,
                AppointmentStatuses = appointmentDto.AppointmentStatuses,
                AppointmentTypes = appointmentDto.AppointmentTypes,
                AppointmentDate = appointmentDto.AppointmentDate,
                AppointmentTime = appointmentDto.AppointmentTime,
                AppointmentEndTime = appointmentDto.AppointmentEndTime,
                _mode = EnMode.Update,
            };

            appointment.Staff = await Staff.FindStaffById(appointmentDto.StaffID);
            appointment.Patient = await Patient.FindPatientById(appointmentDto.PatientID);

            return appointment;
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
        private bool UpdateAppointment(Payment paymentInfo, Policy policy)
        {
            return AppointmentData.UpdateAppointment(ToDto(this), Payment.ToDTO(paymentInfo), Penalty.ToDTO(GetPenaltyToAppointment(policy,
                this))) > 0;
        }
        private async Task<OperationResult> CheckSaveAccess()
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

            if (currentStaff.Role == EnRole.Receptionist ||
                currentStaff.Role == EnRole.Owner)
            {
                OperationResult result = Util.CanAccessForm(
                    EnRole.Receptionist,
                    EnRole.Owner);

                if (!result.IsSuccess)
                    return result;

                if (AppointmentStatuses == EnAppointmentStatuses.Completed)
                {
                    return new OperationResult(
                        "Only the assigned doctor can complete an appointment.",
                        "Access Denied",
                        false);
                }

                return result;
            }

            if (currentStaff.Role != EnRole.Doctor)
            {
                return new OperationResult(
                    "You do not have permission to modify appointments.",
                    "Access Denied",
                    false);
            }

            AppointmentDto existingAppointment =
                await AppointmentData.FindAppointmentByID(AppointmentID, Global.CurrentLoggedInStaff.StaffId);

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

            if (existingAppointment.AppointmentStatuses == EnAppointmentStatuses.Completed ||
                existingAppointment.AppointmentStatuses == EnAppointmentStatuses.Cancelled ||
                existingAppointment.AppointmentStatuses == EnAppointmentStatuses.NoShow)
            {
                return new OperationResult(
                    "This appointment can no longer be completed.",
                    "Not Allowed",
                    false);
            }

            // Doctors can only change the appointment status to Completed.
            // They cannot change the patient, assigned staff, type, date, or time.

            if (AppointmentStatuses != EnAppointmentStatuses.Completed ||
                PatientID != existingAppointment.PatientID ||
                StaffID != existingAppointment.StaffID ||
                AppointmentTypes != existingAppointment.AppointmentTypes ||
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
        public async Task<OperationResult> Save(Payment paymentInfo = null, Policy policy = null)
        {
            OperationResult result = await CheckSaveAccess();

            if (!result.IsSuccess)
                return result;

            Staff currentStaff = Global.CurrentLoggedInStaff;

            // Completing an appointment does not require checking
            // schedule availability or staff activation again.

            if (_mode == EnMode.Update &&
                currentStaff.Role == EnRole.Doctor)
            {
                result = IsAppointmentValidToComplete();

                if (AppointmentStatuses == EnAppointmentStatuses.Completed)
                {
                    if (policy == null)
                        return new OperationResult("No policy was selected", "No Policy", false);

                    if (paymentInfo == null)
                        return new OperationResult("No payment was added", "Not allowed", false);
                }
            }
            else
            {
                result = IsAppointmentValidToSave();
            }

            if (!result.IsSuccess)
                return result;

            result = EnsureNoBackToBackBooking();

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
                            $"Appointment type '{AppointmentTypes}' for PatientID {PatientID} with StaffID {StaffID} was scheduled on {AppointmentDate:d} from {AppointmentTime:hh\\:mm} to {AppointmentEndTime:hh\\:mm} by {performedBy}.",
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
                            $"Appointment type '{AppointmentTypes}' for PatientID {PatientID} with StaffID {StaffID} was updated to {AppointmentDate:d} from {AppointmentTime:hh\\:mm} to {AppointmentEndTime:hh\\:mm} (Status: {AppointmentStatuses}) by {performedBy}.",
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
        public static async Task<Appointment> FindAppointmentByID(int appointmentId)
        {

            AppointmentDto appointmentDto = null;

            if (Global.CurrentLoggedInStaff.Role == EnRole.Receptionist || Global.CurrentLoggedInStaff.Role == EnRole.Owner)
            {
                appointmentDto = await AppointmentData.FindAppointmentByID(appointmentId);

                if (appointmentDto == null)
                    return null;
            }
            else
            {
                appointmentDto = await AppointmentData.FindAppointmentByID(appointmentId, Global.CurrentLoggedInStaff.StaffId);

                if (appointmentDto == null)
                    return null;

                // A doctor can only retrieve their own appointments.
                if (Global.CurrentLoggedInStaff.Role == EnRole.Doctor &&
                    appointmentDto.StaffID != Global.CurrentLoggedInStaff.StaffId)
                    return null;
            }

            return await FromDto(appointmentDto);
        }
        private bool CanAccessCurrentAppointment()
        {
            if (Global.CurrentLoggedInStaff == null)
                return false;

            switch (Global.CurrentLoggedInStaff.Role)
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
        public async Task<OperationResult> CancelAppointment(int appointmentId)
        {
            if (!CanAccessCurrentAppointment())
                return new OperationResult("Can't access this appointment", "Not Allowed", false);

            if (!IsStillActive())
                return new OperationResult("Appointment is not longer active", "Not Active", false);

            OperationResult result = await CheckSaveAccess();

            if (!result.IsSuccess)
                return result;

            if (AppointmentStatuses == EnAppointmentStatuses.InRoom)
                return new OperationResult("Patient is currently in room getting treated", "Not Allowed", false);

            if (AppointmentData.CancelAppointment(appointmentId))
            {
                Auditor.Log(
                           "Appointment cancelled",
                           $"Appointment type '{AppointmentTypes}' for PatientID {PatientID} with StaffID {StaffID} was cancelled on {DateTime.Now:d} performed by staff {Staff.StaffId} => {Staff.GetFullName()}.",
                           $"AppointmentID {AppointmentID}");

                return new OperationResult("appointment got cancelled succssfully", "Cancelled", true);
            }

            return new OperationResult("failed to cancel appointment", "Failed", false);
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
            AppointmentStatuses != EnAppointmentStatuses.Completed &&
            AppointmentStatuses != EnAppointmentStatuses.Cancelled &&
            AppointmentStatuses != EnAppointmentStatuses.NoShow;
        public bool DoesHasAppointmentReminders(int appointmentId) => AppointmentData.DoesHasAppointmentReminders(appointmentId);
        private OperationResult EnsureNoBackToBackBooking()
        {
            if (AppointmentStatuses == EnAppointmentStatuses.InRoom)
                return new OperationResult("Patient is attending in room treatment try again later.", "Not Allowed", false);

            if ((!AppointmentTypes.Equals(EnAppointmentTypes.ChronicCare))
                && AppointmentData.DoesPatientHasActiveAppointment(PatientID, AppointmentDate))
                return new OperationResult("Appointment type is not urgent and patient has an active appointment.", "Not Allowed", false);

            if (AppointmentTypes.Equals(EnAppointmentTypes.ChronicCare))
            {
                OperationResult result = PatientAlreadyHas2ActiveChronicAppointment(PatientID);

                if (!result.IsSuccess)
                    return result;
            }

            Auditor.Log("Added Urgent/Immediate Appointment", $"Receptionist {StaffID} => {Staff.GetFullName()} added an urgent appointment on" +
                $"{DateTime.Now}", "Appointment.EnsureNoBackToBackBooking");

            return new OperationResult("Urgent appointment added successfully", "Added", true);
        }
        private OperationResult PatientAlreadyHas2ActiveChronicAppointment(int patientId)
        {
            if (AppointmentData.PatientAlreadyHas2ActiveChronicAppointment(patientId))
                return new OperationResult("patient already has 2 chronic active appointment", "Not Allowed", false);

            return new OperationResult("patient is ok to book appointment", "Allowed", true);
        }
        public bool IsAppointmentOutDated()
        {
            int appointmentLength = (int)(AppointmentEndTime - AppointmentTime).TotalMinutes;
            return DateTime.Now > (AppointmentDate.AddMinutes(appointmentLength));
        }
    }
}
