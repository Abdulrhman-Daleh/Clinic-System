using Business;
using ClinicSystem.Helpers;

namespace ClinicSystem.Insurance.Controls
{
    public partial class ViewPatientsUnderInsurance : UserCtrlDefaultSettings
    {
        public ViewPatientsUnderInsurance()
        {
            InitializeComponent();
        }

        public void LoadUpPatientsInsurance(int insuranceId)
        {
            LoadPatientInsurance(insuranceId);
        }

        private async void LoadPatientInsurance(int insuranceId)
        {
            CommonOperations.LoadDataToDGV(dgvInsurancePatients, await PatientInsuranceInfo.GetPatientInsurance(insuranceId), lblRecords);
        }
    }
}
