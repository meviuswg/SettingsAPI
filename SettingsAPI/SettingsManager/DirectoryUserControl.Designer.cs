namespace SettingsManager
{
    partial class DirectoryUserControl
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridViewDirectories = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnDirectoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDirectoryDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelApplicationName = new DevExpress.XtraEditors.LabelControl();
            this.textApplicationName = new DevExpress.XtraEditors.TextEdit();
            this.labelDescription = new DevExpress.XtraEditors.LabelControl();
            this.textApplicationDescription = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDirectories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textApplicationName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textApplicationDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(0, 85);
            this.gridControl1.MainView = this.gridViewDirectories;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(821, 440);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDirectories});
            // 
            // gridViewDirectories
            // 
            this.gridViewDirectories.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDirectoryName,
            this.gridColumnDirectoryDescription});
            this.gridViewDirectories.GridControl = this.gridControl1;
            this.gridViewDirectories.Name = "gridViewDirectories";
            this.gridViewDirectories.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridViewDirectories.OptionsDetail.AllowZoomDetail = false;
            this.gridViewDirectories.OptionsDetail.ShowDetailTabs = false;
            this.gridViewDirectories.OptionsDetail.SmartDetailExpand = false;
            this.gridViewDirectories.OptionsView.ShowGroupPanel = false;
            this.gridViewDirectories.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumnDirectoryName
            // 
            this.gridColumnDirectoryName.Caption = "Directory";
            this.gridColumnDirectoryName.FieldName = "Name";
            this.gridColumnDirectoryName.Name = "gridColumnDirectoryName";
            this.gridColumnDirectoryName.Visible = true;
            this.gridColumnDirectoryName.VisibleIndex = 0;
            this.gridColumnDirectoryName.Width = 171;
            // 
            // gridColumnDirectoryDescription
            // 
            this.gridColumnDirectoryDescription.Caption = "Description";
            this.gridColumnDirectoryDescription.FieldName = "Description";
            this.gridColumnDirectoryDescription.Name = "gridColumnDirectoryDescription";
            this.gridColumnDirectoryDescription.Visible = true;
            this.gridColumnDirectoryDescription.VisibleIndex = 1;
            this.gridColumnDirectoryDescription.Width = 632;
            // 
            // labelApplicationName
            // 
            this.labelApplicationName.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplicationName.Location = new System.Drawing.Point(16, 13);
            this.labelApplicationName.Name = "labelApplicationName";
            this.labelApplicationName.Size = new System.Drawing.Size(88, 16);
            this.labelApplicationName.TabIndex = 4;
            this.labelApplicationName.Text = "Directory Name";
            // 
            // textApplicationName
            // 
            this.textApplicationName.Location = new System.Drawing.Point(176, 10);
            this.textApplicationName.Name = "textApplicationName";
            this.textApplicationName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textApplicationName.Properties.Appearance.Options.UseFont = true;
            this.textApplicationName.Size = new System.Drawing.Size(224, 22);
            this.textApplicationName.TabIndex = 5;
            // 
            // labelDescription
            // 
            this.labelDescription.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(16, 45);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(118, 16);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Directory Description";
            // 
            // textApplicationDescription
            // 
            this.textApplicationDescription.Location = new System.Drawing.Point(176, 38);
            this.textApplicationDescription.Name = "textApplicationDescription";
            this.textApplicationDescription.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textApplicationDescription.Properties.Appearance.Options.UseFont = true;
            this.textApplicationDescription.Size = new System.Drawing.Size(224, 22);
            this.textApplicationDescription.TabIndex = 5;
            // 
            // DirectoryUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textApplicationDescription);
            this.Controls.Add(this.textApplicationName);
            this.Controls.Add(this.labelApplicationName);
            this.Controls.Add(this.gridControl1);
            this.Name = "DirectoryUserControl";
            this.Size = new System.Drawing.Size(821, 525);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDirectories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textApplicationName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textApplicationDescription.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDirectories;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDirectoryDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDirectoryName;
        private DevExpress.XtraEditors.LabelControl labelApplicationName;
        private DevExpress.XtraEditors.TextEdit textApplicationName;
        private DevExpress.XtraEditors.LabelControl labelDescription;
        private DevExpress.XtraEditors.TextEdit textApplicationDescription;
    }
}
