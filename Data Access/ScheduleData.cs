using Data_Access.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data_Access
{
    public class ScheduleData
    {
        private static ScheduleDto ScheduleMapper(SqlDataReader reader)
        {
            return new ScheduleDto()
            {
                ScheduleId = (int)reader["ScheduleID"],
                WorkStartFrom = (TimeSpan)reader["WorkStartFrom"],
                WorkEndAt = (TimeSpan)reader["WorkEndAt"],
                ShiftName = (string)reader["ShiftName"],
            };
        }

        private static string MapStaffSchedule(SqlDataReader reader)
        {
            return (string)reader["ShiftName"];
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

        public static int AddNewSchedule(ScheduleDto scheduleDto)
        {
            int scheduleId = -1;

            string query = @"insert into Schedules
	            (WorkStartFrom, WorkEndAt, ShiftName)
	            values
	            (@WorkStartFrom, @WorkEndAt, @ShiftName); select Scope_Identity()";

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@WorkStartFrom", SqlDbType.Time) {Value = scheduleDto.WorkStartFrom },
                    new SqlParameter("@WorkEndAt", SqlDbType.Time) {Value = scheduleDto.WorkEndAt },
                    new SqlParameter("@ShiftName", SqlDbType.VarChar, 40) {Value = scheduleDto.ShiftName }
                };

                object result = DataAccessHelper.ExecuteScalar(query, parameters);

                if (result != null)
                    scheduleId = Convert.ToInt32(result);

            }
            catch (Exception ex)
            {
                throw;
            }

            return scheduleId;
        }

        public static bool UpdateSchedule(ScheduleDto scheduleDto)
        {
            int affectedRows = -1;
            string query = @"update Schedules
            set WorkStartFrom = @WorkStartFrom, WorkEndAt = @WorkEndAt, ShiftName = @ShiftName
            where ScheduleID = @scheduleId";

            try
            {
                List<SqlParameter> paramters = new List<SqlParameter>()
                {
                    new SqlParameter("@scheduleId", SqlDbType.Int) {Value = scheduleDto.ScheduleId },
                    new SqlParameter("@WorkStartFrom", SqlDbType.Time) {Value = scheduleDto.WorkStartFrom },
                    new SqlParameter("@WorkEndAt", SqlDbType.Time) {Value = scheduleDto.WorkEndAt },
                    new SqlParameter("@ShiftName", SqlDbType.VarChar, 40) {Value = scheduleDto.ShiftName },
                };

                affectedRows = DataAccessHelper.ExecuteNonQuery(query, paramters);
            }
            catch (Exception ex)
            {

            }

            return affectedRows > 0;
        }

        public static async Task<BindingList<ScheduleDto>> GetSchedules()
        {
            BindingList<ScheduleDto> schedules = new BindingList<ScheduleDto>();

            string query = @"select PersonID, Firstname, Lastname, Gender from People";

            try
            {
                schedules = await DataAccessHelper.ExecuteObjectList(query, null, ScheduleMapper);
            }
            catch (Exception ex)
            {

            }

            return schedules;
        }
    }
}
