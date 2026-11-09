
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
        private enum Mode { Add = 1, Update }
        private Mode _mode;

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

            return new Staff(staffDto.StaffId, staffDto.ScheduleId)
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
                Gender = staffDto.Gender == 'M' ? EnGender.Male : EnGender.Female,
            };
        }

        private OperationResult CheckStaffManagementAccess()
        {
            if (Global.CurrentLoggedInStaff == null)
            {
                if (_mode == Mode.Add && IsFirstStaff())
                    return new OperationResult("Initial staff setup is allowed.", "Access Granted", true);

                return new OperationResult("You must be logged in to manage staff.", "Access Denied", false);
            }

            return Util.CanAccessForm(EnRole.Owner);
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
            Schedule = new Schedule();
            Gender = EnGender.Male;
            _mode = Mode.Add;
        }

        private Staff(int staffId, int scheduleId)
        {
            StaffId = staffId;
            _mode = Mode.Update;
            Schedule = Schedule.FindScheduleByID(scheduleId);
        }

        public static async Task<BindingList<string>> GetRolesNames()
        {
            return await StaffData.GetRolesNames();
        }

        private bool AddNewStaff()
        {
            (int staffId, int personId, string staffNumber) result =
                StaffData.AddNewStaff(ToDto(this));

            StaffId = result.staffId;
            PersonId = result.personId;
            StaffNumber = result.staffNumber;

            return StaffId > 0;
        }

        private bool UpdateStaff()
        {
            return StaffData.UpdateStaff(ToDto(this));
        }

        public override OperationResult Save()
        {
            OperationResult result = CheckStaffManagementAccess();

            if (!result.IsSuccess)
                return result;

            result = IsPersonValidToSave();

            if (!result.IsSuccess)
                return result;

            result = IsStaffValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy = Global.CurrentLoggedInStaff == null
                ? "Initial setup"
                : $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case Mode.Add:

                    if (AddNewStaff())
                    {
                        _mode = Mode.Update;

                        Auditor.Log(
                            "Staff Added",
                            $"Staff {StaffNumber} ({Firstname} {Lastname}, {RoleInfo.RoleName}) was added by {performedBy}.",
                            $"StaffID {StaffId}");

                        return new OperationResult(
                            "Staff added successfully.",
                            "Added",
                            true);
                    }

                    return new OperationResult(
                        "Failed to add staff.",
                        "Failed",
                        false);

                case Mode.Update:

                    if (UpdateStaff())
                    {
                        Auditor.Log(
                            "Staff Updated",
                            $"Staff {StaffNumber} ({Firstname} {Lastname}, {RoleInfo.RoleName}) was updated by {performedBy}.",
                            $"StaffID {StaffId}");

                        return new OperationResult(
                            "Staff updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update staff.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid staff operation mode.",
                "Failed",
                false);
        }

        public static bool DoesExistsByUsername(string username)
        {
            return StaffData.DoesExistsByUsername(username);
        }

        public bool IsAddMode()
        {
            return _mode == Mode.Add;
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

        public bool DeactivateStaff()
        {
            if (Global.CurrentLoggedInStaff?.IsOwner() != true)
                return false;

            return StaffData.DeactivateStaff(StaffId);
        }

        public bool IsOwner() =>
            RoleInfo.RoleId == EnRole.Owner;

        public bool IsRoleApplicableForAppointment()
        {
            return RoleInfo.RoleId == EnRole.Nurse ||
                   RoleInfo.RoleId == EnRole.Doctor;
        }

        public static bool IsFirstStaff()
        {
            return StaffData.IsFirstStaff();
        }

        private OperationResult IsStaffValidToSave()
        {
            if (Util.IsRequired(Username) &&
                Util.IsRequired(HashedPassword) &&
                Util.IsValidId(ScheduleId) &&
                Util.IsValidId((int)RoleInfo.RoleId))
            {
                return new OperationResult(
                    "Staff information is valid to save.",
                    "Valid",
                    true);
            }

            return new OperationResult(
                "Staff information is invalid.",
                "Invalid",
                false);
        }
    }
}
