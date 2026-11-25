
using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;
using System.Threading.Tasks;

namespace Business
{
    public class Patient : Person
    {
        public int PatientId { get; set; }
        public bool? IsFrozen { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? EndOfBookingFreeze { get; set; }
        public bool? HasUnPaidFees { get; set; }
        public static PatientDto ToDto(Patient patient)
        {
            return new PatientDto()
            {
                PatientId = patient.PatientId,
                PersonId = patient.PersonId,
                IsFrozen = patient.IsFrozen,
                RegisterDate = patient.RegisterDate,
                EndOfBookingFreeze = patient.EndOfBookingFreeze,
                HasUnPaidFees = patient.HasUnPaidFees,
                Firstname = patient.Firstname,
                Lastname = patient.Lastname,
                Gender = patient.Gender == EnGender.Male ? 'M' : 'F',
            };
        }
        private static Patient FromDto(PatientDto patientDto)
        {
            if (patientDto is null)
                return null;

            return new Patient()
            {
                PatientId = patientDto.PatientId,
                PersonId = patientDto.PersonId,
                IsFrozen = patientDto.IsFrozen,
                RegisterDate = patientDto.RegisterDate,
                EndOfBookingFreeze = patientDto.EndOfBookingFreeze,
                HasUnPaidFees = patientDto.HasUnPaidFees,
                Firstname = patientDto.Firstname,
                Lastname = patientDto.Lastname,
                Gender = patientDto.Gender == 'M' ? EnGender.Male : EnGender.Female
            };
        }
        public Patient()
        {
            PatientId = -1;
            PersonId = -1;
            IsFrozen = null;
            RegisterDate = DateTime.Now;
            EndOfBookingFreeze = null;
            HasUnPaidFees = null;
        }
        public bool AddNewPatient()
        {
            if (!Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess)
                return false;

            PatientId = PatientData.AddNewPatient(ToDto(this));

            if (PatientId > 0)
            {
                Auditor.Log(
                    "Patient Added",
                    $"Patient linked to PersonID {PersonId} was registered by StaffID {Global.CurrentLoggedInStaff.StaffId}.",
                    $"PatientID {PatientId}");

                return true;
            }

            return false;
        }
        public static async Task<Patient> FindPatientById(int patientId)
        {
            return FromDto(await PatientData.FindPatientById(patientId));
        }
        public int FreezeUnlockDays()
        {
            if (EndOfBookingFreeze == null)
                return 0;

            return (EndOfBookingFreeze - DateTime.Now).Value.Days;
        }
        public string ISUnpaidText()
        {
            if (HasUnPaidFees == null)
                return "No Fees";

            return HasUnPaidFees == true ? "Yes" : "No";
        }
        public int GetTotalVisits()
        {
            if (!Util.CanAccessForm(
                EnRole.Receptionist,
                EnRole.Doctor,
                EnRole.Nurse,
                EnRole.Owner).IsSuccess)
                return 0;

            return PatientData.GetTotalVisits(PatientId);
        }
        public static int GetTotalUnPaidPatients()
        {
            return PatientData.GetTotalUnPaidPatients();
        }
        public string FrozenUntilText()
        {
            bool frozen = IsFrozen ?? false;

            if (frozen)
            {
                return $"❄ Freeze until{EndOfBookingFreeze?.ToShortDateString()}";
            }
            return null;
        }
        public string UnpaidFeesText()
        {
            bool UnpaidFees = HasUnPaidFees ?? false;

            if (UnpaidFees)
            {
                return $"⚠ Unpaid";
            }

            return null;
        }
    }
}
