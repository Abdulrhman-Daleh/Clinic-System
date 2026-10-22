namespace ClinicSystem.Patients
{
    partial class ViewPatient
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblFullname = new System.Windows.Forms.Label();
            this.viewPersonInfoCtrl1 = new ClinicSystem.People.ViewPersonInfoCtrl();
            this.viewPersonContacts1 = new ClinicSystem.People.Controls.ViewPersonContacts();
            this.patientInfoCtrl1 = new ClinicSystem.Patients.Controls.PatientInfoCtrl();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ClinicSystem.Properties.Resources.user_32;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblFullname
            // 
            this.lblFullname.AutoSize = true;
            this.lblFullname.Location = new System.Drawing.Point(68, 20);
            this.lblFullname.Name = "lblFullname";
            this.lblFullname.Size = new System.Drawing.Size(0, 25);
            this.lblFullname.TabIndex = 1;
            // 
            // viewPersonInfoCtrl1
            // 
            this.viewPersonInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewPersonInfoCtrl1.Location = new System.Drawing.Point(13, 60);
            this.viewPersonInfoCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.viewPersonInfoCtrl1.Name = "viewPersonInfoCtrl1";
            this.viewPersonInfoCtrl1.Size = new System.Drawing.Size(525, 207);
            this.viewPersonInfoCtrl1.TabIndex = 2;
            // 
            // viewPersonContacts1
            // 
            this.viewPersonContacts1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewPersonContacts1.Location = new System.Drawing.Point(12, 275);
            this.viewPersonContacts1.Name = "viewPersonContacts1";
            this.viewPersonContacts1.Size = new System.Drawing.Size(518, 182);
            this.viewPersonContacts1.TabIndex = 3;
            // 
            // patientInfoCtrl1
            // 
            this.patientInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.patientInfoCtrl1.Location = new System.Drawing.Point(23, 463);
            this.patientInfoCtrl1.Name = "patientInfoCtrl1";
            this.patientInfoCtrl1.Size = new System.Drawing.Size(555, 209);
            this.patientInfoCtrl1.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(418, 678);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ViewPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(585, 744);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.patientInfoCtrl1);
            this.Controls.Add(this.viewPersonContacts1);
            this.Controls.Add(this.viewPersonInfoCtrl1);
            this.Controls.Add(this.lblFullname);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ViewPatient";
            this.Text = "View Patient";
            this.Load += new System.EventHandler(this.ViewPatient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblFullname;
        private People.ViewPersonInfoCtrl viewPersonInfoCtrl1;
        private People.Controls.ViewPersonContacts viewPersonContacts1;
        private Controls.PatientInfoCtrl patientInfoCtrl1;
        private System.Windows.Forms.Button btnClose;
    }
}