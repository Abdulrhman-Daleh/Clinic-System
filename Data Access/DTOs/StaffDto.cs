using System;

namespace Data_Access.DTOs
{
    public class StaffDto
    {
        public int StaffId { get; set; }
        public int ScheduleId { get; set; }
        public int RoleId { get; set; }
        public int PersonId { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string StaffNumber { get; set; }
        public DateTime HireDate { get; set; }
    }
}
