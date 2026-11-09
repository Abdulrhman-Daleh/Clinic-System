
using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;

namespace Business
{
    public class LabTest
    {
        private EnMode _mode;
        public int LabID { get; set; }
        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; }
        public (EnTestStatuses TestStatusID, string TestStatusText) TestStatuses { get; set; }
        public int NurseID { get; set; }
        public Staff Nurse { get; set; }
        public string Reason { get; set; }
        public string Instructions { get; set; }
        public string TestResult { get; set; }
        public string TestName { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? PerformDate { get; set; }
        public DateTime? ResultRecordedAt { get; set; }

        public bool IsAddMode()
        {
            return _mode == EnMode.Add;
        }

        public LabTest()
        {
            LabID = -1;
            AppointmentID = -1;
            NurseID = -1;
            TestStatuses = ((EnTestStatuses)0, "None");
            Reason = string.Empty;
            Instructions = string.Empty;
            TestResult = string.Empty;
            TestName = string.Empty;
            RequestDate = DateTime.Now;
            PerformDate = null;
            ResultRecordedAt = null;
            _mode = EnMode.Add;
        }

        public LabTest(int labID)
        {
            LabID = labID;
            _mode = EnMode.Update;
        }

        public static LabTest FromDto(LabTestDto labTestDto)
        {
            if (labTestDto == null)
                return null;

            return new LabTest(labTestDto.LabID)
            {
                LabID = labTestDto.LabID,
                AppointmentID = labTestDto.AppointmentID,
                TestStatuses = labTestDto.TestStatuses,
                NurseID = labTestDto.NurseID,
                Reason = labTestDto.Reason,
                Instructions = labTestDto.Instructions,
                TestResult = labTestDto.TestResult,
                TestName = labTestDto.TestName,
                RequestDate = labTestDto.RequestDate,
                PerformDate = labTestDto.PerformDate,
                ResultRecordedAt = labTestDto.ResultRecordedAt
            };
        }

        public static LabTestDto ToDto(LabTest labTest)
        {
            if (labTest == null)
                return null;

            return new LabTestDto()
            {
                LabID = labTest.LabID,
                AppointmentID = labTest.AppointmentID,
                TestStatuses = labTest.TestStatuses,
                NurseID = labTest.NurseID,
                Reason = labTest.Reason,
                Instructions = labTest.Instructions,
                TestResult = labTest.TestResult,
                TestName = labTest.TestName,
                RequestDate = labTest.RequestDate,
                PerformDate = labTest.PerformDate,
                ResultRecordedAt = labTest.ResultRecordedAt
            };
        }

        private bool AddNewLabTest()
        {
            LabID = LabTestData.AddNewLabTest(ToDto(this));

            return LabID > 0;
        }

        private bool UpdateLabTest()
        {
            return LabTestData.UpdateLabTest(ToDto(this));
        }

        public OperationResult Save()
        {
            OperationResult result = IsLabTestValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy = Global.CurrentLoggedInStaff == null
                ? "Initial setup"
                : $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case EnMode.Add:

                    if (AddNewLabTest())
                    {
                        _mode = EnMode.Update;

                        Auditor.Log(
                            "Lab Test Added",
                            $"Test '{TestName}' for AppointmentID {AppointmentID} (Status: {TestStatuses.TestStatusText}) was requested by {performedBy}.",
                            $"LabID {LabID}");

                        return new OperationResult(
                            "Lab test added successfully.",
                            "Added",
                            true);
                    }

                    return new OperationResult(
                        "Failed to add lab test.",
                        "Failed",
                        false);

                case EnMode.Update:

                    if (UpdateLabTest())
                    {
                        Auditor.Log(
                            "Lab Test Updated",
                            $"Test '{TestName}' for AppointmentID {AppointmentID} (Status: {TestStatuses.TestStatusText}) was updated by {performedBy}.",
                            $"LabID {LabID}");

                        return new OperationResult(
                            "Lab test updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update lab test.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid lab test operation mode.",
                "Failed",
                false);
        }

        public static LabTest FindLabTestByID(int labID)
        {
            return FromDto(LabTestData.FindLabTestByID(labID));
        }

        private OperationResult IsLabTestValidToSave()
        {
            if (Util.IsValidId(AppointmentID) &&
                Util.IsValidId(NurseID) &&
                Util.IsValidId((int)TestStatuses.TestStatusID) &&
                Util.IsRequired(TestName))
            {
                return new OperationResult(
                    "Lab test information is valid to save.",
                    "Valid",
                    true);
            }

            return new OperationResult(
                "Lab test information is invalid.",
                "Invalid",
                false);
        }
    }
}
