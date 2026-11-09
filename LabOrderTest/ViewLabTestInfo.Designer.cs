namespace ClinicSystem.LabOrderTest
{
    partial class ViewLabTestInfo
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
            this.viewMRecordInfo1 = new ClinicSystem.MedicalRecord.Controls.ViewMRecordInfo();
            this.viewAppointmentInfoCtrl1 = new ClinicSystem.Appointments.Controls.ViewAppointmentInfoCtrl();
            this.viewPersonInfoCtrl1 = new ClinicSystem.People.ViewPersonInfoCtrl();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            this.viewLabTestInfoCtrl1 = new ClinicSystem.LabOrderTest.Controls.ViewLabTestInfoCtrl();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // viewMRecordInfo1
            // 
            this.viewMRecordInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewMRecordInfo1.Location = new System.Drawing.Point(12, 88);
            this.viewMRecordInfo1.Name = "viewMRecordInfo1";
            this.viewMRecordInfo1.Size = new System.Drawing.Size(729, 472);
            this.viewMRecordInfo1.TabIndex = 80;
            // 
            // viewAppointmentInfoCtrl1
            // 
            this.viewAppointmentInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewAppointmentInfoCtrl1.Location = new System.Drawing.Point(747, 10);
            this.viewAppointmentInfoCtrl1.Name = "viewAppointmentInfoCtrl1";
            this.viewAppointmentInfoCtrl1.Size = new System.Drawing.Size(619, 556);
            this.viewAppointmentInfoCtrl1.TabIndex = 78;
            // 
            // viewPersonInfoCtrl1
            // 
            this.viewPersonInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewPersonInfoCtrl1.Location = new System.Drawing.Point(747, 574);
            this.viewPersonInfoCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.viewPersonInfoCtrl1.Name = "viewPersonInfoCtrl1";
            this.viewPersonInfoCtrl1.Size = new System.Drawing.Size(611, 207);
            this.viewPersonInfoCtrl1.TabIndex = 81;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(84, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(304, 38);
            this.lblTitle.TabIndex = 83;
            this.lblTitle.Text = "View Lab Test Info";
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.Appointment_64;
            this.pbModeImage.Location = new System.Drawing.Point(8, 10);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 82;
            this.pbModeImage.TabStop = false;
            // 
            // viewLabTestInfoCtrl1
            // 
            this.viewLabTestInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewLabTestInfoCtrl1.Location = new System.Drawing.Point(8, 566);
            this.viewLabTestInfoCtrl1.Name = "viewLabTestInfoCtrl1";
            this.viewLabTestInfoCtrl1.Size = new System.Drawing.Size(736, 389);
            this.viewLabTestInfoCtrl1.TabIndex = 84;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1198, 894);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 85;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ViewLabTestInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1374, 972);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.viewLabTestInfoCtrl1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbModeImage);
            this.Controls.Add(this.viewPersonInfoCtrl1);
            this.Controls.Add(this.viewMRecordInfo1);
            this.Controls.Add(this.viewAppointmentInfoCtrl1);
            this.Name = "ViewLabTestInfo";
            this.Text = "View Lab Test Info";
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MedicalRecord.Controls.ViewMRecordInfo viewMRecordInfo1;
        private Appointments.Controls.ViewAppointmentInfoCtrl viewAppointmentInfoCtrl1;
        private People.ViewPersonInfoCtrl viewPersonInfoCtrl1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbModeImage;
        private Controls.ViewLabTestInfoCtrl viewLabTestInfoCtrl1;
        private System.Windows.Forms.Button btnClose;
    }
}