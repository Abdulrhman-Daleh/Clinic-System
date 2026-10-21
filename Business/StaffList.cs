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
            BindingList<StaffList> list = new BindingList<StaffList>();

            foreach (var l in await StaffData.GetStaff())
            {
                list.Add(FromDto(l));
            }

            return list;
        }

        public static StaffList FindStaffById(int staffId)
        {
            return FromDto(StaffData.FindStaffByIdList(staffId));
        }
    }
}