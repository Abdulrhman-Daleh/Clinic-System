namespace ClinicSystem.AppointmentReminder
{
    partial class ManageAppointmentReminders
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
            this.dgvReminders = new System.Windows.Forms.DataGridView();
            this.cmsReminder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.sendReminderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAppointmentRemindersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            this.viewAppointmentInfoCtrl1 = new ClinicSystem.Appointments.Controls.ViewAppointmentInfoCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReminders)).BeginInit();
            this.cmsReminder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReminders
            // 
            this.dgvReminders.AllowUserToAddRows = false;
            this.dgvReminders.AllowUserToDeleteRows = false;
            this.dgvReminders.AllowUserToOrderColumns = true;
            this.dgvReminders.BackgroundColor = System.Drawing.Color.White;
            this.dgvReminders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReminders.ContextMenuStrip = this.cmsReminder;
            this.dgvReminders.Location = new System.Drawing.Point(12, 109);
            this.dgvReminders.Name = "dgvReminders";
            this.dgvReminders.ReadOnly = true;
            this.dgvReminders.RowHeadersWidth = 51;
            this.dgvReminders.RowTemplate.Height = 24;
            this.dgvReminders.Size = new System.Drawing.Size(835, 347);
            this.dgvReminders.TabIndex = 40;
            // 
            // cmsReminder
            // 
            this.cmsReminder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsReminder.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsReminder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendReminderToolStripMenuItem,
            this.viewAppointmentRemindersToolStripMenuItem});
            this.cmsReminder.Name = "cmsPeople";
            this.cmsReminder.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsReminder.Size = new System.Drawing.Size(384, 108);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 25);
            this.label1.TabIndex = 38;
            this.label1.Text = "Filter:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(74, 69);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(132, 33);
            this.cbFilterBy.TabIndex = 39;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            this.cbFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbFilterBy_KeyPress);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(84, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(317, 38);
            this.lblTitle.TabIndex = 36;
            this.lblTitle.Text = "Manage Reminders";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(275, 472);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 25);
            this.label2.TabIndex = 43;
            this.label2.Text = "double-click = select";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(30, 469);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 25);
            this.lblRecords.TabIndex = 41;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(212, 72);
            this.txtFilter.MaxLength = 6;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(150, 30);
            this.txtFilter.TabIndex = 37;
            this.txtFilter.Visible = false;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // sendReminderToolStripMenuItem
            // 
            this.sendReminderToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.remainder_32;
            this.sendReminderToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sendReminderToolStripMenuItem.Name = "sendReminderToolStripMenuItem";
            this.sendReminderToolStripMenuItem.Size = new System.Drawing.Size(383, 38);
            this.sendReminderToolStripMenuItem.Text = "Add New Reminder";
            this.sendReminderToolStripMenuItem.Click += new System.EventHandler(this.sendReminderToolStripMenuItem_Click);
            // 
            // viewAppointmentRemindersToolStripMenuItem
            // 
            this.viewAppointmentRemindersToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.view_record_32;
            this.viewAppointmentRemindersToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewAppointmentRemindersToolStripMenuItem.Name = "viewAppointmentRemindersToolStripMenuItem";
            this.viewAppointmentRemindersToolStripMenuItem.Size = new System.Drawing.Size(383, 38);
            this.viewAppointmentRemindersToolStripMenuItem.Text = "View Appointment Reminders";
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(687, 461);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 42;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.remainder_32;
            this.pbModeImage.Location = new System.Drawing.Point(12, 8);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 35;
            this.pbModeImage.TabStop = false;
            // 
            // viewAppointmentInfoCtrl1
            // 
            this.viewAppointmentInfoCtrl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewAppointmentInfoCtrl1.Location = new System.Drawing.Point(851, 8);
            this.viewAppointmentInfoCtrl1.Name = "viewAppointmentInfoCtrl1";
            this.viewAppointmentInfoCtrl1.Size = new System.Drawing.Size(619, 556);
            this.viewAppointmentInfoCtrl1.TabIndex = 44;
            // 
            // ManageAppointmentReminders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 575);
            this.Controls.Add(this.viewAppointmentInfoCtrl1);
            this.Controls.Add(this.dgvReminders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.pbModeImage);
            this.Controls.Add(this.txtFilter);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ManageAppointmentReminders";
            this.Text = "Manage Appointment Reminders";
            this.Load += new System.EventHandler(this.ManageAppointmentReminders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReminders)).EndInit();
            this.cmsReminder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem viewAppointmentRemindersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendReminderToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvReminders;
        private System.Windows.Forms.ContextMenuStrip cmsReminder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.PictureBox pbModeImage;
        private System.Windows.Forms.TextBox txtFilter;
        private Appointments.Controls.ViewAppointmentInfoCtrl viewAppointmentInfoCtrl1;
    }
}