namespace SettingsManager
{
    partial class SettingEditForm
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
            this.textKey = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Description = new DevExpress.XtraEditors.LabelControl();
            this.simpleCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOk = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textObjectId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textInfo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelEditor = new DevExpress.XtraEditors.PanelControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.textKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textObjectId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEditor)).BeginInit();
            this.SuspendLayout();
            // 
            // textKey
            // 
            this.textKey.Location = new System.Drawing.Point(66, 52);
            this.textKey.Name = "textKey";
            this.textKey.Properties.Mask.EditMask = "[a-zA-Z0-9_-]+";
            this.textKey.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textKey.Properties.MaxLength = 50;
            this.textKey.Size = new System.Drawing.Size(375, 20);
            this.textKey.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 55);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Key:";
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(12, 118);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(28, 13);
            this.Description.TabIndex = 1;
            this.Description.Text = "Type:";
            // 
            // simpleCancel
            // 
            this.simpleCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleCancel.Location = new System.Drawing.Point(365, 310);
            this.simpleCancel.Name = "simpleCancel";
            this.simpleCancel.Size = new System.Drawing.Size(75, 23);
            this.simpleCancel.TabIndex = 6;
            this.simpleCancel.Text = "Cancel";
            this.simpleCancel.Click += new System.EventHandler(this.simpleCancel_Click);
            // 
            // simpleButtonOk
            // 
            this.simpleButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonOk.Location = new System.Drawing.Point(263, 310);
            this.simpleButtonOk.Name = "simpleButtonOk";
            this.simpleButtonOk.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonOk.TabIndex = 5;
            this.simpleButtonOk.Text = "Ok";
            this.simpleButtonOk.Click += new System.EventHandler(this.simpleButtonOk_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 24);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Object ID:";
            // 
            // textObjectId
            // 
            this.textObjectId.EditValue = "0";
            this.textObjectId.Location = new System.Drawing.Point(68, 21);
            this.textObjectId.Name = "textObjectId";
            this.textObjectId.Properties.Mask.EditMask = "d";
            this.textObjectId.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textObjectId.Size = new System.Drawing.Size(100, 20);
            this.textObjectId.TabIndex = 0;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 84);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Info:";
            // 
            // textInfo
            // 
            this.textInfo.Location = new System.Drawing.Point(66, 81);
            this.textInfo.Name = "textInfo";
            this.textInfo.Properties.MaxLength = 150;
            this.textInfo.Size = new System.Drawing.Size(375, 20);
            this.textInfo.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 150);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(30, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Value:";
            // 
            // panelEditor
            // 
            this.panelEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEditor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelEditor.Location = new System.Drawing.Point(66, 150);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(378, 144);
            this.panelEditor.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "String",
            "Int",
            "Decimal",
            "ByteArray",
            "DateTime",
            "Bool",
            "Json",
            "Xml",
            "Image",
            "Custom"});
            this.comboBox1.Location = new System.Drawing.Point(66, 115);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(195, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // SettingEditForm
            // 
            this.AcceptButton = this.simpleButtonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleCancel;
            this.ClientSize = new System.Drawing.Size(458, 349);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panelEditor);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.textInfo);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.textObjectId);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.simpleButtonOk);
            this.Controls.Add(this.simpleCancel);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(474, 388);
            this.Name = "SettingEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            ((System.ComponentModel.ISupportInitialize)(this.textKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textObjectId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEditor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textKey;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl Description;
        private DevExpress.XtraEditors.SimpleButton simpleCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOk;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textObjectId;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textInfo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelEditor;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}