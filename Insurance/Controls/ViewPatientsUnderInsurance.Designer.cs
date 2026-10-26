namespace ClinicSystem.Insurance.Controls
{
    partial class ViewPatientsUnderInsurance
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
            this.dgvInsurancePatients = new System.Windows.Forms.DataGridView();
            this.lblRecords = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsurancePatients)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvInsurancePatients
            // 
            this.dgvInsurancePatients.AllowUserToAddRows = false;
            this.dgvInsurancePatients.AllowUserToDeleteRows = false;
            this.dgvInsurancePatients.AllowUserToOrderColumns = true;
            this.dgvInsurancePatients.BackgroundColor = System.Drawing.Color.White;
            this.dgvInsurancePatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInsurancePatients.Location = new System.Drawing.Point(7, 8);
            this.dgvInsurancePatients.Name = "dgvInsurancePatients";
            this.dgvInsurancePatients.ReadOnly = true;
            this.dgvInsurancePatients.RowHeadersWidth = 51;
            this.dgvInsurancePatients.RowTemplate.Height = 24;
            this.dgvInsurancePatients.Size = new System.Drawing.Size(617, 123);
            this.dgvInsurancePatients.TabIndex = 0;
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(10, 137);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(140, 25);
            this.lblRecords.TabIndex = 1;
            this.lblRecords.Text = "Not Calculated";
            // 
            // ViewPatientsUnderInsurance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.dgvInsurancePatients);
            this.Name = "ViewPatientsUnderInsurance";
            this.Size = new System.Drawing.Size(633, 165);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsurancePatients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInsurancePatients;
        private System.Windows.Forms.Label lblRecords;
    }
}
