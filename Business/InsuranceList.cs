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
            if (!Util.CanAccessForm(AppEnums.cs.EnRole.Receptionist, EnRole.Owner).IsSuccess)
                return null;

            BindingList<InsuranceList> list = new BindingList<InsuranceList>();

            foreach (InsuranceListDto dto in await InsuranceData.GetInsurances(Global.CurrentLoggedInStaff.StaffId))
                list.Add(FromDto(dto));

            return list;
        }
        public static InsuranceList FindInsurancebyID(int InsuranceId)
        {
            if (!Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess)
                return null;

            return FromDto(InsuranceData.FindInsuranceListByID(InsuranceId));
        }
    }
}
