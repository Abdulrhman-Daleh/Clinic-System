using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class Staff : Person
    {
        public enum EnMode { Add = 1, Update = 2 }
        private EnMode Mode { get; set; }
        public int StaffId { get; set; }
        public int ScheduleId { get; set; }
        public (EnRole RoleId, string RoleName) RoleInfo { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string StaffNumber { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public Schedule Schedule { get; set; }

        private static StaffDto ToDto(Staff staff)
        {
            return new StaffDto()
            {
                StaffId = staff.StaffId,
                ScheduleId = staff.ScheduleId,
                RoleInfo = (staff.RoleInfo.RoleId, staff.RoleInfo.RoleName),
                PersonId = staff.PersonId,
                Username = staff.Username,
                HashedPassword = staff.HashedPassword,
                StaffNumber = staff.StaffNumber,
                HireDate = staff.HireDate,
                IsActive = staff.IsActive,
                Firstname = staff.Firstname,
                Lastname = staff.Lastname,
                Gender = staff.Gender == EnGender.Male ? 'M' : 'F'
            };
        }

        private static Staff FromDto(StaffDto staffDto)
        {
            if (staffDto is null)
                return null;

            return new Staff(staffDto.StaffId)
            {
                ScheduleId = staffDto.ScheduleId,
                RoleInfo = ((EnRole)staffDto.RoleInfo.RoleId, staffDto.RoleInfo.RoleName),
                PersonId = staffDto.PersonId,
                Username = staffDto.Username,
                HashedPassword = staffDto.HashedPassword,
                StaffNumber = staffDto.StaffNumber,
                HireDate = staffDto.HireDate,
                IsActive = staffDto.IsActive,
                Firstname = staffDto.Firstname,
                Lastname = staffDto.Lastname,
                Gender = staffDto.Gender == 'M' ? EnGender.Male : EnGender.Female
            };
        }

        public Staff()
        {
            StaffId = -1;
            ScheduleId = -1;
            RoleInfo = ((EnRole)0, "None");
            PersonId = -1;
            Username = string.Empty;
            HashedPassword = string.Empty;
            StaffNumber = string.Empty;
            IsActive = false;
            HireDate = DateTime.MinValue;
            Firstname = string.Empty;
            Lastname = string.Empty;
            this.Schedule = new Schedule();
            Gender = EnGender.Male;
            Mode = EnMode.Add;
        }

        private Staff(int staffId)
        {
            StaffId = staffId;
            this.Schedule = Schedule.FindScheduleByID(this.ScheduleId);
            Mode = EnMode.Update;
        }

        public static async Task<BindingList<string>> GetRolesNames()
        {
            return await StaffData.GetRolesNames();
        }

        private bool AddNewStaff()
        {
            (int staffId, int personId, string staffNumber) result = StaffData.AddNewStaff(ToDto(this));

            this.StaffId = result.staffId;
            this.PersonId = result.personId;
            this.StaffNumber = result.staffNumber;

            return StaffId > 0;
        }

        private bool UpdateStaff()
        {
            return StaffData.UpdateStaff(ToDto(this));
        }

        public override bool Save()
        {
            switch (Mode)
            {
                case EnMode.Add:
                    if (AddNewStaff())
                    {
                        Mode = EnMode.Update;
                        Auditor.Log("Staff Added", $"Staff {StaffNumber} ({RoleInfo.RoleName}) was added by StaffID {Global.CurrentLoggedInStaff.StaffId}.", $"StaffID {StaffId}");
                        return true;
                    }
                    return false;

                case EnMode.Update:
                    if (UpdateStaff())
                    {
                        Auditor.Log("Staff Updated", $"Staff {StaffNumber} ({RoleInfo.RoleName}) was updated by StaffID {Global.CurrentLoggedInStaff.StaffId}.", $"StaffID {StaffId}");
                        return true;
                    }

                    return false;
            }

            return false;
        }

        public static bool DoesExistsByUsername(string username)
        {
            return StaffData.DoesExistsByUsername(username);
        }

        public bool IsAddMode()
        {
            return Mode == EnMode.Add;
        }

        public static Staff FindStaffById(int staffId)
        {
            return FromDto(StaffData.FindStaffById(staffId));
        }

        public static Staff FindStaffByPersonId(int personId)
        {
            return FromDto(StaffData.FindStaffByPersonId(personId));
        }

        public static Staff FindStaffByUsername(string userName)
        {
            return FromDto(StaffData.FindStaffByUsername(userName));
        }

        public bool DeactivateStaff() => StaffData.DeactivateStaff(this.StaffId);
        public bool IsOwner() => RoleInfo.RoleId == EnRole.Owner;

        public bool IsRoleApplicableForAppointment()
        {
            return RoleInfo.RoleId == EnRole.Nurse || RoleInfo.RoleId == EnRole.Doctor;
        }
    }
}
