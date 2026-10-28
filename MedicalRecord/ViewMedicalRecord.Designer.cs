namespace ClinicSystem.MedicalRecord
{
    partial class ViewMedicalRecord
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
            this.lblMedicalId = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.findMRecord1 = new ClinicSystem.MedicalRecord.Controls.FindMRecord();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMedicalId
            // 
            this.lblMedicalId.AutoSize = true;
            this.lblMedicalId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedicalId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMedicalId.Location = new System.Drawing.Point(83, 22);
            this.lblMedicalId.Name = "lblMedicalId";
            this.lblMedicalId.Size = new System.Drawing.Size(0, 29);
            this.lblMedicalId.TabIndex = 73;
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblClass.Location = new System.Drawing.Point(451, 22);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(0, 29);
            this.lblClass.TabIndex = 77;
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::ClinicSystem.Properties.Resources.update_32;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(580, 664);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(160, 66);
            this.btnEdit.TabIndex = 76;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(413, 664);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 75;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ClinicSystem.Properties.Resources.manage_medical_records_64;
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(65, 52);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 72;
            this.pictureBox2.TabStop = false;
            // 
            // findMRecord1
            // 
            this.findMRecord1.AllowedFind = true;
            this.findMRecord1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.findMRecord1.Location = new System.Drawing.Point(0, 70);
            this.findMRecord1.Name = "findMRecord1";
            this.findMRecord1.Size = new System.Drawing.Size(740, 588);
            this.findMRecord1.TabIndex = 78;
            // 
            // ViewMedicalRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 739);
            this.Controls.Add(this.findMRecord1);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblMedicalId);
            this.Controls.Add(this.pictureBox2);
            this.Name = "ViewMedicalRecord";
            this.Text = "View Medical Record";
            this.Load += new System.EventHandler(this.ViewMedicalRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMedicalId;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblClass;
        private Controls.FindMRecord findMRecord1;
    }
}