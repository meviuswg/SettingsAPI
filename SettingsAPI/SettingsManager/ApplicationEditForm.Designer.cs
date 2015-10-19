namespace SettingsManager
{
    partial class ApplicationEditForm
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
            this.textName = new DevExpress.XtraEditors.TextEdit();
            this.textDescription = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Description = new DevExpress.XtraEditors.LabelControl();
            this.simpleCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(96, 13);
            this.textName.Name = "textName";
            this.textName.Properties.Mask.EditMask = "[a-zA-Z0-9]+";
            this.textName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textName.Properties.MaxLength = 50;
            this.textName.Size = new System.Drawing.Size(345, 20);
            this.textName.TabIndex = 0;
            // 
            // textDescription
            // 
            this.textDescription.Location = new System.Drawing.Point(96, 48);
            this.textDescription.Name = "textDescription";
            this.textDescription.Properties.MaxLength = 150;
            this.textDescription.Size = new System.Drawing.Size(345, 20);
            this.textDescription.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(25, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(31, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Name:";
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(25, 51);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(55, 13);
            this.Description.TabIndex = 1;
            this.Description.Text = "Descrption:";
            // 
            // simpleCancel
            // 
            this.simpleCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleCancel.Location = new System.Drawing.Point(366, 88);
            this.simpleCancel.Name = "simpleCancel";
            this.simpleCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleCancel.TabIndex = 3;
            this.simpleCancel.Text = "Cancel";
            this.simpleCancel.Click += new System.EventHandler(this.simpleCancel_Click);
            // 
            // simpleButtonOk
            // 
            this.simpleButtonOk.Location = new System.Drawing.Point(268, 88);
            this.simpleButtonOk.Name = "simpleButtonOk";
            this.simpleButtonOk.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOk.TabIndex = 2;
            this.simpleButtonOk.Text = "Ok";
            this.simpleButtonOk.Click += new System.EventHandler(this.simpleButtonOk_Click);
            // 
            // ApplicationEditForm
            // 
            this.AcceptButton = this.simpleButtonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleCancel;
            this.ClientSize = new System.Drawing.Size(458, 123);
            this.Controls.Add(this.simpleButtonOk);
            this.Controls.Add(this.simpleCancel);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textDescription);
            this.Controls.Add(this.textName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ApplicationEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Application";
            ((System.ComponentModel.ISupportInitialize)(this.textName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDescription.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textName;
        private DevExpress.XtraEditors.TextEdit textDescription;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl Description;
        private DevExpress.XtraEditors.SimpleButton simpleCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
    }
}