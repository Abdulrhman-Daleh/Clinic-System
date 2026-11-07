using AppEnums.cs;
using System;

namespace Data_Access.DTOs
{
    public class LabTestDto
    {
        public int LabID { get; set; }
        public int AppointmentID { get; set; }
        public (EnTestStatuses TestStatusID, string TestStatusText) TestStatuses { get; set; }
        public int NurseID { get; set; }
        public string Reason { get; set; }
        public string Instructions { get; set; }
        public string TestResult { get; set; }
        public string TestName { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? PerformDate { get; set; }
        public DateTime? ResultRecordedAt { get; set; }
    }
}
