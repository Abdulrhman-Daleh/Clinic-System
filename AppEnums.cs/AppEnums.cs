namespace AppEnums.cs
{
    public enum EnAddMode { AddImmediately = 1, ReturnDraft };
    public enum EnGender { Male = 1, Female }
    public enum EnContactTypes { Normal = 1, Emergency }
    public enum EnRole { Receptionist = 1, Nurse = 2, Doctor = 3, Owner = 4 }
    public enum EnPolicyStatuses { Draft = 1, AwaitingApprovel, Active, Archived, Suspended }
    public enum EnInsuranceStatuses { Active = 1, Expired, UnderReviewing, Rejected, Submited }
    public enum EnInsuranceTypes { GovernmentInsurance = 1, Private, FamilyDeductible }
    public enum EnMode { Add = 1, Update = 2 }
}
