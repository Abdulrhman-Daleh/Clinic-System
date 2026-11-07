using AppEnums.cs;
using Business;
using ClinicSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.LabOrderTest
{
    public partial class AddUpdateLabTest : FormsDefaultSettings
    {
        LabTest Test { get; set; }
        public AddUpdateLabTest(int appointmentId)
        {
            InitializeComponent();
            SetFormTextBasedMode("Add Order Lab Test", "Add New Lab Order Test", "Save Lab Order");
            SetUpCtrlsInfo(appointmentId);
            findStaffCtrl1.OnFound += ReciveStaffInfo;
            CommonOperations.SetUpDefaultControlsValue(new List<Control>()
            {
                lblTestId, lblStatus, lblRequestdate
            }, "Not Set");
        }

        public delegate void SendTestInfoOnSave(LabTest test);
        public event SendTestInfoOnSave OnSave;
        private void ReciveStaffInfo(Business.Staff staff)
        {
            if (ValidationHelper.IsEmptyOrNull(staff, "Staff is not found"))
                return;

            if (staff.RoleInfo.RoleId != AppEnums.cs.EnRole.Nurse)
            {
                errors.SetError(findStaffCtrl1, "Selected staff is not a nurse.");
                Test.Nurse = null;
                Test.NurseID = -1;
                return;
            }
            else
                errors.SetError(findStaffCtrl1, null);

            Test.Nurse = staff;
            Test.NurseID = staff.StaffId;

        }

        private void SetFormTextBasedMode(string labelTitle, string pageTitle, string saveButtonText)
        {
            lblTitle.Text = labelTitle;
            Text = pageTitle;
            btnSave.Text = saveButtonText;
        }

        private void SetUpCtrlsInfo(int appointmentId)
        {
            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(appointmentId);

            Appointment appointment = viewAppointmentInfoCtrl1.CurrentAppointment;

            if (ValidationHelper.IsEmptyOrNull(appointment))
                return;

            Test = new LabTest();
            Test.Appointment = appointment;
            Test.AppointmentID = appointmentId;
            viewPersonInfoCtrl1.SetData(appointment.Patient);
            viewMRecordInfo1.LoadMedicalRecordInfo(Business.MedicalRecord.FindMedicalRecordByPatientID(appointment.PatientID));
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => Close();

        private void txtTestname_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ValidationHelper.IsEmptyOrNull(txtTestname.Text.Trim()))
                errors.SetError(txtTestname, "test name is required");
            else
                errors.SetError(txtTestname, null);
        }

        private void CollectDate()
        {
            Test.RequestDate = DateTime.Now;
            Test.Reason = txtReason.Text.Trim();
            Test.TestName = txtTestname.Text.Trim();
            Test.Instructions = txtInstructions.Text.Trim();
            Test.TestStatuses = (EnTestStatuses.Ordered, "Ordered");
        }

        private void PerformSave()
        {
            CollectDate();

            OperationResult result = Test.Save();

            MessageBox.Show(result.Message, result.Title, MessageBoxButtons.OK, result.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (!result.IsSuccess)
                return;

            SetFormTextBasedMode("Update Order Lab Test", "Update Lab Test", "Update");
            findStaffCtrl1.AllowFind = false;
            lblStatus.Text = Test.TestStatuses.TestStatusText;
            lblRequestdate.Text = Test.RequestDate.ToShortDateString();
            viewAppointmentInfoCtrl1.LoadUpAppointmentInfo(Test.AppointmentID);
            lblTestId.Text = $"Appt-{Test.AppointmentID} . Te-{Test.LabID}";
            OnSave?.Invoke(Test);
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            PerformSave();
        }
    }
}
