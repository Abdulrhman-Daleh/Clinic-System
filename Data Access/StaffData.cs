using Data_Access.DTOs;
using Data_Access.ListingDTOs;
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
        private static StaffDto StaffMapper(SqlDataReader reader)
        {
            return new StaffDto()
            {
                StaffId = (int)reader["StaffID"],
                ScheduleId = (int)reader["ScheduleID"],
                RoleId = (int)reader["RoleID"],
                PersonId = (int)reader["PersonID"],
                Username = (string)reader["Username"],
                HashedPassword = (string)reader["HashedPassword"],
                StaffNumber = (string)reader["StaffNumber"],
                HireDate = (DateTime)reader["HireDate"],
                IsActive = (bool)reader["IsActive"],
                Firstname = (string)reader["Firstname"],
                Lastname = (string)reader["Lastname"],
                Gender = Convert.ToChar(reader["Gender"])
            };
        }

        private static StaffListingDto StaffListingMapper(SqlDataReader reader)
        {
            return new StaffListingDto()
            {
                StaffId = (int)reader["StaffID"],
                HireDate = (DateTime)reader["HireDate"],
                StaffNumber = (string)reader["StaffNumber"],
                Status = (string)reader["Status"],
                Username = (string)reader["Username"],
                RoleName = (string)reader["RoleName"],
                FullName = (string)reader["FullName"],
                Gender = (string)reader["Gender"],
                ShiftName = (string)reader["ShiftName"],
            };
        }
        private static string MapStaffRole(SqlDataReader reader)
        {
            return (string)reader["RoleName"];
        }

        public static Tuple<int, int, string> AddNewStaff(StaffDto staffInfo)
        {
            Tuple<int, int, string> result = new Tuple<int, int, string>(0, 0, "");

            string query = "AddStaff";

            try
            {
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter(@"Firstname", SqlDbType.VarChar, 40) {Value = staffInfo.Firstname},
                    new SqlParameter(@"Lastname", SqlDbType.VarChar, 40) {Value = staffInfo.Lastname},
                    new SqlParameter(@"Gender", SqlDbType.VarChar, 1) {Value = staffInfo.Gender},
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

        public static bool UpdateStaff(StaffDto staffInfo)
        {
            int affectedRows = -1;

            try
            {
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter(@"Firstname", SqlDbType.VarChar, 40) {Value = staffInfo.Firstname},
                    new SqlParameter(@"Lastname", SqlDbType.VarChar, 40) {Value = staffInfo.Lastname},
                    new SqlParameter(@"Gender", SqlDbType.VarChar, 1) {Value = staffInfo.Gender},
                    new SqlParameter(@"ScheduleId", SqlDbType.Int) {Value = staffInfo.ScheduleId},
                    new SqlParameter(@"RoleId", SqlDbType.Int) {Value = staffInfo.RoleId},
                    new SqlParameter(@"Username", SqlDbType.VarChar, 40) {Value = staffInfo.Username},
                    new SqlParameter(@"HashedPassword", SqlDbType.VarChar, 200) {Value = staffInfo.HashedPassword},
                    new SqlParameter(@"StaffId", SqlDbType.Int) {Value = staffInfo.StaffId}
                };

                affectedRows = DataAccessHelper.ExecuteNonQueryStoredProcedure<int>("UpdateStaff", inputParams);
            }
            catch (Exception ex)
            {

            }

            return affectedRows > 0;
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

        public static StaffDto FindStaffById(int staffId)
        {
            StaffDto staffDto = new StaffDto();

            string query = @"select 
            	StaffID,
            	ScheduleID, 
            	RoleID,
            	s.PersonID, 
            	Username, 
            	HashedPassword, 
            	StaffNumber,
            	HireDate, 
            	IsActive,
                Firstname,
                Lastname,
                Gender
            from Staff as s inner join People as p on s.PersonID = p.PersonID
            	where StaffID = @staffId";
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@staffId", SqlDbType.Int ) {Value = staffId}
                };

                staffDto = DataAccessHelper.ExecuteOneObject(query, parameters, StaffMapper);
            }
            catch (Exception ex)
            {
                throw;
            }

            return staffDto;
        }

        public static StaffDto FindStaffByPersonId(int personId)
        {
            StaffDto staffDto = new StaffDto();

            string query = @"select 
            	StaffID,
            	ScheduleID, 
            	RoleID,
            	s.PersonID, 
            	Username, 
            	HashedPassword, 
            	StaffNumber,
            	HireDate, 
            	IsActive,
                Firstname,
                Lastname,
                Gender
            from Staff as s inner join People as p on s.PersonID = p.PersonID
            	where s.PersonID = @personId";
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@personId", SqlDbType.Int) {Value = personId}
                };

                staffDto = DataAccessHelper.ExecuteOneObject(query, parameters, StaffMapper);
            }
            catch (Exception ex)
            {
                throw;
            }

            return staffDto;
        }

        public static StaffDto FindStaffByUsername(string userName)
        {
            StaffDto staffDto = new StaffDto();

            string query = @"select 
            	StaffID,
            	ScheduleID, 
            	RoleID,
            	s.PersonID, 
            	Username, 
            	HashedPassword, 
            	StaffNumber,
            	HireDate, 
            	IsActive,
                Firstname,
                Lastname,
                Gender
            from Staff as s inner join People as p on s.PersonID = p.PersonID
            	where s.Username = @userName";
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@userName", SqlDbType.VarChar, 50) {Value = userName}
                };

                staffDto = DataAccessHelper.ExecuteOneObject(query, parameters, StaffMapper);
            }
            catch (Exception ex)
            {
                throw;
            }

            return staffDto;
        }

        public static bool DeactivateStaff(int staffId)
        {
            int affectedRows = -1;

            string query = @"update Staff set IsActive = 0
                    where StaffID = @staffId";
            try
            {
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter(@"StaffId", SqlDbType.Int) {Value = staffId}
                };

                affectedRows = DataAccessHelper.ExecuteNonQuery(query, inputParams);
            }
            catch (Exception ex)
            {

            }

            return affectedRows > 0;
        }

        public static async Task<BindingList<StaffListingDto>> GetStaff()
        {
            BindingList<StaffListingDto> list = new BindingList<StaffListingDto>();

            string query = @"select * from vw_StaffList";

            try
            {
                list = await DataAccessHelper.ExecuteObjectList(query, null, StaffListingMapper);
            }
            catch (Exception ex)
            {
                throw;
            }

            return list;
        }
    }
}
