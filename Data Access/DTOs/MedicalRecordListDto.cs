using System;

namespace Data_Access.DTOs
{
    public class MedicalRecordListDto
    {
        public int MedicalRecordID { get; set; }
        public string MedicalNumber { get; set; }
        public string PatientName { get; set; }
        public string Classification { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool ConsentToCare { get; set; }
    }
}
