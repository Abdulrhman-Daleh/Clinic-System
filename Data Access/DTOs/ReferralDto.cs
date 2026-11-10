using AppEnums.cs;
using System;

namespace Data_Access.DTOs
{
    public class ReferralDto
    {
        public int ReferralID { get; set; }
        public int DoctorID { get; set; }
        public int AppointmentID { get; set; }
        public int NoteID { get; set; }
        public string ReferralReason { get; set; }
        public DateTime ReferralDate { get; set; }
        public (EnReferralUrgencies ReferralUrgencyID, string ReferralUrgencyText) ReferralUrgencies { get; set; }
        public string Speciality { get; set; }
    }
}
