-- ContactTypes seed begins

Set Identity_Insert ContactTypes ON
if NOT EXISTS (select 1 from ContactTypes where ContactTypeID=1)begin
    insert into ContactTypes(ContactTypeID, ContactTypeName)
    values(1, N'Normal')
end
if NOT EXISTS (select 1 from ContactTypes where ContactTypeID=2)begin
    insert into ContactTypes(ContactTypeID, ContactTypeName)
    values(2, N'Emergency')
end
Set Identity_Insert ContactTypes OFF
-- ContactTypes seed ends

-- PolicyStatuses seed begins
SET Identity_Insert PolicyStatuses ON
IF NOT EXISTS (SELECT 1 FROM PolicyStatuses WHERE PolicyStatusID=1)BEGIN
    INSERT INTO PolicyStatuses(PolicyStatusID, PolicyStatusText)
    VALUES(1, N'Draft')
END
IF NOT EXISTS (SELECT 1 FROM PolicyStatuses WHERE PolicyStatusID=2)BEGIN
    INSERT INTO PolicyStatuses(PolicyStatusID, PolicyStatusText)
    VALUES(2, N'Awaiting Approval')
END
IF NOT EXISTS (SELECT 1 FROM PolicyStatuses WHERE PolicyStatusID=3)BEGIN
    INSERT INTO PolicyStatuses(PolicyStatusID, PolicyStatusText)
    VALUES(3, N'Active')
END
IF NOT EXISTS (SELECT 1 FROM PolicyStatuses WHERE PolicyStatusID=4)BEGIN
    INSERT INTO PolicyStatuses(PolicyStatusID, PolicyStatusText)
    VALUES(4, N'Archived')
END
IF NOT EXISTS (SELECT 1 FROM PolicyStatuses WHERE PolicyStatusID=5)BEGIN
    INSERT INTO PolicyStatuses(PolicyStatusID, PolicyStatusText)
    VALUES(5, N'Suspended')
END
SET Identity_Insert PolicyStatuses OFF
-- PolicyStatuses seed ends

-- AppointmentStatuses seed begins
SET Identity_Insert AppointmentStatuses ON
if NOT EXISTS (select 1 from AppointmentStatuses where AppointmentStatusID=1)begin
    insert into AppointmentStatuses(AppointmentStatusID, AppointmentStatusText)
    values(1, N'Scheduled')
end
if NOT EXISTS (select 1 from AppointmentStatuses where AppointmentStatusID=2)begin
    insert into AppointmentStatuses(AppointmentStatusID, AppointmentStatusText)
    values(2, N'Reminder Sent')
end
if NOT EXISTS (select 1 from AppointmentStatuses where AppointmentStatusID=3)begin
    insert into AppointmentStatuses(AppointmentStatusID, AppointmentStatusText)
    values(3, N'Remainder Viewed')
end
if NOT EXISTS (select 1 from AppointmentStatuses where AppointmentStatusID=4)begin
    insert into AppointmentStatuses(AppointmentStatusID, AppointmentStatusText)
    values(4, N'Arrived')
end
if NOT EXISTS (select 1 from AppointmentStatuses where AppointmentStatusID=5)begin
    insert into AppointmentStatuses(AppointmentStatusID, AppointmentStatusText)
    values(5, N'In Room')
end
if NOT EXISTS (select 1 from AppointmentStatuses where AppointmentStatusID=6)begin
    insert into AppointmentStatuses(AppointmentStatusID, AppointmentStatusText)
    values(6, N'Completed')
end
if NOT EXISTS (select 1 from AppointmentStatuses where AppointmentStatusID=7)begin
    insert into AppointmentStatuses(AppointmentStatusID, AppointmentStatusText)
    values(7, N'Cancelled')
end
if NOT EXISTS (select 1 from AppointmentStatuses where AppointmentStatusID=8)begin
    insert into AppointmentStatuses(AppointmentStatusID, AppointmentStatusText)
    values(8, N'No Show')
end
if NOT EXISTS (select 1 from AppointmentStatuses where AppointmentStatusID=9)begin
    insert into AppointmentStatuses(AppointmentStatusID, AppointmentStatusText)
    values(9, N'Rescheduled')
end
SET Identity_Insert AppointmentStatuses OFF

-- AppointmentStatuses seed ends

-- AppointmentTypes seed begins
SET Identity_Insert AppointmentTypes ON
if NOT EXISTS (select 1 from AppointmentTypes where AppointmentTypeID=1)begin
    insert into AppointmentTypes(AppointmentTypeID, AppointmentTypeText)
    values(1, N'New Register')
end
if NOT EXISTS (select 1 from AppointmentTypes where AppointmentTypeID=2)begin
    insert into AppointmentTypes(AppointmentTypeID, AppointmentTypeText)
    values(2, N'Routine Test')
end
if NOT EXISTS (select 1 from AppointmentTypes where AppointmentTypeID=3)begin
    insert into AppointmentTypes(AppointmentTypeID, AppointmentTypeText)
    values(3, N'Routine Care')
end
if NOT EXISTS (select 1 from AppointmentTypes where AppointmentTypeID=4)begin
    insert into AppointmentTypes(AppointmentTypeID, AppointmentTypeText)
    values(4, N'Chronic Care')
end
if NOT EXISTS (select 1 from AppointmentTypes where AppointmentTypeID=5)begin
    insert into AppointmentTypes(AppointmentTypeID, AppointmentTypeText)
    values(5, N'Treatment')
end
SET Identity_Insert AppointmentTypes OFF
-- AppointmentTypes seed ends

-- insuranceTypes seed begins
SET Identity_Insert insuranceTypes ON
if NOT EXISTS (select 1 from insuranceTypes where InsurenceTypeID=1)begin
    insert into insuranceTypes(InsurenceTypeID, InsurenceTypeName)
    values(1, N'Government Insurance')
end
if NOT EXISTS (select 1 from insuranceTypes where InsurenceTypeID=2)begin
    insert into insuranceTypes(InsurenceTypeID, InsurenceTypeName)
    values(2, N'Private')
end
if NOT EXISTS (select 1 from insuranceTypes where InsurenceTypeID=3)begin
    insert into insuranceTypes(InsurenceTypeID, InsurenceTypeName)
    values(3, N'Family Deductible')
end
SET Identity_Insert insuranceTypes OFF
-- insuranceTypes seed ends

-- insuranceStatuses seed begins
SET Identity_Insert insuranceStatuses ON
if NOT EXISTS (select 1 from insuranceStatuses where insuranceStatusID=1)begin
    insert into insuranceStatuses(insuranceStatusID, insuranceStatusID)
    values(1, N'Active')
end
if NOT EXISTS (select 1 from insuranceStatuses where insuranceStatusID=2)begin
    insert into insuranceStatuses(insuranceStatusID, insuranceStatusID)
    values(2, N'Expired')
end
if NOT EXISTS (select 1 from insuranceStatuses where insuranceStatusID=3)begin
    insert into insuranceStatuses(insuranceStatusID, insuranceStatusID)
    values(3, N'Under Reviewing')
end
if NOT EXISTS (select 1 from insuranceStatuses where insuranceStatusID=4)begin
    insert into insuranceStatuses(insuranceStatusID, insuranceStatusID)
    values(4, N'Rejected')
end
if NOT EXISTS (select 1 from insuranceStatuses where insuranceStatusID=5)begin
    insert into insuranceStatuses(insuranceStatusID, insuranceStatusID)
    values(5, N'Submited')
end
SET Identity_Insert insuranceStatuses OFF
-- insuranceStatuses seed ends

-- PaymentStatuses seed begins
SET Identity_Insert PaymentStatuses ON
if NOT EXISTS (select 1 from PaymentStatuses where PaymentStatusID=1)begin
    insert into PaymentStatuses(PaymentStatusID, PaymentStatusText)
    values(1, N'Pending')
end
if NOT EXISTS (select 1 from PaymentStatuses where PaymentStatusID=2)begin
    insert into PaymentStatuses(PaymentStatusID, PaymentStatusText)
    values(2, N'Completed')
end
if NOT EXISTS (select 1 from PaymentStatuses where PaymentStatusID=3)begin
    insert into PaymentStatuses(PaymentStatusID, PaymentStatusText)
    values(3, N'Cancelled')
end
if NOT EXISTS (select 1 from PaymentStatuses where PaymentStatusID=4)begin
    insert into PaymentStatuses(PaymentStatusID, PaymentStatusText)
    values(4, N'Declined')
end
SET Identity_Insert PaymentStatuses OFF
-- PaymentStatuses seed ends

-- PenaltyStatuses seed begins
SET Identity_Insert PenaltyStatuses ON
if NOT EXISTS (select 1 from PenaltyStatuses where PenaltyStatusID=1)begin
    insert into PenaltyStatuses(PenaltyStatusID, PenaltyStatusText)
    values(1, N'Unpaid')
end
if NOT EXISTS (select 1 from PenaltyStatuses where PenaltyStatusID=2)begin
    insert into PenaltyStatuses(PenaltyStatusID, PenaltyStatusText)
    values(2, N'Paid')
end
if NOT EXISTS (select 1 from PenaltyStatuses where PenaltyStatusID=3)begin
    insert into PenaltyStatuses(PenaltyStatusID, PenaltyStatusText)
    values(3, N'Overdue')
end
if NOT EXISTS (select 1 from PenaltyStatuses where PenaltyStatusID=4)begin
    insert into PenaltyStatuses(PenaltyStatusID, PenaltyStatusText)
    values(4, N'Suspended')
end
SET Identity_Insert PenaltyStatuses OFF
-- PenaltyStatuses seed ends

-- TestsStatuses seed begins
SET Identity_Insert TestsStatuses ON
if NOT EXISTS (select 1 from TestsStatuses where TestStatusID=1)begin
    insert into TestsStatuses(TestStatusID, TestStatusText)
    values(1, N'Ordered')
end
if NOT EXISTS (select 1 from TestsStatuses where TestStatusID=1)begin
    insert into TestsStatuses(TestStatusID, TestStatusText)
    values(1, N'Scheduled')
end
if NOT EXISTS (select 1 from TestsStatuses where TestStatusID=1)begin
    insert into TestsStatuses(TestStatusID, TestStatusText)
    values(1, N'Collected ')
end
if NOT EXISTS (select 1 from TestsStatuses where TestStatusID=1)begin
    insert into TestsStatuses(TestStatusID, TestStatusText)
    values(1, N'In Progress')
end
if NOT EXISTS (select 1 from TestsStatuses where TestStatusID=1)begin
    insert into TestsStatuses(TestStatusID, TestStatusText)
    values(1, N'Pending')
end
if NOT EXISTS (select 1 from TestsStatuses where TestStatusID=1)begin
    insert into TestsStatuses(TestStatusID, TestStatusText)
    values(1, N'Completed')
end
SET Identity_Insert TestsStatuses OFF
-- TestsStatuses seed ends

-- StaffRoles seed begins
SET Identity_Insert StaffRoles ON
if NOT EXISTS (select 1 from StaffRoles where RoleID=1)begin
    insert into StaffRoles(RoleID, RoleName)values(1, N'Receptionist')
end
if NOT EXISTS (select 1 from StaffRoles where RoleID=2)begin
    insert into StaffRoles(RoleID, RoleName)values(2, N'Nurse')
end
if NOT EXISTS (select 1 from StaffRoles where RoleID=3)begin
    insert into StaffRoles(RoleID, RoleName)values(3, N'Doctor')
end
if NOT EXISTS (select 1 from StaffRoles where RoleID=4)begin
    insert into StaffRoles(RoleID, RoleName)values(4, N'Owner')
end
SET Identity_Insert StaffRoles OFF
-- StaffRoles seed ends

-- ReferralUrgencies seed begins
SET Identity_Insert ReferralUrgencies ON
if NOT EXISTS (select 1 from ReferralUrgencies where ReferralUrgencyID=1)begin
    insert into ReferralUrgencies(ReferralUrgencyID, ReferralUrgencyLevel)
    values(1, N'Immediate')
end
if NOT EXISTS (select 1 from ReferralUrgencies where ReferralUrgencyID=2)begin
    insert into ReferralUrgencies(ReferralUrgencyID, ReferralUrgencyLevel)
    values(2, N'Urgent')
end
if NOT EXISTS (select 1 from ReferralUrgencies where ReferralUrgencyID=3)begin
    insert into ReferralUrgencies(ReferralUrgencyID, ReferralUrgencyLevel)
    values(3, N'Priority')
end
if NOT EXISTS (select 1 from ReferralUrgencies where ReferralUrgencyID=4)begin
    insert into ReferralUrgencies(ReferralUrgencyID, ReferralUrgencyLevel)
    values(4, N'Routine')
end
SET Identity_Insert ReferralUrgencies OFF
-- ReferralUrgencies seed ends

-- Schedules seed ends
set Identity_Insert Schedules ON
if NOT EXISTS (select 1 from Schedules Having Count(*)>=1)begin
    insert into Schedules(ScheduleID, WorkStartFrom, WorkEndAt, ShiftName)
    values(1, '08:00:00', '17:00:00', 'Day Shift')
end
set Identity_Insert Schedules OFF
-- Schedules seed ends