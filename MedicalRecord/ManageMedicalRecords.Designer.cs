namespace ClinicSystem.MedicalRecord
{
    partial class ManageMedicalRecord
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
            this.dgvMedicalRecords = new System.Windows.Forms.DataGridView();
            this.cmsRecord = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addMedicalRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMedicalRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecords = new System.Windows.Forms.Label();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            this.cbConsentToCare = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalRecords)).BeginInit();
            this.cmsRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMedicalRecords
            // 
            this.dgvMedicalRecords.AllowUserToAddRows = false;
            this.dgvMedicalRecords.AllowUserToDeleteRows = false;
            this.dgvMedicalRecords.AllowUserToOrderColumns = true;
            this.dgvMedicalRecords.BackgroundColor = System.Drawing.Color.White;
            this.dgvMedicalRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicalRecords.ContextMenuStrip = this.cmsRecord;
            this.dgvMedicalRecords.Location = new System.Drawing.Point(12, 113);
            this.dgvMedicalRecords.Name = "dgvMedicalRecords";
            this.dgvMedicalRecords.ReadOnly = true;
            this.dgvMedicalRecords.RowHeadersWidth = 51;
            this.dgvMedicalRecords.RowTemplate.Height = 24;
            this.dgvMedicalRecords.Size = new System.Drawing.Size(787, 347);
            this.dgvMedicalRecords.TabIndex = 31;
            this.dgvMedicalRecords.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMedicalRecords_CellDoubleClick);
            // 
            // cmsRecord
            // 
            this.cmsRecord.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsRecord.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsRecord.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addMedicalRecordToolStripMenuItem,
            this.updateMedicalRecordToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.cmsRecord.Name = "cmsPeople";
            this.cmsRecord.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsRecord.Size = new System.Drawing.Size(322, 146);
            this.cmsRecord.Opening += new System.ComponentModel.CancelEventHandler(this.cmsMedicalRecords_Opening);
            // 
            // addMedicalRecordToolStripMenuItem
            // 
            this.addMedicalRecordToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.add_mdeical_record_32;
            this.addMedicalRecordToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addMedicalRecordToolStripMenuItem.Name = "addMedicalRecordToolStripMenuItem";
            this.addMedicalRecordToolStripMenuItem.Size = new System.Drawing.Size(321, 38);
            this.addMedicalRecordToolStripMenuItem.Text = "Add Medical Record";
            this.addMedicalRecordToolStripMenuItem.Click += new System.EventHandler(this.addNewMedicalRecordToolStripMenuItem_Click);
            // 
            // updateMedicalRecordToolStripMenuItem
            // 
            this.updateMedicalRecordToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.update_32;
            this.updateMedicalRecordToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.updateMedicalRecordToolStripMenuItem.Name = "updateMedicalRecordToolStripMenuItem";
            this.updateMedicalRecordToolStripMenuItem.Size = new System.Drawing.Size(321, 38);
            this.updateMedicalRecordToolStripMenuItem.Text = "Update Medical Record";
            this.updateMedicalRecordToolStripMenuItem.Click += new System.EventHandler(this.updateMedicalRecordToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.view_record_32;
            this.viewToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(321, 38);
            this.viewToolStripMenuItem.Text = "View Medical Record";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(269, 76);
            this.txtFilter.MaxLength = 6;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(150, 30);
            this.txtFilter.TabIndex = 28;
            this.txtFilter.Visible = false;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 25);
            this.label1.TabIndex = 29;
            this.label1.Text = "Filter:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(74, 73);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(189, 33);
            this.cbFilterBy.TabIndex = 30;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            this.cbFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbFilterBy_KeyPress);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(83, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(409, 38);
            this.lblTitle.TabIndex = 27;
            this.lblTitle.Text = "Manage Medical Records";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(284, 487);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 25);
            this.label2.TabIndex = 34;
            this.label2.Text = "double-click = select";
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(639, 466);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(18, 493);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 25);
            this.lblRecords.TabIndex = 32;
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.People_32;
            this.pbModeImage.Location = new System.Drawing.Point(12, 9);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 26;
            this.pbModeImage.TabStop = false;
            // 
            // cbConsentToCare
            // 
            this.cbConsentToCare.FormattingEnabled = true;
            this.cbConsentToCare.Location = new System.Drawing.Point(269, 73);
            this.cbConsentToCare.Name = "cbConsentToCare";
            this.cbConsentToCare.Size = new System.Drawing.Size(121, 33);
            this.cbConsentToCare.TabIndex = 35;
            this.cbConsentToCare.SelectedIndexChanged += new System.EventHandler(this.cbConsentToCare_SelectedIndexChanged);
            this.cbConsentToCare.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbFilterBy_KeyPress);
            // 
            // ManageMedicalRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 540);
            this.Controls.Add(this.cbConsentToCare);
            this.Controls.Add(this.dgvMedicalRecords);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.pbModeImage);
            this.Name = "ManageMedicalRecord";
            this.Text = "ManageMedicalRecords";
            this.Load += new System.EventHandler(this.ManageMedicalRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalRecords)).EndInit();
            this.cmsRecord.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMedicalRecords;
        private System.Windows.Forms.ContextMenuStrip cmsRecord;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.PictureBox pbModeImage;
        private System.Windows.Forms.ToolStripMenuItem addMedicalRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMedicalRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbConsentToCare;
    }
}