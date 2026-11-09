using Data_Access;
using Data_Access.ListingDTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class LabTestQueueListing
    {
        public int LabID { get; set; }
        public string TestName { get; set; }
        public string PatientName { get; set; }
        public DateTime RequestDate { get; set; }
        public string TestStatusText { get; set; }

        public string Age
        {
            get
            {
                TimeSpan age = DateTime.Now - RequestDate;

                if (age.TotalDays >= 1)
                    return $"{(int)age.TotalDays}d {age.Hours}h";

                if (age.TotalHours >= 1)
                    return $"{(int)age.TotalHours}h";

                return $"{age.Minutes}m";
            }
        }

        private static LabTestQueueListing FromDto(LabTestQueueListingDto labTestDto)
        {
            return new LabTestQueueListing()
            {
                LabID = labTestDto.LabID,
                TestName = labTestDto.TestName,
                PatientName = labTestDto.PatientName,
                RequestDate = labTestDto.RequestDate,
                TestStatusText = labTestDto.TestStatusText
            };
        }

        public static async Task<BindingList<LabTestQueueListing>> GetMyLabQueue()
        {
            BindingList<LabTestQueueListing> list =
                new BindingList<LabTestQueueListing>();

            foreach (LabTestQueueListingDto dto in
                await LabTestData.GetMyLabQueue(Global.CurrentLoggedInStaff.StaffId))
            {
                list.Add(FromDto(dto));
            }

            return list;
        }
    }
}