using System;

namespace Data_Access.DTOs
{
    public class ScheduleDto
    {
        public int ScheduleId { get; set; }
        public TimeSpan WorkStartFrom { get; set; }
        public TimeSpan WorkEndAt { get; set; }
        public string ShiftName { get; set; }

    }
}
