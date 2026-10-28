using ClinicSystem.Helpers;

namespace ClinicSystem.MedicalRecord
{
    public partial class ViewMedicalRecord : FormsDefaultSettings
    {
        public ViewMedicalRecord()
        {
            InitializeComponent();
            RefreshMedicalRecordInfo();
        }

        public ViewMedicalRecord(int medicalRecordId)
        {
            InitializeComponent();
            SetUp(medicalRecordId);
        }

        private void SetUp(int medicalRecordId)
        {
            findMRecord1.LoadMedicalRecordInfo(medicalRecordId);
            RefreshMedicalRecordInfo();
        }

        private void RefreshMedicalRecordInfo()
        {
            var obj = findMRecord1.CurrentMRecord;

            if (ValidationHelper.IsEmptyOrNull(obj))
            {
                lblMedicalId.Text = "Not Set";
                lblClass.Text = "Not Set";
                btnEdit.Enabled = false;
                return;
            }

            lblMedicalId.Text = obj.MedicalNumber + " - " + obj.Patient.GetFullName();
            lblClass.Text = $"[{obj.Classification}] {obj.GetCareText()}";
            findMRecord1.AllowedFind = false;
            btnEdit.Enabled = true;
        }

        private void btnClose_Click(object sender, System.EventArgs e) => Close();

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            var obj = findMRecord1.CurrentMRecord;

            if (ValidationHelper.IsEmptyOrNull(obj))
                return;

            AddUpdateMedicalRecord updateMedicalRecord =
                new AddUpdateMedicalRecord(obj.MedicalRecordID);

            updateMedicalRecord.SendMedicalRecordInfo += ReciveMedicalInfo;
            updateMedicalRecord.ShowDialog();
        }

        private void ReciveMedicalInfo(Business.MedicalRecord mRecord)
        {
            if (ValidationHelper.IsEmptyOrNull(mRecord))
                return;

            findMRecord1.LoadMedicalRecordInfo(mRecord.MedicalRecordID);
            RefreshMedicalRecordInfo();
        }

        private void ViewMedicalRecord_Load(object sender, System.EventArgs e)
        {
            findMRecord1.SendMedicalRecord += FindMRecord1_SendMedicalRecord;
        }

        private void FindMRecord1_SendMedicalRecord(Business.MedicalRecord medicalRecord)
        {
            RefreshMedicalRecordInfo();
            findMRecord1.AllowedFind = true;
        }
    }
}