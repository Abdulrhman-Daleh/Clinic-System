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
        public int PolicyStatusID { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public short? NoShowFreezeDays { get; set; }
        public decimal? LateFeesPerDay { get; set; }
        public decimal? NoShowFee { get; set; }
        public Staff Owner { get; set; }

        public static Policy FromDto(PolicyDto policyDto)
        {
            if (policyDto == null)
                return null;

            return new Policy(policyDto.PolicyID)
            {
                PolicyID = policyDto.PolicyID,
                OwnerID = policyDto.OwnerID,
                PolicyStatusID = policyDto.PolicyStatusID,
                ActiveFrom = policyDto.ActiveFrom,
                ActiveTo = policyDto.ActiveTo,
                NoShowFreezeDays = policyDto.NoShowFreezeDays,
                LateFeesPerDay = policyDto.LateFeesPerDay,
                NoShowFee = policyDto.NoShowFee,
                Owner = Staff.FindStaffById(policyDto.OwnerID),
                _mode = Mode.Update
            };
        }

        public static PolicyDto ToDto(Policy policy)
        {
            return new PolicyDto
            {
                PolicyID = policy.PolicyID,
                OwnerID = policy.OwnerID,
                PolicyStatusID = policy.PolicyStatusID,
                ActiveFrom = policy.ActiveFrom,
                ActiveTo = policy.ActiveTo,
                NoShowFreezeDays = policy.NoShowFreezeDays,
                LateFeesPerDay = policy.LateFeesPerDay,
                NoShowFee = policy.NoShowFee
            };
        }

        public string BookingFreezeDaysText()
        {
            return NoShowFreezeDays != null ? $"{NoShowFreezeDays.ToString()} days after a no-show" : "No freeze days";
        }

        public string LateFeesPerDayText()
        {
            return LateFeesPerDay != null ? $"$ {LateFeesPerDay.ToString()} per day" : "No late fees";
        }

        public string NoShowFeesText()
        {
            return NoShowFee != null ? $"$ {NoShowFee.ToString()}" : "Empty No show fees";
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

        public bool Save()
        {
            if (_mode == Mode.Add)
            {
                if (_AddNewPolicy())
                {
                    _mode = Mode.Update;
                    return true;
                }

                return false;
            }

            return _UpdatePolicy();
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
    }
}