namespace AppEnums.cs
{
    public enum EnGender { Male = 1, Female }
    public enum EnContactTypes { Normal = 1, Emergency }
    public enum EnRole { Receptionist = 1, Nurse = 2, Doctor = 3, Owner = 4 }
    public enum EnPolicyStatuses { Draft = 1, AwaitingApprovel, Active, Archived, Suspended }
    public enum EnInsuranceStatuses { Active = 1, Expired, UnderReviewing, Rejected, Submited }
    public enum EnInsuranceTypes { GovernmentInsurance = 1, Private, FamilyDeductible }
    public enum EnMode { Add = 1, Update = 2 }
    public enum EnAppointmentStatuses { Scheduled = 1, ReminderSent, RemainderViewed, Arrived, InRoom, Completed, Cancelled, NoShow, Rescheduled }
    public enum EnAppointmentTypes { NewRegister = 1, RoutineTest, RoutineCare, ChronicCare, Treatment }
    public enum EnDosageForms { Tablet = 1, Capsule = 2, Syrup = 3, Suspension = 4, Injection = 5, Cream = 6, Ointment = 7, Gel = 8, EyeDrops = 9, EarDrops = 10, NasalSpray = 11, Inhaler = 12, Suppository = 13, Patch = 14, Lozenge = 15 }

    public enum EnStrengthUnits { Mcg = 1, Mg = 2, G = 3, Ml = 4, MgPerMl = 5, Percentage = 6, IU = 7, IUPerMl = 8 }
}
