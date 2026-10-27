using Data_Access;
using Data_Access.DTOs;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class MedicalRecordList
    {
        public int MedicalRecordID { get; set; }
        public string MedicalNumber { get; set; }
        public string PatientName { get; set; }
        public string Classification { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool ConsentToCare { get; set; }

        private static MedicalRecordList FromDto(MedicalRecordListDto recordDto)
        {
            if (recordDto == null)
                return null;

            return new MedicalRecordList()
            {
                MedicalRecordID = recordDto.MedicalRecordID,
                MedicalNumber = recordDto.MedicalNumber,
                PatientName = recordDto.PatientName,
                Classification = recordDto.Classification,
                CreatedDate = recordDto.CreatedDate,
                ConsentToCare = recordDto.ConsentToCare
            };
        }

        public static async Task<BindingList<MedicalRecordList>> GetMedicalRecords()
        {
            BindingList<MedicalRecordListDto> recordDtos =
                await MedicalRecordData.GetMedicalRecords();

            BindingList<MedicalRecordList> medicalRecords =
                new BindingList<MedicalRecordList>();

            foreach (MedicalRecordListDto recordDto in recordDtos)
                medicalRecords.Add(FromDto(recordDto));

            return medicalRecords;
        }
    }
}