
using Data_Access;
using Data_Access.ListingDTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class LabTestListing
    {
        public int LabID { get; set; }
        public string TestName { get; set; }
        public string NurseName { get; set; }
        public string TestStatusText { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? PerformDateValue { get; set; }
        public DateTime? ResultRecordedAtValue { get; set; }
        public string TestResultValue { get; set; }

        private static LabTestListing FromDto(LabTestListingDto labTestDto)
        {
            if (labTestDto == null)
                return null;

            return new LabTestListing()
            {
                LabID = labTestDto.LabID,
                TestName = labTestDto.TestName,
                NurseName = labTestDto.NurseName,
                TestStatusText = labTestDto.TestStatusText,
                RequestDate = labTestDto.RequestDate,
                PerformDateValue = labTestDto.PerformDate,
                ResultRecordedAtValue = labTestDto.ResultRecordedAt,
                TestResultValue = labTestDto.TestResult
            };
        }

        public static async Task<BindingList<LabTestListing>> GetLabTestsByAppointmentID(int appointmentID)
        {
            BindingList<LabTestListingDto> labTestDtos =
                await LabTestData.GetLabTestsByAppointmentID(appointmentID);

            BindingList<LabTestListing> labTests =
                new BindingList<LabTestListing>();

            foreach (LabTestListingDto labTestDto in labTestDtos)
                labTests.Add(FromDto(labTestDto));

            return labTests;
        }

        public static async Task<BindingList<LabTestListing>> GetLabTests()
        {
            BindingList<LabTestListingDto> labTestDtos =
                await LabTestData.GetLabTests();

            BindingList<LabTestListing> labTests =
                new BindingList<LabTestListing>();

            foreach (LabTestListingDto labTestDto in labTestDtos)
                labTests.Add(FromDto(labTestDto));

            return labTests;
        }

        public static LabTestListing FindLabTestByID(int labId)
        {
            return FromDto(LabTestData.FindLabTestListByID(labId));
        }
    }
}
