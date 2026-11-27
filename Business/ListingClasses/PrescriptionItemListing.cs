
using Data_Access;
using Data_Access.ListingDTOs;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class PrescriptionItemListing
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
        public string Medicine =>
            $"{DrugName} {StrengthValue:0.####}{StrengthUnitText} · {DosageFormText}";
        private static PrescriptionItemListing FromDto(PrescriptionItemListingDto itemDto)
        {
            if (itemDto == null)
                return null;

            return new PrescriptionItemListing()
            {
                ItemID = itemDto.ItemID,
                MedicineID = itemDto.MedicineID,
                PrescriptionID = itemDto.PrescriptionID,
                DrugName = itemDto.DrugName,
                StrengthValue = itemDto.StrengthValue,
                StrengthUnitText = itemDto.StrengthUnitText,
                DosageFormText = itemDto.DosageFormText,
                Dose = itemDto.Dose,
                Frequency = itemDto.Frequency,
                Quantity = itemDto.Quantity,
                Duration = itemDto.Duration,
                LineTotal = itemDto.LineTotal
            };
        }
        public static async Task<BindingList<PrescriptionItemListing>> GetPrescriptionItems(int prescriptionID)
        {
            BindingList<PrescriptionItemListingDto> itemDtos =
                await PrescriptionItemData.GetPrescriptionItemsListing(prescriptionID);

            BindingList<PrescriptionItemListing> items =
                new BindingList<PrescriptionItemListing>();

            foreach (PrescriptionItemListingDto itemDto in itemDtos)
                items.Add(FromDto(itemDto));

            return items;
        }
    }
}
