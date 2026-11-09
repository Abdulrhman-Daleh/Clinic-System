
using AppEnums.cs;
using ClinicSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ClinicSystem.Medicine
{
    public partial class ManageMedicines : FormsDefaultSettings
    {
        private BindingList<Business.Medicine> Medicines { get; set; }

        public ManageMedicines()
        {
            InitializeComponent();
        }

        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvMedicines, Medicines, lblRecords, null);
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "MedicineID" }, CommonOperations.GetFilterName(cbFilterBy),
                5, 10);
        }

        private void SetUp()
        {
            cbFilterBy.Items.AddRange(new object[]
            {
                "None",
                "MedicineID",
                "DrugName",
                "StrengthUnitText",
                "DosageFormText"
            });

            cbFilterBy.SelectedIndex = 0;
        }

        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);

            if (filterName == "MedicineID")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            object value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(
                dgvMedicines, Medicines, lblRecords, value.ToString()))
                return;

            List<object> temp = CommonOperations.FilterData(
                Medicines, filterName, value);

            CommonOperations.LoadDataToDGV(
                dgvMedicines, temp, lblRecords);
        }

        private async void LoadMedicineInfo()
        {
            Medicines = await Business.Medicine.GetMedicines();

            CommonOperations.LoadDataToDGV(
                dgvMedicines, Medicines, lblRecords);

            RefreshFilter(0);
        }

        private void ManageMedicines_Load(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
            {
                Close();
                return;
            }

            LoadMedicineInfo();
            SetUp();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void SetControlEnableStatus(bool status)
        {
            updateMedicineToolStripMenuItem.Enabled = status;
        }

        private void cmsMedicine_Opening(object sender, CancelEventArgs e)
        {
            SetControlEnableStatus(
                !ValidationHelper.IsDgvEmptyOrNull(dgvMedicines));
        }

        private void RefreshDgv(Business.Medicine m)
        {
            int targetIndex = -1;

            Business.Medicine medicine =
                Business.Medicine.FindMedicineByID(m.MedicineID);

            if (medicine == null)
                return;

            if (Medicines.Any(x => x.MedicineID == medicine.MedicineID))
            {
                targetIndex = Medicines.IndexOf(
                    Medicines.First(x => x.MedicineID == medicine.MedicineID));
            }

            CommonOperations.UpdateAfterAdding(
                Medicines, medicine, targetIndex);
        }

        private void addNewMedicineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            AddUpdateMedicine addMedicine = new AddUpdateMedicine();

            addMedicine.OnSave += RefreshDgv;
            addMedicine.ShowDialog();
        }

        private void updateMedicineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvMedicines))
                return;

            int medicineID = (int)dgvMedicines.CurrentRow.Cells[0].Value;

            AddUpdateMedicine updateMedicine =
                new AddUpdateMedicine(medicineID);

            updateMedicine.OnSave += RefreshDgv;
            updateMedicine.ShowDialog();
        }

        private void dgvMedicines_CellDoubleClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (!ValidationHelper.CheckAccess(EnRole.Owner))
                return;

            if (ValidationHelper.IsDgvEmptyOrNull(dgvMedicines))
                return;

            if (e.RowIndex < 0)
                return;

            int medicineID = (int)dgvMedicines.Rows[e.RowIndex].Cells[0].Value;

            AddUpdateMedicine updateMedicine =
                new AddUpdateMedicine(medicineID);

            updateMedicine.OnSave += RefreshDgv;
            updateMedicine.ShowDialog();
        }
    }
}
