using System;

namespace Data_Access.ListingDTOs
{
    public class InsuranceListDto
    {
        public int InsuranceID { get; set; }
        public string PolicyNumber { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsuranceType { get; set; }
        public string HolderName { get; set; }
        public int Members { get; set; }
        public DateTime CoverageStartDate { get; set; }
        public DateTime CoverageEndDate { get; set; }
        public string StatusText { get; set; }
    }
}
