namespace ClinicSystem.Appointments.SOAPNotes
{
    partial class ViewSOAPNote
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
            this.lblNoteId = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.viewAppointmentInfoCtrl1 = new ClinicSystem.Appointments.Controls.ViewAppointmentInfoCtrl();
            this.viewPersonInfoCtrl1 = new ClinicSystem.People.ViewPersonInfoCtrl();
            this.viewMRecordInfo1 = new ClinicSystem.MedicalRecord.Controls.ViewMRecordInfo();
            this.viewSoapNoteCtrl1 = new ClinicSystem.Appointments.SOAPNotes.Controls.ViewSoapNoteCtrl();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNoteId
            // 
            this.lblNoteId.AutoSize = true;
            this.lblNoteId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNoteId.Location = new System.Drawing.Point(84, 24);
            this.lblNoteId.Name = "lblNoteId";
            this.lblNoteId.Size = new System.Drawing.Size(0, 29);
            this.lblNoteId.TabIndex = 73;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ClinicSystem.Properties.Resources.note_32;
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(65, 52);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 72;
            this.pictureBox2.TabStop = false;
            // 
            // viewAppointmentInfoCtrl1
            // 
            this.viewAppointmentInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewAppointmentInfoCtrl1.Location = new System.Drawing.Point(7, 378);
            this.viewAppointmentInfoCtrl1.Name = "viewAppointmentInfoCtrl1";
            this.viewAppointmentInfoCtrl1.Size = new System.Drawing.Size(619, 556);
            this.viewAppointmentInfoCtrl1.TabIndex = 75;
            // 
            // viewPersonInfoCtrl1
            // 
            this.viewPersonInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewPersonInfoCtrl1.Location = new System.Drawing.Point(922, 178);
            this.viewPersonInfoCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.viewPersonInfoCtrl1.Name = "viewPersonInfoCtrl1";
            this.viewPersonInfoCtrl1.Size = new System.Drawing.Size(611, 207);
            this.viewPersonInfoCtrl1.TabIndex = 76;
            // 
            // viewMRecordInfo1
            // 
            this.viewMRecordInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewMRecordInfo1.Location = new System.Drawing.Point(632, 378);
            this.viewMRecordInfo1.Name = "viewMRecordInfo1";
            this.viewMRecordInfo1.Size = new System.Drawing.Size(864, 472);
            this.viewMRecordInfo1.TabIndex = 77;
            // 
            // viewSoapNoteCtrl1
            // 
            this.viewSoapNoteCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewSoapNoteCtrl1.Location = new System.Drawing.Point(12, 72);
            this.viewSoapNoteCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.viewSoapNoteCtrl1.Name = "viewSoapNoteCtrl1";
            this.viewSoapNoteCtrl1.Size = new System.Drawing.Size(902, 298);
            this.viewSoapNoteCtrl1.TabIndex = 78;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(1376, 868);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 66);
            this.btnCancel.TabIndex = 79;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ViewSOAPNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1548, 946);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.viewSoapNoteCtrl1);
            this.Controls.Add(this.viewMRecordInfo1);
            this.Controls.Add(this.viewPersonInfoCtrl1);
            this.Controls.Add(this.viewAppointmentInfoCtrl1);
            this.Controls.Add(this.lblNoteId);
            this.Controls.Add(this.pictureBox2);
            this.Name = "ViewSOAPNote";
            this.Text = "View SOAP Note";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNoteId;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Appointments.Controls.ViewAppointmentInfoCtrl viewAppointmentInfoCtrl1;
        private People.ViewPersonInfoCtrl viewPersonInfoCtrl1;
        private MedicalRecord.Controls.ViewMRecordInfo viewMRecordInfo1;
        private Controls.ViewSoapNoteCtrl viewSoapNoteCtrl1;
        private System.Windows.Forms.Button btnCancel;
    }
}