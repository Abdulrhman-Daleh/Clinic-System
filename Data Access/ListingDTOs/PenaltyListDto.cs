using System;

namespace Data_Access.ListingDTOs
{
    public class PenaltyListDto
    {
        public int PenaltyID { get; set; }
        public string Patient { get; set; }
        public decimal Price { get; set; }
        public DateTime PenaltyDueDate { get; set; }
        public DateTime? PenaltyClosedDate { get; set; }
        public string Status { get; set; }
    }
}
