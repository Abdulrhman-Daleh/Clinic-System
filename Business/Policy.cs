
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
        private EnMode _mode;
        public int PolicyID { get; set; }
        public int OwnerID { get; set; }
        public EnPolicyStatuses PolicyStatus { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public short? NoShowFreezeDays { get; set; }
        public decimal? LateFeesPerDay { get; set; }
        public decimal? NoShowFee { get; set; }
        public Staff Owner { get; set; }
        [Browsable(true)]
        public string Status => PolicyStatus.ToString();
        public static (int PolicyCount, int ActivePolicies, int SuspendedPolicies) PolicySummary = (0, 0, 0);
        private static async Task<Policy> FromDto(PolicyDto policyDto)
        {
            if (policyDto == null)
                return null;

            Policy policy = new Policy(policyDto.PolicyID)
            {
                PolicyID = policyDto.PolicyID,
                OwnerID = policyDto.OwnerID,
                PolicyStatus = policyDto.PolicyStatus,
                ActiveFrom = policyDto.ActiveFrom,
                ActiveTo = policyDto.ActiveTo,
                NoShowFreezeDays = policyDto.NoShowFreezeDays,
                LateFeesPerDay = policyDto.LateFeesPerDay,
                NoShowFee = policyDto.NoShowFee,
            };

            policy.Owner = await Staff.FindStaffById(policy.OwnerID);

            return policy;
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
                ? $"{NoShowFreezeDays} days after a no-show"
                : "No freeze days";
        }
        public string LateFeesPerDayText()
        {
            return LateFeesPerDay != null
                ? $"$ {LateFeesPerDay} per day"
                : "No late fees";
        }
        public string NoShowFeesText()
        {
            return NoShowFee != null
                ? $"$ {NoShowFee}"
                : "Empty No show fees";
        }
        public Policy()
        {
            PolicyID = -1;
            _mode = EnMode.Add;
        }
        public Policy(int policyID)
        {
            PolicyID = policyID;
            _mode = EnMode.Update;
        }
        private bool AddNewPolicy()
        {
            PolicyID = PolicyData.AddNewPolicy(ToDto(this));
            return PolicyID != -1;
        }
        private bool UpdatePolicy()
        {
            return PolicyData.UpdatePolicy(ToDto(this));
        }
        public OperationResult Save()
        {
            OperationResult result = Util.CanAccessForm(EnRole.Owner);

            if (!result.IsSuccess)
                return result;

            result = IsPolicyValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy = $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case EnMode.Add:

                    if (AddNewPolicy())
                    {
                        _mode = EnMode.Update;

                        Auditor.Log(
                            "Policy Added",
                            $"Policy for OwnerID {OwnerID} (Status: {PolicyStatus}, Active: {ActiveFrom:d} - {ActiveTo:d}) was added by {performedBy}.",
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

                case EnMode.Update:

                    if (UpdatePolicy())
                    {
                        Auditor.Log(
                            "Policy Updated",
                            $"Policy for OwnerID {OwnerID} (Status: {PolicyStatus}, Active: {ActiveFrom:d} - {ActiveTo:d}) was updated by {performedBy}.",
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
        public static async Task<Policy> FindPolicyByID(int policyID)
        {
            PolicyDto policyDto = await PolicyData.FindPolicyByID(policyID);

            return await FromDto(policyDto);
        }
        public static async Task<Policy> FindPolicyByOwnerID(int ownerID)
        {
            PolicyDto policyDto = await PolicyData.FindPolicyByOwnerID(ownerID);

            return await FromDto(policyDto);
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
            if (!Util.CanAccessForm(EnRole.Owner).IsSuccess)
                return null;

            BindingList<Policy> list = new BindingList<Policy>();

            PolicySummary = (0, 0, 0);
            foreach (PolicyDto policyDto in await PolicyData.GetPolicies(Global.CurrentLoggedInStaff.StaffId))
            {
                Policy policy = await FromDto(policyDto);
                list.Add(policy);
                UpdatePolicySummary(policy);
            }

            PolicySummary.PolicyCount = list.Count;

            return list;
        }

        private static void UpdatePolicySummary(Policy policy)
        {
            if (policy.PolicyStatus.Equals(EnPolicyStatuses.Active) && policy.ActiveTo > DateTime.Now)
                PolicySummary.ActivePolicies++;
            else if (policy.PolicyStatus.Equals(EnPolicyStatuses.Suspended))
                PolicySummary.SuspendedPolicies++;
        }
        public static async Task<BindingList<(int policyId, string ActiveDateRange)>> GetPoliciesRanges()
        {
            return await PolicyData.GetPoliciesRanges();
        }
        public static bool UpdatePolicyStatus(int policyId, EnPolicyStatuses newStatus)
        {
            if (!Util.CanAccessForm(EnRole.Owner).IsSuccess)
                return false;

            return PolicyData.UpdatePolicyStatus(policyId, (int)newStatus);
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
    }
}
