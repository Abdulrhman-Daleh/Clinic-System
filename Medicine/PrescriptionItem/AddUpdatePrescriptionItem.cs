using Business;
using ClinicSystem.Helpers;
using System.Windows.Forms;

namespace ClinicSystem.Medicine.PrescriptionItem
{
    public partial class AddUpdatePrescriptionItem : FormsDefaultSettings
    {
        Business.PrescriptionItem PrescriptionItem { get; set; }

        public delegate void SendPrescriptionItem(Business.PrescriptionItem prescriptionItem);
        public event SendPrescriptionItem OnSend;
        public AddUpdatePrescriptionItem()
        {
            InitializeComponent();
            findMedicineCtrl1.OnFind += ReciveMedicineInfo;
            PrescriptionItem = new Business.PrescriptionItem();
            SetFormTextBasedMode("Add Prescription Item", "Add Item", "Add Item");
            findMedicineCtrl1.AllowAdd = false;
        }

        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            Text = pageTitle;
            btnSave.Text = saveButtonText;
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => Close();

        private void ReciveMedicineInfo(Business.Medicine medicine)
        {
            PrescriptionItem.Medicine = medicine;

            if (ValidationHelper.IsEmptyOrNull(medicine))
                PrescriptionItem.MedicineID = -1;
            else
                PrescriptionItem.MedicineID = medicine.MedicineID;
        }

        private void nudDose_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NumericUpDown nudCurrent = (NumericUpDown)sender;

            if (!Util.IsValidId((int)nudCurrent.Value))
                errors.SetError(nudCurrent, "invalid value");
            else
                errors.SetError(nudCurrent, null);
        }

        private void txtFrequency_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox box = (TextBox)sender;

            if (!Util.IsRequired(box.Text))
                errors.SetError(box, "failed is required");
            else
                errors.SetError(box, null);
        }

        private void CollectData()
        {
            PrescriptionItem.Dose = (byte)nudDose.Value;
            PrescriptionItem.Quantity = (byte)nudQuantity.Value;
            PrescriptionItem.Frequency = txtFrequency.Text;
            PrescriptionItem.Duration = txtDuration.Text;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            CollectData();
            this.ValidateChildren();

            OperationResult result = ValidationHelper.ValidateForm(this, errors);

            if (!result.IsSuccess)
                return;

            result = PrescriptionItem.Save();

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblItemId.Text = $"It-{PrescriptionItem.ItemID}";
            SetFormTextBasedMode("Update Prescription Item", "Update Item", "Update");
            findMedicineCtrl1.AllowFind = false;
            lblLineTotal.Text = $"{PrescriptionItem.CalculateLineTotal()} $";
            OnSend?.Invoke(PrescriptionItem);
        }
    }
}
