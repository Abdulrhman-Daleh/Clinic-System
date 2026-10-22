using Data_Access;
using Data_Access.ListingDTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class PatientList
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public char Gender { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Status { get; set; }
        public int Visits { get; set; }
        public string InsuranceName { get; set; }


        private static PatientList FromDto(PatientListingDto dto)
        {
            return new PatientList()
            {
                PatientId = dto.PatientId,
                FullName = dto.FullName,
                Gender = dto.Gender,
                RegisterDate = dto.RegisterDate,
                Status = dto.Status,
                Visits = dto.Visits,
                InsuranceName = dto.InsuranceName,
            };
        }

        public static PatientList FindPatientByIdList(int patientId)
        {
            return FromDto(PatientData.FindPatientByIdList(patientId));
        }
        public static async Task<BindingList<PatientList>> GetPatientListingDatas()
        {
            BindingList<PatientList> list = new BindingList<PatientList>();

            foreach (PatientListingDto item in await PatientData.GetPatientListingDatas())
                list.Add(FromDto(item));


            return list;
        }
    }
}
