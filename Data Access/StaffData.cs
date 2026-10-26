using AppEnums.cs;
using Data_Access.DTOs;
using Data_Access.ListingDTOs;
using Log;
using LoggerMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Data_Access
{
    public class StaffData
    {
        private static StaffDto StaffMapper(SqlDataReader reader)
        {
            try
            {
                return new StaffDto()
                {
                    StaffId = (int)reader["StaffID"],
                    ScheduleId = (int)reader["ScheduleID"],
                    RoleInfo = ((EnRole)reader["RoleID"], (string)reader["RoleName"]),
                    PersonId = (int)reader["PersonID"],
                    Username = (string)reader["Username"],
                    HashedPassword = (string)reader["HashedPassword"],
                    StaffNumber = (string)reader["StaffNumber"],
                    HireDate = (DateTime)reader["HireDate"],
                    IsActive = (bool)reader["IsActive"],
                    Firstname = (string)reader["Firstname"],
                    Lastname = (string)reader["Lastname"],
                    Gender = Convert.ToChar(reader["Gender"]),
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(ex.Message, "StaffData", "Application", EventLogEntryType.Error));
                return null;
            }
        }

        private static StaffListingDto StaffListingMapper(SqlDataReader reader)
        {
            try
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
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(ex.Message, "StaffData", "Application", EventLogEntryType.Error));
                return null;
            }
        }

        private static string MapStaffRole(SqlDataReader reader)
        {
            return (string)reader["RoleName"];
        }

        public static (int staffId, int personId, string staffNumber) AddNewStaff(StaffDto staffInfo)
        {
            (int staffId, int personId, string staffNumber) result = (0, 0, "");

            string query = "AddStaff";

            List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter("@Firstname", SqlDbType.VarChar, 40) {Value = staffInfo.Firstname},
                    new SqlParameter("@Lastname", SqlDbType.VarChar, 40) {Value = staffInfo.Lastname},
                    new SqlParameter("@Gender", SqlDbType.VarChar, 1) {Value = staffInfo.Gender},
                    new SqlParameter("@ScheduleId", SqlDbType.Int) {Value = staffInfo.ScheduleId},
                    new SqlParameter("@RoleId", SqlDbType.Int) {Value = staffInfo.RoleInfo.RoleId},
                    new SqlParameter("@Username", SqlDbType.VarChar, 40) {Value = staffInfo.Username},
                    new SqlParameter("@HashedPassword", SqlDbType.VarChar, 200) {Value = staffInfo.HashedPassword}
                };

            List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter("@StaffId", SqlDbType.Int) {Direction = ParameterDirection.Output},
                    new SqlParameter("@PersonId", SqlDbType.Int) {Direction = ParameterDirection.Output},
                    new SqlParameter("@StaffNumber", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                };

            bool r = DataAccessHelper.ExecuteNonQueryStoredProcedure<object>(query, inputParams, outputParams);

            if (r == true)
            {
                result.staffId = Convert.ToInt32(outputParams[0]);
                result.personId = Convert.ToInt32(outputParams[1]);
                result.staffNumber = Convert.ToString(outputParams[2]);
            }

            return result;
        }

        public static bool UpdateStaff(StaffDto staffInfo)
        {
            int affectedRows;

            List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter("@Firstname", SqlDbType.VarChar, 40) {Value = staffInfo.Firstname},
                    new SqlParameter("@Lastname", SqlDbType.VarChar, 40) {Value = staffInfo.Lastname},
                    new SqlParameter("@Gender", SqlDbType.VarChar, 1) {Value = staffInfo.Gender},
                    new SqlParameter("@ScheduleId", SqlDbType.Int) {Value = staffInfo.ScheduleId},
                    new SqlParameter("@RoleId", SqlDbType.Int) {Value = staffInfo.RoleInfo.RoleId},
                    new SqlParameter("@Username", SqlDbType.VarChar, 40) {Value = staffInfo.Username},
                    new SqlParameter("@HashedPassword", SqlDbType.VarChar, 200) {Value = staffInfo.HashedPassword},
                    new SqlParameter("@StaffId", SqlDbType.Int) {Value = staffInfo.StaffId}
                };

            affectedRows = DataAccessHelper.ExecuteNonQueryStoredProcedure<int>("UpdateStaff", inputParams);

            return affectedRows > 0;
        }

        public static async Task<BindingList<string>> GetRolesNames()
        {
            BindingList<string> roles = new BindingList<string>();

            string query = @"select RoleName from StaffRoles
            order by RoleID asc";

            roles = await DataAccessHelper.ExecuteObjectList(query, null, MapStaffRole);

            return roles;
        }

        public static bool DoesExistsByUsername(string username)
        {
            bool result = false;

            string query = @"select Top 1 1 from Staff where Username = @Username";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Username", SqlDbType.VarChar, 40) {Value  = username}
                };

            object obj = DataAccessHelper.ExecuteScalar(query, parameters);

            if (obj != null)
                result = true;

            return result;
        }

        public static StaffDto FindStaffById(int staffId)
        {
            StaffDto staffDto = new StaffDto();

            string query = @"select 
            	StaffID,
            	ScheduleID, 
            	r.RoleID,
                r.RoleName,
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
                inner join StaffRoles as r on r.RoleID = s.RoleID
            	where StaffID = @staffId";
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@staffId", SqlDbType.Int ) {Value = staffId}
                };

            staffDto = DataAccessHelper.ExecuteOneObject(query, parameters, StaffMapper);

            return staffDto;
        }

        public static StaffDto FindStaffByPersonId(int personId)
        {
            StaffDto staffDto = new StaffDto();

            string query = @"select 
            	StaffID,
            	ScheduleID, 
            	r.RoleID,
                r.RoleName,
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
                inner join StaffRoles as r on r.RoleID = s.RoleID
            	where s.PersonID = @personId";
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@personId", SqlDbType.Int) {Value = personId}
                };

            staffDto = DataAccessHelper.ExecuteOneObject(query, parameters, StaffMapper);

            return staffDto;
        }

        public static StaffDto FindStaffByUsername(string userName)
        {
            StaffDto staffDto = new StaffDto();

            string query = @"select 
            	StaffID,
            	ScheduleID, 
            	r.RoleID,
                r.RoleName,
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
                inner join StaffRoles as r on r.RoleID = s.RoleID
            	where s.Username = @userName";
            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@userName", SqlDbType.VarChar, 50) {Value = userName}
                };

            staffDto = DataAccessHelper.ExecuteOneObject(query, parameters, StaffMapper);

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
                Logger.LogMessage(new LogMessageInformation(ex.Message, "ClinicSystem", "Application", EventLogEntryType.Error));
            }

            return affectedRows > 0;
        }

        public static async Task<BindingList<StaffListingDto>> GetStaff()
        {
            BindingList<StaffListingDto> list = new BindingList<StaffListingDto>();

            string query = @"select * from vw_StaffList";

            list = await DataAccessHelper.ExecuteObjectList(query, null, StaffListingMapper);

            return list;
        }

        public static StaffListingDto FindStaffByIdList(int staffId)
        {
            StaffListingDto staffList = new StaffListingDto();
            string query = @"select 
                	StaffID,
                	StaffNumber,
                	FullName,
                	Gender,
                	RoleName,
                	ShiftName, 
                	Username,
                	HireDate,
                	Status
                from vw_StaffList
                	where StaffID = @staffId;";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@staffId", SqlDbType.Int) {Value = staffId},
                };


            staffList = DataAccessHelper.ExecuteOneObject(query, parameters, StaffListingMapper);

            return staffList;
        }
    }
}
