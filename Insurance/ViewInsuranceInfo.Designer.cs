namespace ClinicSystem.Insurance
{
    partial class ViewInsuranceInfo
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblInsuranceId = new System.Windows.Forms.Label();
            this.viewInsuranceInfoCtrl1 = new ClinicSystem.Insurance.Controls.ViewInsuranceInfoCtrl();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ClinicSystem.Properties.Resources.Manage_Insurance_64;
            this.pictureBox2.Location = new System.Drawing.Point(3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(65, 52);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 70;
            this.pictureBox2.TabStop = false;
            // 
            // lblInsuranceId
            // 
            this.lblInsuranceId.AutoSize = true;
            this.lblInsuranceId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuranceId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblInsuranceId.Location = new System.Drawing.Point(74, 14);
            this.lblInsuranceId.Name = "lblInsuranceId";
            this.lblInsuranceId.Size = new System.Drawing.Size(0, 29);
            this.lblInsuranceId.TabIndex = 71;
            // 
            // viewInsuranceInfoCtrl1
            // 
            this.viewInsuranceInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewInsuranceInfoCtrl1.Location = new System.Drawing.Point(12, 60);
            this.viewInsuranceInfoCtrl1.Name = "viewInsuranceInfoCtrl1";
            this.viewInsuranceInfoCtrl1.Size = new System.Drawing.Size(754, 244);
            this.viewInsuranceInfoCtrl1.TabIndex = 72;
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::ClinicSystem.Properties.Resources.update_32;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(606, 319);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(160, 66);
            this.btnEdit.TabIndex = 74;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(439, 319);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 73;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ViewInsuranceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 392);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.viewInsuranceInfoCtrl1);
            this.Controls.Add(this.lblInsuranceId);
            this.Controls.Add(this.pictureBox2);
            this.Name = "ViewInsuranceInfo";
            this.Text = "View Insurance Info";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblInsuranceId;
        private Controls.ViewInsuranceInfoCtrl viewInsuranceInfoCtrl1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClose;
    }
}