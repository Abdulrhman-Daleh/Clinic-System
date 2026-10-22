using System;

namespace Data_Access.DTOs
{
    public class PatientDto : PersonDto
    {
        public int PatientId { get; set; }
        public override int PersonId { get; set; }
        public bool? IsFrozen { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? EndOfBookingFreeze { get; set; }
        public bool? HasUnPaidFees { get; set; }
    }
}
