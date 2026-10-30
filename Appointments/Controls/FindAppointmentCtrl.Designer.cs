namespace ClinicSystem.Appointments.Controls
{
    partial class FindAppointmentCtrl
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
            this.components = new System.ComponentModel.Container();
            this.viewAppointmentInfoCtrl1 = new ClinicSystem.Appointments.Controls.ViewAppointmentInfoCtrl();
            this.gbFindBy = new System.Windows.Forms.GroupBox();
            this.AddNewAppointment = new System.Windows.Forms.Button();
            this.BtnFindAppointment = new System.Windows.Forms.Button();
            this.txtFindBy = new System.Windows.Forms.TextBox();
            this.cbFindBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errors = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFindBy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).BeginInit();
            this.SuspendLayout();
            // 
            // viewAppointmentInfoCtrl1
            // 
            this.viewAppointmentInfoCtrl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.viewAppointmentInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewAppointmentInfoCtrl1.Location = new System.Drawing.Point(0, 112);
            this.viewAppointmentInfoCtrl1.Name = "viewAppointmentInfoCtrl1";
            this.viewAppointmentInfoCtrl1.Size = new System.Drawing.Size(597, 599);
            this.viewAppointmentInfoCtrl1.TabIndex = 0;
            // 
            // gbFindBy
            // 
            this.gbFindBy.Controls.Add(this.AddNewAppointment);
            this.gbFindBy.Controls.Add(this.BtnFindAppointment);
            this.gbFindBy.Controls.Add(this.txtFindBy);
            this.gbFindBy.Controls.Add(this.cbFindBy);
            this.gbFindBy.Controls.Add(this.label1);
            this.gbFindBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFindBy.Location = new System.Drawing.Point(0, 0);
            this.gbFindBy.Name = "gbFindBy";
            this.gbFindBy.Size = new System.Drawing.Size(597, 112);
            this.gbFindBy.TabIndex = 2;
            this.gbFindBy.TabStop = false;
            this.gbFindBy.Text = "Find Appointment";
            // 
            // AddNewAppointment
            // 
            this.AddNewAppointment.Image = global::ClinicSystem.Properties.Resources.view_record_32;
            this.AddNewAppointment.Location = new System.Drawing.Point(520, 39);
            this.AddNewAppointment.Name = "AddNewAppointment";
            this.AddNewAppointment.Size = new System.Drawing.Size(60, 49);
            this.AddNewAppointment.TabIndex = 4;
            this.AddNewAppointment.UseVisualStyleBackColor = true;
            this.AddNewAppointment.Click += new System.EventHandler(this.AddNewAppointment_Click);
            // 
            // BtnFindAppointment
            // 
            this.BtnFindAppointment.Image = global::ClinicSystem.Properties.Resources.add_appointment;
            this.BtnFindAppointment.Location = new System.Drawing.Point(456, 39);
            this.BtnFindAppointment.Name = "BtnFindAppointment";
            this.BtnFindAppointment.Size = new System.Drawing.Size(60, 49);
            this.BtnFindAppointment.TabIndex = 3;
            this.BtnFindAppointment.UseVisualStyleBackColor = true;
            this.BtnFindAppointment.Click += new System.EventHandler(this.BtnFindAppointment_Click);
            // 
            // txtFindBy
            // 
            this.txtFindBy.Location = new System.Drawing.Point(307, 48);
            this.txtFindBy.MaxLength = 5;
            this.txtFindBy.Name = "txtFindBy";
            this.txtFindBy.Size = new System.Drawing.Size(138, 30);
            this.txtFindBy.TabIndex = 2;
            this.txtFindBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFindBy_KeyPress);
            this.txtFindBy.Validating += new System.ComponentModel.CancelEventHandler(this.txtFindBy_Validating);
            // 
            // cbFindBy
            // 
            this.cbFindBy.FormattingEnabled = true;
            this.cbFindBy.Location = new System.Drawing.Point(148, 47);
            this.cbFindBy.Name = "cbFindBy";
            this.cbFindBy.Size = new System.Drawing.Size(151, 33);
            this.cbFindBy.TabIndex = 1;
            this.cbFindBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbFindBy_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find By:";
            // 
            // errors
            // 
            this.errors.ContainerControl = this;
            // 
            // FindAppointmentCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFindBy);
            this.Controls.Add(this.viewAppointmentInfoCtrl1);
            this.Name = "FindAppointmentCtrl";
            this.Size = new System.Drawing.Size(597, 711);
            this.gbFindBy.ResumeLayout(false);
            this.gbFindBy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ViewAppointmentInfoCtrl viewAppointmentInfoCtrl1;
        private System.Windows.Forms.GroupBox gbFindBy;
        private System.Windows.Forms.Button AddNewAppointment;
        private System.Windows.Forms.Button BtnFindAppointment;
        private System.Windows.Forms.TextBox txtFindBy;
        private System.Windows.Forms.ComboBox cbFindBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errors;
    }
}
