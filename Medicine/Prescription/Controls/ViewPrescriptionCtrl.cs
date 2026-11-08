using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Medicine.Prescription
{
    public partial class ViewPrescriptionCtrl : UserCtrlDefaultSettings
    {
        private Business.Prescription Prescription { get; set; }
        public Business.Prescription CurrentPrescription
        {
            get => Prescription;
        }
        public ViewPrescriptionCtrl()
        {
            InitializeComponent();
            CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblSignedBy, lblTotal, lblValidUntil, lblWrittenDate
            }, "Not Set");
        }

        public void LoadPrescriptionInfo(int appointmetnId)
        {
            Prescription = Business.Prescription.FindPrescriptionByAppointmentID(appointmetnId);

            if (ValidationHelper.IsEmptyOrNull(Prescription, "Prescription Is not present"))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblSignedBy, lblTotal, lblValidUntil, lblWrittenDate
            }, "Not Set");
                return;
            }

            viewPrescriptionItems1.LoadPrescriptionItems(Prescription.PrescriptionID, Prescription.TotalPrice);
            lblWrittenDate.Text = Prescription.WriteDate.ToShortDateString();
            lblSignedBy.Text = $"Dr . {Prescription.SignedByName()}";
            lblTotal.Text = $"{Prescription.TotalPrice} $";
            lblValidUntil.Text = Prescription.GetValidUntilDate();
        }
    }
}
