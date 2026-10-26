using System;

namespace Data_Access.DTOs
{
    public class AuditTrailDto
    {
        public int TrailID { get; set; }
        public int StaffID { get; set; }
        public string TrailTitle { get; set; }
        public DateTime OperationDate { get; set; }
        public string MessageDescription { get; set; }
        public string WorkedOnObjectName { get; set; }
    }
}
