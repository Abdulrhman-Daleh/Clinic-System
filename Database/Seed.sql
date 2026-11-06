USE ClinicDatabase;
GO

/* Seed lookup/reference tables. Explicit identity values are used for stable IDs. */

/* ContactTypes seed begins */
SET IDENTITY_INSERT dbo.ContactTypes ON;

IF NOT EXISTS (SELECT 1 FROM dbo.ContactTypes WHERE ContactTypeID = 1)
BEGIN
    INSERT INTO dbo.ContactTypes (ContactTypeID, ContactTypeName)
    VALUES (1, N'Normal');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.ContactTypes WHERE ContactTypeID = 2)
BEGIN
    INSERT INTO dbo.ContactTypes (ContactTypeID, ContactTypeName)
    VALUES (2, N'Emergency');
END;

SET IDENTITY_INSERT dbo.ContactTypes OFF;
/* ContactTypes seed ends */
GO

/* PolicyStatuses seed begins */
SET IDENTITY_INSERT dbo.PolicyStatuses ON;

IF NOT EXISTS (SELECT 1 FROM dbo.PolicyStatuses WHERE PolicyStatusID = 1)
BEGIN
    INSERT INTO dbo.PolicyStatuses (PolicyStatusID, PolicyStatusText)
    VALUES (1, N'Draft');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PolicyStatuses WHERE PolicyStatusID = 2)
BEGIN
    INSERT INTO dbo.PolicyStatuses (PolicyStatusID, PolicyStatusText)
    VALUES (2, N'Awaiting Approval');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PolicyStatuses WHERE PolicyStatusID = 3)
BEGIN
    INSERT INTO dbo.PolicyStatuses (PolicyStatusID, PolicyStatusText)
    VALUES (3, N'Active');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PolicyStatuses WHERE PolicyStatusID = 4)
BEGIN
    INSERT INTO dbo.PolicyStatuses (PolicyStatusID, PolicyStatusText)
    VALUES (4, N'Archived');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PolicyStatuses WHERE PolicyStatusID = 5)
BEGIN
    INSERT INTO dbo.PolicyStatuses (PolicyStatusID, PolicyStatusText)
    VALUES (5, N'Suspended');
END;

SET IDENTITY_INSERT dbo.PolicyStatuses OFF;
/* PolicyStatuses seed ends */
GO

/* AppointmentStatuses seed begins */
SET IDENTITY_INSERT dbo.AppointmentStatuses ON;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 1)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (1, N'Scheduled');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 2)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (2, N'Reminder Sent');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 3)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (3, N'Reminder Viewed');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 4)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (4, N'Arrived');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 5)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (5, N'In Room');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 6)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (6, N'Completed');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 7)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (7, N'Cancelled');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 8)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (8, N'No Show');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentStatuses WHERE AppointmentStatusID = 9)
BEGIN
    INSERT INTO dbo.AppointmentStatuses (AppointmentStatusID, AppointmentStatusText)
    VALUES (9, N'Rescheduled');
END;

SET IDENTITY_INSERT dbo.AppointmentStatuses OFF;
/* AppointmentStatuses seed ends */
GO

/* AppointmentTypes seed begins */
SET IDENTITY_INSERT dbo.AppointmentTypes ON;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentTypes WHERE AppointmentTypeID = 1)
BEGIN
    INSERT INTO dbo.AppointmentTypes (AppointmentTypeID, AppointmentTypeText)
    VALUES (1, N'New Patient');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentTypes WHERE AppointmentTypeID = 2)
BEGIN
    INSERT INTO dbo.AppointmentTypes (AppointmentTypeID, AppointmentTypeText)
    VALUES (2, N'Routine Test');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentTypes WHERE AppointmentTypeID = 3)
BEGIN
    INSERT INTO dbo.AppointmentTypes (AppointmentTypeID, AppointmentTypeText)
    VALUES (3, N'Routine Care');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentTypes WHERE AppointmentTypeID = 4)
BEGIN
    INSERT INTO dbo.AppointmentTypes (AppointmentTypeID, AppointmentTypeText)
    VALUES (4, N'Chronic Care');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.AppointmentTypes WHERE AppointmentTypeID = 5)
BEGIN
    INSERT INTO dbo.AppointmentTypes (AppointmentTypeID, AppointmentTypeText)
    VALUES (5, N'Treatment');
END;

SET IDENTITY_INSERT dbo.AppointmentTypes OFF;
/* AppointmentTypes seed ends */
GO

/* insuranceTypes seed begins */
SET IDENTITY_INSERT dbo.insuranceTypes ON;

IF NOT EXISTS (SELECT 1 FROM dbo.insuranceTypes WHERE InsurenceTypeID = 1)
BEGIN
    INSERT INTO dbo.insuranceTypes (InsurenceTypeID, InsuranceTypeName)
    VALUES (1, N'Government Insurance');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.insuranceTypes WHERE InsurenceTypeID = 2)
BEGIN
    INSERT INTO dbo.insuranceTypes (InsurenceTypeID, InsuranceTypeName)
    VALUES (2, N'Private');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.insuranceTypes WHERE InsurenceTypeID = 3)
BEGIN
    INSERT INTO dbo.insuranceTypes (InsurenceTypeID, InsuranceTypeName)
    VALUES (3, N'Family Deductible');
END;

SET IDENTITY_INSERT dbo.insuranceTypes OFF;
/* insuranceTypes seed ends */
GO

/* insuranceStatuses seed begins */
SET IDENTITY_INSERT dbo.insuranceStatuses ON;

IF NOT EXISTS (SELECT 1 FROM dbo.insuranceStatuses WHERE InsuranceStatusID = 1)
BEGIN
    INSERT INTO dbo.insuranceStatuses (InsuranceStatusID, InsuranceStatusText)
    VALUES (1, N'Active');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.insuranceStatuses WHERE InsuranceStatusID = 2)
BEGIN
    INSERT INTO dbo.insuranceStatuses (InsuranceStatusID, InsuranceStatusText)
    VALUES (2, N'Expired');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.insuranceStatuses WHERE InsuranceStatusID = 3)
BEGIN
    INSERT INTO dbo.insuranceStatuses (InsuranceStatusID, InsuranceStatusText)
    VALUES (3, N'Under Review');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.insuranceStatuses WHERE InsuranceStatusID = 4)
BEGIN
    INSERT INTO dbo.insuranceStatuses (InsuranceStatusID, InsuranceStatusText)
    VALUES (4, N'Rejected');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.insuranceStatuses WHERE InsuranceStatusID = 5)
BEGIN
    INSERT INTO dbo.insuranceStatuses (InsuranceStatusID, InsuranceStatusText)
    VALUES (5, N'Submitted');
END;

SET IDENTITY_INSERT dbo.insuranceStatuses OFF;
/* insuranceStatuses seed ends */
GO

/* PaymentStatuses seed begins */
SET IDENTITY_INSERT dbo.PaymentStatuses ON;

IF NOT EXISTS (SELECT 1 FROM dbo.PaymentStatuses WHERE PaymentStatusID = 1)
BEGIN
    INSERT INTO dbo.PaymentStatuses (PaymentStatusID, PaymentStatusText)
    VALUES (1, N'Pending');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PaymentStatuses WHERE PaymentStatusID = 2)
BEGIN
    INSERT INTO dbo.PaymentStatuses (PaymentStatusID, PaymentStatusText)
    VALUES (2, N'Completed');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PaymentStatuses WHERE PaymentStatusID = 3)
BEGIN
    INSERT INTO dbo.PaymentStatuses (PaymentStatusID, PaymentStatusText)
    VALUES (3, N'Cancelled');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PaymentStatuses WHERE PaymentStatusID = 4)
BEGIN
    INSERT INTO dbo.PaymentStatuses (PaymentStatusID, PaymentStatusText)
    VALUES (4, N'Declined');
END;

SET IDENTITY_INSERT dbo.PaymentStatuses OFF;
/* PaymentStatuses seed ends */
GO

/* PenaltyStatuses seed begins */
SET IDENTITY_INSERT dbo.PenaltyStatuses ON;

IF NOT EXISTS (SELECT 1 FROM dbo.PenaltyStatuses WHERE PenaltyStatusID = 1)
BEGIN
    INSERT INTO dbo.PenaltyStatuses (PenaltyStatusID, PenaltyStatusText)
    VALUES (1, N'Unpaid');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PenaltyStatuses WHERE PenaltyStatusID = 2)
BEGIN
    INSERT INTO dbo.PenaltyStatuses (PenaltyStatusID, PenaltyStatusText)
    VALUES (2, N'Paid');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PenaltyStatuses WHERE PenaltyStatusID = 3)
BEGIN
    INSERT INTO dbo.PenaltyStatuses (PenaltyStatusID, PenaltyStatusText)
    VALUES (3, N'Overdue');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.PenaltyStatuses WHERE PenaltyStatusID = 4)
BEGIN
    INSERT INTO dbo.PenaltyStatuses (PenaltyStatusID, PenaltyStatusText)
    VALUES (4, N'Suspended');
END;

SET IDENTITY_INSERT dbo.PenaltyStatuses OFF;
/* PenaltyStatuses seed ends */
GO

/* TestsStatuses seed begins */
SET IDENTITY_INSERT dbo.TestsStatuses ON;

IF NOT EXISTS (SELECT 1 FROM dbo.TestsStatuses WHERE TestStatusID = 1)
BEGIN
    INSERT INTO dbo.TestsStatuses (TestStatusID, TestStatusText)
    VALUES (1, N'Ordered');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.TestsStatuses WHERE TestStatusID = 2)
BEGIN
    INSERT INTO dbo.TestsStatuses (TestStatusID, TestStatusText)
    VALUES (2, N'Scheduled');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.TestsStatuses WHERE TestStatusID = 3)
BEGIN
    INSERT INTO dbo.TestsStatuses (TestStatusID, TestStatusText)
    VALUES (3, N'Collected');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.TestsStatuses WHERE TestStatusID = 4)
BEGIN
    INSERT INTO dbo.TestsStatuses (TestStatusID, TestStatusText)
    VALUES (4, N'In Progress');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.TestsStatuses WHERE TestStatusID = 5)
BEGIN
    INSERT INTO dbo.TestsStatuses (TestStatusID, TestStatusText)
    VALUES (5, N'Pending');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.TestsStatuses WHERE TestStatusID = 6)
BEGIN
    INSERT INTO dbo.TestsStatuses (TestStatusID, TestStatusText)
    VALUES (6, N'Completed');
END;

SET IDENTITY_INSERT dbo.TestsStatuses OFF;
/* TestsStatuses seed ends */
GO

/* StaffRoles seed begins */
SET IDENTITY_INSERT dbo.StaffRoles ON;

IF NOT EXISTS (SELECT 1 FROM dbo.StaffRoles WHERE RoleID = 1)
BEGIN
    INSERT INTO dbo.StaffRoles (RoleID, RoleName)
    VALUES (1, N'Receptionist');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StaffRoles WHERE RoleID = 2)
BEGIN
    INSERT INTO dbo.StaffRoles (RoleID, RoleName)
    VALUES (2, N'Nurse');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StaffRoles WHERE RoleID = 3)
BEGIN
    INSERT INTO dbo.StaffRoles (RoleID, RoleName)
    VALUES (3, N'Doctor');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StaffRoles WHERE RoleID = 4)
BEGIN
    INSERT INTO dbo.StaffRoles (RoleID, RoleName)
    VALUES (4, N'Owner');
END;

SET IDENTITY_INSERT dbo.StaffRoles OFF;
/* StaffRoles seed ends */
GO

/* ReferralUrgencies seed begins */
SET IDENTITY_INSERT dbo.ReferralUrgencies ON;

IF NOT EXISTS (SELECT 1 FROM dbo.ReferralUrgencies WHERE ReferralUrgencyID = 1)
BEGIN
    INSERT INTO dbo.ReferralUrgencies (ReferralUrgencyID, ReferralUrgencyLevel)
    VALUES (1, N'Immediate');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.ReferralUrgencies WHERE ReferralUrgencyID = 2)
BEGIN
    INSERT INTO dbo.ReferralUrgencies (ReferralUrgencyID, ReferralUrgencyLevel)
    VALUES (2, N'Urgent');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.ReferralUrgencies WHERE ReferralUrgencyID = 3)
BEGIN
    INSERT INTO dbo.ReferralUrgencies (ReferralUrgencyID, ReferralUrgencyLevel)
    VALUES (3, N'Priority');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.ReferralUrgencies WHERE ReferralUrgencyID = 4)
BEGIN
    INSERT INTO dbo.ReferralUrgencies (ReferralUrgencyID, ReferralUrgencyLevel)
    VALUES (4, N'Routine');
END;

SET IDENTITY_INSERT dbo.ReferralUrgencies OFF;
/* ReferralUrgencies seed ends */
GO

/* Schedules seed begins */
SET IDENTITY_INSERT dbo.Schedules ON;

IF NOT EXISTS (SELECT 1 FROM dbo.Schedules WHERE ScheduleID = 1)
BEGIN
    INSERT INTO dbo.Schedules (ScheduleID, WorkStartFrom, WorkEndAt, ShiftName)
    VALUES (1, '08:00:00', '17:00:00', N'Day Shift');
END;

SET IDENTITY_INSERT dbo.Schedules OFF;
/* Schedules seed ends */
GO

/* DosageForms seed begins */
SET IDENTITY_INSERT dbo.DosageForms ON;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 1)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (1, N'Tablet');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 2)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (2, N'Capsule');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 3)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (3, N'Syrup');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 4)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (4, N'Suspension');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 5)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (5, N'Injection');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 6)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (6, N'Cream');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 7)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (7, N'Ointment');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 8)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (8, N'Gel');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 9)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (9, N'Eye Drops');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 10)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (10, N'Ear Drops');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 11)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (11, N'Nasal Spray');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 12)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (12, N'Inhaler');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 13)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (13, N'Suppository');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 14)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (14, N'Patch');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.DosageForms WHERE DosageFormID = 15)
BEGIN
    INSERT INTO dbo.DosageForms (DosageFormID, DosageFormText)
    VALUES (15, N'Lozenge');
END;

SET IDENTITY_INSERT dbo.DosageForms OFF;
/* DosageForms seed ends */
GO

/* StrengthUnits seed begins */
SET IDENTITY_INSERT dbo.StrengthUnits ON;

IF NOT EXISTS (SELECT 1 FROM dbo.StrengthUnits WHERE StrengthUnitID = 1)
BEGIN
    INSERT INTO dbo.StrengthUnits (StrengthUnitID, StrengthUnitText)
    VALUES (1, N'mcg');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StrengthUnits WHERE StrengthUnitID = 2)
BEGIN
    INSERT INTO dbo.StrengthUnits (StrengthUnitID, StrengthUnitText)
    VALUES (2, N'mg');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StrengthUnits WHERE StrengthUnitID = 3)
BEGIN
    INSERT INTO dbo.StrengthUnits (StrengthUnitID, StrengthUnitText)
    VALUES (3, N'g');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StrengthUnits WHERE StrengthUnitID = 4)
BEGIN
    INSERT INTO dbo.StrengthUnits (StrengthUnitID, StrengthUnitText)
    VALUES (4, N'mL');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StrengthUnits WHERE StrengthUnitID = 5)
BEGIN
    INSERT INTO dbo.StrengthUnits (StrengthUnitID, StrengthUnitText)
    VALUES (5, N'mg/mL');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StrengthUnits WHERE StrengthUnitID = 6)
BEGIN
    INSERT INTO dbo.StrengthUnits (StrengthUnitID, StrengthUnitText)
    VALUES (6, N'%');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StrengthUnits WHERE StrengthUnitID = 7)
BEGIN
    INSERT INTO dbo.StrengthUnits (StrengthUnitID, StrengthUnitText)
    VALUES (7, N'IU');
END;

IF NOT EXISTS (SELECT 1 FROM dbo.StrengthUnits WHERE StrengthUnitID = 8)
BEGIN
    INSERT INTO dbo.StrengthUnits (StrengthUnitID, StrengthUnitText)
    VALUES (8, N'IU/mL');
END;

SET IDENTITY_INSERT dbo.StrengthUnits OFF;
/* StrengthUnits seed ends */
GO
