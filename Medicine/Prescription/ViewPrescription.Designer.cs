namespace ClinicSystem.Medicine.Prescription
{
    partial class ViewPrescription
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
            this.btnClose = new System.Windows.Forms.Button();
            this.viewAppointmentInfoCtrl1 = new ClinicSystem.Appointments.Controls.ViewAppointmentInfoCtrl();
            this.lblPrescriptionId = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.patientInfoCtrl1 = new ClinicSystem.Patients.Controls.PatientInfoCtrl();
            this.viewMRecordInfo1 = new ClinicSystem.MedicalRecord.Controls.ViewMRecordInfo();
            this.viewPrescriptionCtrl1 = new ClinicSystem.Medicine.Prescription.ViewPrescriptionCtrl();
            this.btnEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1109, 913);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // viewAppointmentInfoCtrl1
            // 
            this.viewAppointmentInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewAppointmentInfoCtrl1.Location = new System.Drawing.Point(747, 7);
            this.viewAppointmentInfoCtrl1.Name = "viewAppointmentInfoCtrl1";
            this.viewAppointmentInfoCtrl1.Size = new System.Drawing.Size(619, 556);
            this.viewAppointmentInfoCtrl1.TabIndex = 17;
            // 
            // lblPrescriptionId
            // 
            this.lblPrescriptionId.AutoSize = true;
            this.lblPrescriptionId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrescriptionId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblPrescriptionId.Location = new System.Drawing.Point(84, 24);
            this.lblPrescriptionId.Name = "lblPrescriptionId";
            this.lblPrescriptionId.Size = new System.Drawing.Size(0, 29);
            this.lblPrescriptionId.TabIndex = 75;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ClinicSystem.Properties.Resources.note_32;
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(65, 52);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 74;
            this.pictureBox2.TabStop = false;
            // 
            // patientInfoCtrl1
            // 
            this.patientInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.patientInfoCtrl1.Location = new System.Drawing.Point(787, 568);
            this.patientInfoCtrl1.Name = "patientInfoCtrl1";
            this.patientInfoCtrl1.Size = new System.Drawing.Size(653, 204);
            this.patientInfoCtrl1.TabIndex = 76;
            // 
            // viewMRecordInfo1
            // 
            this.viewMRecordInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewMRecordInfo1.Location = new System.Drawing.Point(12, 85);
            this.viewMRecordInfo1.Name = "viewMRecordInfo1";
            this.viewMRecordInfo1.Size = new System.Drawing.Size(729, 472);
            this.viewMRecordInfo1.TabIndex = 77;
            // 
            // viewPrescriptionCtrl1
            // 
            this.viewPrescriptionCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewPrescriptionCtrl1.Location = new System.Drawing.Point(13, 570);
            this.viewPrescriptionCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.viewPrescriptionCtrl1.Name = "viewPrescriptionCtrl1";
            this.viewPrescriptionCtrl1.Size = new System.Drawing.Size(771, 413);
            this.viewPrescriptionCtrl1.TabIndex = 78;
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::ClinicSystem.Properties.Resources.update_32;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(1276, 914);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(160, 66);
            this.btnEdit.TabIndex = 16;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // ViewPrescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1448, 997);
            this.Controls.Add(this.viewPrescriptionCtrl1);
            this.Controls.Add(this.viewMRecordInfo1);
            this.Controls.Add(this.patientInfoCtrl1);
            this.Controls.Add(this.lblPrescriptionId);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.viewAppointmentInfoCtrl1);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClose);
            this.Name = "ViewPrescription";
            this.Text = "View Prescription";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private Appointments.Controls.ViewAppointmentInfoCtrl viewAppointmentInfoCtrl1;
        private System.Windows.Forms.Label lblPrescriptionId;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Patients.Controls.PatientInfoCtrl patientInfoCtrl1;
        private MedicalRecord.Controls.ViewMRecordInfo viewMRecordInfo1;
        private ViewPrescriptionCtrl viewPrescriptionCtrl1;
        private System.Windows.Forms.Button btnEdit;
    }
}