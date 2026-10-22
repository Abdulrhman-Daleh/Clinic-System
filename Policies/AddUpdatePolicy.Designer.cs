namespace ClinicSystem.Policies
{
    partial class AddUpdatePolicy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUpdatePolicy));
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nudNoShowFreezeDays = new System.Windows.Forms.NumericUpDown();
            this.nudFeesPerDay = new System.Windows.Forms.NumericUpDown();
            this.nudNoShowFee = new System.Windows.Forms.NumericUpDown();
            this.chkFreezeDays = new System.Windows.Forms.CheckBox();
            this.chk_FeesPerDay = new System.Windows.Forms.CheckBox();
            this.chk_No_ShowFees = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbPolicyStatus = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpActiveTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpActiveFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.errors = new System.Windows.Forms.ErrorProvider(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.lblPolicyId = new System.Windows.Forms.Label();
            this.findStaffCtrl1 = new ClinicSystem.Staff.Controls.FindStaffCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoShowFreezeDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFeesPerDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoShowFee)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(73, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(261, 38);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "Add New Policy";
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.shield_32;
            this.pbModeImage.Location = new System.Drawing.Point(1, 11);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 12;
            this.pbModeImage.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::ClinicSystem.Properties.Resources.save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(457, 835);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(188, 66);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Add Policy";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(291, 835);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 66);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.nudNoShowFreezeDays);
            this.groupBox1.Controls.Add(this.nudFeesPerDay);
            this.groupBox1.Controls.Add(this.nudNoShowFee);
            this.groupBox1.Controls.Add(this.chkFreezeDays);
            this.groupBox1.Controls.Add(this.chk_FeesPerDay);
            this.groupBox1.Controls.Add(this.chk_No_ShowFees);
            this.groupBox1.Location = new System.Drawing.Point(12, 490);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 195);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Penalty Rules";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ClinicSystem.Properties.Resources.hash_32;
            this.pictureBox3.Location = new System.Drawing.Point(12, 138);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(48, 41);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 21;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(11, 89);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 41);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 41);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // nudNoShowFreezeDays
            // 
            this.nudNoShowFreezeDays.Location = new System.Drawing.Point(377, 143);
            this.nudNoShowFreezeDays.Name = "nudNoShowFreezeDays";
            this.nudNoShowFreezeDays.Size = new System.Drawing.Size(120, 30);
            this.nudNoShowFreezeDays.TabIndex = 5;
            this.nudNoShowFreezeDays.Validated += new System.EventHandler(this.nudNoShowFreezeDays_Validating);
            // 
            // nudFeesPerDay
            // 
            this.nudFeesPerDay.Location = new System.Drawing.Point(279, 94);
            this.nudFeesPerDay.Name = "nudFeesPerDay";
            this.nudFeesPerDay.Size = new System.Drawing.Size(120, 30);
            this.nudFeesPerDay.TabIndex = 4;
            this.nudFeesPerDay.Validating += new System.ComponentModel.CancelEventHandler(this.nudFeesPerDay_Validating_1);
            // 
            // nudNoShowFee
            // 
            this.nudNoShowFee.Location = new System.Drawing.Point(229, 42);
            this.nudNoShowFee.Name = "nudNoShowFee";
            this.nudNoShowFee.Size = new System.Drawing.Size(120, 30);
            this.nudNoShowFee.TabIndex = 3;
            this.nudNoShowFee.Validating += new System.ComponentModel.CancelEventHandler(this.nudNoShowFee_Validating_1);
            // 
            // chkFreezeDays
            // 
            this.chkFreezeDays.AutoSize = true;
            this.chkFreezeDays.Location = new System.Drawing.Point(73, 144);
            this.chkFreezeDays.Name = "chkFreezeDays";
            this.chkFreezeDays.Size = new System.Drawing.Size(293, 29);
            this.chkFreezeDays.TabIndex = 2;
            this.chkFreezeDays.Text = "Booking freeze after no-show:";
            this.chkFreezeDays.UseVisualStyleBackColor = true;
            this.chkFreezeDays.CheckedChanged += new System.EventHandler(this.chkFreezeDays_CheckedChanged);
            // 
            // chk_FeesPerDay
            // 
            this.chk_FeesPerDay.AutoSize = true;
            this.chk_FeesPerDay.Location = new System.Drawing.Point(73, 95);
            this.chk_FeesPerDay.Name = "chk_FeesPerDay";
            this.chk_FeesPerDay.Size = new System.Drawing.Size(194, 29);
            this.chk_FeesPerDay.TabIndex = 1;
            this.chk_FeesPerDay.Text = "Late fee — per day:";
            this.chk_FeesPerDay.UseVisualStyleBackColor = true;
            this.chk_FeesPerDay.CheckedChanged += new System.EventHandler(this.chk_FeesPerDay_CheckedChanged);
            // 
            // chk_No_ShowFees
            // 
            this.chk_No_ShowFees.AutoSize = true;
            this.chk_No_ShowFees.Location = new System.Drawing.Point(69, 43);
            this.chk_No_ShowFees.Name = "chk_No_ShowFees";
            this.chk_No_ShowFees.Size = new System.Drawing.Size(150, 29);
            this.chk_No_ShowFees.TabIndex = 0;
            this.chk_No_ShowFees.Text = "No-show fee:";
            this.chk_No_ShowFees.UseVisualStyleBackColor = true;
            this.chk_No_ShowFees.CheckedChanged += new System.EventHandler(this.chk_No_ShowFees_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbPolicyStatus);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dtpActiveTo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtpActiveFrom);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.pictureBox4);
            this.groupBox2.Controls.Add(this.pictureBox5);
            this.groupBox2.Controls.Add(this.pictureBox6);
            this.groupBox2.Location = new System.Drawing.Point(22, 691);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(623, 138);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Validity ";
            // 
            // cbPolicyStatus
            // 
            this.cbPolicyStatus.FormattingEnabled = true;
            this.cbPolicyStatus.Location = new System.Drawing.Point(500, 89);
            this.cbPolicyStatus.Name = "cbPolicyStatus";
            this.cbPolicyStatus.Size = new System.Drawing.Size(117, 33);
            this.cbPolicyStatus.TabIndex = 27;
            this.cbPolicyStatus.Validating += new System.ComponentModel.CancelEventHandler(this.cbPolicyStatus_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(426, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 25);
            this.label3.TabIndex = 26;
            this.label3.Text = "Status";
            // 
            // dtpActiveTo
            // 
            this.dtpActiveTo.Location = new System.Drawing.Point(158, 94);
            this.dtpActiveTo.Name = "dtpActiveTo";
            this.dtpActiveTo.Size = new System.Drawing.Size(200, 30);
            this.dtpActiveTo.TabIndex = 25;
            this.dtpActiveTo.ValueChanged += new System.EventHandler(this.dtpActiveTo_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "Active To:";
            // 
            // dtpActiveFrom
            // 
            this.dtpActiveFrom.Location = new System.Drawing.Point(171, 42);
            this.dtpActiveFrom.Name = "dtpActiveFrom";
            this.dtpActiveFrom.Size = new System.Drawing.Size(200, 30);
            this.dtpActiveFrom.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 25);
            this.label1.TabIndex = 22;
            this.label1.Text = "Active from";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::ClinicSystem.Properties.Resources.status_32;
            this.pictureBox4.Location = new System.Drawing.Point(376, 85);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(48, 41);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 21;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::ClinicSystem.Properties.Resources.calendar_32;
            this.pictureBox5.Location = new System.Drawing.Point(11, 89);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(48, 41);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 20;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::ClinicSystem.Properties.Resources.calendar_32;
            this.pictureBox6.Location = new System.Drawing.Point(10, 37);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(48, 41);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 19;
            this.pictureBox6.TabStop = false;
            // 
            // errors
            // 
            this.errors.ContainerControl = this;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 25);
            this.label4.TabIndex = 23;
            this.label4.Text = "Policy Id:";
            // 
            // lblPolicyId
            // 
            this.lblPolicyId.AutoSize = true;
            this.lblPolicyId.Location = new System.Drawing.Point(161, 66);
            this.lblPolicyId.Name = "lblPolicyId";
            this.lblPolicyId.Size = new System.Drawing.Size(0, 25);
            this.lblPolicyId.TabIndex = 24;
            // 
            // findStaffCtrl1
            // 
            this.findStaffCtrl1.AllowFind = true;
            this.findStaffCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.findStaffCtrl1.Location = new System.Drawing.Point(12, 94);
            this.findStaffCtrl1.Name = "findStaffCtrl1";
            this.findStaffCtrl1.Size = new System.Drawing.Size(596, 372);
            this.findStaffCtrl1.TabIndex = 25;
            // 
            // AddUpdatePolicy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 908);
            this.Controls.Add(this.findStaffCtrl1);
            this.Controls.Add(this.lblPolicyId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbModeImage);
            this.Name = "AddUpdatePolicy";
            this.Text = "Add Update Policy";
            this.Load += new System.EventHandler(this.AddUpdatePolicy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoShowFreezeDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFeesPerDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoShowFee)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbModeImage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkFreezeDays;
        private System.Windows.Forms.CheckBox chk_FeesPerDay;
        private System.Windows.Forms.CheckBox chk_No_ShowFees;
        private System.Windows.Forms.NumericUpDown nudNoShowFee;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown nudNoShowFreezeDays;
        private System.Windows.Forms.NumericUpDown nudFeesPerDay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.ComboBox cbPolicyStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpActiveTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpActiveFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errors;
        private System.Windows.Forms.Label lblPolicyId;
        private System.Windows.Forms.Label label4;
        private Staff.Controls.FindStaffCtrl findStaffCtrl1;
    }
}