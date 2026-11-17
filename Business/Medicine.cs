
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
        public EnStrengthUnits StrengthType { get; set; }
        public EnDosageForms DosageForm { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsAddMode()
        {
            return _mode == EnMode.Add;
        }
        public static async Task<BindingList<string>> GetMedicineStrengthUnits()
        {
            BindingList<string> list = new BindingList<string>();

            if (!Util.CanAccessForm(EnRole.Owner).IsSuccess)
                return list;

            foreach (var s in await MedicineData.GetMedicineStrengthUnits())
                list.Add(s);

            return list;
        }
        public static async Task<BindingList<string>> GetMedicineDosageForms()
        {
            BindingList<string> list = new BindingList<string>();

            if (!Util.CanAccessForm(EnRole.Owner).IsSuccess)
                return list;

            foreach (var s in await MedicineData.GetMedicineDosageForms())
                list.Add(s);

            return list;
        }
        public Medicine()
        {
            MedicineID = -1;
            DrugName = string.Empty;
            StrengthType = 0;
            DosageForm = 0;
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
            OperationResult result = Util.CanAccessForm(EnRole.Owner, EnRole.Doctor);

            if (!result.IsSuccess)
                return result;

            result = IsMedicineValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy = $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case EnMode.Add:

                    if (AddMedicine())
                    {
                        _mode = EnMode.Update;

                        Auditor.Log(
                            "Medicine Added",
                            $"Medicine '{DrugName}' ({StrengthValue} {StrengthType}, {DosageForm}) was added by {performedBy}.",
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
                            $"Medicine '{DrugName}' ({StrengthValue} {StrengthType}, {DosageForm}) was updated by {performedBy}.",
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
        public static async Task<Medicine> FindMedicineByID(int medicineID)
        {
            if (!Util.CanAccessForm(EnRole.Doctor, EnRole.Owner).IsSuccess)
                return null;

            MedicineDto medicineDto = await MedicineData.FindMedicineByID(medicineID);

            return FromDto(medicineDto);
        }
        public static async Task<Medicine> FindMedicineByDrugName(string drugName)
        {
            if (!Util.CanAccessForm(EnRole.Doctor, EnRole.Owner).IsSuccess)
                return null;

            MedicineDto medicineDto = await MedicineData.FindMedicineByDrugName(drugName);

            return FromDto(medicineDto);
        }
        public static async Task<BindingList<Medicine>> GetMedicines()
        {
            BindingList<Medicine> medicines = new BindingList<Medicine>();

            if (!Util.CanAccessForm(EnRole.Doctor, EnRole.Owner).IsSuccess)
                return medicines;

            BindingList<MedicineDto> medicinesDto = await MedicineData.GetMedicines();

            foreach (MedicineDto row in medicinesDto)
                medicines.Add(FromDto(row));

            return medicines;
        }
        private OperationResult IsMedicineValidToSave()
        {
            if (Util.IsRequired(DrugName) &&
                StrengthValue > 0 &&
                Util.IsValidId((int)StrengthType) &&
                Util.IsValidId((int)DosageForm) &&
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
