using Business;
using ClinicSystem.Helpers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem.MedicalRecord.Controls
{
    public partial class FindMRecord : UserCtrlDefaultSettings
    {
        private Business.MedicalRecord medicalRecord;
        public FindMRecord()
        {
            InitializeComponent();
            DefaultForm();
        }
        public bool AllowedFind
        {
            get => gbFilter.Enabled;
            set => gbFilter.Enabled = value;
        }
        public Business.MedicalRecord CurrentMRecord => medicalRecord;

        public delegate void SendMedicalRecordInfo(Business.MedicalRecord medicalRecord);
        public event SendMedicalRecordInfo SendMedicalRecord;
        private void DefaultForm()
        {
            cbFindBy.Items.Add("MedicalRecordID");
            cbFindBy.SelectedIndex = 0;
        }
        private void cbFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.HandleInputType(e, true, false);
        }
        public async Task Find()
        {
            ValidateChildren();

            OperationResult result = ValidationHelper.ValidateForm(this, errors);

            if (!result.IsSuccess)
                return;

            if (!int.TryParse(txtFindBy.Text.Trim(), out int medicalRecordID))
            {
                errors.SetError(txtFindBy, "Enter a valid medical record ID");
                return;
            }

            medicalRecord = await Business.MedicalRecord.FindMedicalRecordByID(medicalRecordID);

            viewMRecordInfo1.LoadMedicalRecordInfo(medicalRecord);

            if (AllowedFind)
                SendMedicalRecord?.Invoke(medicalRecord);
        }
        private async void btnFind_Click(object sender, System.EventArgs e)
        {
            await Find();
        }
        public async void LoadMedicalRecordInfo(int medicalRecordID)
        {
            txtFindBy.Text = medicalRecordID.ToString();
            await Find();
        }
        private void txtFindBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(txtFindBy.Text))
                errors.SetError(txtFindBy, "enter a value id");
            else
                errors.SetError(txtFindBy, null);
        }
    }
}