namespace Data_Access.DTOs
{
    public class PrescriptionItemDto
    {
        public int ItemID { get; set; }
        public int MedicineID { get; set; }
        public int PrescriptionID { get; set; }
        public byte Dose { get; set; }
        public string Frequency { get; set; }
        public byte Quantity { get; set; }
        public string Duration { get; set; }
    }
}
