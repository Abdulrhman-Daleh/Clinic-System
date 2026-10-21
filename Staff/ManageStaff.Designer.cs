namespace ClinicSystem.Staff
{
    partial class ManageStaff
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
            this.dgvStaff = new System.Windows.Forms.DataGridView();
            this.cmsStaff = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewStaffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateStaffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewStaffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecords = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStaff)).BeginInit();
            this.cmsStaff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStaff
            // 
            this.dgvStaff.AllowUserToAddRows = false;
            this.dgvStaff.AllowUserToDeleteRows = false;
            this.dgvStaff.AllowUserToOrderColumns = true;
            this.dgvStaff.BackgroundColor = System.Drawing.Color.White;
            this.dgvStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStaff.ContextMenuStrip = this.cmsStaff;
            this.dgvStaff.Location = new System.Drawing.Point(12, 118);
            this.dgvStaff.Name = "dgvStaff";
            this.dgvStaff.ReadOnly = true;
            this.dgvStaff.RowHeadersWidth = 51;
            this.dgvStaff.RowTemplate.Height = 24;
            this.dgvStaff.Size = new System.Drawing.Size(787, 347);
            this.dgvStaff.TabIndex = 21;
            this.dgvStaff.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStaff_CellDoubleClick);
            // 
            // cmsStaff
            // 
            this.cmsStaff.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsStaff.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsStaff.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewStaffToolStripMenuItem,
            this.updateStaffToolStripMenuItem,
            this.viewStaffToolStripMenuItem});
            this.cmsStaff.Name = "cmsPeople";
            this.cmsStaff.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsStaff.Size = new System.Drawing.Size(240, 146);
            this.cmsStaff.Opening += new System.ComponentModel.CancelEventHandler(this.cmsStaff_Opening);
            // 
            // addNewStaffToolStripMenuItem
            // 
            this.addNewStaffToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.plus_32;
            this.addNewStaffToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addNewStaffToolStripMenuItem.Name = "addNewStaffToolStripMenuItem";
            this.addNewStaffToolStripMenuItem.Size = new System.Drawing.Size(239, 38);
            this.addNewStaffToolStripMenuItem.Text = "Add New Staff";
            this.addNewStaffToolStripMenuItem.Click += new System.EventHandler(this.addNewStaffToolStripMenuItem_Click);
            // 
            // updateStaffToolStripMenuItem
            // 
            this.updateStaffToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.update_32;
            this.updateStaffToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.updateStaffToolStripMenuItem.Name = "updateStaffToolStripMenuItem";
            this.updateStaffToolStripMenuItem.Size = new System.Drawing.Size(239, 38);
            this.updateStaffToolStripMenuItem.Text = "Update Staff";
            this.updateStaffToolStripMenuItem.Click += new System.EventHandler(this.updateStaffToolStripMenuItem_Click);
            // 
            // viewStaffToolStripMenuItem
            // 
            this.viewStaffToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.view_32;
            this.viewStaffToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewStaffToolStripMenuItem.Name = "viewStaffToolStripMenuItem";
            this.viewStaffToolStripMenuItem.Size = new System.Drawing.Size(239, 38);
            this.viewStaffToolStripMenuItem.Text = "View Staff";
            this.viewStaffToolStripMenuItem.Click += new System.EventHandler(this.viewStaffToolStripMenuItem_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(215, 78);
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
            this.label1.Location = new System.Drawing.Point(10, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "Filter:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(74, 77);
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
            this.lblTitle.Location = new System.Drawing.Point(93, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(222, 38);
            this.lblTitle.TabIndex = 16;
            this.lblTitle.Text = "Manage Staff";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(284, 492);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "double-click = select";
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(639, 471);
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
            this.lblRecords.Location = new System.Drawing.Point(18, 501);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 25);
            this.lblRecords.TabIndex = 22;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ClinicSystem.Properties.Resources.refresh_32;
            this.pictureBox1.Location = new System.Drawing.Point(724, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 52);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.People_32;
            this.pbModeImage.Location = new System.Drawing.Point(12, 12);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 15;
            this.pbModeImage.TabStop = false;
            // 
            // ManageStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 542);
            this.Controls.Add(this.dgvStaff);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbModeImage);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ManageStaff";
            this.Text = "Manage Staff";
            this.Load += new System.EventHandler(this.ManageStaff_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStaff)).EndInit();
            this.cmsStaff.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStaff;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pbModeImage;
        private System.Windows.Forms.ContextMenuStrip cmsStaff;
        private System.Windows.Forms.ToolStripMenuItem addNewStaffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateStaffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewStaffToolStripMenuItem;
    }
}