using Data_Access;
using Data_Access.DTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class PatientInsuranceInfo
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
        public DateTime PatientSince { get; set; }

        private static PatientInsuranceInfo FromDto(PatientInsuranceInfoDto dto)
        {
            return new PatientInsuranceInfo()
            {
                Name = dto.Name,
                Relationship = dto.Relationship,
                PatientSince = dto.PatientSince
            };
        }

        public static async Task<BindingList<PatientInsuranceInfo>> GetPatientInsurance(int InsuranceId)
        {
            BindingList<PatientInsuranceInfo> list = new BindingList<PatientInsuranceInfo>();

            foreach (var item in await InsuranceData.GetPatientsUnderInsurance(InsuranceId))
                list.Add(FromDto(item));

            return list;
        }
    }
}
