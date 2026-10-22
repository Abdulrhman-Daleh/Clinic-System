namespace ClinicSystem.Policies
{
    partial class ViewPolicyInfo
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
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.findPolicyInfo1 = new ClinicSystem.Policies.Controls.FindPolicyInfo();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ClinicSystem.Properties.Resources.find_policy_32;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::ClinicSystem.Properties.Resources.update_32;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(644, 382);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(160, 66);
            this.btnEdit.TabIndex = 16;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(477, 382);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.2F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(74, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 25);
            this.lblTitle.TabIndex = 19;
            // 
            // findPolicyInfo1
            // 
            this.findPolicyInfo1.AllowedFind = true;
            this.findPolicyInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.findPolicyInfo1.Location = new System.Drawing.Point(12, 63);
            this.findPolicyInfo1.Name = "findPolicyInfo1";
            this.findPolicyInfo1.Size = new System.Drawing.Size(792, 314);
            this.findPolicyInfo1.TabIndex = 20;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.2F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStatus.Location = new System.Drawing.Point(660, 23);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 25);
            this.lblStatus.TabIndex = 21;
            // 
            // ViewPolicyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 460);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.findPolicyInfo1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClose);
            this.Name = "ViewPolicyInfo";
            this.Text = "View Policy Info";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private Controls.FindPolicyInfo findPolicyInfo1;
        private System.Windows.Forms.Label lblStatus;
    }
}