namespace ClinicSystem.Staff.Controls
{
    partial class FindStaffCtrl
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
            this.gbFindBy = new System.Windows.Forms.GroupBox();
            this.AddStaff = new System.Windows.Forms.Button();
            this.BtnFindStaff = new System.Windows.Forms.Button();
            this.txtFindBy = new System.Windows.Forms.TextBox();
            this.cbFindBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.viewStaffInfoCtrl = new ClinicSystem.Staff.Controls.ViewStaffInfoCtrl();
            this.errors = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFindBy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFindBy
            // 
            this.gbFindBy.Controls.Add(this.AddStaff);
            this.gbFindBy.Controls.Add(this.BtnFindStaff);
            this.gbFindBy.Controls.Add(this.txtFindBy);
            this.gbFindBy.Controls.Add(this.cbFindBy);
            this.gbFindBy.Controls.Add(this.label1);
            this.gbFindBy.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFindBy.Location = new System.Drawing.Point(0, 0);
            this.gbFindBy.Name = "gbFindBy";
            this.gbFindBy.Size = new System.Drawing.Size(596, 100);
            this.gbFindBy.TabIndex = 0;
            this.gbFindBy.TabStop = false;
            this.gbFindBy.Text = "Find Staff";
            // 
            // AddStaff
            // 
            this.AddStaff.Image = global::ClinicSystem.Properties.Resources.find_32;
            this.AddStaff.Location = new System.Drawing.Point(520, 31);
            this.AddStaff.Name = "AddStaff";
            this.AddStaff.Size = new System.Drawing.Size(60, 49);
            this.AddStaff.TabIndex = 4;
            this.AddStaff.UseVisualStyleBackColor = true;
            this.AddStaff.Click += new System.EventHandler(this.AddStaff_Click);
            // 
            // BtnFindStaff
            // 
            this.BtnFindStaff.Image = global::ClinicSystem.Properties.Resources.view_32;
            this.BtnFindStaff.Location = new System.Drawing.Point(456, 31);
            this.BtnFindStaff.Name = "BtnFindStaff";
            this.BtnFindStaff.Size = new System.Drawing.Size(60, 49);
            this.BtnFindStaff.TabIndex = 3;
            this.BtnFindStaff.UseVisualStyleBackColor = true;
            this.BtnFindStaff.Click += new System.EventHandler(this.BtnFindStaff_Click);
            // 
            // txtFindBy
            // 
            this.txtFindBy.Location = new System.Drawing.Point(307, 40);
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
            this.cbFindBy.Location = new System.Drawing.Point(148, 39);
            this.cbFindBy.Name = "cbFindBy";
            this.cbFindBy.Size = new System.Drawing.Size(151, 33);
            this.cbFindBy.TabIndex = 1;
            this.cbFindBy.SelectedIndexChanged += new System.EventHandler(this.cbFindBy_SelectedIndexChanged);
            this.cbFindBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbFindBy_KeyPress);
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
            // viewStaffInfoCtrl
            // 
            this.viewStaffInfoCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewStaffInfoCtrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewStaffInfoCtrl.LoadedStaff = null;
            this.viewStaffInfoCtrl.Location = new System.Drawing.Point(0, 100);
            this.viewStaffInfoCtrl.Name = "viewStaffInfoCtrl";
            this.viewStaffInfoCtrl.Size = new System.Drawing.Size(596, 272);
            this.viewStaffInfoCtrl.TabIndex = 1;
            // 
            // errors
            // 
            this.errors.ContainerControl = this;
            // 
            // FindStaffCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.viewStaffInfoCtrl);
            this.Controls.Add(this.gbFindBy);
            this.Name = "FindStaffCtrl";
            this.Size = new System.Drawing.Size(596, 372);
            this.gbFindBy.ResumeLayout(false);
            this.gbFindBy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFindBy;
        private System.Windows.Forms.ComboBox cbFindBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnFindStaff;
        private System.Windows.Forms.TextBox txtFindBy;
        private System.Windows.Forms.Button AddStaff;
        private ViewStaffInfoCtrl viewStaffInfoCtrl;
        private System.Windows.Forms.ErrorProvider errors;
    }
}
