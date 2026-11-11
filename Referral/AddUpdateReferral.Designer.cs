namespace ClinicSystem.Referral
{
    partial class AddUpdateReferral
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            this.lblReferralId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.viewAppointmentInfoCtrl1 = new ClinicSystem.Appointments.Controls.ViewAppointmentInfoCtrl();
            this.viewSoapNoteCtrl1 = new ClinicSystem.Appointments.SOAPNotes.Controls.ViewSoapNoteCtrl();
            this.errors = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbReferralUrgency = new System.Windows.Forms.ComboBox();
            this.viewStaffInfoCtrl1 = new ClinicSystem.Staff.Controls.ViewStaffInfoCtrl();
            this.gbReferral = new System.Windows.Forms.GroupBox();
            this.txtReferralReason = new System.Windows.Forms.TextBox();
            this.txtSpeciality = new System.Windows.Forms.TextBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).BeginInit();
            this.gbReferral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(83, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 38);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "New Referral";
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.Referral_64;
            this.pbModeImage.Location = new System.Drawing.Point(11, 7);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 6;
            this.pbModeImage.TabStop = false;
            // 
            // lblReferralId
            // 
            this.lblReferralId.AutoSize = true;
            this.lblReferralId.Location = new System.Drawing.Point(203, 57);
            this.lblReferralId.Name = "lblReferralId";
            this.lblReferralId.Size = new System.Drawing.Size(0, 25);
            this.lblReferralId.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "Referral Id:";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::ClinicSystem.Properties.Resources.save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1330, 724);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(202, 70);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save Record";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCancel.Location = new System.Drawing.Point(1166, 724);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(158, 70);
            this.BtnCancel.TabIndex = 16;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // viewAppointmentInfoCtrl1
            // 
            this.viewAppointmentInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewAppointmentInfoCtrl1.Location = new System.Drawing.Point(11, 85);
            this.viewAppointmentInfoCtrl1.Name = "viewAppointmentInfoCtrl1";
            this.viewAppointmentInfoCtrl1.Size = new System.Drawing.Size(619, 556);
            this.viewAppointmentInfoCtrl1.TabIndex = 18;
            // 
            // viewSoapNoteCtrl1
            // 
            this.viewSoapNoteCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewSoapNoteCtrl1.Location = new System.Drawing.Point(639, 47);
            this.viewSoapNoteCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.viewSoapNoteCtrl1.Name = "viewSoapNoteCtrl1";
            this.viewSoapNoteCtrl1.Size = new System.Drawing.Size(902, 298);
            this.viewSoapNoteCtrl1.TabIndex = 19;
            // 
            // errors
            // 
            this.errors.ContainerControl = this;
            // 
            // cbReferralUrgency
            // 
            this.cbReferralUrgency.FormattingEnabled = true;
            this.cbReferralUrgency.Location = new System.Drawing.Point(155, 97);
            this.cbReferralUrgency.Name = "cbReferralUrgency";
            this.cbReferralUrgency.Size = new System.Drawing.Size(201, 33);
            this.cbReferralUrgency.TabIndex = 20;
            this.cbReferralUrgency.Validating += new System.ComponentModel.CancelEventHandler(this.cbReferralUrgency_Validating);
            // 
            // viewStaffInfoCtrl1
            // 
            this.viewStaffInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewStaffInfoCtrl1.LoadedStaff = null;
            this.viewStaffInfoCtrl1.Location = new System.Drawing.Point(648, 353);
            this.viewStaffInfoCtrl1.Name = "viewStaffInfoCtrl1";
            this.viewStaffInfoCtrl1.Size = new System.Drawing.Size(651, 288);
            this.viewStaffInfoCtrl1.TabIndex = 21;
            // 
            // gbReferral
            // 
            this.gbReferral.Controls.Add(this.txtReferralReason);
            this.gbReferral.Controls.Add(this.txtSpeciality);
            this.gbReferral.Controls.Add(this.pictureBox4);
            this.gbReferral.Controls.Add(this.label8);
            this.gbReferral.Controls.Add(this.cbReferralUrgency);
            this.gbReferral.Controls.Add(this.pictureBox1);
            this.gbReferral.Controls.Add(this.label4);
            this.gbReferral.Controls.Add(this.pictureBox3);
            this.gbReferral.Controls.Add(this.label3);
            this.gbReferral.Location = new System.Drawing.Point(11, 647);
            this.gbReferral.Name = "gbReferral";
            this.gbReferral.Size = new System.Drawing.Size(1133, 150);
            this.gbReferral.TabIndex = 22;
            this.gbReferral.TabStop = false;
            this.gbReferral.Text = "Referral  Details";
            // 
            // txtReferralReason
            // 
            this.txtReferralReason.Location = new System.Drawing.Point(631, 40);
            this.txtReferralReason.Multiline = true;
            this.txtReferralReason.Name = "txtReferralReason";
            this.txtReferralReason.Size = new System.Drawing.Size(482, 104);
            this.txtReferralReason.TabIndex = 64;
            this.txtReferralReason.Validating += new System.ComponentModel.CancelEventHandler(this.txtReferralReason_Validating);
            // 
            // txtSpeciality
            // 
            this.txtSpeciality.Location = new System.Drawing.Point(162, 42);
            this.txtSpeciality.Name = "txtSpeciality";
            this.txtSpeciality.Size = new System.Drawing.Size(194, 30);
            this.txtSpeciality.TabIndex = 63;
            this.txtSpeciality.Validating += new System.ComponentModel.CancelEventHandler(this.txtSpeciality_Validating);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::ClinicSystem.Properties.Resources.note_32;
            this.pictureBox4.Location = new System.Drawing.Point(495, 38);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(41, 38);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 62;
            this.pictureBox4.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(543, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 25);
            this.label8.TabIndex = 61;
            this.label8.Text = "Reason:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ClinicSystem.Properties.Resources.warning_32;
            this.pictureBox1.Location = new System.Drawing.Point(10, 93);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 56;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 25);
            this.label4.TabIndex = 55;
            this.label4.Text = "Urgency:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ClinicSystem.Properties.Resources.outbox_32;
            this.pictureBox3.Location = new System.Drawing.Point(10, 38);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(41, 38);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 53;
            this.pictureBox3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 25);
            this.label3.TabIndex = 52;
            this.label3.Text = "Specialty:";
            // 
            // AddUpdateReferral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1544, 807);
            this.Controls.Add(this.gbReferral);
            this.Controls.Add(this.viewStaffInfoCtrl1);
            this.Controls.Add(this.viewSoapNoteCtrl1);
            this.Controls.Add(this.viewAppointmentInfoCtrl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.lblReferralId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbModeImage);
            this.Name = "AddUpdateReferral";
            this.Text = "New Referral";
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).EndInit();
            this.gbReferral.ResumeLayout(false);
            this.gbReferral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbModeImage;
        private System.Windows.Forms.Label lblReferralId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button BtnCancel;
        private Appointments.Controls.ViewAppointmentInfoCtrl viewAppointmentInfoCtrl1;
        private Appointments.SOAPNotes.Controls.ViewSoapNoteCtrl viewSoapNoteCtrl1;
        private System.Windows.Forms.ErrorProvider errors;
        private System.Windows.Forms.ComboBox cbReferralUrgency;
        private System.Windows.Forms.GroupBox gbReferral;
        private Staff.Controls.ViewStaffInfoCtrl viewStaffInfoCtrl1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReferralReason;
        private System.Windows.Forms.TextBox txtSpeciality;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}