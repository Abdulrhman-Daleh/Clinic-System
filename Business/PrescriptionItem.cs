using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class PrescriptionItem
    {
        private EnMode _mode;
        public int ItemID { get; set; }
        public int MedicineID { get; set; }
        public Medicine Medicine { get; set; }
        public int PrescriptionID { get; set; }
        public byte Dose { get; set; }
        public string Frequency { get; set; }
        public byte Quantity { get; set; }
        public string Duration { get; set; }

        public bool IsAddMode()
        {
            return _mode == EnMode.Add;
        }

        public PrescriptionItem()
        {
            ItemID = -1;
            MedicineID = -1;
            PrescriptionID = -1;
            Frequency = string.Empty;
            Duration = string.Empty;
            _mode = EnMode.Add;
        }

        private PrescriptionItem(int itemID)
        {
            ItemID = itemID;
            _mode = EnMode.Update;
        }

        public static PrescriptionItem FromDto(PrescriptionItemDto itemDto)
        {
            if (itemDto == null)
                return null;

            return new PrescriptionItem(itemDto.ItemID)
            {
                ItemID = itemDto.ItemID,
                MedicineID = itemDto.MedicineID,
                PrescriptionID = itemDto.PrescriptionID,
                Dose = itemDto.Dose,
                Frequency = itemDto.Frequency,
                Quantity = itemDto.Quantity,
                Duration = itemDto.Duration
            };
        }

        public static PrescriptionItemDto ToDto(PrescriptionItem item)
        {
            if (item == null)
                return null;

            return new PrescriptionItemDto()
            {
                ItemID = item.ItemID,
                MedicineID = item.MedicineID,
                PrescriptionID = item.PrescriptionID,
                Dose = item.Dose,
                Frequency = item.Frequency,
                Quantity = item.Quantity,
                Duration = item.Duration
            };
        }

        private bool AddNewPrescriptionItem()
        {
            ItemID = PrescriptionItemData.AddNewPrescriptionItem(ToDto(this));

            return ItemID > 0;
        }

        private bool UpdatePrescriptionItem()
        {
            return PrescriptionItemData.UpdatePrescriptionItem(ToDto(this));
        }

        public decimal CalculateLineTotal()
        {
            return Quantity * Medicine.UnitPrice;
        }
        public OperationResult Save()
        {
            OperationResult result = IsPrescriptionItemValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy = Global.CurrentLoggedInStaff == null
                ? "Initial setup"
                : $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case EnMode.Add:
                    _mode = EnMode.Update;

                    Auditor.Log(
                        "Prescription Item Added",
                        $"MedicineID {MedicineID} was prescribed with dose {Dose}, {Frequency}, quantity {Quantity}, for {Duration} by {performedBy}.",
                        $"PrescriptionItemID {ItemID}");

                    return new OperationResult(
                        "Prescription item added successfully.",
                        "Added",
                        true);

                case EnMode.Update:

                    if (UpdatePrescriptionItem())
                    {
                        Auditor.Log(
                            "Prescription Item Updated",
                            $"MedicineID {MedicineID} prescription details (dose {Dose}, {Frequency}, quantity {Quantity}, duration {Duration}) were updated by {performedBy}.",
                            $"PrescriptionItemID {ItemID}");

                        return new OperationResult(
                            "Prescription item updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update prescription item.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid prescription item operation mode.",
                "Failed",
                false);
        }

        public static PrescriptionItem FindPrescriptionItemByID(int itemID)
        {
            return FromDto(PrescriptionItemData.FindPrescriptionItemByID(itemID));
        }

        public static async Task<BindingList<PrescriptionItem>> GetPrescriptionItems(int prescriptionID)
        {
            BindingList<PrescriptionItem> items = new BindingList<PrescriptionItem>();

            foreach (PrescriptionItemDto itemDto in
                await PrescriptionItemData.GetPrescriptionItems(prescriptionID))
            {
                items.Add(FromDto(itemDto));
            }

            return items;
        }

        private OperationResult IsPrescriptionItemValidToSave()
        {
            if (Util.IsValidId(MedicineID) &&
                Dose > 0 &&
                Quantity > 0 &&
                Util.IsRequired(Frequency) &&
                Util.IsRequired(Duration))
            {
                return new OperationResult(
                    "Prescription item information is valid to save.",
                    "Valid",
                    true);
            }

            return new OperationResult(
                "Prescription item information is invalid.",
                "Invalid",
                false);
        }
    }
}
