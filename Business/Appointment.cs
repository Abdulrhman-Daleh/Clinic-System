using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class Appointment
    {
        private EnMode _mode;
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int StaffID { get; set; }
        public (EnAppointmentStatuses AppointmentStatusID, string AppointmentStatusText) AppointmentStatuses { get; set; }
        public (EnAppointmentTypes AppointmentTypeID, string AppointmentTypeText) AppointmentTypes { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public TimeSpan AppointmentEndTime { get; set; }
        public Patient Patient { get; set; }
        public Staff Staff { get; set; }
        public Appointment()
        {
            AppointmentID = -1;
            PatientID = -1;
            StaffID = -1;
            AppointmentStatuses = ((EnAppointmentStatuses)EnAppointmentStatuses.Scheduled, "Scheduled");
            AppointmentTypes = ((EnAppointmentTypes)0, "None");
            AppointmentDate = DateTime.Now;
            AppointmentTime = new TimeSpan();
            AppointmentEndTime = new TimeSpan();
            Patient = new Patient();
            Staff = new Staff();
            _mode = EnMode.Add;
        }

        private static AppointmentDto ToDto(Appointment appointment)
        {
            return new AppointmentDto()
            {
                AppointmentID = appointment.AppointmentID,
                PatientID = appointment.PatientID,
                StaffID = appointment.StaffID,
                AppointmentStatuses = (appointment.AppointmentStatuses.AppointmentStatusID, appointment.AppointmentStatuses.AppointmentStatusText),
                AppointmentTypes = (appointment.AppointmentTypes.AppointmentTypeID, appointment.AppointmentTypes.AppointmentTypeText),
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                AppointmentEndTime = appointment.AppointmentEndTime
            };
        }

        private static Appointment FromDto(AppointmentDto appointment)
        {
            return new Appointment()
            {
                AppointmentID = appointment.AppointmentID,
                PatientID = appointment.PatientID,
                StaffID = appointment.StaffID,
                AppointmentStatuses = appointment.AppointmentStatuses,
                AppointmentTypes = appointment.AppointmentTypes,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                AppointmentEndTime = appointment.AppointmentEndTime,
                _mode = EnMode.Update,

                Staff = Staff.FindStaffById(appointment.StaffID),
                Patient = Patient.FindPatientById(appointment.PatientID)
            };
        }
        public static async Task<BindingList<string>> GetAppointmentTypesNames()
        {
            return await AppointmentData.GetAppointmentTypesNames();
        }

        private bool AddNewAppointment()
        {
            AppointmentID = AppointmentData.AddNewAppointment(ToDto(this));

            return AppointmentID > 0;
        }

        private bool UpdateAppointment()
        {
            return AppointmentData.UpdateAppointment(ToDto(this)) > 0;
        }

        public bool Save()
        {
            if (!IsAppointmentValidToSave())
                return false;

            switch (_mode)
            {
                case EnMode.Add:
                    if (AddNewAppointment())
                    {
                        _mode = EnMode.Update;
                        Auditor.Log("Appointment Added", $"Appointment for PatientID {PatientID} with StaffID {StaffID} was scheduled for {AppointmentDate} at {AppointmentTime} by StaffID {Global.CurrentLoggedInStaff.StaffId}.", $"AppointmentID {AppointmentID}");
                        return true;
                    }
                    return false;

                case EnMode.Update:
                    if (UpdateAppointment())
                    {
                        Auditor.Log("Appointment Updated", $"Appointment for PatientID {PatientID} with StaffID {StaffID} was rescheduled to {AppointmentDate} at {AppointmentTime} by StaffID {Global.CurrentLoggedInStaff.StaffId}.", $"AppointmentID {AppointmentID}");
                        return true;
                    }
                    return false;
            }

            return false;
        }

        public bool NoOverlapWithLastAppointment()
        {
            return AppointmentData.NoOverlapWithLastAppointment(ToDto(this));
        }

        public static Appointment FindAppointmentByID(int appointmentId)
        {
            return FromDto(AppointmentData.FindAppointmentByID(appointmentId));
        }

        private bool IsAppointmentValidToSave()
        {
            return Util.IsValidId(PatientID) && Util.IsValidId(StaffID) && Patient != null && Staff != null;
        }

    }
}
