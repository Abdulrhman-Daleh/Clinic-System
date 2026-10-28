using AppEnums.cs;
using System;

namespace Data_Access.DTOs
{
    public class AppointmentDto
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int StaffID { get; set; }
        public (EnAppointmentStatuses AppointmentStatusID, string AppointmentStatusText) AppointmentStatuses { get; set; }
        public (EnAppointmentTypes AppointmentTypeID, string AppointmentTypeText) AppointmentTypes { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public TimeSpan AppointmentEndTime { get; set; }
    }
}


