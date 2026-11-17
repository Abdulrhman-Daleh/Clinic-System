using AppEnums.cs;

namespace Data_Access.DTOs
{
    public class MedicineDto
    {
        public int MedicineID { get; set; }
        public string DrugName { get; set; }
        public decimal StrengthValue { get; set; }
        public decimal UnitPrice { get; set; }
        public EnStrengthUnits StrengthType { get; set; }
        public EnDosageForms DosageForm { get; set; }
    }
}
