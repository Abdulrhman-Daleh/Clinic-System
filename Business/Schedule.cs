
using Data_Access;
using Data_Access.DTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class Schedule
    {
        private enum Mode { Add = 1, Update }
        private Mode _mode;

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
            ScheduleId = scheduleId;
            _mode = Mode.Update;
        }

        public Schedule()
        {
            ScheduleId = -1;
            WorkStartFrom = TimeSpan.MinValue;
            WorkEndAt = TimeSpan.MinValue;
            ShiftName = string.Empty;
            _mode = Mode.Add;
        }

        public static async Task<BindingList<string>> GetSchedulesNames()
        {
            return await ScheduleData.GetSchedulesNames();
        }

        private bool AddNewSchedule()
        {
            int id = ScheduleData.AddNewSchedule(ToDto(this));

            if (id == 0) return false;

            ScheduleId = id;
            return true;
        }

        private bool UpdateSchedule()
        {
            return ScheduleData.UpdateSchedule(ToDto(this));
        }

        public OperationResult Save()
        {
            OperationResult result = IsScheduleValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy = Global.CurrentLoggedInStaff == null
                ? "Initial setup"
                : $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case Mode.Add:

                    if (AddNewSchedule())
                    {
                        _mode = Mode.Update;

                        Auditor.Log(
                            "Schedule Added",
                            $"Shift '{ShiftName}' ({WorkStartFrom:hh\\:mm}–{WorkEndAt:hh\\:mm}) was added by {performedBy}.",
                            $"ScheduleID {ScheduleId}");

                        return new OperationResult(
                            "Schedule added successfully.",
                            "Added",
                            true);
                    }

                    return new OperationResult(
                        "Failed to add schedule.",
                        "Failed",
                        false);

                case Mode.Update:

                    if (UpdateSchedule())
                    {
                        Auditor.Log(
                            "Schedule Updated",
                            $"Shift '{ShiftName}' ({WorkStartFrom:hh\\:mm}–{WorkEndAt:hh\\:mm}) was updated by {performedBy}.",
                            $"ScheduleID {ScheduleId}");

                        return new OperationResult(
                            "Schedule updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update schedule.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid schedule operation mode.",
                "Failed",
                false);
        }

        private OperationResult IsScheduleValidToSave()
        {
            if (Util.IsRequired(ShiftName) &&
                WorkStartFrom != TimeSpan.MinValue &&
                WorkEndAt != TimeSpan.MinValue &&
                WorkStartFrom != WorkEndAt)
            {
                return new OperationResult(
                    "Schedule information is valid to save.",
                    "Valid",
                    true);
            }

            return new OperationResult(
                "Schedule information is invalid.",
                "Invalid",
                false);
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

        public static Schedule FindScheduleByID(int scheduleId)
        {
            return FromDto(ScheduleData.FindScheduleByID(scheduleId));
        }
    }
}
