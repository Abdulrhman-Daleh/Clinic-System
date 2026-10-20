using Data_Access;
using Data_Access.DTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class Schedule
    {
        private enum EnMode { Add = 1, Update };
        private EnMode Mode;
        public int ScheduleId { get; set; }
        public TimeSpan WorkStartFrom { get; set; }
        public TimeSpan WorkEndAt { get; set; }
        public string ShiftName { get; set; }

        public static Schedule FromDto(ScheduleDto scheduleDto)
        {
            if (scheduleDto == null) return null;

            return new Schedule(scheduleDto.ScheduleId)
            {
                WorkStartFrom = scheduleDto.WorkStartFrom,
                WorkEndAt = scheduleDto.WorkEndAt,
                ShiftName = scheduleDto.ShiftName,
            };
        }

        public static ScheduleDto ToDto(Schedule schedule)
        {
            return new ScheduleDto
            {
                ScheduleId = schedule.ScheduleId,
                WorkStartFrom = schedule.WorkStartFrom,
                WorkEndAt = schedule.WorkEndAt,
                ShiftName = schedule.ShiftName
            };
        }

        public Schedule(int scheduleId)
        {
            this.ScheduleId = scheduleId;
            Mode = EnMode.Update;
        }

        public Schedule()
        {
            this.ScheduleId = -1;
            this.WorkStartFrom = TimeSpan.MinValue;
            this.WorkEndAt = TimeSpan.MinValue;
            this.ShiftName = string.Empty;
            Mode = EnMode.Add;
        }

        public static async Task<BindingList<string>> GetSchedulesNames()
        {
            return await ScheduleData.GetSchedulesNames();
        }

        private bool AddNewSchedule()
        {
            int id = ScheduleData.AddNewSchedule(ToDto(this));

            if (id == 0) return false;

            this.ScheduleId = id;
            return true;
        }

        private bool UpdateSchedule()
        {
            return ScheduleData.UpdateSchedule(ToDto(this));
        }

        public bool Save()
        {
            switch (Mode)
            {
                case EnMode.Add:
                    if (AddNewSchedule())
                    {
                        Mode = EnMode.Update;
                        return true;
                    }
                    return false;

                case EnMode.Update:

                    return UpdateSchedule();
            }

            return false;
        }

        public static async Task<BindingList<Schedule>> GetSchedules()
        {
            BindingList<Schedule> list = new BindingList<Schedule>();

            foreach (var s in await ScheduleData.GetSchedules())
            {
                list.Add(FromDto(s));
            }

            return list;
        }
    }
}
