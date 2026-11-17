
using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class Insurance
    {
        private EnMode _mode;
        public int InsuranceID { get; set; }
        public int PolicyId { get; set; }
        public int RecordedBy { get; set; }
        public EnInsuranceTypes InsuranceType { get; set; }
        public EnInsuranceStatuses InsuranceStatus { get; set; }
        public DateTime UploadedDate { get; set; }
        public string InsuranceProvider { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime CoverageStartDate { get; set; }
        public DateTime CoverageEndDate { get; set; }
        public string PatientClaims { get; set; }
        public int PolicyholderPatientId { get; set; }
        public string Relationship { get; set; }
        public Policy Policy { get; set; }
        public Patient PolicyPatientHolder { get; set; }
        public Staff RecorderStaff { get; set; }
        public bool IsAddMode()
        {
            return _mode == EnMode.Add;
        }
        public Insurance()
        {
            InsuranceID = -1;
            RecordedBy = -1;
            InsuranceType = 0;
            InsuranceStatus = 0;
            PolicyNumber = string.Empty;
            CoverageStartDate = DateTime.Now.AddYears(-1);
            CoverageEndDate = DateTime.Now.AddYears(1);
            PatientClaims = string.Empty;
            PolicyholderPatientId = -1;
            PolicyId = -1;
            Policy = null;
            PolicyPatientHolder = null;
            RecorderStaff = null;
            _mode = EnMode.Add;
        }
        public Insurance(int insuranceId)
        {
            InsuranceID = insuranceId;
            _mode = EnMode.Update;
        }
        public static async Task<Insurance> FromDto(InsuranceDto insuranceDto)
        {
            if (insuranceDto == null)
                return null;

            Insurance insurance = new Insurance(insuranceDto.InsuranceID)
            {
                InsuranceID = insuranceDto.InsuranceID,
                PolicyId = insuranceDto.PolicyId,
                RecordedBy = insuranceDto.RecordedBy,
                InsuranceType = insuranceDto.InsuranceType,
                InsuranceStatus = insuranceDto.InsuranceStatus,
                UploadedDate = insuranceDto.UploadedDate,
                InsuranceProvider = insuranceDto.InsuranceProvider,
                PolicyNumber = insuranceDto.PolicyNumber,
                CoverageStartDate = insuranceDto.CoverageStartDate,
                CoverageEndDate = insuranceDto.CoverageEndDate,
                PatientClaims = insuranceDto.PatientClaims,
                PolicyholderPatientId = insuranceDto.PolicyholderPatientId,
                Relationship = insuranceDto.Relationship,
            };

            insurance.Policy = await Policy.FindPolicyByID(insurance.PolicyId);
            insurance.PolicyPatientHolder = await Patient.FindPatientById(insurance.PolicyholderPatientId);
            insurance.RecorderStaff = await Staff.FindStaffById(insurance.RecordedBy);

            return insurance;
        }
        public static InsuranceDto ToDto(Insurance insurance)
        {
            if (insurance == null)
                return null;

            return new InsuranceDto
            {
                InsuranceID = insurance.InsuranceID,
                PolicyId = insurance.PolicyId,
                RecordedBy = insurance.RecordedBy,
                InsuranceType = insurance.InsuranceType,
                InsuranceStatus = insurance.InsuranceStatus,
                UploadedDate = insurance.UploadedDate,
                InsuranceProvider = insurance.InsuranceProvider,
                PolicyNumber = insurance.PolicyNumber,
                CoverageStartDate = insurance.CoverageStartDate,
                CoverageEndDate = insurance.CoverageEndDate,
                PatientClaims = insurance.PatientClaims,
                PolicyholderPatientId = insurance.PolicyholderPatientId,
                Relationship = insurance.Relationship
            };
        }
        private bool AddNewInsurance()
        {
            var result = InsuranceData.AddNewInsurance(
                ToDto(this),
                Patient.ToDto(PolicyPatientHolder));

            if (result.InsuranceId == -1)
                return false;

            InsuranceID = result.InsuranceId;
            PolicyholderPatientId = result.PatientId;

            return true;
        }
        private bool UpdateInsurance()
        {
            return InsuranceData.UpdateInsurance(ToDto(this));
        }
        public OperationResult Save()
        {
            OperationResult result = Util.CanAccessForm(
                EnRole.Receptionist,
                EnRole.Owner);

            if (!result.IsSuccess)
                return result;

            if (Global.CurrentLoggedInStaff == null)
                return new OperationResult("No user is logged in", "Not valid", false);

            if (_mode == EnMode.Add)
            {
                RecordedBy = Global.CurrentLoggedInStaff.StaffId;
                RecorderStaff = Global.CurrentLoggedInStaff;
            }

            result = IsInsuranceValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy =
                $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case EnMode.Add:

                    if (AddNewInsurance())
                    {
                        _mode = EnMode.Update;

                        Auditor.Log(
                            "Insurance Added",
                            $"Insurance type '{InsuranceType}' ({InsuranceStatus}) from '{InsuranceProvider}' for PatientID {PolicyholderPatientId} was added by {performedBy}.",
                            $"InsuranceID {InsuranceID}");

                        return new OperationResult(
                            "Insurance added successfully.",
                            "Added",
                            true);
                    }

                    return new OperationResult(
                        "Failed to add insurance.",
                        "Failed",
                        false);

                case EnMode.Update:

                    if (UpdateInsurance())
                    {
                        Auditor.Log(
                            "Insurance Updated",
                            $"Insurance type '{InsuranceType}' ({InsuranceStatus}) from '{InsuranceProvider}' for PatientID {PolicyholderPatientId} was updated by {performedBy}.",
                            $"InsuranceID {InsuranceID}");

                        return new OperationResult(
                            "Insurance updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update insurance.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid insurance operation mode.",
                "Failed",
                false);
        }
        public static async Task<Insurance> FindInsuranceByID(int insuranceId)
        {
            if (!Util.CanAccessForm(
                EnRole.Receptionist,
                EnRole.Doctor,
                EnRole.Owner).IsSuccess)
                return null;

            InsuranceDto insuranceDto = await InsuranceData.FindInsuranceByID(insuranceId);

            return await FromDto(insuranceDto);
        }
        public static async Task<Insurance> FindInsuranceByPatientId(int patientId)
        {
            if (!Util.CanAccessForm(
                EnRole.Receptionist,
                EnRole.Doctor,
                EnRole.Owner).IsSuccess)
                return null;

            InsuranceDto insuranceDto = await InsuranceData.FindInsuranceByPatientId(patientId);

            return await FromDto(insuranceDto);
        }
        public static async Task<BindingList<string>> GetInsuranceTypesNames()
        {
            if (!Util.CanAccessForm(
                EnRole.Receptionist,
                EnRole.Owner).IsSuccess)
                return new BindingList<string>();

            return await InsuranceData.GetInsuranceTypesNames();
        }
        public static async Task<BindingList<string>> GetInsuranceStatusesNames()
        {
            if (!Util.CanAccessForm(
                EnRole.Receptionist,
                EnRole.Owner).IsSuccess)
                return new BindingList<string>();

            return await InsuranceData.GetInsuranceStatusesNames();
        }
        public static async Task<BindingList<PatientInsuranceInfoDto>> GetPatientsUnderInsurance(int insuranceId)
        {
            if (!Util.CanAccessForm(
                EnRole.Receptionist,
                EnRole.Owner).IsSuccess)
                return new BindingList<PatientInsuranceInfoDto>();

            return await InsuranceData.GetPatientsUnderInsurance(insuranceId);
        }
        private OperationResult IsInsuranceValidToSave()
        {
            if (Policy == null ||
                PolicyPatientHolder == null ||
                RecorderStaff == null)
            {
                return new OperationResult(
                    "Insurance information is invalid.",
                    "Invalid",
                    false);
            }

            if (Util.IsRequired(InsuranceProvider) &&
                Util.IsRequired(Relationship) &&
                Util.IsRequired(PatientClaims) &&
                Util.IsValidId(PolicyId) &&
                Util.IsValidId(RecordedBy) &&
                Util.IsValidId(PolicyholderPatientId) &&
                Util.IsValidId((int)InsuranceType) &&
                Util.IsValidId((int)InsuranceStatus))
            {
                return new OperationResult(
                    "Insurance information is valid to save.",
                    "Valid",
                    true);
            }

            return new OperationResult(
                "Insurance information is invalid.",
                "Invalid",
                false);
        }
    }
}
