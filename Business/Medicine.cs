
using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class Medicine
    {
        private EnMode _mode;

        public int MedicineID { get; set; }
        public string DrugName { get; set; }
        public decimal StrengthValue { get; set; }

        [Browsable(false)]
        public (EnStrengthUnits StrengthTypeID, string StrengthTypeName) StrengthType { get; set; }

        [Browsable(false)]
        public (EnDosageForms DosageFormID, string DosageFormName) DosageForm { get; set; }

        public decimal UnitPrice { get; set; }

        // Computed properties for DataGridView
        public int StrengthUnitID => (int)StrengthType.StrengthTypeID;
        public string StrengthUnitText => StrengthType.StrengthTypeName;

        public int DosageFormID => (int)DosageForm.DosageFormID;
        public string DosageFormText => DosageForm.DosageFormName;

        public bool IsAddMode()
        {
            return _mode == EnMode.Add;
        }

        public static async Task<BindingList<string>> GetMedicineStrengthUnits()
        {
            BindingList<string> list = new BindingList<string>();

            foreach (var s in await MedicineData.GetMedicineStrengthUnits())
                list.Add(s);

            return list;
        }

        public static async Task<BindingList<string>> GetMedicineDosageForms()
        {
            BindingList<string> list = new BindingList<string>();

            foreach (var s in await MedicineData.GetMedicineDosageForms())
                list.Add(s);

            return list;
        }

        public Medicine()
        {
            MedicineID = -1;
            DrugName = string.Empty;
            StrengthType = ((EnStrengthUnits)0, "None");
            DosageForm = ((EnDosageForms)0, "None");
            _mode = EnMode.Add;
        }

        public Medicine(int medicineID)
        {
            MedicineID = medicineID;
            _mode = EnMode.Update;
        }

        public static Medicine FromDto(MedicineDto medicineDto)
        {
            if (medicineDto == null)
                return null;

            return new Medicine(medicineDto.MedicineID)
            {
                MedicineID = medicineDto.MedicineID,
                DrugName = medicineDto.DrugName,
                StrengthValue = medicineDto.StrengthValue,
                StrengthType = medicineDto.StrengthType,
                DosageForm = medicineDto.DosageForm,
                UnitPrice = medicineDto.UnitPrice
            };
        }

        public static MedicineDto ToDto(Medicine medicine)
        {
            if (medicine == null)
                return null;

            return new MedicineDto()
            {
                MedicineID = medicine.MedicineID,
                DrugName = medicine.DrugName,
                StrengthValue = medicine.StrengthValue,
                StrengthType = medicine.StrengthType,
                DosageForm = medicine.DosageForm,
                UnitPrice = medicine.UnitPrice
            };
        }

        private bool UpdateMedicine()
        {
            return MedicineData.UpdateMedicine(ToDto(this));
        }

        private bool AddMedicine()
        {
            MedicineID = MedicineData.AddNewMedicine(ToDto(this));

            return MedicineID > 0;
        }

        public OperationResult Save()
        {
            OperationResult result = IsMedicineValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy = Global.CurrentLoggedInStaff == null
                ? "Initial setup"
                : $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case EnMode.Add:
                    if (AddMedicine())
                    {
                        _mode = EnMode.Update;

                        Auditor.Log(
                            "Medicine Added",
                            $"Medicine '{DrugName}' ({StrengthValue} {StrengthType.StrengthTypeName}, {DosageForm.DosageFormName}) was added by {performedBy}.",
                            $"MedicineID {MedicineID}");

                        return new OperationResult(
                            "Medicine added successfully.",
                            "Added",
                            true);
                    }

                    return new OperationResult(
                        "Failed to add medicine.",
                        "Failed",

                        false);
                case EnMode.Update:

                    if (UpdateMedicine())
                    {
                        Auditor.Log(
                            "Medicine Updated",
                            $"Medicine '{DrugName}' ({StrengthValue} {StrengthType.StrengthTypeName}, {DosageForm.DosageFormName}) was updated by {performedBy}.",
                            $"MedicineID {MedicineID}");

                        return new OperationResult(
                            "Medicine updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update medicine.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid medicine operation mode.",
                "Failed",
                false);
        }

        public static Medicine FindMedicineByID(int medicineID)
        {
            return FromDto(MedicineData.FindMedicineByID(medicineID));
        }

        public static Medicine FindMedicineByDrugName(string drugName)
        {
            return FromDto(MedicineData.FindMedicineByDrugName(drugName));
        }

        public static async Task<BindingList<Medicine>> GetMedicines()
        {
            BindingList<Medicine> medicines = new BindingList<Medicine>();

            foreach (MedicineDto medicineDto in await MedicineData.GetMedicines())
            {
                medicines.Add(FromDto(medicineDto));
            }

            return medicines;
        }

        private OperationResult IsMedicineValidToSave()
        {
            if (Util.IsRequired(DrugName) &&
                StrengthValue > 0 &&
                Util.IsValidId((int)StrengthType.StrengthTypeID) &&
                Util.IsValidId((int)DosageForm.DosageFormID) &&
                UnitPrice >= 0)
            {
                return new OperationResult(
                    "Medicine information is valid to save.",
                    "Valid",
                    true);
            }

            return new OperationResult(
                "Medicine information is invalid.",
                "Invalid",
                false);
        }
    }
}
