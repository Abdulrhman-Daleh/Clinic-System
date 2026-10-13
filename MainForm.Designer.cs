namespace ClinicSystem
{
    partial class MainForm
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
            this.MSOperations = new System.Windows.Forms.MenuStrip();
            this.managePeopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPersonContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MSOperations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MSOperations
            // 
            this.MSOperations.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MSOperations.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MSOperations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.managePeopleToolStripMenuItem});
            this.MSOperations.Location = new System.Drawing.Point(0, 0);
            this.MSOperations.Name = "MSOperations";
            this.MSOperations.Size = new System.Drawing.Size(1762, 72);
            this.MSOperations.TabIndex = 0;
            this.MSOperations.Text = "menuStrip1";
            // 
            // managePeopleToolStripMenuItem
            // 
            this.managePeopleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewPersonToolStripMenuItem,
            this.viewPersonToolStripMenuItem,
            this.addPersonContactToolStripMenuItem});
            this.managePeopleToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.managePeopleToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.manage_people_64;
            this.managePeopleToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.managePeopleToolStripMenuItem.Name = "managePeopleToolStripMenuItem";
            this.managePeopleToolStripMenuItem.Size = new System.Drawing.Size(256, 68);
            this.managePeopleToolStripMenuItem.Text = "Manage People";
            this.managePeopleToolStripMenuItem.Click += new System.EventHandler(this.managePeopleToolStripMenuItem_Click);
            // 
            // addNewPersonToolStripMenuItem
            // 
            this.addNewPersonToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.plus_32;
            this.addNewPersonToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addNewPersonToolStripMenuItem.Name = "addNewPersonToolStripMenuItem";
            this.addNewPersonToolStripMenuItem.Size = new System.Drawing.Size(325, 38);
            this.addNewPersonToolStripMenuItem.Text = "Add New Person";
            this.addNewPersonToolStripMenuItem.Click += new System.EventHandler(this.addNewPersonToolStripMenuItem_Click);
            // 
            // viewPersonToolStripMenuItem
            // 
            this.viewPersonToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.view_32;
            this.viewPersonToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewPersonToolStripMenuItem.Name = "viewPersonToolStripMenuItem";
            this.viewPersonToolStripMenuItem.Size = new System.Drawing.Size(325, 38);
            this.viewPersonToolStripMenuItem.Text = "View Person";
            this.viewPersonToolStripMenuItem.Click += new System.EventHandler(this.viewPersonToolStripMenuItem_Click);
            // 
            // addPersonContactToolStripMenuItem
            // 
            this.addPersonContactToolStripMenuItem.Image = global::ClinicSystem.Properties.Resources.add_contact_32;
            this.addPersonContactToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addPersonContactToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addPersonContactToolStripMenuItem.Name = "addPersonContactToolStripMenuItem";
            this.addPersonContactToolStripMenuItem.Size = new System.Drawing.Size(325, 38);
            this.addPersonContactToolStripMenuItem.Text = "Add Person Contact";
            this.addPersonContactToolStripMenuItem.Click += new System.EventHandler(this.addPersonContactToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::ClinicSystem.Properties.Resources.clinic_background;
            this.pictureBox1.Location = new System.Drawing.Point(0, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1762, 774);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1762, 846);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.MSOperations);
            this.MainMenuStrip = this.MSOperations;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.MSOperations.ResumeLayout(false);
            this.MSOperations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MSOperations;
        private System.Windows.Forms.ToolStripMenuItem managePeopleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPersonContactToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}