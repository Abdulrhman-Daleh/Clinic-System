using System;

namespace Data_Access.ListingDTOs
{
    public class LabTestQueueListingDto
    {
        public int LabID { get; set; }
        public string TestName { get; set; }
        public string PatientName { get; set; }
        public DateTime RequestDate { get; set; }
        public string TestStatusText { get; set; }
    }
}