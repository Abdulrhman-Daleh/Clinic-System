using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;

namespace Business
{
    public class Patient : Person
    {
        public int PatientId { get; set; }
        public new int PersonId { get; set; }
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
            this.PatientId = PatientData.AddNewPatient(ToDto(this));

            return this.PatientId > 0;
        }

        public static Patient FindPatientById(int patientId)
        {
            return FromDto(PatientData.FindPatientById(patientId));
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
                return "No";

            return HasUnPaidFees == true ? "Yes" : "No";
        }

        public int GetTotalVisits()
        {
            return PatientData.GetTotalVisits(PatientId);
        }
    }
}
