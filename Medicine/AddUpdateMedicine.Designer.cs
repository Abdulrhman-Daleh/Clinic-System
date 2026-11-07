namespace ClinicSystem.Medicine
{
    partial class AddUpdateMedicine
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
            this.lblMedicineId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mtxtUnitPrice = new System.Windows.Forms.MaskedTextBox();
            this.cbDosageForm = new System.Windows.Forms.ComboBox();
            this.cbStrengthType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.txtStrength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtDrugName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            this.errors = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMedicineId
            // 
            this.lblMedicineId.AutoSize = true;
            this.lblMedicineId.Location = new System.Drawing.Point(131, 70);
            this.lblMedicineId.Name = "lblMedicineId";
            this.lblMedicineId.Size = new System.Drawing.Size(0, 25);
            this.lblMedicineId.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Medicine Id:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(85, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(305, 38);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Add New Medicine";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mtxtUnitPrice);
            this.groupBox1.Controls.Add(this.cbDosageForm);
            this.groupBox1.Controls.Add(this.cbStrengthType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.txtStrength);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.txtDrugName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(18, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(582, 223);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Medicine Info";
            // 
            // mtxtUnitPrice
            // 
            this.mtxtUnitPrice.Location = new System.Drawing.Point(379, 185);
            this.mtxtUnitPrice.Mask = "0.00";
            this.mtxtUnitPrice.Name = "mtxtUnitPrice";
            this.mtxtUnitPrice.Size = new System.Drawing.Size(89, 30);
            this.mtxtUnitPrice.TabIndex = 28;
            this.mtxtUnitPrice.ValidatingType = typeof(int);
            this.mtxtUnitPrice.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtUnitPrice_Validating);
            // 
            // cbDosageForm
            // 
            this.cbDosageForm.FormattingEnabled = true;
            this.cbDosageForm.Location = new System.Drawing.Point(30, 182);
            this.cbDosageForm.Name = "cbDosageForm";
            this.cbDosageForm.Size = new System.Drawing.Size(180, 33);
            this.cbDosageForm.TabIndex = 27;
            this.cbDosageForm.Validating += new System.ComponentModel.CancelEventHandler(this.cbDosageForm_Validating);
            // 
            // cbStrengthType
            // 
            this.cbStrengthType.FormattingEnabled = true;
            this.cbStrengthType.Location = new System.Drawing.Point(450, 79);
            this.cbStrengthType.Name = "cbStrengthType";
            this.cbStrengthType.Size = new System.Drawing.Size(75, 33);
            this.cbStrengthType.TabIndex = 26;
            this.cbStrengthType.Validating += new System.ComponentModel.CancelEventHandler(this.cbStrengthType_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(430, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 25);
            this.label5.TabIndex = 24;
            this.label5.Text = "Unit Price";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::ClinicSystem.Properties.Resources.money_32;
            this.pictureBox4.Location = new System.Drawing.Point(380, 131);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(40, 45);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 23;
            this.pictureBox4.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 25);
            this.label4.TabIndex = 21;
            this.label4.Text = "Dosage Form";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ClinicSystem.Properties.Resources.plus_32;
            this.pictureBox3.Location = new System.Drawing.Point(30, 131);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(40, 45);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 20;
            this.pictureBox3.TabStop = false;
            // 
            // txtStrength
            // 
            this.txtStrength.Location = new System.Drawing.Point(380, 82);
            this.txtStrength.Name = "txtStrength";
            this.txtStrength.Size = new System.Drawing.Size(64, 30);
            this.txtStrength.TabIndex = 19;
            this.txtStrength.Validating += new System.ComponentModel.CancelEventHandler(this.txtStrength_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(430, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 25);
            this.label3.TabIndex = 18;
            this.label3.Text = "Strength";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ClinicSystem.Properties.Resources.balance_scale_32;
            this.pictureBox2.Location = new System.Drawing.Point(380, 31);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 45);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // txtDrugName
            // 
            this.txtDrugName.Location = new System.Drawing.Point(30, 82);
            this.txtDrugName.Name = "txtDrugName";
            this.txtDrugName.Size = new System.Drawing.Size(195, 30);
            this.txtDrugName.TabIndex = 16;
            this.txtDrugName.Validating += new System.ComponentModel.CancelEventHandler(this.txtDrugName_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 25);
            this.label2.TabIndex = 15;
            this.label2.Text = "Drug Name";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ClinicSystem.Properties.Resources.pill_32;
            this.pictureBox1.Location = new System.Drawing.Point(30, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::ClinicSystem.Properties.Resources.save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(372, 327);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(228, 66);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save Medicine";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(204, 327);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 66);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.first_aid_box_32;
            this.pbModeImage.Location = new System.Drawing.Point(12, 14);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 7;
            this.pbModeImage.TabStop = false;
            // 
            // errors
            // 
            this.errors.ContainerControl = this;
            // 
            // AddUpdateMedicine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 400);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblMedicineId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbModeImage);
            this.Name = "AddUpdateMedicine";
            this.Text = "Add Update Medicine";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMedicineId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbModeImage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TextBox txtStrength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtDrugName;
        private System.Windows.Forms.MaskedTextBox mtxtUnitPrice;
        private System.Windows.Forms.ComboBox cbDosageForm;
        private System.Windows.Forms.ComboBox cbStrengthType;
        private System.Windows.Forms.ErrorProvider errors;
    }
}