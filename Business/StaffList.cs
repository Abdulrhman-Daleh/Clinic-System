using AppEnums.cs;
using Data_Access;
using Data_Access.ListingDTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class StaffList
    {
        public int StaffId { get; set; }
        public string StaffNumber { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string RoleName { get; set; }
        public string ShiftName { get; set; }
        public string Username { get; set; }
        public DateTime HireDate { get; set; }
        public string Status { get; set; }

        public static (int StaffCount, int DoctorCount, int NurseCount, int Receptionist) StaffSummary = (0, 0, 0, 0);
        private static StaffList FromDto(StaffListingDto listDto)
        {
            return new StaffList()
            {
                StaffId = listDto.StaffId,
                StaffNumber = listDto.StaffNumber,
                FullName = listDto.FullName,
                Gender = listDto.Gender,
                RoleName = listDto.RoleName,
                ShiftName = listDto.ShiftName,
                Username = listDto.Username,
                HireDate = listDto.HireDate,
                Status = listDto.Status
            };
        }
        public static async Task<BindingList<StaffList>> GetStaff()
        {
            if (!Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess)
                return null;

            BindingList<StaffList> staff = new BindingList<StaffList>();
            StaffSummary = (0, 0, 0, 0);

            foreach (StaffListingDto s in await StaffData.GetStaff())
            {
                staff.Add(FromDto(s));
                UpdateStaffSummary(s);
            }

            StaffSummary.StaffCount = staff.Count;
            return staff;
        }
        private static void UpdateStaffSummary(StaffListingDto staff)
        {
            switch (staff.RoleName)
            {
                case nameof(EnRole.Doctor):
                    StaffSummary.DoctorCount++;
                    break;

                case nameof(EnRole.Nurse):
                    StaffSummary.NurseCount++;
                    break;

                case nameof(EnRole.Receptionist):
                    StaffSummary.Receptionist++;
                    break;
            }
        }
        public static async Task<StaffList> FindStaffById(int staffId)
        {
            if (!Util.CanAccessForm(EnRole.Owner).IsSuccess)
                return null;

            return FromDto(await StaffData.FindStaffByIdList(staffId));
        }
    }
}