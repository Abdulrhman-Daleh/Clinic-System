namespace ClinicSystem.Medicine
{
    partial class ManageMedicines
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
            this.dgvMedicines = new System.Windows.Forms.DataGridView();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            this.cmsMedicine = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateMedicineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewMedicineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            this.cmsMedicine.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMedicines
            // 
            this.dgvMedicines.AllowUserToAddRows = false;
            this.dgvMedicines.AllowUserToDeleteRows = false;
            this.dgvMedicines.AllowUserToOrderColumns = true;
            this.dgvMedicines.BackgroundColor = System.Drawing.Color.White;
            this.dgvMedicines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicines.ContextMenuStrip = this.cmsMedicine;
            this.dgvMedicines.Location = new System.Drawing.Point(12, 112);
            this.dgvMedicines.Name = "dgvMedicines";
            this.dgvMedicines.ReadOnly = true;
            this.dgvMedicines.RowHeadersWidth = 51;
            this.dgvMedicines.RowTemplate.Height = 24;
            this.dgvMedicines.Size = new System.Drawing.Size(787, 347);
            this.dgvMedicines.TabIndex = 31;
            this.dgvMedicines.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMedicines_CellDoubleClick);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(214, 71);
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
            this.label1.Location = new System.Drawing.Point(10, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 25);
            this.label1.TabIndex = 29;
            this.label1.Text = "Filter:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(74, 70);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(132, 33);
            this.cbFilterBy.TabIndex = 30;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            this.cbFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbFilterBy_KeyPress);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(93, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(306, 38);
            this.lblTitle.TabIndex = 27;
            this.lblTitle.Text = "Manage Medicines";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(284, 486);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 25);
            this.label2.TabIndex = 34;
            this.label2.Text = "double-click = select";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(18, 495);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 25);
            this.lblRecords.TabIndex = 32;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(639, 465);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.pill_32;
            this.pbModeImage.Location = new System.Drawing.Point(12, 6);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 26;
            this.pbModeImage.TabStop = false;
            // 
            // cmsMedicine
            // 
            this.cmsMedicine.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsMedicine.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsMedicine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewMedicineToolStripMenuItem,
            this.updateMedicineToolStripMenuItem});
            this.cmsMedicine.Name = "cmsPeople";
            this.cmsMedicine.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsMedicine.Size = new System.Drawing.Size(281, 80);
            this.cmsMedicine.Opening += new System.ComponentModel.CancelEventHandler(this.cmsMedicine_Opening);
            // 
            // updateMedicineToolStripMenuItem
            // 
            this.updateMedicineToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.update_32;
            this.updateMedicineToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.updateMedicineToolStripMenuItem.Name = "updateMedicineToolStripMenuItem";
            this.updateMedicineToolStripMenuItem.Size = new System.Drawing.Size(280, 38);
            this.updateMedicineToolStripMenuItem.Text = "Update Medicine";
            this.updateMedicineToolStripMenuItem.Click += new System.EventHandler(this.updateMedicineToolStripMenuItem_Click);
            // 
            // addNewMedicineToolStripMenuItem
            // 
            this.addNewMedicineToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.add_policy_32;
            this.addNewMedicineToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addNewMedicineToolStripMenuItem.Name = "addNewMedicineToolStripMenuItem";
            this.addNewMedicineToolStripMenuItem.Size = new System.Drawing.Size(280, 38);
            this.addNewMedicineToolStripMenuItem.Text = "Add New Medicine";
            this.addNewMedicineToolStripMenuItem.Click += new System.EventHandler(this.addNewMedicineToolStripMenuItem_Click);
            // 
            // ManageMedicines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 537);
            this.Controls.Add(this.dgvMedicines);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.pbModeImage);
            this.Name = "ManageMedicines";
            this.Text = "Manage Medicines";
            this.Load += new System.EventHandler(this.ManageMedicines_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            this.cmsMedicine.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMedicines;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.PictureBox pbModeImage;
        private System.Windows.Forms.ContextMenuStrip cmsMedicine;
        private System.Windows.Forms.ToolStripMenuItem updateMedicineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewMedicineToolStripMenuItem;
    }
}