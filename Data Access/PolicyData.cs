using Data_Access.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data_Access
{
    public class PolicyData
    {
        private static PolicyDto PolicyMapper(SqlDataReader reader)
        {
            return new PolicyDto()
            {
                PolicyID = (int)reader["PolicyID"],
                OwnerID = (int)reader["OwnerID"],
                PolicyStatusID = (int)reader["PolicyStatusID"],
                ActiveFrom = (DateTime)reader["ActiveFrom"],
                ActiveTo = (DateTime)reader["ActiveTo"],
                NoShowFreezeDays = reader["NoShowFreezeDays"] == DBNull.Value ? (byte?)null : (byte)reader["NoShowFreezeDays"],
                LateFeesPerDay = reader["LateFeesPerDay"] == DBNull.Value ? (decimal?)null : (decimal)reader["LateFeesPerDay"],
                NoShowFee = reader["NoShowFee"] == DBNull.Value ? (decimal?)null : (decimal)reader["NoShowFee"]
            };
        }

        private static string StatusMapper(SqlDataReader reader)
        {
            return (string)reader["PolicyStatusText"];
        }

        public static int AddNewPolicy(PolicyDto policy)
        {
            int policyId = -1;

            string query = @"
                INSERT INTO Policies
                (
                    OwnerID,
                    PolicyStatusID,
                    ActiveFrom,
                    ActiveTo,
                    NoShowFreezeDays,
                    LateFeesPerDay,
                    NoShowFee
                )
                VALUES
                (
                    @OwnerID,
                    @PolicyStatusID,
                    @ActiveFrom,
                    @ActiveTo,
                    @NoShowFreezeDays,
                    @LateFeesPerDay,
                    @NoShowFee
                );

                SELECT SCOPE_IDENTITY();";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@OwnerID", policy.OwnerID),
                new SqlParameter("@PolicyStatusID", policy.PolicyStatusID),
                new SqlParameter("@ActiveFrom", policy.ActiveFrom),
                new SqlParameter("@ActiveTo", policy.ActiveTo),
                new SqlParameter("@NoShowFreezeDays", (object)policy.NoShowFreezeDays ?? DBNull.Value),
                new SqlParameter("@LateFeesPerDay", (object)policy.LateFeesPerDay ?? DBNull.Value),
                new SqlParameter("@NoShowFee", (object)policy.NoShowFee ?? DBNull.Value)
            };

            object obj = DataAccessHelper.ExecuteScalar(query, parameters);

            if (obj != null && obj != DBNull.Value &&
                int.TryParse(obj.ToString(), out int id))
            {
                policyId = id;
            }

            return policyId;
        }

        public static bool UpdatePolicy(PolicyDto policy)
        {
            string query = @"
                UPDATE Policies
                SET
                    OwnerID = @OwnerID,
                    PolicyStatusID = @PolicyStatusID,
                    ActiveFrom = @ActiveFrom,
                    ActiveTo = @ActiveTo,
                    NoShowFreezeDays = @NoShowFreezeDays,
                    LateFeesPerDay = @LateFeesPerDay,
                    NoShowFee = @NoShowFee
                WHERE PolicyID = @PolicyID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@PolicyID", policy.PolicyID),
                new SqlParameter("@OwnerID", policy.OwnerID),
                new SqlParameter("@PolicyStatusID", policy.PolicyStatusID),
                new SqlParameter("@ActiveFrom", policy.ActiveFrom),
                new SqlParameter("@ActiveTo", policy.ActiveTo),
                new SqlParameter("@NoShowFreezeDays", (object)policy.NoShowFreezeDays ?? DBNull.Value),
                new SqlParameter("@LateFeesPerDay", (object)policy.LateFeesPerDay ?? DBNull.Value),
                new SqlParameter("@NoShowFee", (object)policy.NoShowFee ?? DBNull.Value)
            };

            return DataAccessHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public static PolicyDto FindPolicyByID(int policyID)
        {
            string query = @"
                SELECT *
                FROM Policies
                WHERE PolicyID = @PolicyID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@PolicyID", policyID)
            };

            return DataAccessHelper.ExecuteOneObject(
                query, parameters, PolicyMapper);
        }

        public static PolicyDto FindPolicyByOwnerID(int ownerID)
        {
            string query = @"
                SELECT *
                FROM Policies
                WHERE OwnerID = @OwnerID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@OwnerID", ownerID)
            };

            return DataAccessHelper.ExecuteOneObject(
                query, parameters, PolicyMapper);
        }

        public static async Task<BindingList<string>> GetPolicyStatusesNames()
        {
            string query = @"
                SELECT PolicyStatusText
                FROM PolicyStatuses
                ORDER BY PolicyStatusID";

            BindingList<string> policyStatuses = await DataAccessHelper.ExecuteObjectList(query, null, StatusMapper);

            return policyStatuses;
        }
    }
}