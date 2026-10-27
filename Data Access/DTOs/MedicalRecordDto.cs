using System;

namespace Data_Access.DTOs
{
    public class MedicalRecordDto
    {
        public int MedicalRecordID { get; set; }
        public int PatientID { get; set; }
        public string MedicalNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CurrentMedication { get; set; }
        public DateTime CurrentMedicationDuration { get; set; }
        public string PastSocialHistory { get; set; }
        public string PastSurgeries { get; set; }
        public bool ConcentToCare { get; set; }
        public string SocialStatus { get; set; }
        public string Classification { get; set; }
        public string Allergies { get; set; }
    }
}
