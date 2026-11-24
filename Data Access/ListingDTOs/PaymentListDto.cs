namespace Data_Access.ListingDTOs
{
    public class PaymentListDto
    {
        public int PaymentID { get; set; }
        public string Patient { get; set; }
        public string Doctor { get; set; }
        public int AppointmentID { get; set; }
        public decimal Amount { get; set; }
        public string ProceedBy { get; set; }
        public int TotalPayments { get; set; }
    }
}
