using Business;
using ClinicSystem.Helpers;
using System;
using System.Windows.Forms;

namespace ClinicSystem.Medicine.Controls
{
    public partial class FindMedicineCtrl : UserCtrlDefaultSettings
    {
        public FindMedicineCtrl()
        {
            InitializeComponent();
            SetUp();
        }

        private void SetUp()
        {
            cbFindBy.Items.Add("MedicineID");
            cbFindBy.Items.Add("DrugName");
            cbFindBy.SelectedIndex = 0;
            txtFindBy.Focus();

        }
        public Business.Medicine CurrentMedicine
        {
            get => viewMedicineCtrl1.CurrentMedicine;
        }

        public bool AllowAdd
        {
            get => btnAddNew.Enabled;
            set => btnAddNew.Enabled = value;
        }

        public bool AllowFind
        {
            get => gbFilter.Enabled;
            set => gbFilter.Enabled = value;
        }

        public delegate void FindMedicineCtrlEventHandler(Business.Medicine medicine);
        public event FindMedicineCtrlEventHandler OnFind;

        private void Find()
        {
            this.ValidateChildren();

            OperationResult result = ValidationHelper.ValidateForm(this, errors);
            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FindFilterHelper();

            if (AllowFind)
                OnFind?.Invoke(CurrentMedicine);
        }

        private void FindFilterHelper()
        {
            string findBy = CommonOperations.GetFilterName(cbFindBy);

            if (findBy.ToLower() == "medicineid")
                viewMedicineCtrl1.LoadMedicineInfo(Business.Medicine.FindMedicineByID(Convert.ToInt32(txtFindBy.Text.Trim())));
            else
                viewMedicineCtrl1.LoadMedicineInfo(Business.Medicine.FindMedicineByDrugName(txtFindBy.Text.Trim()));

        }

        private void btnFind_Click(object sender, System.EventArgs e)
        {
            Find();
        }

        private void btnAddNew_Click(object sender, System.EventArgs e)
        {
            AddUpdateMedicine addMedicine = new AddUpdateMedicine();
            addMedicine.OnSave += ReciveMedicineInfo;
            addMedicine.ShowDialog();
        }

        private void ReciveMedicineInfo(Business.Medicine medicine)
        {
            viewMedicineCtrl1.LoadMedicineInfo(medicine);
        }

        private void cbFindBy_KeyPress(object sender, KeyPressEventArgs e) => ValidationHelper.LockComboBox(e);

        private void txtFindBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(txtFindBy.Text))
                errors.SetError(txtFindBy, "enter a valid value");
            else
                errors.SetError(txtFindBy, null);
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFindBy.Text = string.Empty;
            txtFindBy.Focus();
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            string findBy = CommonOperations.GetFilterName(cbFindBy);

            if (findBy.ToLower() == "medicineid")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);
        }
    }
}
