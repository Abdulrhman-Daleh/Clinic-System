using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;
using System.Threading.Tasks;

namespace Business
{
    public class Referral
    {
        private EnMode _mode;
        public int ReferralID { get; set; }
        public int DoctorID { get; set; }
        public Staff Doctor { get; set; }
        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; }
        public int NoteID { get; set; }
        public SOAPNote Note { get; set; }
        public string ReferralReason { get; set; }
        public DateTime ReferralDate { get; set; }
        public EnReferralUrgencies ReferralUrgency { get; set; }
        public string Speciality { get; set; }
        public Referral()
        {
            ReferralID = -1;
            DoctorID = -1;
            AppointmentID = -1;
            NoteID = -1;
            ReferralReason = string.Empty;
            ReferralDate = DateTime.Now;
            ReferralUrgency = 0;
            Speciality = string.Empty;

            _mode = EnMode.Add;
        }
        public Referral(int referralID)
        {
            ReferralID = referralID;
            _mode = EnMode.Update;
        }
        public static async Task<Referral> FromDto(ReferralDto referralDto)
        {
            if (referralDto == null)
                return null;

            var referral = new Referral(referralDto.ReferralID)
            {
                ReferralID = referralDto.ReferralID,
                DoctorID = referralDto.DoctorID,
                AppointmentID = referralDto.AppointmentID,
                NoteID = referralDto.NoteID,
                ReferralReason = referralDto.ReferralReason,
                ReferralDate = referralDto.ReferralDate,
                ReferralUrgency = referralDto.ReferralUrgency,
                Speciality = referralDto.Speciality
            };

            referral.Doctor = await Staff.FindStaffById(referral.DoctorID);
            referral.Appointment = await Appointment.FindAppointmentByID(referral.AppointmentID);
            referral.Note = await SOAPNote.FindSOAPNoteByID(referral.NoteID);

            return referral;
        }
        public static ReferralDto ToDto(Referral referral)
        {
            if (referral == null)
                return null;

            return new ReferralDto()
            {
                ReferralID = referral.ReferralID,
                DoctorID = referral.DoctorID,
                AppointmentID = referral.AppointmentID,
                NoteID = referral.NoteID,
                ReferralReason = referral.ReferralReason,
                ReferralDate = referral.ReferralDate,
                ReferralUrgency = referral.ReferralUrgency,
                Speciality = referral.Speciality
            };
        }
        private bool AddNewReferral()
        {
            ReferralID = ReferralData.AddNewReferral(ToDto(this));

            return ReferralID > 0;
        }
        private bool UpdateReferral()
        {
            return ReferralData.UpdateReferral(ToDto(this));
        }
        private OperationResult CheckAddAccess()
        {
            OperationResult result = Util.CanAccessForm(EnRole.Doctor);

            if (!result.IsSuccess)
                return result;

            if (DoctorID != Global.CurrentLoggedInStaff.StaffId)
            {
                return new OperationResult(
                    "You can only create referrals under your own account.",
                    "Access Denied",
                    false);
            }


            if (Appointment == null ||
                Appointment.Staff == null ||
                Appointment.Staff.StaffId != Global.CurrentLoggedInStaff.StaffId)
            {
                return new OperationResult(
                    "You are not authorized to create referrals for this appointment.",
                    "Access Denied",
                    false);
            }


            if (Doctor == null ||
                Doctor.Role != EnRole.Doctor ||
                !Doctor.IsActive)
            {
                return new OperationResult(
                    "The selected doctor is invalid or inactive.",
                    "Invalid Doctor",
                    false);
            }


            if (Note == null)
            {
                return new OperationResult(
                    "The selected medical note does not exist.",
                    "Invalid Medical Note",
                    false);
            }

            return new OperationResult(
                "Referral creation access granted.",
                "Access Granted",
                true);
        }
        private async Task<OperationResult> CheckUpdateAccess()
        {
            OperationResult result = Util.CanAccessForm(EnRole.Doctor);

            if (!result.IsSuccess)
                return result;

            if (this == null)
            {
                return new OperationResult(
                    "Referral does not exist.",
                    "Not Found",
                    false);
            }

            if (this.DoctorID != Global.CurrentLoggedInStaff.StaffId)
            {
                return new OperationResult(
                    "You are not authorized to update this referral.",
                    "Access Denied",
                    false);
            }

            if (DoctorID != DoctorID ||
                AppointmentID != AppointmentID ||
                NoteID != NoteID)
            {
                return new OperationResult(
                    "You cannot change the doctor, appointment, or medical note associated with this referral.",
                    "Access Denied",
                    false);
            }

            // Preserve the original referral information.
            DoctorID = DoctorID;
            AppointmentID = AppointmentID;
            NoteID = NoteID;
            ReferralDate = ReferralDate;

            Doctor = await Staff.FindStaffById(DoctorID);
            Appointment = await Appointment.FindAppointmentByID(AppointmentID);
            Note = await SOAPNote.FindSOAPNoteByID(NoteID);

            return new OperationResult(
                "Referral update access granted.",
                "Access Granted",
                true);
        }
        public async Task<OperationResult> Save()
        {
            OperationResult result;

            if (_mode == EnMode.Add)
                result = CheckAddAccess();
            else
                result = await CheckUpdateAccess();

            if (!result.IsSuccess)
                return result;

            result = IsReferralValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy =
                $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case EnMode.Add:

                    if (AddNewReferral())
                    {
                        _mode = EnMode.Update;

                        Auditor.Log(
                            "Referral Added",
                            $"Referral for AppointmentID {AppointmentID} (Speciality: {Speciality}, Urgency: {ReferralUrgency}) was created by {performedBy}.",
                            $"ReferralID {ReferralID}");

                        return new OperationResult(
                            "Referral added successfully.",
                            "Added",
                            true);
                    }

                    return new OperationResult(
                        "Failed to add referral.",
                        "Failed",
                        false);

                case EnMode.Update:

                    if (UpdateReferral())
                    {
                        Auditor.Log(
                            "Referral Updated",
                            $"Referral for AppointmentID {AppointmentID} (Speciality: {Speciality}, Urgency: {ReferralUrgency}) was updated by {performedBy}.",
                            $"ReferralID {ReferralID}");

                        return new OperationResult(
                            "Referral updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update referral.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid referral operation mode.",
                "Failed",
                false);
        }
        public static async Task<Referral> FindReferralByAppointmentID(int appointmentID)
        {
            if (!Util.CanAccessForm(EnRole.Doctor).IsSuccess)
                return null;

            var referral = await ReferralData.FindReferralByAppointmentID(appointmentID, Global.CurrentLoggedInStaff.StaffId);

            return await FromDto(referral);
        }
        private OperationResult IsReferralValidToSave()
        {
            if (Util.IsValidId(DoctorID) &&
                Util.IsValidId(AppointmentID) &&
                Util.IsValidId(NoteID) &&
                Enum.IsDefined(typeof(EnReferralUrgencies), ReferralUrgency) &&
                Util.IsRequired(ReferralReason) &&
                Util.IsRequired(Speciality) &&
                ReferralDate != DateTime.MinValue)
            {
                if (!Appointment.IsStillActive())
                    return new OperationResult("Appointment is no longer active", "Not Active", false);

                return new OperationResult(
                    "Referral information is valid to save.",
                    "Valid",
                    true);
            }

            return new OperationResult(
                "Referral information is invalid.",
                "Invalid",
                false);
        }
    }
}