namespace ClinicSystem.Schedule
{
    partial class ManageSchedules
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
            this.dgvSchedules = new System.Windows.Forms.DataGridView();
            this.cmsSchedules = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecords = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedules)).BeginInit();
            this.cmsSchedules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSchedules
            // 
            this.dgvSchedules.AllowUserToAddRows = false;
            this.dgvSchedules.AllowUserToDeleteRows = false;
            this.dgvSchedules.AllowUserToOrderColumns = true;
            this.dgvSchedules.BackgroundColor = System.Drawing.Color.White;
            this.dgvSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedules.ContextMenuStrip = this.cmsSchedules;
            this.dgvSchedules.Location = new System.Drawing.Point(12, 127);
            this.dgvSchedules.Name = "dgvSchedules";
            this.dgvSchedules.ReadOnly = true;
            this.dgvSchedules.RowHeadersWidth = 51;
            this.dgvSchedules.RowTemplate.Height = 24;
            this.dgvSchedules.Size = new System.Drawing.Size(787, 347);
            this.dgvSchedules.TabIndex = 21;
            this.dgvSchedules.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedules_CellDoubleClick);
            // 
            // cmsSchedules
            // 
            this.cmsSchedules.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsSchedules.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsSchedules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewScheduleToolStripMenuItem,
            this.updateScheduleToolStripMenuItem});
            this.cmsSchedules.Name = "cmsPeople";
            this.cmsSchedules.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsSchedules.Size = new System.Drawing.Size(279, 80);
            // 
            // addNewScheduleToolStripMenuItem
            // 
            this.addNewScheduleToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.plus_32;
            this.addNewScheduleToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addNewScheduleToolStripMenuItem.Name = "addNewScheduleToolStripMenuItem";
            this.addNewScheduleToolStripMenuItem.Size = new System.Drawing.Size(278, 38);
            this.addNewScheduleToolStripMenuItem.Text = "Add New Schedule";
            this.addNewScheduleToolStripMenuItem.Click += new System.EventHandler(this.addNewScheduleToolStripMenuItem_Click);
            // 
            // updateScheduleToolStripMenuItem
            // 
            this.updateScheduleToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.update_32;
            this.updateScheduleToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.updateScheduleToolStripMenuItem.Name = "updateScheduleToolStripMenuItem";
            this.updateScheduleToolStripMenuItem.Size = new System.Drawing.Size(278, 38);
            this.updateScheduleToolStripMenuItem.Text = "Update Schedule";
            this.updateScheduleToolStripMenuItem.Click += new System.EventHandler(this.updateScheduleToolStripMenuItem_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(214, 86);
            this.txtFilter.MaxLength = 6;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(150, 30);
            this.txtFilter.TabIndex = 18;
            this.txtFilter.Visible = false;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "Filter:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(74, 85);
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
            this.lblTitle.Location = new System.Drawing.Point(93, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(312, 38);
            this.lblTitle.TabIndex = 16;
            this.lblTitle.Text = "Manage Schedules";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(274, 503);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "double-click = select";
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(639, 482);
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
            this.lblRecords.Location = new System.Drawing.Point(18, 505);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 25);
            this.lblRecords.TabIndex = 22;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ClinicSystem.Properties.Resources.refresh_32;
            this.pictureBox1.Location = new System.Drawing.Point(734, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 52);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.schedule_64;
            this.pbModeImage.Location = new System.Drawing.Point(12, 16);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 15;
            this.pbModeImage.TabStop = false;
            // 
            // ManageSchedules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 553);
            this.Controls.Add(this.dgvSchedules);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbModeImage);
            this.Name = "ManageSchedules";
            this.Text = "ManageSchedules";
            this.Load += new System.EventHandler(this.ManageSchedules_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedules)).EndInit();
            this.cmsSchedules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSchedules;
        private System.Windows.Forms.ContextMenuStrip cmsSchedules;
        private System.Windows.Forms.ToolStripMenuItem addNewScheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateScheduleToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pbModeImage;
    }
}