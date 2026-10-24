namespace ClinicSystem.Insurance
{
    partial class ManageInsurances
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
            this.dgvInsurances = new System.Windows.Forms.DataGridView();
            this.cmsInsurances = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewInsuranceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateInsuranceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewInsuranceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecords = new System.Windows.Forms.Label();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsurances)).BeginInit();
            this.cmsInsurances.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvInsurances
            // 
            this.dgvInsurances.AllowUserToAddRows = false;
            this.dgvInsurances.AllowUserToDeleteRows = false;
            this.dgvInsurances.AllowUserToOrderColumns = true;
            this.dgvInsurances.BackgroundColor = System.Drawing.Color.White;
            this.dgvInsurances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInsurances.ContextMenuStrip = this.cmsInsurances;
            this.dgvInsurances.Location = new System.Drawing.Point(12, 109);
            this.dgvInsurances.Name = "dgvInsurances";
            this.dgvInsurances.ReadOnly = true;
            this.dgvInsurances.RowHeadersWidth = 51;
            this.dgvInsurances.RowTemplate.Height = 24;
            this.dgvInsurances.Size = new System.Drawing.Size(787, 347);
            this.dgvInsurances.TabIndex = 21;
            this.dgvInsurances.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInsurances_CellDoubleClick);
            // 
            // cmsInsurances
            // 
            this.cmsInsurances.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsInsurances.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsInsurances.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewInsuranceToolStripMenuItem,
            this.updateInsuranceToolStripMenuItem,
            this.viewInsuranceToolStripMenuItem});
            this.cmsInsurances.Name = "cmsPeople";
            this.cmsInsurances.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsInsurances.Size = new System.Drawing.Size(266, 118);
            this.cmsInsurances.Opening += new System.ComponentModel.CancelEventHandler(this.cmsInsurances_Opening);
            // 
            // addNewInsuranceToolStripMenuItem
            // 
            this.addNewInsuranceToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.plus_32;
            this.addNewInsuranceToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addNewInsuranceToolStripMenuItem.Name = "addNewInsuranceToolStripMenuItem";
            this.addNewInsuranceToolStripMenuItem.Size = new System.Drawing.Size(265, 38);
            this.addNewInsuranceToolStripMenuItem.Text = "Add Insurance";
            this.addNewInsuranceToolStripMenuItem.Click += new System.EventHandler(this.addNewInsuranceToolStripMenuItem_Click);
            // 
            // updateInsuranceToolStripMenuItem
            // 
            this.updateInsuranceToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.update_32;
            this.updateInsuranceToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.updateInsuranceToolStripMenuItem.Name = "updateInsuranceToolStripMenuItem";
            this.updateInsuranceToolStripMenuItem.Size = new System.Drawing.Size(265, 38);
            this.updateInsuranceToolStripMenuItem.Text = "Update Insurance";
            this.updateInsuranceToolStripMenuItem.Click += new System.EventHandler(this.updateInsuranceToolStripMenuItem_Click);
            // 
            // viewInsuranceToolStripMenuItem
            // 
            this.viewInsuranceToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.show_password_32;
            this.viewInsuranceToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewInsuranceToolStripMenuItem.Name = "viewInsuranceToolStripMenuItem";
            this.viewInsuranceToolStripMenuItem.Size = new System.Drawing.Size(265, 38);
            this.viewInsuranceToolStripMenuItem.Text = "View Insurance";
            this.viewInsuranceToolStripMenuItem.Click += new System.EventHandler(this.viewInsuranceToolStripMenuItem_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(212, 72);
            this.txtFilter.MaxLength = 6;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(150, 30);
            this.txtFilter.TabIndex = 18;
            this.txtFilter.Visible = false;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "Filter:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(74, 71);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(132, 33);
            this.cbFilterBy.TabIndex = 20;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            this.cbFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbFilterBy_KeyPress);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(93, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(302, 38);
            this.lblTitle.TabIndex = 16;
            this.lblTitle.Text = "Manage Insurance";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(284, 483);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "double-click = select";
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(639, 462);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(18, 497);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 25);
            this.lblRecords.TabIndex = 22;
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.Manage_Insurance_64;
            this.pbModeImage.Location = new System.Drawing.Point(12, 8);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 15;
            this.pbModeImage.TabStop = false;
            // 
            // ManageInsurances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 538);
            this.Controls.Add(this.dgvInsurances);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.pbModeImage);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ManageInsurances";
            this.Text = "Manage Insurances";
            this.Load += new System.EventHandler(this.ManageInsurances_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsurances)).EndInit();
            this.cmsInsurances.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInsurances;
        private System.Windows.Forms.ContextMenuStrip cmsInsurances;
        private System.Windows.Forms.ToolStripMenuItem addNewInsuranceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateInsuranceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewInsuranceToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.PictureBox pbModeImage;
    }
}