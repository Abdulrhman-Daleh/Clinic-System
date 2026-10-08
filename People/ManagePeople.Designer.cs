namespace ClinicSystem.People
{
    partial class ManagePeople
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbModeImage = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.dgvPeople = new System.Windows.Forms.DataGridView();
            this.lblRecords = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbGender = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(93, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(257, 38);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Manage People";
            // 
            // pbModeImage
            // 
            this.pbModeImage.Image = global::ClinicSystem.Properties.Resources.People_32;
            this.pbModeImage.Location = new System.Drawing.Point(12, 17);
            this.pbModeImage.Name = "pbModeImage";
            this.pbModeImage.Size = new System.Drawing.Size(65, 52);
            this.pbModeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModeImage.TabIndex = 4;
            this.pbModeImage.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ClinicSystem.Properties.Resources.refresh_32;
            this.pictureBox1.Location = new System.Drawing.Point(723, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 52);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(212, 87);
            this.txtFilter.MaxLength = 6;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(150, 30);
            this.txtFilter.TabIndex = 7;
            this.txtFilter.Visible = false;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Filter:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(74, 86);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(132, 33);
            this.cbFilterBy.TabIndex = 9;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            this.cbFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbFilterBy_KeyPress);
            // 
            // dgvPeople
            // 
            this.dgvPeople.AllowUserToAddRows = false;
            this.dgvPeople.AllowUserToDeleteRows = false;
            this.dgvPeople.AllowUserToOrderColumns = true;
            this.dgvPeople.BackgroundColor = System.Drawing.Color.White;
            this.dgvPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeople.Location = new System.Drawing.Point(12, 134);
            this.dgvPeople.Name = "dgvPeople";
            this.dgvPeople.ReadOnly = true;
            this.dgvPeople.RowHeadersWidth = 51;
            this.dgvPeople.RowTemplate.Height = 24;
            this.dgvPeople.Size = new System.Drawing.Size(787, 347);
            this.dgvPeople.TabIndex = 10;
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(18, 506);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 25);
            this.lblRecords.TabIndex = 11;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::ClinicSystem.Properties.Resources.x_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(639, 487);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 66);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(284, 508);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "double-click = select";
            // 
            // cbGender
            // 
            this.cbGender.FormattingEnabled = true;
            this.cbGender.Location = new System.Drawing.Point(212, 87);
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(106, 33);
            this.cbGender.TabIndex = 14;
            this.cbGender.Visible = false;
            this.cbGender.SelectedIndexChanged += new System.EventHandler(this.cbGender_SelectedIndexChanged);
            this.cbGender.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbFilterBy_KeyPress);
            // 
            // ManagePeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 562);
            this.Controls.Add(this.cbGender);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.dgvPeople);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbModeImage);
            this.Name = "ManagePeople";
            this.Text = "Manage People";
            this.Load += new System.EventHandler(this.ManagePeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbModeImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbModeImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.DataGridView dgvPeople;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbGender;
    }
}