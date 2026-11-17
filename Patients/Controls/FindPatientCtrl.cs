using Business;
using ClinicSystem.Helpers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem.Patients.Controls
{
    public partial class FindPatientCtrl : UserCtrlDefaultSettings
    {
        private Patient patient = null;
        public FindPatientCtrl()
        {
            InitializeComponent();
            DefaultForm();
        }
        public bool AllowedFind
        {
            get => gbFindBy.Enabled;
            set => gbFindBy.Enabled = value;
        }

        public delegate void SendPatientInfo(Patient patient);
        public event SendPatientInfo SendPatient;

        public bool AllowAddNew
        {
            get => AddPatient.Enabled;
            set => AddPatient.Enabled = value;
        }
        private void DefaultForm()
        {
            cbFindBy.Items.Add("PatientID");
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

        private void txtFindBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(txtFindBy.Text))
                errors.SetError(txtFindBy, "Enter a patient ID");
            else
                errors.SetError(txtFindBy, null);
        }

        public async Task Find()
        {
            this.ValidateChildren();

            OperationResult result = ValidationHelper.ValidateForm(this, errors);

            if (!result.IsSuccess)
                return;

            int.TryParse(txtFindBy.Text, out int patientId);

            patient = await Patient.FindPatientById(patientId);

            patientInfoCtrl1.LoadPatientInfo(patient);

            if (AllowedFind)
                SendPatient?.Invoke(patient);
        }

        private async void btnFind_Click(object sender, System.EventArgs e)
        {
            await Find();
        }

        public async void LoadPatientInfo(int patientId)
        {
            txtFindBy.Text = patientId.ToString();
            await Find();
        }

        private void btnAddNew_Click(object sender, System.EventArgs e)
        {
            RegisterPatient addPatient = new RegisterPatient();
            addPatient.OnRegisterSave += RecivePatientId;
            addPatient.ShowDialog();
        }

        private async void RecivePatientId(Patient patient)
        {
            this.patient = patient;
            LoadPatientInfo(patient.PatientId);
        }
    }
}