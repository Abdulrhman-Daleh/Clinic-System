using AppEnums.cs;
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

        public static (int TotalMedicalRecords, int MissingConsent) MedicalRecordsSummary = (0, 0);
        public static async Task<BindingList<MedicalRecordList>> GetMedicalRecords()
        {
            if (!Util.CanAccessForm(EnRole.Doctor, EnRole.Owner).IsSuccess)
                return null;

            BindingList<MedicalRecordListDto> recordDtos =
                await MedicalRecordData.GetMedicalRecords(Global.CurrentLoggedInStaff.StaffId);

            MedicalRecordsSummary = (0, 0);

            BindingList<MedicalRecordList> medicalRecords =
                new BindingList<MedicalRecordList>();

            foreach (MedicalRecordListDto recordDto in recordDtos)
            {
                medicalRecords.Add(FromDto(recordDto));
                UpdateMissingConsentCount(recordDto);
            }

            MedicalRecordsSummary.TotalMedicalRecords = medicalRecords.Count;
            return medicalRecords;
        }

        private static void UpdateMissingConsentCount(MedicalRecordListDto medicalRecord)
        {
            if (!medicalRecord.ConsentToCare)
                MedicalRecordsSummary.MissingConsent++;
        }
    }
}