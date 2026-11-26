using AppEnums.cs;
using Data_Access;
using Data_Access.ListingDTOs;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class AppointmentList
    {
        public int AppointmentID { get; set; }
        public string Time { get; set; }
        public string Patient { get; set; }
        public string Doctor { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public static (int BookingCount, int CompletedCount, int CancelledCount) AppointmentSummary = (0, 0, 0);
        private static AppointmentList FromDto(AppointmentListDto appointmentList)
        {
            if (appointmentList == null)
                return new AppointmentList();

            return new AppointmentList()
            {
                AppointmentID = appointmentList.AppointmentID,
                Time = appointmentList.Time,
                Patient = appointmentList.Patient,
                Type = appointmentList.Type,
                Status = appointmentList.Status,
                Doctor = appointmentList.Doctor
            };
        }
        private static int PageNumber = 0;
        private static int RowsPerPage = 2;
        private static int RowsToSkip => PageNumber * RowsPerPage;
        public static async Task<BindingList<AppointmentList>> GetAppointment(int pageNumber)
        {
            PageNumber = pageNumber;
            BindingList<AppointmentList> appointments = new BindingList<AppointmentList>();
            var AppointmentList = new BindingList<AppointmentListDto>();
            AppointmentSummary = (0, 0, 0);

            if (Util.CanAccessForm(EnRole.Owner, EnRole.Receptionist).IsSuccess)
                AppointmentList = await AppointmentData.GetAllAppointments(RowsPerPage, RowsToSkip);
            else
                AppointmentList = await AppointmentData.GetStaffAppointments(Global.CurrentLoggedInStaff.StaffId);

            foreach (var appo in AppointmentList)
            {
                appointments.Add(FromDto(appo));
                UpdateAppointmentSummary(appo);
            }

            AppointmentSummary.BookingCount = appointments.Count;
            return appointments;
        }

        private static void UpdateAppointmentSummary(AppointmentListDto appointment)
        {
            if (appointment.Status.Equals(EnAppointmentStatuses.Completed.ToString()))
                AppointmentSummary.CompletedCount++;
            else if (appointment.Status.Equals(EnAppointmentStatuses.Cancelled.ToString()))
                AppointmentSummary.CancelledCount++;
        }
        public static async Task<AppointmentList> FindAppointmentListByID(int appointmentId)
        {
            return FromDto(await AppointmentData.FindAppointmentListByID(appointmentId));
        }
    }
}
