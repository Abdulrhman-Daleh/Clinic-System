using AppEnums.cs;
using System;

namespace Data_Access.DTOs
{
    public class InsuranceDto
    {
        public int InsuranceID { get; set; }
        public int PolicyId { get; set; }
        public int RecordedBy { get; set; }
        public (EnInsuranceTypes InsuranceTypeId, string InsuranceTypeName) InsuranceType { get; set; }
        public (EnInsuranceStatuses InsuranceStatusId, string InsuranceStatusName) InsuranceStatus { get; set; }
        public DateTime UploadedDate { get; set; }
        public string InsuranceProvider { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime CoverageStartDate { get; set; }
        public DateTime CoverageEndDate { get; set; }
        public string PatientClaims { get; set; }
        public int PolicyholderPatientId { get; set; }
        public string Relationship { get; set; }
    }
}
