
namespace Data_Access.ListingDTOs
{
    public class PrescriptionItemListingDto
    {
        public int ItemID { get; set; }
        public int MedicineID { get; set; }
        public int PrescriptionID { get; set; }
        public string DrugName { get; set; }
        public decimal StrengthValue { get; set; }
        public string StrengthUnitText { get; set; }
        public string DosageFormText { get; set; }
        public byte Dose { get; set; }
        public string Frequency { get; set; }
        public byte Quantity { get; set; }
        public string Duration { get; set; }
        public decimal LineTotal { get; set; }
    }
}
