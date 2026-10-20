using System;

namespace Data_Access.ListingDTOs
{
    public class StaffListingDto
    {
        public int StaffId { get; set; }
        public string StaffNumber { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string RoleName { get; set; }
        public string ShiftName { get; set; }
        public string Username { get; set; }
        public DateTime HireDate { get; set; }
        public string Status { get; set; }
    }
}
