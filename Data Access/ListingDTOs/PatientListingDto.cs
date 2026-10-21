using System;

namespace Data_Access.ListingDTOs
{
    public class PatientListingDto
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public char Gender { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Status { get; set; }
        public int Visits { get; set; }
        public string InsuranceName { get; set; }
    }
}
