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
        public EnRole Role { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string StaffNumber { get; set; }
        public DateTime HireDate { get; set; }
        public bool? IsActive { get; set; }

        private static StaffDto ToDto(Staff staff)
        {
            return new StaffDto()
            {
                StaffId = staff.StaffId,
                ScheduleId = staff.ScheduleId,
                RoleId = (int)staff.Role,
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
            return new Staff(staffDto.StaffId)
            {
                ScheduleId = staffDto.ScheduleId,
                Role = (EnRole)staffDto.RoleId,
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
            Role = EnRole.Receptionist;
            PersonId = -1;
            Username = string.Empty;
            HashedPassword = string.Empty;
            StaffNumber = string.Empty;
            IsActive = null;
            HireDate = DateTime.MinValue;
            Firstname = string.Empty;
            Lastname = string.Empty;
            Gender = EnGender.Male;
            Mode = EnMode.Add;
        }

        private Staff(int staffId)
        {
            StaffId = staffId;
            Mode = EnMode.Update;
        }

        public static async Task<BindingList<string>> GetSchedulesNames()
        {
            return await StaffData.GetSchedulesNames();
        }

        public static async Task<BindingList<string>> GetRolesNames()
        {
            return await StaffData.GetRolesNames();
        }

        private bool AddNewStaff()
        {
            Tuple<int, int, string> result = StaffData.AddNewStaff(ToDto(this));

            this.StaffId = result.Item1;
            this.PersonId = result.Item2;
            this.StaffNumber = result.Item3;

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
                        return true;
                    }
                    return false;

                case EnMode.Update:
                    return UpdateStaff();
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

    }
}
