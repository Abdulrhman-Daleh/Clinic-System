namespace ClinicSystem
{
    partial class AddUpdatePerson
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.personalInfoCtrl = new ClinicSystem.People.Controls.PersonalInfoCtrl();
            this.personContactCtrl = new ClinicSystem.People.Controls.PersonContactCtrl();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPersonId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ClinicSystem.Properties.Resources.plus_32;
            this.pictureBox1.Location = new System.Drawing.Point(20, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 52);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(101, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(277, 38);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Add New Person";
            // 
            // personalInfoCtrl
            // 
            this.personalInfoCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.personalInfoCtrl.Location = new System.Drawing.Point(20, 118);
            this.personalInfoCtrl.Name = "personalInfoCtrl";
            this.personalInfoCtrl.Size = new System.Drawing.Size(597, 294);
            this.personalInfoCtrl.TabIndex = 2;
            // 
            // personContactCtrl
            // 
            this.personContactCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.personContactCtrl.Location = new System.Drawing.Point(20, 418);
            this.personContactCtrl.Name = "personContactCtrl";
            this.personContactCtrl.Size = new System.Drawing.Size(597, 244);
            this.personContactCtrl.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(263, 668);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 66);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::ClinicSystem.Properties.Resources.save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(429, 668);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(188, 66);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Add Person";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Person Id:";
            // 
            // lblPersonId
            // 
            this.lblPersonId.AutoSize = true;
            this.lblPersonId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersonId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblPersonId.Location = new System.Drawing.Point(133, 86);
            this.lblPersonId.Name = "lblPersonId";
            this.lblPersonId.Size = new System.Drawing.Size(0, 25);
            this.lblPersonId.TabIndex = 7;
            // 
            // AddUpdatePerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 742);
            this.Controls.Add(this.lblPersonId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.personContactCtrl);
            this.Controls.Add(this.personalInfoCtrl);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AddUpdatePerson";
            this.Text = "Add New Person";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private People.Controls.PersonalInfoCtrl personalInfoCtrl;
        private People.Controls.PersonContactCtrl personContactCtrl;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPersonId;
    }
}

