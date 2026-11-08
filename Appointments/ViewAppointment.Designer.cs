namespace ClinicSystem.Appointments
{
    partial class ViewAppointment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAppointmentId = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnReschedule = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.findAppointmentCtrl1 = new ClinicSystem.Appointments.Controls.FindAppointmentCtrl();
            this.patientInfoCtrl1 = new ClinicSystem.Patients.Controls.PatientInfoCtrl();
            this.viewPersonContacts1 = new ClinicSystem.People.Controls.ViewPersonContacts();
            this.viewPersonInfoCtrl1 = new ClinicSystem.People.ViewPersonInfoCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAppointmentId
            // 
            this.lblAppointmentId.AutoSize = true;
            this.lblAppointmentId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAppointmentId.Location = new System.Drawing.Point(84, 24);
            this.lblAppointmentId.Name = "lblAppointmentId";
            this.lblAppointmentId.Size = new System.Drawing.Size(0, 29);
            this.lblAppointmentId.TabIndex = 73;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStatus.Location = new System.Drawing.Point(453, 24);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 29);
            this.lblStatus.TabIndex = 78;
            // 
            // btnReschedule
            // 
            this.btnReschedule.Image = global::ClinicSystem.Properties.Resources.update_32;
            this.btnReschedule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReschedule.Location = new System.Drawing.Point(475, 685);
            this.btnReschedule.Name = "btnReschedule";
            this.btnReschedule.Size = new System.Drawing.Size(215, 66);
            this.btnReschedule.TabIndex = 76;
            this.btnReschedule.Text = "Reschedule";
            this.btnReschedule.UseVisualStyleBackColor = true;
            this.btnReschedule.Click += new System.EventHandler(this.btnReschedule_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(309, 685);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 75;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ClinicSystem.Properties.Resources.Appointment_64;
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(65, 52);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 72;
            this.pictureBox2.TabStop = false;
            // 
            // findAppointmentCtrl1
            // 
            this.findAppointmentCtrl1.AllowFind = true;
            this.findAppointmentCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.findAppointmentCtrl1.Location = new System.Drawing.Point(696, 80);
            this.findAppointmentCtrl1.Name = "findAppointmentCtrl1";
            this.findAppointmentCtrl1.Size = new System.Drawing.Size(638, 671);
            this.findAppointmentCtrl1.TabIndex = 79;
            // 
            // patientInfoCtrl1
            // 
            this.patientInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.patientInfoCtrl1.Location = new System.Drawing.Point(20, 70);
            this.patientInfoCtrl1.Name = "patientInfoCtrl1";
            this.patientInfoCtrl1.Size = new System.Drawing.Size(670, 204);
            this.patientInfoCtrl1.TabIndex = 80;
            // 
            // viewPersonContacts1
            // 
            this.viewPersonContacts1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewPersonContacts1.Location = new System.Drawing.Point(20, 497);
            this.viewPersonContacts1.Name = "viewPersonContacts1";
            this.viewPersonContacts1.Size = new System.Drawing.Size(671, 182);
            this.viewPersonContacts1.TabIndex = 81;
            // 
            // viewPersonInfoCtrl1
            // 
            this.viewPersonInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewPersonInfoCtrl1.Location = new System.Drawing.Point(20, 282);
            this.viewPersonInfoCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.viewPersonInfoCtrl1.Name = "viewPersonInfoCtrl1";
            this.viewPersonInfoCtrl1.Size = new System.Drawing.Size(670, 207);
            this.viewPersonInfoCtrl1.TabIndex = 82;
            // 
            // ViewAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 763);
            this.Controls.Add(this.viewPersonInfoCtrl1);
            this.Controls.Add(this.viewPersonContacts1);
            this.Controls.Add(this.patientInfoCtrl1);
            this.Controls.Add(this.findAppointmentCtrl1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnReschedule);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblAppointmentId);
            this.Controls.Add(this.pictureBox2);
            this.Name = "ViewAppointment";
            this.Text = "View Appointment";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAppointmentId;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnReschedule;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblStatus;
        private Controls.FindAppointmentCtrl findAppointmentCtrl1;
        private Patients.Controls.PatientInfoCtrl patientInfoCtrl1;
        private People.Controls.ViewPersonContacts viewPersonContacts1;
        private People.ViewPersonInfoCtrl viewPersonInfoCtrl1;
    }
}