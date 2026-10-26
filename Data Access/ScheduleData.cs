using Data_Access.DTOs;
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
    public class ScheduleData
    {
        private static ScheduleDto ScheduleMapper(SqlDataReader reader)
        {
            try
            {
                return new ScheduleDto()
                {
                    ScheduleId = (int)reader["ScheduleID"],
                    WorkStartFrom = (TimeSpan)reader["WorkStartFrom"],
                    WorkEndAt = (TimeSpan)reader["WorkEndAt"],
                    ShiftName = (string)reader["ShiftName"],
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(ex.Message, "ScheduleData", "Application", EventLogEntryType.Error));
                return null;
            }
        }

        private static string MapStaffSchedule(SqlDataReader reader)
        {
            try
            {
                return (string)reader["ShiftName"];
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(ex.Message, "ScheduleData", "Application", EventLogEntryType.Error));
                return null;
            }
        }

        public static async Task<BindingList<string>> GetSchedulesNames()
        {
            BindingList<string> schedules = new BindingList<string>();

            string query = @"select ShiftName from Schedules
                order by ScheduleID asc";

            schedules = await DataAccessHelper.ExecuteObjectList(query, null, MapStaffSchedule);

            return schedules;
        }

        public static int AddNewSchedule(ScheduleDto scheduleDto)
        {
            int scheduleId = -1;

            string query = @"insert into Schedules
	            (WorkStartFrom, WorkEndAt, ShiftName)
	            values
	            (@WorkStartFrom, @WorkEndAt, @ShiftName); select Scope_Identity()";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@WorkStartFrom", SqlDbType.Time) {Value = scheduleDto.WorkStartFrom },
                    new SqlParameter("@WorkEndAt", SqlDbType.Time) {Value = scheduleDto.WorkEndAt },
                    new SqlParameter("@ShiftName", SqlDbType.VarChar, 40) {Value = scheduleDto.ShiftName }
                };

            object result = DataAccessHelper.ExecuteScalar(query, parameters);

            if (result != null)
                scheduleId = Convert.ToInt32(result);


            return scheduleId;
        }

        public static bool UpdateSchedule(ScheduleDto scheduleDto)
        {
            int affectedRows = -1;
            string query = @"update Schedules
            set WorkStartFrom = @WorkStartFrom, WorkEndAt = @WorkEndAt, ShiftName = @ShiftName
            where ScheduleID = @scheduleId";

            List<SqlParameter> paramters = new List<SqlParameter>()
                {
                    new SqlParameter("@scheduleId", SqlDbType.Int) {Value = scheduleDto.ScheduleId },
                    new SqlParameter("@WorkStartFrom", SqlDbType.Time) {Value = scheduleDto.WorkStartFrom },
                    new SqlParameter("@WorkEndAt", SqlDbType.Time) {Value = scheduleDto.WorkEndAt },
                    new SqlParameter("@ShiftName", SqlDbType.VarChar, 40) {Value = scheduleDto.ShiftName },
                };

            affectedRows = DataAccessHelper.ExecuteNonQuery(query, paramters);

            return affectedRows > 0;
        }

        public static async Task<BindingList<ScheduleDto>> GetSchedules()
        {
            BindingList<ScheduleDto> schedules = new BindingList<ScheduleDto>();

            string query = @"select ScheduleID, WorkStartFrom, WorkEndAt, ShiftName from Schedules";

            schedules = await DataAccessHelper.ExecuteObjectList(query, null, ScheduleMapper);

            return schedules;
        }

        public static ScheduleDto FindScheduleByID(int scheduleId)
        {
            ScheduleDto scheduleDto = new ScheduleDto();

            string query = @"select ScheduleID, WorkStartFrom, WorkEndAt, ShiftName from Schedules
                    where ScheduleID = @ScheduleId";

            List<SqlParameter> paramters = new List<SqlParameter>()
                {
                    new SqlParameter("@ScheduleId", SqlDbType.Int) {Value = scheduleId}
                };

            scheduleDto = DataAccessHelper.ExecuteOneObject(query, paramters, ScheduleMapper);

            return scheduleDto;
        }
    }
}
