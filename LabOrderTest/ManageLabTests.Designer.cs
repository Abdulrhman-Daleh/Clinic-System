namespace ClinicSystem.LabOrderTest
{
    partial class ManageLabTests
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            this.findAppointmentCtrl1 = new ClinicSystem.Appointments.Controls.FindAppointmentCtrl();
            this.dgvLabTests = new System.Windows.Forms.DataGridView();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmsLabTests = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addLabTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLabTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRecords = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabTests)).BeginInit();
            this.cmsLabTests.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(71, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(303, 38);
            this.lblTitle.TabIndex = 85;
            this.lblTitle.Text = "Manage Lab Tests";
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.schedule_64;
            this.pbModeImage.Location = new System.Drawing.Point(2, 3);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 84;
            this.pbModeImage.TabStop = false;
            // 
            // findAppointmentCtrl1
            // 
            this.findAppointmentCtrl1.AllowFind = true;
            this.findAppointmentCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.findAppointmentCtrl1.Location = new System.Drawing.Point(12, 61);
            this.findAppointmentCtrl1.Name = "findAppointmentCtrl1";
            this.findAppointmentCtrl1.Size = new System.Drawing.Size(631, 666);
            this.findAppointmentCtrl1.TabIndex = 86;
            // 
            // dgvLabTests
            // 
            this.dgvLabTests.AllowUserToAddRows = false;
            this.dgvLabTests.AllowUserToDeleteRows = false;
            this.dgvLabTests.AllowUserToOrderColumns = true;
            this.dgvLabTests.BackgroundColor = System.Drawing.Color.White;
            this.dgvLabTests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabTests.ContextMenuStrip = this.cmsLabTests;
            this.dgvLabTests.Location = new System.Drawing.Point(649, 108);
            this.dgvLabTests.Name = "dgvLabTests";
            this.dgvLabTests.ReadOnly = true;
            this.dgvLabTests.RowHeadersWidth = 51;
            this.dgvLabTests.RowTemplate.Height = 24;
            this.dgvLabTests.Size = new System.Drawing.Size(879, 440);
            this.dgvLabTests.TabIndex = 90;
            this.dgvLabTests.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLabTests_CellDoubleClick);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(849, 71);
            this.txtFilter.MaxLength = 40;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(150, 30);
            this.txtFilter.TabIndex = 87;
            this.txtFilter.Visible = false;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(647, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 25);
            this.label1.TabIndex = 88;
            this.label1.Text = "Filter:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(711, 70);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(132, 33);
            this.cbFilterBy.TabIndex = 89;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(978, 570);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 25);
            this.label2.TabIndex = 91;
            this.label2.Text = "double-click = select";
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1368, 661);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 92;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmsLabTests
            // 
            this.cmsLabTests.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsLabTests.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsLabTests.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLabTestToolStripMenuItem,
            this.viewLabTestToolStripMenuItem});
            this.cmsLabTests.Name = "cmsPeople";
            this.cmsLabTests.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsLabTests.Size = new System.Drawing.Size(230, 108);
            this.cmsLabTests.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLabTests_Opening);
            // 
            // addLabTestToolStripMenuItem
            // 
            this.addLabTestToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.plus_32;
            this.addLabTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addLabTestToolStripMenuItem.Name = "addLabTestToolStripMenuItem";
            this.addLabTestToolStripMenuItem.Size = new System.Drawing.Size(229, 38);
            this.addLabTestToolStripMenuItem.Text = "Add Lab Test";
            // 
            // viewLabTestToolStripMenuItem
            // 
            this.viewLabTestToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.show_password_32;
            this.viewLabTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewLabTestToolStripMenuItem.Name = "viewLabTestToolStripMenuItem";
            this.viewLabTestToolStripMenuItem.Size = new System.Drawing.Size(229, 38);
            this.viewLabTestToolStripMenuItem.Text = "View Lab Test";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(663, 561);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 25);
            this.lblRecords.TabIndex = 94;
            // 
            // ManageLabTests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 740);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvLabTests);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.findAppointmentCtrl1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbModeImage);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ManageLabTests";
            this.Text = "Manage Lab Tests";
            this.Load += new System.EventHandler(this.ManageLabTests_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabTests)).EndInit();
            this.cmsLabTests.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbModeImage;
        private Appointments.Controls.FindAppointmentCtrl findAppointmentCtrl1;
        private System.Windows.Forms.DataGridView dgvLabTests;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip cmsLabTests;
        private System.Windows.Forms.ToolStripMenuItem addLabTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLabTestToolStripMenuItem;
        private System.Windows.Forms.Label lblRecords;
    }
}