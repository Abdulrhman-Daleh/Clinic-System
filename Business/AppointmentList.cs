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
        public static async Task<BindingList<AppointmentList>> GetAppointment()
        {
            BindingList<AppointmentList> appointments = new BindingList<AppointmentList>();
            var AppointmentList = new BindingList<AppointmentListDto>();

            if (Util.CanAccessForm(AppEnums.cs.EnRole.Owner, EnRole.Receptionist).IsSuccess)
                AppointmentList = await AppointmentData.GetAllAppointments();
            else
                AppointmentList = await AppointmentData.GetStaffAppointments(Global.CurrentLoggedInStaff.StaffId);

            foreach (var appo in AppointmentList)
                appointments.Add(FromDto(appo));

            return appointments;
        }
        public static async Task<AppointmentList> FindAppointmentListByID(int appointmentId)
        {
            return FromDto(await AppointmentData.FindAppointmentListByID(appointmentId));
        }
    }
}
