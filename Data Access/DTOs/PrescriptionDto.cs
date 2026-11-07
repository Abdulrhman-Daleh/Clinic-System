using System;

namespace Data_Access.DTOs
{
    public class PrescriptionDto
    {
        public int PrescriptionID { get; set; }
        public int AppointmentID { get; set; }
        public decimal TotalPrice { get; set; }
        public bool DoctorDidSign { get; set; }
        public DateTime WriteDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
