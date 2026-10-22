using System;

namespace Data_Access.DTOs
{
    public class InsuranceDto
    {
        public int InsurenceId { get; set; }
        public int PolicyId { get; set; }
        public int RecordedBy { get; set; }
        public int InsuranceTypeId { get; set; }
        public int InsuranceStatusId { get; set; }
        public DateTime UploadedDate { get; set; }
        public string InsuranceProvider { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime CoverageStartDate { get; set; }
        public DateTime CoverageEndDate { get; set; }
        public string PatientClaims { get; set; }
        public int PolicyholderPatientId { get; set; }
    }
}
