
using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem.Medicine
{
    public partial class AddUpdateMedicine : FormsDefaultSettings
    {
        private Business.Medicine medicine;
        public AddUpdateMedicine()
        {
            InitializeComponent();

            medicine = new Business.Medicine();

            Load += AddUpdateMedicine_Load;

            SetFormTextBasedMode("Add New Medicine", "Add Medicine", "Add Medicine");
            lblMedicineId.Text = "Not Set";
        }

        public AddUpdateMedicine(int medicineID)
        {
            InitializeComponent();

            medicine = Business.Medicine.FindMedicineByID(medicineID);

            if (ValidationHelper.IsEmptyOrNull(medicine, "Medicine not found"))
                return;

            Load += AddUpdateMedicine_Load;

            SetFormTextBasedMode("Update Medicine", "Update Medicine", "Update");
        }

        public delegate void SendMedicineInfo(Business.Medicine medicine);
        public event SendMedicineInfo OnSave;

        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            Text = pageTitle;
            btnSave.Text = saveButtonText;
        }

        private async Task LoadStrengthUnits()
        {
            cbStrengthType.DataSource = await Business.Medicine.GetMedicineStrengthUnits();
        }

        private async Task LoadDosageForms()
        {
            cbDosageForm.DataSource = await Business.Medicine.GetMedicineDosageForms();
        }

        private async Task SetPage()
        {
            await Task.WhenAll(
                LoadStrengthUnits(),
                LoadDosageForms());

            mtxtUnitPrice.Mask = "999.99";

            if (medicine.IsAddMode())
            {
                cbStrengthType.SelectedIndex = 0;
                cbDosageForm.SelectedIndex = 0;
            }
            else
            {
                SetControls();
            }
        }

        private async void AddUpdateMedicine_Load(object sender, EventArgs e)
        {
            await SetPage();
        }

        private void SetControls()
        {
            txtDrugName.Text = medicine.DrugName;
            txtStrength.Text = medicine.StrengthValue.ToString();

            cbStrengthType.SelectedIndex = (int)medicine.StrengthType.StrengthTypeID - 1;
            cbDosageForm.SelectedIndex = (int)medicine.DosageForm.DosageFormID - 1;
            lblMedicineId.Text = $"Med-{medicine.MedicineID.ToString()}";

            mtxtUnitPrice.Text = medicine.UnitPrice.ToString("0.00");
        }

        private void CollectData()
        {
            medicine.DrugName = txtDrugName.Text.Trim();

            decimal.TryParse(txtStrength.Text.Trim(), out decimal strength);
            medicine.StrengthValue = strength;

            medicine.StrengthType = (
                (EnStrengthUnits)(cbStrengthType.SelectedIndex + 1),
                cbStrengthType.SelectedItem.ToString());

            medicine.DosageForm = (
                (EnDosageForms)(cbDosageForm.SelectedIndex + 1),
                cbDosageForm.SelectedItem.ToString());

            decimal.TryParse(mtxtUnitPrice.Text, out decimal unitPrice);
            medicine.UnitPrice = unitPrice;
        }

        private void txtDrugName_Validating(object sender, CancelEventArgs e)
        {
            ValidationHelper.ValidateRequiredTextBox((TextBox)sender, errors);
        }

        private void txtStrength_Validating(object sender, CancelEventArgs e)
        {
            if (!decimal.TryParse(txtStrength.Text.Trim(), out decimal strength) || strength <= 0)
            {
                errors.SetError(txtStrength, "Enter a valid strength greater than 0");
                return;
            }

            errors.SetError(txtStrength, null);
        }

        private void cbStrengthType_Validating(object sender, CancelEventArgs e)
        {
            if (cbStrengthType.SelectedIndex == -1)
            {
                errors.SetError(cbStrengthType, "Select a strength type");
                return;
            }

            errors.SetError(cbStrengthType, null);
        }

        private void cbDosageForm_Validating(object sender, CancelEventArgs e)
        {
            if (cbDosageForm.SelectedIndex == -1)
            {
                errors.SetError(cbDosageForm, "Select a dosage form");
                return;
            }

            errors.SetError(cbDosageForm, null);
        }

        private void mtxtUnitPrice_Validating(object sender, CancelEventArgs e)
        {
            if (!decimal.TryParse(mtxtUnitPrice.Text, out decimal price) || price < 0)
            {
                errors.SetError(mtxtUnitPrice, "Enter a valid unit price");
                return;
            }

            errors.SetError(mtxtUnitPrice, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ValidateChildren();

            OperationResult validationResult = ValidationHelper.ValidateForm(this, errors);

            if (!validationResult.IsSuccess)
            {
                MessageBox.Show(
                    validationResult.Message,
                    validationResult.Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            CollectData();

            OperationResult result = medicine.Save();

            MessageBox.Show(
                result.Message,
                result.Title,
                MessageBoxButtons.OK,
                result.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.IsSuccess)
            {
                SetFormTextBasedMode(
                    "Update Medicine",
                    "Update Medicine",
                    "Update");

                OnSave?.Invoke(medicine);
                lblMedicineId.Text = $"Med-{medicine.MedicineID.ToString()}";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
