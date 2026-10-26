using System;

namespace Data_Access.DTOs
{
    public class PatientInsuranceInfoDto
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
        public DateTime PatientSince { get; set; }
    }
}
