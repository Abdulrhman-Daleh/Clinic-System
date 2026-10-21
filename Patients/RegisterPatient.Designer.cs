namespace ClinicSystem.Patients
{
    partial class RegisterPatient
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
            this.findPersonCtrl1 = new ClinicSystem.People.Controls.FindPersonCtrl();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPatientId = new System.Windows.Forms.Label();
            this.viewPersonContacts1 = new ClinicSystem.People.Controls.ViewPersonContacts();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(86, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(344, 38);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Register New Patient";
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.plus_32;
            this.pbModeImage.Location = new System.Drawing.Point(5, 14);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 2;
            this.pbModeImage.TabStop = false;
            // 
            // findPersonCtrl1
            // 
            this.findPersonCtrl1.AllowFilter = true;
            this.findPersonCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.findPersonCtrl1.Location = new System.Drawing.Point(13, 98);
            this.findPersonCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.findPersonCtrl1.Name = "findPersonCtrl1";
            this.findPersonCtrl1.Size = new System.Drawing.Size(555, 287);
            this.findPersonCtrl1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Patient Id";
            // 
            // lblPatientId
            // 
            this.lblPatientId.AutoSize = true;
            this.lblPatientId.Location = new System.Drawing.Point(119, 69);
            this.lblPatientId.Name = "lblPatientId";
            this.lblPatientId.Size = new System.Drawing.Size(0, 25);
            this.lblPatientId.TabIndex = 6;
            // 
            // viewPersonContacts1
            // 
            this.viewPersonContacts1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewPersonContacts1.Location = new System.Drawing.Point(17, 391);
            this.viewPersonContacts1.Name = "viewPersonContacts1";
            this.viewPersonContacts1.Size = new System.Drawing.Size(518, 182);
            this.viewPersonContacts1.TabIndex = 7;
            // 
            // btnRegister
            // 
            this.btnRegister.Image = global::ClinicSystem.Properties.Resources.save_32;
            this.btnRegister.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegister.Location = new System.Drawing.Point(340, 576);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(228, 66);
            this.btnRegister.TabIndex = 9;
            this.btnRegister.Text = "Register Patient";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(172, 576);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 66);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddUpdatePatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 647);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.viewPersonContacts1);
            this.Controls.Add(this.lblPatientId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.findPersonCtrl1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbModeImage);
            this.Name = "AddUpdatePatient";
            this.Text = "Add Update Patient";
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbModeImage;
        private People.Controls.FindPersonCtrl findPersonCtrl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPatientId;
        private People.Controls.ViewPersonContacts viewPersonContacts1;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCancel;
    }
}