using AppEnums.cs;
using Data_Access;
using Data_Access.ListingDTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class InsuranceList
    {
        public int InsuranceID { get; set; }
        public string PolicyNumber { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsuranceType { get; set; }
        public string HolderName { get; set; }
        public int Members { get; set; }
        public DateTime CoverageStartDate { get; set; }
        public DateTime CoverageEndDate { get; set; }
        public string StatusText { get; set; }

        public static (int InsuranceCount, int ActiveCount, int ExpiredCount) InsuranceSummary = (0, 0, 0);
        private static InsuranceList FromDto(InsuranceListDto insuranceDto)
        {
            if (insuranceDto == null) return null;

            return new InsuranceList()
            {
                InsuranceID = insuranceDto.InsuranceID,
                PolicyNumber = insuranceDto.PolicyNumber,
                InsuranceProvider = insuranceDto.InsuranceProvider,
                HolderName = insuranceDto.HolderName,
                Members = insuranceDto.Members,
                CoverageStartDate = insuranceDto.CoverageStartDate,
                CoverageEndDate = insuranceDto.CoverageEndDate,
                StatusText = insuranceDto.StatusText
            };
        }
        public static async Task<BindingList<InsuranceList>> GetInsurances()
        {
            if (!Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess)
                return null;

            BindingList<InsuranceList> list = new BindingList<InsuranceList>();

            InsuranceSummary = (0, 0, 0);
            foreach (InsuranceListDto dto in await InsuranceData.GetInsurances(Global.CurrentLoggedInStaff.StaffId))
            {
                list.Add(FromDto(dto));
                UpdateInsuranceSummary(dto);
            }

            InsuranceSummary.InsuranceCount = list.Count;
            return list;
        }

        private static void UpdateInsuranceSummary(InsuranceListDto InsuranceDto)
        {
            if (InsuranceDto.StatusText.Equals("active", StringComparison.OrdinalIgnoreCase)
                && InsuranceDto.CoverageEndDate > DateTime.Now)
                InsuranceSummary.ActiveCount++;

            else if (InsuranceDto.StatusText.Equals("expired", StringComparison.OrdinalIgnoreCase)
                || InsuranceDto.StatusText.Equals("active", StringComparison.OrdinalIgnoreCase)
                && InsuranceDto.CoverageEndDate <= DateTime.Now)
                InsuranceSummary.ExpiredCount++;
        }
        public static async Task<InsuranceList> FindInsurancebyID(int InsuranceId)
        {
            if (!Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess)
                return null;

            return FromDto(await InsuranceData.FindInsuranceListByID(InsuranceId));
        }
    }
}
