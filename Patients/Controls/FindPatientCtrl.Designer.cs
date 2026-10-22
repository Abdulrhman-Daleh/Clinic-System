namespace ClinicSystem.Patients.Controls
{
    partial class FindPatientCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbFindBy = new System.Windows.Forms.GroupBox();
            this.AddPatient = new System.Windows.Forms.Button();
            this.BtnFindPatient = new System.Windows.Forms.Button();
            this.txtFindBy = new System.Windows.Forms.TextBox();
            this.cbFindBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.patientInfoCtrl1 = new ClinicSystem.Patients.Controls.PatientInfoCtrl();
            this.gbFindBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFindBy
            // 
            this.gbFindBy.Controls.Add(this.AddPatient);
            this.gbFindBy.Controls.Add(this.BtnFindPatient);
            this.gbFindBy.Controls.Add(this.txtFindBy);
            this.gbFindBy.Controls.Add(this.cbFindBy);
            this.gbFindBy.Controls.Add(this.label1);
            this.gbFindBy.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFindBy.Location = new System.Drawing.Point(0, 0);
            this.gbFindBy.Name = "gbFindBy";
            this.gbFindBy.Size = new System.Drawing.Size(609, 100);
            this.gbFindBy.TabIndex = 1;
            this.gbFindBy.TabStop = false;
            this.gbFindBy.Text = "Find Patient";
            // 
            // AddPatient
            // 
            this.AddPatient.Image = global::ClinicSystem.Properties.Resources.add_staff_32;
            this.AddPatient.Location = new System.Drawing.Point(520, 31);
            this.AddPatient.Name = "AddPatient";
            this.AddPatient.Size = new System.Drawing.Size(60, 49);
            this.AddPatient.TabIndex = 4;
            this.AddPatient.UseVisualStyleBackColor = true;
            // 
            // BtnFindPatient
            // 
            this.BtnFindPatient.Image = global::ClinicSystem.Properties.Resources.view_32;
            this.BtnFindPatient.Location = new System.Drawing.Point(456, 31);
            this.BtnFindPatient.Name = "BtnFindPatient";
            this.BtnFindPatient.Size = new System.Drawing.Size(60, 49);
            this.BtnFindPatient.TabIndex = 3;
            this.BtnFindPatient.UseVisualStyleBackColor = true;
            // 
            // txtFindBy
            // 
            this.txtFindBy.Location = new System.Drawing.Point(307, 40);
            this.txtFindBy.MaxLength = 5;
            this.txtFindBy.Name = "txtFindBy";
            this.txtFindBy.Size = new System.Drawing.Size(138, 30);
            this.txtFindBy.TabIndex = 2;
            // 
            // cbFindBy
            // 
            this.cbFindBy.FormattingEnabled = true;
            this.cbFindBy.Location = new System.Drawing.Point(148, 39);
            this.cbFindBy.Name = "cbFindBy";
            this.cbFindBy.Size = new System.Drawing.Size(151, 33);
            this.cbFindBy.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find By:";
            // 
            // patientInfoCtrl1
            // 
            this.patientInfoCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.patientInfoCtrl1.Location = new System.Drawing.Point(0, 100);
            this.patientInfoCtrl1.Name = "patientInfoCtrl1";
            this.patientInfoCtrl1.Size = new System.Drawing.Size(609, 210);
            this.patientInfoCtrl1.TabIndex = 2;
            // 
            // FindPatientCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.patientInfoCtrl1);
            this.Controls.Add(this.gbFindBy);
            this.Name = "FindPatientCtrl";
            this.Size = new System.Drawing.Size(609, 310);
            this.gbFindBy.ResumeLayout(false);
            this.gbFindBy.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFindBy;
        private System.Windows.Forms.Button AddPatient;
        private System.Windows.Forms.Button BtnFindPatient;
        private System.Windows.Forms.TextBox txtFindBy;
        private System.Windows.Forms.ComboBox cbFindBy;
        private System.Windows.Forms.Label label1;
        private PatientInfoCtrl patientInfoCtrl1;
    }
}
