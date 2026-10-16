using Data_Access.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data_Access
{
    public class StaffData
    {
        private static string MapStaffSchedule(SqlDataReader reader)
        {
            return (string)reader["ShiftName"];
        }

        private static string MapStaffRole(SqlDataReader reader)
        {
            return (string)reader["RoleName"];
        }

        public static Tuple<int, int, string> AddNewStaff(StaffDto staffInfo, PersonDto personInfo)
        {
            Tuple<int, int, string> result = new Tuple<int, int, string>(0, 0, "");

            string query = "AddStaff";

            try
            {
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter(@"Firstname", SqlDbType.VarChar, 40) {Value = personInfo.Firstname},
                    new SqlParameter(@"Lastname", SqlDbType.VarChar, 40) {Value = personInfo.Lastname},
                    new SqlParameter(@"Gender", SqlDbType.VarChar, 1) {Value = personInfo.Gender},
                    new SqlParameter(@"ScheduleId", SqlDbType.Int) {Value = staffInfo.ScheduleId},
                    new SqlParameter(@"RoleId", SqlDbType.Int) {Value = staffInfo.RoleId},
                    new SqlParameter(@"Username", SqlDbType.VarChar, 40) {Value = staffInfo.Username},
                    new SqlParameter(@"HashedPassword", SqlDbType.VarChar, 200) {Value = staffInfo.HashedPassword}
                };

                List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter("@PersonId", SqlDbType.Int) {Direction = ParameterDirection.Output},
                    new SqlParameter("@StaffId", SqlDbType.Int) {Direction = ParameterDirection.Output},
                    new SqlParameter("@StaffNumber", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                };

                List<object> objs = DataAccessHelper.ExecuteNonQueryStoredProcedure<object>(query, inputParams, outputParams);

                if (objs != null)
                {
                    return new Tuple<int, int, string>((int)objs[0], (int)objs[1], (string)objs[2]);
                }
            }
            catch (Exception ex)
            {

            }

            return new Tuple<int, int, string>(-1, -1, "");
        }

        public static bool UpdateStaff(StaffDto staffInfo, PersonDto personInfo)
        {
            int affectedRows = -1;

            try
            {
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter(@"Firstname", SqlDbType.VarChar, 40) {Value = personInfo.Firstname},
                    new SqlParameter(@"Lastname", SqlDbType.VarChar, 40) {Value = personInfo.Lastname},
                    new SqlParameter(@"Gender", SqlDbType.VarChar, 1) {Value = personInfo.Gender},
                    new SqlParameter(@"ScheduleId", SqlDbType.Int) {Value = staffInfo.ScheduleId},
                    new SqlParameter(@"RoleId", SqlDbType.Int) {Value = staffInfo.RoleId},
                    new SqlParameter(@"Username", SqlDbType.VarChar, 40) {Value = staffInfo.Username},
                    new SqlParameter(@"HashedPassword", SqlDbType.VarChar, 200) {Value = staffInfo.HashedPassword}
                };

                affectedRows = DataAccessHelper.ExecuteNonQueryStoredProcedure<int>("UpdateStaff", inputParams);
            }
            catch (Exception ex)
            {

            }

            return affectedRows > 0;
        }

        public static async Task<BindingList<string>> GetSchedulesNames()
        {
            BindingList<string> schedules = new BindingList<string>();

            string query = @"select ShiftName from Schedules
                order by ScheduleID asc";

            try
            {
                schedules = await DataAccessHelper.ExecuteObjectList(query, null, MapStaffSchedule);
            }
            catch (Exception ex)
            {
                throw;
            }

            return schedules;
        }

        public static async Task<BindingList<string>> GetRolesNames()
        {
            BindingList<string> roles = new BindingList<string>();

            string query = @"select RoleName from StaffRoles
            order by RoleID asc";

            try
            {
                roles = await DataAccessHelper.ExecuteObjectList(query, null, MapStaffRole);
            }
            catch (Exception ex)
            {
                throw;
            }

            return roles;
        }

        public static bool DoesExistsByUsername(string username)
        {
            bool result = false;

            string query = @"select Top 1 1 from Staff where Username = @Username";

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Username", SqlDbType.VarChar, 40) {Value  = username}
                };

                object obj = DataAccessHelper.ExecuteScalar(query, parameters);

                if (obj != null)
                    result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
