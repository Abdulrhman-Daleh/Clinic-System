using ClinicSystem.Helpers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.MedicalRecord.Controls
{
    public partial class ViewMRecordInfo : UserCtrlDefaultSettings
    {

        private Business.MedicalRecord MRecord { get; set; }

        public Business.MedicalRecord CurrentMRecord
        {
            get => MRecord;
        }
        public ViewMRecordInfo()
        {
            InitializeComponent();
            CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblClassification, lblConsent, lblCreated, lblCurrentMedication, lblMedicalNo, lblPastSoical, lblPastSurgeries, lblStatus,
            }, "Not Set");

        }

        public void LoadMedicalRecordInfo(int medicalRecordId)
        {
            MRecord = Business.MedicalRecord.FindMedicalRecordByID(medicalRecordId);

            if (ValidationHelper.IsEmptyOrNull(MRecord, "Medical record is not found"))
            {
                CommonOperations.SetUpDefaultControlsValue(new List<Control>()
                 {
                lblClassification, lblConsent, lblCreated, lblCurrentMedication, lblMedicalNo, lblPastSoical, lblPastSurgeries, lblStatus,
                 }, "Not Set");

                return;
            }

            lblClassification.Text = MRecord.Classification;
            lblConsent.Text = MRecord.ConcentToCare ? "Given" : "Not Given";
            lblCreated.Text = MRecord.CreatedDate.ToShortDateString();
            lblCurrentMedication.Text = MRecord.CurrentMedication;
            lblMedicalNo.Text = MRecord.MedicalNumber;
            lblPastSoical.Text = MRecord.PastSocialHistory;
            lblPastSurgeries.Text = MRecord.PastSurgeries;
            lblStatus.Text = MRecord.SocialStatus;
        }
    }
}
