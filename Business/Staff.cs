
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
        private EnMode _mode;
        public int StaffId { get; set; }
        public int ScheduleId { get; set; }
        public EnRole Role { get; set; }
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
                Role = staff.Role,
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
        private static async Task<Staff> FromDto(StaffDto staffDto)
        {
            if (staffDto is null)
                return null;

            Staff staff = new Staff(staffDto.StaffId)
            {
                ScheduleId = staffDto.ScheduleId,
                Role = staffDto.Role,
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

            staff.Schedule = await Schedule.FindScheduleByID(staffDto.ScheduleId);

            return staff;
        }
        private OperationResult CheckStaffManagementAccess()
        {
            if (Global.CurrentLoggedInStaff == null)
            {
                if (_mode == EnMode.Add && IsFirstStaff())
                    return new OperationResult("Initial staff setup is allowed.", "Access Granted", true);

                return new OperationResult("You must be logged in to manage staff.", "Access Denied", false);
            }

            return Util.CanAccessForm(EnRole.Owner);
        }
        public Staff()
        {
            StaffId = -1;
            ScheduleId = -1;
            Role = 0;
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
            _mode = EnMode.Add;
        }
        private Staff(int staffId)
        {
            StaffId = staffId;
            _mode = EnMode.Update;
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
                case EnMode.Add:

                    if (AddNewStaff())
                    {
                        _mode = EnMode.Update;

                        Auditor.Log(
                            "Staff Added",
                            $"Staff {StaffNumber} ({Firstname} {Lastname}, {Role}) was added by {performedBy}.",
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

                case EnMode.Update:

                    if (UpdateStaff())
                    {
                        Auditor.Log(
                            "Staff Updated",
                            $"Staff {StaffNumber} ({Firstname} {Lastname}, {Role}) was updated by {performedBy}.",
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
            return _mode == EnMode.Add;
        }
        public static async Task<Staff> FindStaffById(int staffId)
        {
            StaffDto staffDto = await StaffData.FindStaffById(staffId);

            return await FromDto(staffDto);
        }
        public static async Task<Staff> FindStaffByPersonId(int personId)
        {
            if (!Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess)
                return null;

            StaffDto staffDto = await StaffData.FindStaffByPersonId(personId);

            return await FromDto(staffDto);
        }
        public static async Task<Staff> FindStaffByUsername(string userName)
        {
            StaffDto staffDto = await StaffData.FindStaffByUsername(userName);

            return await FromDto(staffDto);
        }
        public bool DeactivateStaff()
        {
            if (Global.CurrentLoggedInStaff?.IsOwner() == true)
                return false;

            return StaffData.DeactivateStaff(StaffId);
        }
        public bool IsOwner() =>
            Role == EnRole.Owner;
        public bool IsRoleApplicableForAppointment()
        {
            return Role == EnRole.Nurse ||
                   Role == EnRole.Doctor;
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
                Util.IsValidId((int)Role))
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
        public static int GetTotalStaffCount()
        {
            return StaffData.GetTotalPatientsCount();
        }
    }
}
