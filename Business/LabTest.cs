
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

        public LabTest(int labID, int appointmentId, int staffId)
        {
            LabID = labID;
            Appointment = Appointment.FindAppointmentByID(appointmentId);
            Nurse = Staff.FindStaffById(staffId);
            _mode = EnMode.Update;
        }

        public static LabTest FromDto(LabTestDto labTestDto)
        {
            if (labTestDto == null)
                return null;

            return new LabTest(labTestDto.LabID, labTestDto.AppointmentID, labTestDto.NurseID)
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

        private OperationResult CheckAddAccess()
        {
            OperationResult result = Util.CanAccessForm(EnRole.Doctor);

            if (!result.IsSuccess)
                return result;

            Appointment appointment = Business.Appointment.FindAppointmentByID(AppointmentID);

            if (appointment == null ||
                appointment.Staff == null ||
                appointment.Staff.StaffId != Global.CurrentLoggedInStaff.StaffId)
            {
                return new OperationResult(
                    "You are not authorized to order lab tests for this appointment.",
                    "Access Denied",
                    false);
            }

            Staff nurse = Staff.FindStaffById(NurseID);

            if (nurse == null ||
                nurse.RoleInfo.RoleId != EnRole.Nurse ||
                !nurse.IsActive)
            {
                return new OperationResult(
                    "The selected nurse is invalid or inactive.",
                    "Invalid Nurse",
                    false);
            }

            Appointment = appointment;
            Nurse = nurse;

            return new OperationResult(
                "Lab test ordering access granted.",
                "Access Granted",
                true);
        }

        private OperationResult CheckUpdateAccess()
        {
            OperationResult result = Util.CanAccessForm(EnRole.Nurse);

            if (!result.IsSuccess)
                return result;

            LabTestDto existingLabTest = LabTestData.FindLabTestByID(LabID);

            if (existingLabTest == null)
            {
                return new OperationResult(
                    "Lab test does not exist.",
                    "Not Found",
                    false);
            }

            if (existingLabTest.NurseID != Global.CurrentLoggedInStaff.StaffId)
            {
                return new OperationResult(
                    "You are not assigned to this lab test.",
                    "Access Denied",
                    false);
            }

            if (AppointmentID != existingLabTest.AppointmentID ||
                NurseID != existingLabTest.NurseID)
            {
                return new OperationResult(
                    "You cannot change the appointment or assigned nurse of this lab test.",
                    "Access Denied",
                    false);
            }

            // Preserve the original order information.
            AppointmentID = existingLabTest.AppointmentID;
            NurseID = existingLabTest.NurseID;
            TestName = existingLabTest.TestName;
            Reason = existingLabTest.Reason;
            Instructions = existingLabTest.Instructions;
            RequestDate = existingLabTest.RequestDate;

            return new OperationResult(
                "Lab test update access granted.",
                "Access Granted",
                true);
        }

        public OperationResult Save()
        {
            OperationResult result;

            if (_mode == EnMode.Add)
                result = CheckAddAccess();
            else
                result = CheckUpdateAccess();

            if (!result.IsSuccess)
                return result;

            result = IsLabTestValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy = $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

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
            if (!Util.CanAccessForm(EnRole.Doctor, EnRole.Nurse).IsSuccess)
                return null;

            LabTestDto labTestDto = LabTestData.FindLabTestByID(labID);

            if (labTestDto == null)
                return null;

            if (Global.CurrentLoggedInStaff.RoleInfo.RoleId == EnRole.Nurse)
            {
                if (labTestDto.NurseID != Global.CurrentLoggedInStaff.StaffId)
                    return null;
            }
            else if (Global.CurrentLoggedInStaff.RoleInfo.RoleId == EnRole.Doctor)
            {
                Appointment appointment =
                    Business.Appointment.FindAppointmentByID(labTestDto.AppointmentID);

                if (appointment?.Staff == null ||
                    appointment.Staff.StaffId != Global.CurrentLoggedInStaff.StaffId)
                    return null;
            }

            return FromDto(labTestDto);
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
