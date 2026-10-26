using Data_Access;
using Data_Access.DTOs;
using System;

namespace Business
{
    public class Auditor
    {
        public int TrailID { get; set; }
        public int StaffID { get; set; }
        public string TrailTitle { get; set; }
        public DateTime OperationDate { get; set; }
        public string MessageDescription { get; set; }
        public string WorkedOnObjectName { get; set; }
        public static bool Log(string trailTitle, string messageDescription, string workedOnObjectName)
        {
            return AuditorData.AddAuditTrail(new AuditTrailDto()
            {
                StaffID = Global.CurrentLoggedInStaff.StaffId,
                TrailTitle = trailTitle,
                MessageDescription = messageDescription,
                WorkedOnObjectName = workedOnObjectName
            });
        }
    }
}
