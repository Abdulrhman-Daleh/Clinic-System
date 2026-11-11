using System;

namespace Data_Access.DTOs
{
    public class PenaltyDto
    {
        public int PenaltyID { get; set; }
        public int PenaltyStatusID { get; set; }
        public int? AppointmentID { get; set; }
        public decimal PenaltyAmount { get; set; }
        public DateTime PenaltyWrittenDate { get; set; }
        public int PolicyID { get; set; }
        public DateTime PenaltyDueDate { get; set; }
        public DateTime? PenaltyClosedDate { get; set; }
    }
}
