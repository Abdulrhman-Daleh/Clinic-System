namespace ClinicSystem.Payment
{
    partial class ViewPayment
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            this.viewAppointmentInfoCtrl1 = new ClinicSystem.Appointments.Controls.ViewAppointmentInfoCtrl();
            this.viewPaymentCtrl1 = new ClinicSystem.Payment.Controls.ViewPaymentCtrl();
            this.viewStaffInfoCtrl1 = new ClinicSystem.Staff.Controls.ViewStaffInfoCtrl();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSubTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(82, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(238, 38);
            this.lblTitle.TabIndex = 15;
            this.lblTitle.Text = "View Payment";
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.money;
            this.pbModeImage.Location = new System.Drawing.Point(5, 2);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 14;
            this.pbModeImage.TabStop = false;
            // 
            // viewAppointmentInfoCtrl1
            // 
            this.viewAppointmentInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewAppointmentInfoCtrl1.Location = new System.Drawing.Point(5, 68);
            this.viewAppointmentInfoCtrl1.Name = "viewAppointmentInfoCtrl1";
            this.viewAppointmentInfoCtrl1.Size = new System.Drawing.Size(619, 556);
            this.viewAppointmentInfoCtrl1.TabIndex = 16;
            // 
            // viewPaymentCtrl1
            // 
            this.viewPaymentCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewPaymentCtrl1.Location = new System.Drawing.Point(630, 9);
            this.viewPaymentCtrl1.Name = "viewPaymentCtrl1";
            this.viewPaymentCtrl1.Size = new System.Drawing.Size(693, 240);
            this.viewPaymentCtrl1.TabIndex = 17;
            // 
            // viewStaffInfoCtrl1
            // 
            this.viewStaffInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewStaffInfoCtrl1.LoadedStaff = null;
            this.viewStaffInfoCtrl1.Location = new System.Drawing.Point(630, 255);
            this.viewStaffInfoCtrl1.Name = "viewStaffInfoCtrl1";
            this.viewStaffInfoCtrl1.Size = new System.Drawing.Size(678, 288);
            this.viewStaffInfoCtrl1.TabIndex = 18;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1163, 549);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Location = new System.Drawing.Point(84, 44);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(0, 25);
            this.lblSubTitle.TabIndex = 20;
            // 
            // ViewPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 632);
            this.Controls.Add(this.lblSubTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.viewStaffInfoCtrl1);
            this.Controls.Add(this.viewPaymentCtrl1);
            this.Controls.Add(this.viewAppointmentInfoCtrl1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbModeImage);
            this.Name = "ViewPayment";
            this.Text = "View Payment";
            this.Load += new System.EventHandler(this.ViewPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbModeImage;
        private Appointments.Controls.ViewAppointmentInfoCtrl viewAppointmentInfoCtrl1;
        private Controls.ViewPaymentCtrl viewPaymentCtrl1;
        private Staff.Controls.ViewStaffInfoCtrl viewStaffInfoCtrl1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSubTitle;
    }
}