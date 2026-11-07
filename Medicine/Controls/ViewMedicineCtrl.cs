using ClinicSystem.Helpers;

namespace ClinicSystem.Medicine.Controls
{
    public partial class ViewMedicineCtrl : UserCtrlDefaultSettings
    {
        public ViewMedicineCtrl()
        {
            InitializeComponent();
            CommonOperations.SetUpDefaultControlsValue(new System.Collections.Generic.List<System.Windows.Forms.Control>()
            { lblDrugName, lblStrength, lblUnitPrice, lblDosageForm}, "Not Set");
        }

        private Business.Medicine Medicine;
        public Business.Medicine CurrentMedicine
        {
            get => Medicine;
        }

        public void LoadMedicineInfo(Business.Medicine medicine)
        {
            Medicine = medicine;
            if (ValidationHelper.IsEmptyOrNull(medicine, "Medicine is not found"))
            {
                CommonOperations.SetUpDefaultControlsValue(new System.Collections.Generic.List<System.Windows.Forms.Control>()
            { lblDrugName, lblStrength, lblUnitPrice, lblDosageForm}, "Not Set");
                return;
            }

            lblDrugName.Text = medicine.DrugName;
            lblStrength.Text = medicine.StrengthUnitText;
            lblUnitPrice.Text = medicine.UnitPrice.ToString();
            lblDosageForm.Text = medicine.DosageFormText;
        }
    }
}
