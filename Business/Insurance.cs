using Data_Access;
using Data_Access.DTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class Insurance
    {
        private enum Mode { Add = 1, Update }
        private Mode _mode;
        public int InsuranceID { get; set; }
        public int PolicyId { get; set; }
        public int RecordedBy { get; set; }
        public int InsuranceTypeId { get; set; }
        public int InsuranceStatusId { get; set; }
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
        public Insurance()
        {
            InsuranceID = -1;
            RecordedBy = -1;
            InsuranceTypeId = -1;
            PolicyNumber = string.Empty;
            CoverageStartDate = DateTime.Now.AddYears(-1);
            CoverageEndDate = DateTime.Now.AddYears(1);
            PatientClaims = string.Empty;
            PolicyholderPatientId = -1;
            PolicyId = -1;
            Policy = null;
            PolicyPatientHolder = null;
            RecorderStaff = null;
            _mode = Mode.Add;
        }

        public Insurance(int insuranceId)
        {
            InsuranceID = insuranceId;
            Policy = Policy.FindPolicyByID(PolicyId);
            PolicyPatientHolder = Patient.FindPatientById(PolicyholderPatientId);
            RecorderStaff = Staff.FindStaffById(RecordedBy);
            _mode = Mode.Update;
        }

        public static Insurance FromDto(InsuranceDto insuranceDto)
        {
            if (insuranceDto == null)
                return null;

            return new Insurance(insuranceDto.InsuranceID)
            {
                InsuranceID = insuranceDto.InsuranceID,
                PolicyId = insuranceDto.PolicyId,
                RecordedBy = insuranceDto.RecordedBy,
                InsuranceTypeId = insuranceDto.InsuranceTypeId,
                InsuranceStatusId = insuranceDto.InsuranceStatusId,
                UploadedDate = insuranceDto.UploadedDate,
                InsuranceProvider = insuranceDto.InsuranceProvider,
                PolicyNumber = insuranceDto.PolicyNumber,
                CoverageStartDate = insuranceDto.CoverageStartDate,
                CoverageEndDate = insuranceDto.CoverageEndDate,
                PatientClaims = insuranceDto.PatientClaims,
                PolicyholderPatientId = insuranceDto.PolicyholderPatientId,
                Relationship = insuranceDto.Relationship,
                _mode = Mode.Update
            };
        }

        public static InsuranceDto ToDto(Insurance insurance)
        {
            if (insurance == null)
                return null;

            return new InsuranceDto
            {
                InsuranceID = insurance.InsuranceID,
                PolicyId = insurance.PolicyId,
                RecordedBy = insurance.RecorderStaff.StaffId,
                InsuranceTypeId = insurance.InsuranceTypeId,
                InsuranceStatusId = insurance.InsuranceStatusId,
                UploadedDate = insurance.UploadedDate,
                InsuranceProvider = insurance.InsuranceProvider,
                PolicyNumber = insurance.PolicyNumber,
                CoverageStartDate = insurance.CoverageStartDate,
                CoverageEndDate = insurance.CoverageEndDate,
                PatientClaims = insurance.PatientClaims,
                PolicyholderPatientId = insurance.PolicyPatientHolder.PatientId,
                Relationship = insurance.Relationship
            };
        }

        private bool _AddNewInsurance()
        {
            var result = InsuranceData.AddNewInsurance(ToDto(this), Patient.ToDto(PolicyPatientHolder));

            if (result.InsuranceId == -1)
                return false;

            InsuranceID = result.InsuranceId;
            PolicyholderPatientId = result.PatientId;

            return true;
        }

        private bool _UpdateInsurance()
        {
            return InsuranceData.UpdateInsurance(ToDto(this));
        }

        public bool Save()
        {
            if (NotValidToSave())
                return false;

            if (_mode == Mode.Add)
            {
                if (_AddNewInsurance())
                {
                    _mode = Mode.Update;
                    return true;
                }

                return false;
            }

            return _UpdateInsurance();
        }

        private bool NotValidToSave()
        {
            if (Policy is null || PolicyPatientHolder is null || RecorderStaff is null)
                return true;

            return false;
        }

        public static Insurance FindInsuranceByID(int insuranceId)
        {
            return FromDto(InsuranceData.FindInsuranceByID(insuranceId));
        }

        public static Insurance FindInsuranceByPolicyID(int policyId)
        {
            return FromDto(InsuranceData.FindInsuranceByPolicyID(policyId));
        }

        public static async Task<BindingList<string>> GetInsuranceTypesNames()
        {
            return await InsuranceData.GetInsuranceTypesNames();
        }

        public static async Task<BindingList<string>> GetInsuranceStatusesNames()
        {
            return await InsuranceData.GetInsuranceStatusesNames();
        }

    }

}