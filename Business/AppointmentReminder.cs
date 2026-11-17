using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class AppointmentReminder
    {
        public int ReminderID { get; set; }
        public int StaffID { get; set; } = Global.CurrentLoggedInStaff?.StaffId ?? -1;
        public int AppointmentID { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string CreatedBy => Staff.GetFullName();
        [Browsable(false)]
        public Appointment Appointment { get; set; }
        [Browsable(false)]
        public Staff Staff { get; set; }
        public AppointmentReminder()
        {
            ReminderID = -1;
            AppointmentID = -1;
            ReminderDate = DateTime.MinValue;
            Title = null;
            Subject = null;
            Message = null;
            Appointment = null;
            Staff = Global.CurrentLoggedInStaff;
        }
        private AppointmentReminder(int staffId)
        {
            StaffID = staffId;
        }
        private static ReminderDto ToDTO(AppointmentReminder reminder)
        {
            return new ReminderDto()
            {
                AppointmentID = reminder.AppointmentID,
                ReminderDate = reminder.ReminderDate,
                Title = reminder.Title,
                Subject = reminder.Subject,
                Message = reminder.Message,
                StaffID = reminder.StaffID
            };
        }
        private static async Task<AppointmentReminder> FromDTO(ReminderDto reminderDto)
        {
            AppointmentReminder appReminder = new AppointmentReminder(reminderDto.StaffID)
            {
                ReminderDate = reminderDto.ReminderDate,
                Title = reminderDto.Title,
                Subject = reminderDto.Subject,
                Message = reminderDto.Message
            };

            appReminder.Appointment = await Appointment.FindAppointmentByID(appReminder.AppointmentID);
            appReminder.Staff = await Staff.FindStaffById(appReminder.StaffID);

            return appReminder;
        }
        private bool AddNewReminder()
        {
            ReminderID = ReminderData.AddNewReminder(ToDTO(this));

            return ReminderID > 0;
        }
        public OperationResult Save()
        {
            OperationResult result;

            result = CheckAccessPermission();

            if (!result.IsSuccess)
                return result;

            result = IsReminderValidToSave();

            if (!result.IsSuccess)
                return result;

            if (Global.CurrentLoggedInStaff == null)
                return new OperationResult("No user is logged in", "Not logged", false);

            string performedBy = Global.CurrentLoggedInStaff.GetFullName();

            if (!AddNewReminder())
                return new OperationResult("Failed to add appointment reminder", "Failed", false);

            Auditor.Log("Added appointment reminder", $"appointment reminder for Appointment {Appointment.AppointmentID} was created by {performedBy}.",
                            $"Reminder Id {ReminderID}");

            return new OperationResult("Appointment reminder got added successfully", "Added", true);
        }
        private OperationResult CheckAccessPermission()
        {
            OperationResult result = Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner);

            if (!result.IsSuccess)
                return result;

            if (!Appointment.IsStillActive())
                return new OperationResult("Appintment is no longer active", "Inactive", false);


            return new OperationResult("Reminder grant", "valid", true);
        }
        private OperationResult IsReminderValidToSave()
        {
            if (Appointment == null
                || Staff == null)
                return new OperationResult("Reminder is not valid to be saved", "Invalid", false);

            if (!Appointment.IsStillActive())
                return new OperationResult("Appointment is not longer active", "Not Active", false);

            if (!Util.IsValidId(AppointmentID) ||
                ReminderDate <= DateTime.MinValue ||
                !Util.IsRequired(Title) || !Util.IsRequired(Message) || !Util.IsValidId(StaffID))
            {
                return new OperationResult("Reminder is not valid to be saved", "Invalid", false);
            }

            return new OperationResult("Reminder is valid to be saved", "Valid", true);
        }
        public static async Task<BindingList<AppointmentReminder>> GetAppointmentReminders(int appointmentId)
        {
            if (!Util.CanAccessForm(EnRole.Receptionist, EnRole.Owner).IsSuccess)
                return null;

            BindingList<AppointmentReminder> reminders = new BindingList<AppointmentReminder>();

            foreach (var r in await ReminderData.GetAppointmentReminders(appointmentId))
                reminders.Add(await FromDTO(r));

            return reminders;
        }
    }
}
