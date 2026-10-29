namespace Data_Access.ListingDTOs
{
    public class AppointmentListDto
    {
        public int AppointmentID { get; set; }
        public string Time { get; set; }
        public string Patient { get; set; }
        public string Doctor { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
