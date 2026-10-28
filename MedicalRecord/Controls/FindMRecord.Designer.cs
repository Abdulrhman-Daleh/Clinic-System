namespace ClinicSystem.MedicalRecord.Controls
{
    partial class FindMRecord
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
            this.components = new System.ComponentModel.Container();
            this.viewMRecordInfo1 = new ClinicSystem.MedicalRecord.Controls.ViewMRecordInfo();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtFindBy = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFindBy = new System.Windows.Forms.ComboBox();
            this.errors = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).BeginInit();
            this.SuspendLayout();
            // 
            // viewMRecordInfo1
            // 
            this.viewMRecordInfo1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.viewMRecordInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.viewMRecordInfo1.Location = new System.Drawing.Point(0, 82);
            this.viewMRecordInfo1.Name = "viewMRecordInfo1";
            this.viewMRecordInfo1.Size = new System.Drawing.Size(740, 506);
            this.viewMRecordInfo1.TabIndex = 0;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnFind);
            this.gbFilter.Controls.Add(this.txtFindBy);
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Controls.Add(this.cbFindBy);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFilter.Location = new System.Drawing.Point(0, 0);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(740, 82);
            this.gbFilter.TabIndex = 3;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Find Policy";
            // 
            // btnFind
            // 
            this.btnFind.Image = global::ClinicSystem.Properties.Resources.find_policy_32;
            this.btnFind.Location = new System.Drawing.Point(486, 26);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(46, 45);
            this.btnFind.TabIndex = 3;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtFindBy
            // 
            this.txtFindBy.Location = new System.Drawing.Point(321, 33);
            this.txtFindBy.MaxLength = 20;
            this.txtFindBy.Name = "txtFindBy";
            this.txtFindBy.Size = new System.Drawing.Size(150, 30);
            this.txtFindBy.TabIndex = 0;
            this.txtFindBy.Validating += new System.ComponentModel.CancelEventHandler(this.txtFindBy_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find By:";
            // 
            // cbFindBy
            // 
            this.cbFindBy.FormattingEnabled = true;
            this.cbFindBy.Location = new System.Drawing.Point(126, 32);
            this.cbFindBy.Name = "cbFindBy";
            this.cbFindBy.Size = new System.Drawing.Size(189, 33);
            this.cbFindBy.TabIndex = 1;
            // 
            // errors
            // 
            this.errors.ContainerControl = this;
            // 
            // FindMRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.viewMRecordInfo1);
            this.Name = "FindMRecord";
            this.Size = new System.Drawing.Size(740, 588);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ViewMRecordInfo viewMRecordInfo1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtFindBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFindBy;
        private System.Windows.Forms.ErrorProvider errors;
    }
}
