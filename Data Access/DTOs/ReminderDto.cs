using System;

namespace Data_Access.DTOs
{
    public class ReminderDto
    {
        public int ReminderID { get; set; }
        public int StaffID { get; set; }
        public int AppointmentID { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
