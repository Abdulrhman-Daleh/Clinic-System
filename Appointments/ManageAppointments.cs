using Business;
using ClinicSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ClinicSystem.Appointments
{
    public partial class ManageAppointments : FormsDefaultSettings
    {
        private BindingList<AppointmentList> Appointments { get; set; }
        public ManageAppointments()
        {
            InitializeComponent();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filterName = CommonOperations.GetFilterName(cbFilterBy);
            object value = txtFilter.Text.Trim();

            if (CommonOperations.ResetDataGrid(dgvAppointments, Appointments, lblRecords, value.ToString()))
                return;

            List<object> temp = CommonOperations.FilterData(Appointments, filterName, value);
            CommonOperations.LoadDataToDGV(dgvAppointments, temp, lblRecords);
        }

        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationHelper.LockComboBox(e);
        }
        private void SetUp()
        {
            cbFilterBy.Items.AddRange(new object[] { "None", "AppointmentID", "Patient", "Doctor", "Status" });
            cbFilterBy.SelectedIndex = 0;
        }

        private void RefreshFilter(int newFilterIndex)
        {
            cbFilterBy.SelectedIndex = newFilterIndex;
            CommonOperations.HandleSwitchingFilter(cbFilterBy, null, txtFilter, dgvAppointments, Appointments, lblRecords, null);
            ValidationHelper.SetApproperiteMaxLength(txtFilter, new List<string> { "AppointmentID" }, CommonOperations.GetFilterName(cbFilterBy),
                5, 50);
        }

        private async void LoadAppointments()
        {
            Appointments = await AppointmentList.GetAppointment();
            CommonOperations.LoadDataToDGV(dgvAppointments, Appointments, lblRecords);
            RefreshFilter(0);
        }

        private void ManageAppointments_Load(object sender, EventArgs e)
        {
            SetUp();
            LoadAppointments();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFilter(cbFilterBy.SelectedIndex);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "AppointmentID")
                ValidationHelper.HandleInputType(e, true, false);
            else
                ValidationHelper.HandleInputType(e, false, true);
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void bookAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BookAppointment appointment = new BookAppointment(AppEnums.cs.EnAppointmentStatuses.Scheduled);
            appointment.OnSave += RefreshDgv;
            appointment.ShowDialog();
        }

        private void RefreshDgv(Appointment appointment)
        {
            int targetIndex = -1;
            AppointmentList appointmentList = AppointmentList.FindAppointmentListByID(appointment.AppointmentID);

            if (Appointments.Any(x => x.AppointmentID == appointment.AppointmentID))
                targetIndex = Appointments.IndexOf(Appointments.First(x => x.AppointmentID == appointment.AppointmentID));

            CommonOperations.UpdateAfterAdding(Appointments, appointmentList, targetIndex);
        }

        private void viewAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int appointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            ViewAppointment viewAppointment = new ViewAppointment(appointmentId);
            viewAppointment.ShowDialog();
        }

        private void SetControlEnableStatus(bool status)
        {
            rescheduleToolStripMenuItem.Enabled = status;
            viewAppointmentToolStripMenuItem.Enabled = status;

        }
        private void cmsAppointments_Opening(object sender, CancelEventArgs e)
        {
            SetControlEnableStatus(!ValidationHelper.IsDgvEmptyOrNull(dgvAppointments));
        }

        private void dgvAppointments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ValidationHelper.IsDgvEmptyOrNull(dgvAppointments))
                return;

            int appointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            ViewAppointment viewAppointment = new ViewAppointment(appointmentId);
            viewAppointment.ShowDialog();
        }

        private void rescheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int appointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            BookAppointment viewAppointment = new BookAppointment(appointmentId, AppEnums.cs.EnAppointmentStatuses.Rescheduled);
            viewAppointment.ShowDialog();
        }
    }
}
