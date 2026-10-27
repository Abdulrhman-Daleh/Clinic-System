using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;

namespace Business
{
    public class MedicalRecord
    {
        private EnMode _mode;

        public int MedicalRecordID { get; set; }
        public int PatientID { get; set; }
        public string MedicalNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CurrentMedication { get; set; }
        public DateTime CurrentMedicationDuration { get; set; }
        public string PastSocialHistory { get; set; }
        public string PastSurgeries { get; set; }
        public bool ConcentToCare { get; set; }
        public string SocialStatus { get; set; }
        public string Classification { get; set; }
        public string Allergies { get; set; }
        public Patient Patient { get; set; }

        public MedicalRecord()
        {
            MedicalRecordID = -1;
            PatientID = -1;
            _mode = EnMode.Add;
        }

        private MedicalRecord(int medicalRecordID)
        {
            MedicalRecordID = medicalRecordID;
            _mode = EnMode.Update;
        }

        private static MedicalRecord FromDto(MedicalRecordDto recordDto)
        {
            if (recordDto is null)
                return null;

            return new MedicalRecord(recordDto.MedicalRecordID)
            {
                MedicalRecordID = recordDto.MedicalRecordID,
                PatientID = recordDto.PatientID,
                MedicalNumber = recordDto.MedicalNumber,
                CreatedDate = recordDto.CreatedDate,
                CurrentMedication = recordDto.CurrentMedication,
                CurrentMedicationDuration = recordDto.CurrentMedicationDuration,
                PastSocialHistory = recordDto.PastSocialHistory,
                PastSurgeries = recordDto.PastSurgeries,
                ConcentToCare = recordDto.ConcentToCare,
                SocialStatus = recordDto.SocialStatus,
                Classification = recordDto.Classification,
                Allergies = recordDto.Allergies,
                Patient = Patient.FindPatientById(recordDto.PatientID)
            };
        }

        private static MedicalRecordDto ToDto(MedicalRecord record)
        {
            return new MedicalRecordDto()
            {
                MedicalRecordID = record.MedicalRecordID,
                PatientID = record.PatientID,
                MedicalNumber = record.MedicalNumber,
                CreatedDate = record.CreatedDate,
                CurrentMedication = record.CurrentMedication,
                CurrentMedicationDuration = record.CurrentMedicationDuration,
                PastSocialHistory = record.PastSocialHistory,
                PastSurgeries = record.PastSurgeries,
                ConcentToCare = record.ConcentToCare,
                SocialStatus = record.SocialStatus,
                Classification = record.Classification,
                Allergies = record.Allergies
            };
        }

        private bool AddMedicalRecord()
        {
            MedicalRecordID = MedicalRecordData.AddMedicalRecord(ToDto(this));

            return MedicalRecordID > 0;
        }

        private bool UpdateMedicalRecord()
        {
            return MedicalRecordData.UpdateMedicalRecord(ToDto(this));
        }

        public bool Save()
        {
            switch (_mode)
            {
                case EnMode.Add:
                    if (AddMedicalRecord())
                    {
                        Auditor.Log(
                            "Medical Record Added",
                            $"Medical record for PatientID {PatientID} was created by StaffID {Global.CurrentLoggedInStaff.StaffId}.",
                            $"MedicalRecordID {MedicalRecordID}");

                        _mode = EnMode.Update;
                        return true;
                    }
                    return false;

                case EnMode.Update:
                    if (UpdateMedicalRecord())
                    {
                        Auditor.Log(
                            "Medical Record Updated",
                            $"Medical record for PatientID {PatientID} was updated by StaffID {Global.CurrentLoggedInStaff.StaffId}.",
                            $"MedicalRecordID {MedicalRecordID}");

                        return true;
                    }
                    return false;
            }

            return false;
        }

        public static MedicalRecord FindMedicalRecordByID(int medicalRecordID)
        {
            return FromDto(MedicalRecordData.FindMedicalRecordByID(medicalRecordID));
        }
    }
}