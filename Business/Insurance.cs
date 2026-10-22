using Data_Access;
using Data_Access.DTOs;
using System;

namespace Business
{
    public class Insurance
    {
        private enum Mode { Add = 1, Update }
        private Mode _mode;
        public int InsurenceId { get; set; }
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

        public Insurance()
        {
            InsurenceId = -1;
            PolicyId = -1;
            _mode = Mode.Add;
        }

        public Insurance(int insuranceId)
        {
            InsurenceId = insuranceId;
            _mode = Mode.Update;
        }

        public static Insurance FromDto(InsuranceDto insuranceDto)
        {
            if (insuranceDto == null)
                return null;

            return new Insurance(insuranceDto.InsurenceId)
            {
                InsurenceId = insuranceDto.InsurenceId,
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
                _mode = Mode.Update
            };
        }

        public static InsuranceDto ToDto(Insurance insurance)
        {
            if (insurance == null)
                return null;

            return new InsuranceDto
            {
                InsurenceId = insurance.InsurenceId,
                PolicyId = insurance.PolicyId,
                RecordedBy = insurance.RecordedBy,
                InsuranceTypeId = insurance.InsuranceTypeId,
                InsuranceStatusId = insurance.InsuranceStatusId,
                UploadedDate = insurance.UploadedDate,
                InsuranceProvider = insurance.InsuranceProvider,
                PolicyNumber = insurance.PolicyNumber,
                CoverageStartDate = insurance.CoverageStartDate,
                CoverageEndDate = insurance.CoverageEndDate,
                PatientClaims = insurance.PatientClaims,
                PolicyholderPatientId = insurance.PolicyholderPatientId
            };
        }

        private bool _AddNewInsurance()
        {
            var result = InsuranceData.AddNewInsurance(ToDto(this));

            if (result.InsuranceId == -1)
                return false;

            InsurenceId = result.InsuranceId;
            PolicyId = result.PolicyId;

            return true;
        }

        private bool _UpdateInsurance()
        {
            return InsuranceData.UpdateInsurance(ToDto(this));
        }

        public bool Save()
        {
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

        public static Insurance FindInsuranceByID(int insuranceId)
        {
            return FromDto(InsuranceData.FindInsuranceByID(insuranceId));
        }

        public static Insurance FindInsuranceByPolicyID(int policyId)
        {
            return FromDto(InsuranceData.FindInsuranceByPolicyID(policyId));
        }
    }
}