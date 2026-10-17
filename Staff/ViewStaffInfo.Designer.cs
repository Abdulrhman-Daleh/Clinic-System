namespace ClinicSystem.Staff
{
    partial class ViewStaffInfo
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
            this.findStaffCtrl1 = new ClinicSystem.Staff.Controls.FindStaffCtrl();
            this.viewPersonInfoCtrl1 = new ClinicSystem.People.ViewPersonInfoCtrl();
            this.viewPersonContacts1 = new ClinicSystem.People.Controls.ViewPersonContacts();
            this.BtnDeactivate = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
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
            this.lblTitle.Size = new System.Drawing.Size(235, 38);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "View staff info";
            // 
            // findStaffCtrl1
            // 
            this.findStaffCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.findStaffCtrl1.Location = new System.Drawing.Point(12, 66);
            this.findStaffCtrl1.Name = "findStaffCtrl1";
            this.findStaffCtrl1.Size = new System.Drawing.Size(596, 372);
            this.findStaffCtrl1.TabIndex = 14;
            // 
            // viewPersonInfoCtrl1
            // 
            this.viewPersonInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewPersonInfoCtrl1.Location = new System.Drawing.Point(12, 446);
            this.viewPersonInfoCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.viewPersonInfoCtrl1.Name = "viewPersonInfoCtrl1";
            this.viewPersonInfoCtrl1.Size = new System.Drawing.Size(525, 207);
            this.viewPersonInfoCtrl1.TabIndex = 15;
            // 
            // viewPersonContacts1
            // 
            this.viewPersonContacts1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewPersonContacts1.Location = new System.Drawing.Point(12, 661);
            this.viewPersonContacts1.Name = "viewPersonContacts1";
            this.viewPersonContacts1.Size = new System.Drawing.Size(518, 182);
            this.viewPersonContacts1.TabIndex = 16;
            // 
            // BtnDeactivate
            // 
            this.BtnDeactivate.Image = global::ClinicSystem.Properties.Resources.deactivite_32;
            this.BtnDeactivate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnDeactivate.Location = new System.Drawing.Point(300, 850);
            this.BtnDeactivate.Name = "BtnDeactivate";
            this.BtnDeactivate.Size = new System.Drawing.Size(173, 66);
            this.BtnDeactivate.TabIndex = 19;
            this.BtnDeactivate.Text = "Deactivate";
            this.BtnDeactivate.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::ClinicSystem.Properties.Resources.save_32;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(484, 848);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(127, 66);
            this.btnEdit.TabIndex = 18;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(134, 850);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.view_32;
            this.pbModeImage.Location = new System.Drawing.Point(1, 2);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 12;
            this.pbModeImage.TabStop = false;
            // 
            // ViewStaffInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 926);
            this.Controls.Add(this.BtnDeactivate);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.viewPersonContacts1);
            this.Controls.Add(this.viewPersonInfoCtrl1);
            this.Controls.Add(this.findStaffCtrl1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbModeImage);
            this.Name = "ViewStaffInfo";
            this.Text = "View Staff Info";
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbModeImage;
        private Controls.FindStaffCtrl findStaffCtrl1;
        private People.ViewPersonInfoCtrl viewPersonInfoCtrl1;
        private People.Controls.ViewPersonContacts viewPersonContacts1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button BtnDeactivate;
    }
}