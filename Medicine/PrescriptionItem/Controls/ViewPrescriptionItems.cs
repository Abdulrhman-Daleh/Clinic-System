using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClinicSystem.Medicine.PrescriptionItem
{
    public partial class ViewPrescriptionItems : UserCtrlDefaultSettings
    {
        public ViewPrescriptionItems()
        {
            InitializeComponent();
            CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblRecord, lblTotal
            }, "Not Set");
        }

        public async void LoadPrescriptionItems(int prescriptionId, decimal totalPrice)
        {
            BindingList<Business.PrescriptionItemListing> prescriptionItems = await Business.PrescriptionItemListing.GetPrescriptionItems(prescriptionId);
            CommonOperations.LoadDataToDGV(dgvPrescriptionitems, prescriptionItems, lblRecord);
            lblTotal.Text = $"{totalPrice} $";
        }
    }
}
