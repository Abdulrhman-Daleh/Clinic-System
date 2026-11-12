
using AppEnums.cs;
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
            OperationResult result = Util.CanAccessForm(
                EnRole.Doctor,
                EnRole.Nurse);

            BindingList<LabTestListing> list = new BindingList<LabTestListing>();

            if (!result.IsSuccess)
                return list;

            int? doctorStaffId = null;
            int? nurseStaffId = null;

            switch (Global.CurrentLoggedInStaff.RoleInfo.RoleId)
            {
                case EnRole.Doctor:
                    doctorStaffId = Global.CurrentLoggedInStaff.StaffId;
                    break;

                case EnRole.Nurse:
                    nurseStaffId = Global.CurrentLoggedInStaff.StaffId;
                    break;
            }

            var records = await LabTestData.GetLabTestsByAppointmentID(
                appointmentID,
                doctorStaffId,
                nurseStaffId);

            foreach (var record in records)
                list.Add(FromDto(record));

            return list;
        }
        public static LabTestListing FindLabTestByID(int labId)
        {
            if (!Util.CanAccessForm(EnRole.Nurse).IsSuccess)
                return null;

            return FromDto(LabTestData.FindLabTestListByID(labId));
        }
    }
}
