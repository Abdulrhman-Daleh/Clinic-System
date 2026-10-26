using Data_Access.DTOs;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data_Access
{
    public class AuditorData
    {
        public static bool AddAuditTrail(AuditTrailDto auditDto)
        {
            string query = @"insert into AuditTrails (StaffID, TrailTitle, OperationDate, MessageDescrption, WorkedOnObjectName)
            values (@StaffId, @TrailTitle, GETDATE(), @MessageDescription, @WorkedOnObjectName)";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@StaffId", SqlDbType.Int) {Value = auditDto.StaffID },
                new SqlParameter("@TrailTitle", SqlDbType.VarChar, 50) {Value = auditDto.TrailTitle },
                new SqlParameter("@MessageDescription", SqlDbType.VarChar, 2000) {Value = auditDto.MessageDescription },
                new SqlParameter("@WorkedOnObjectName", SqlDbType.VarChar, 50) {Value = auditDto.WorkedOnObjectName },
            };

            return DataAccessHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
