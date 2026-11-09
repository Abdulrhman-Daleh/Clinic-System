
using System;

namespace Data_Access.ListingDTOs
{
    public class LabTestListingDto
    {
        public int LabID { get; set; }
        public string TestName { get; set; }
        public string NurseName { get; set; }
        public string TestStatusText { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? PerformDate { get; set; }
        public DateTime? ResultRecordedAt { get; set; }
        public string TestResult { get; set; }
    }
}
