
using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class Policy
    {
        private enum Mode { Add = 1, Update }
        private Mode _mode;

        public int PolicyID { get; set; }
        public int OwnerID { get; set; }
        public (EnPolicyStatuses PolicyStatusID, string PolicyStatusText) PolicyStatus { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public short? NoShowFreezeDays { get; set; }
        public decimal? LateFeesPerDay { get; set; }
        public decimal? NoShowFee { get; set; }
        public Staff Owner { get; set; }

        private static Policy FromDto(PolicyDto policyDto)
        {
            if (policyDto == null)
                return null;

            return new Policy(policyDto.PolicyID)
            {
                PolicyID = policyDto.PolicyID,
                OwnerID = policyDto.OwnerID,
                PolicyStatus = policyDto.PolicyStatus,
                ActiveFrom = policyDto.ActiveFrom,
                ActiveTo = policyDto.ActiveTo,
                NoShowFreezeDays = policyDto.NoShowFreezeDays,
                LateFeesPerDay = policyDto.LateFeesPerDay,
                NoShowFee = policyDto.NoShowFee,
                Owner = Staff.FindStaffById(policyDto.OwnerID)
            };
        }

        private static PolicyDto ToDto(Policy policy)
        {
            return new PolicyDto
            {
                PolicyID = policy.PolicyID,
                OwnerID = policy.OwnerID,
                PolicyStatus = policy.PolicyStatus,
                ActiveFrom = policy.ActiveFrom,
                ActiveTo = policy.ActiveTo,
                NoShowFreezeDays = policy.NoShowFreezeDays,
                LateFeesPerDay = policy.LateFeesPerDay,
                NoShowFee = policy.NoShowFee
            };
        }

        public string BookingFreezeDaysText()
        {
            return NoShowFreezeDays != null
                ? $"{NoShowFreezeDays.ToString()} days after a no-show"
                : "No freeze days";
        }

        public string LateFeesPerDayText()
        {
            return LateFeesPerDay != null
                ? $"$ {LateFeesPerDay.ToString()} per day"
                : "No late fees";
        }

        public string NoShowFeesText()
        {
            return NoShowFee != null
                ? $"$ {NoShowFee.ToString()}"
                : "Empty No show fees";
        }

        public Policy()
        {
            PolicyID = -1;
            _mode = Mode.Add;
        }

        public Policy(int policyID)
        {
            PolicyID = policyID;
            _mode = Mode.Update;
        }

        private bool _AddNewPolicy()
        {
            PolicyID = PolicyData.AddNewPolicy(ToDto(this));
            return PolicyID != -1;
        }

        private bool _UpdatePolicy()
        {
            return PolicyData.UpdatePolicy(ToDto(this));
        }

        public OperationResult Save()
        {
            OperationResult result = IsPolicyValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy = Global.CurrentLoggedInStaff == null
                ? "Initial setup"
                : $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case Mode.Add:

                    if (_AddNewPolicy())
                    {
                        _mode = Mode.Update;

                        Auditor.Log(
                            "Policy Added",
                            $"Policy for OwnerID {OwnerID} (Status: {PolicyStatus.PolicyStatusText}, Active: {ActiveFrom:d} - {ActiveTo:d}) was added by {performedBy}.",
                            $"PolicyID {PolicyID}");

                        return new OperationResult(
                            "Policy added successfully.",
                            "Added",
                            true);
                    }

                    return new OperationResult(
                        "Failed to add policy.",
                        "Failed",
                        false);

                case Mode.Update:

                    if (_UpdatePolicy())
                    {
                        Auditor.Log(
                            "Policy Updated",
                            $"Policy for OwnerID {OwnerID} (Status: {PolicyStatus.PolicyStatusText}, Active: {ActiveFrom:d} - {ActiveTo:d}) was updated by {performedBy}.",
                            $"PolicyID {PolicyID}");

                        return new OperationResult(
                            "Policy updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update policy.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid policy operation mode.",
                "Failed",
                false);
        }

        public static Policy FindPolicyByID(int policyID)
        {
            return FromDto(PolicyData.FindPolicyByID(policyID));
        }

        public static Policy FindPolicyByOwnerID(int ownerID)
        {
            return FromDto(PolicyData.FindPolicyByOwnerID(ownerID));
        }

        public static async Task<BindingList<string>> GetPolicyStatusesNames()
        {
            return await PolicyData.GetPolicyStatusesNames();
        }

        public bool IsActive()
        {
            return ActiveTo > DateTime.Now;
        }

        public static async Task<BindingList<Policy>> GetPolicies()
        {
            BindingList<Policy> list = new BindingList<Policy>();

            foreach (PolicyDto p in await PolicyData.GetPolicies())
                list.Add(FromDto(p));

            return list;
        }

        public static async Task<BindingList<(int policyId, string ActiveDateRange)>> GetPoliciesRanges()
        {
            return await PolicyData.GetPoliciesRanges();
        }

        private OperationResult IsPolicyValidToSave()
        {
            if (Util.IsValidId(OwnerID) && ActiveFrom < ActiveTo)
            {
                return new OperationResult(
                    "Policy information is valid to save.",
                    "Valid",
                    true);
            }

            return new OperationResult(
                "Policy information is invalid.",
                "Invalid",
                false);
        }

        public static bool UpdatePolicyStatus(int policyId, int newStatusId) =>
            PolicyData.UpdatePolicyStatus(policyId, newStatusId);
    }
}
