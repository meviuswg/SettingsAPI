using System;

namespace SettingsManager
{
    partial class ApplicationUserControl
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
            this.gridViewVersions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlDirectories = new DevExpress.XtraGrid.GridControl();
            this.gridViewDirectories = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnDirectoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDirectoryDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelApplicationName = new DevExpress.XtraEditors.LabelControl();
            this.labelDescription = new DevExpress.XtraEditors.LabelControl();
            this.labelCreated = new DevExpress.XtraEditors.LabelControl();
            this.LabelCreatedValue = new DevExpress.XtraEditors.LabelControl();
            this.labelApplicationDescriptionValue = new DevExpress.XtraEditors.LabelControl();
            this.labelApplicationNameValue = new DevExpress.XtraEditors.LabelControl();
            this.labelDirectoryName = new DevExpress.XtraEditors.LabelControl();
            this.labelDirectoryNameValue = new DevExpress.XtraEditors.LabelControl();
            this.mainPanel = new DevExpress.XtraEditors.PanelControl();
            this.gridControlSettings = new DevExpress.XtraGrid.GridControl();
            this.gridViewSettings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnSettingObjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSettingKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSettingValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelDirectoryDescription = new DevExpress.XtraEditors.LabelControl();
            this.labelDirectoryDescriptionValue = new DevExpress.XtraEditors.LabelControl();
            this.radioGroupVersions = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVersions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDirectories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDirectories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupVersions.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewVersions
            // 
            this.gridViewVersions.GridControl = this.gridControlDirectories;
            this.gridViewVersions.Name = "gridViewVersions";
            // 
            // gridControlDirectories
            // 
            this.gridControlDirectories.Location = new System.Drawing.Point(0, 0);
            this.gridControlDirectories.MainView = this.gridViewDirectories;
            this.gridControlDirectories.Name = "gridControlDirectories";
            this.gridControlDirectories.Size = new System.Drawing.Size(818, 199);
            this.gridControlDirectories.TabIndex = 3;
            this.gridControlDirectories.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDirectories,
            this.gridView1,
            this.gridViewVersions});
            // 
            // gridViewDirectories
            // 
            this.gridViewDirectories.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDirectoryName,
            this.gridColumnDirectoryDescription});
            this.gridViewDirectories.GridControl = this.gridControlDirectories;
            this.gridViewDirectories.Name = "gridViewDirectories";
            this.gridViewDirectories.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewDirectories.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewDirectories.OptionsBehavior.Editable = false;
            this.gridViewDirectories.OptionsDetail.AllowZoomDetail = false;
            this.gridViewDirectories.OptionsDetail.ShowDetailTabs = false;
            this.gridViewDirectories.OptionsDetail.SmartDetailExpand = false;
            this.gridViewDirectories.OptionsView.ShowGroupPanel = false;
            this.gridViewDirectories.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewDirectories.DoubleClick += new System.EventHandler(this.gridViewDirectories_DoubleClick);
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
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlDirectories;
            this.gridView1.Name = "gridView1";
            // 
            // labelApplicationName
            // 
            this.labelApplicationName.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplicationName.Location = new System.Drawing.Point(16, 13);
            this.labelApplicationName.Name = "labelApplicationName";
            this.labelApplicationName.Size = new System.Drawing.Size(99, 16);
            this.labelApplicationName.TabIndex = 4;
            this.labelApplicationName.Text = "Application Name";
            // 
            // labelDescription
            // 
            this.labelDescription.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(16, 41);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(129, 16);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Application Description";
            // 
            // labelCreated
            // 
            this.labelCreated.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreated.Location = new System.Drawing.Point(16, 73);
            this.labelCreated.Name = "labelCreated";
            this.labelCreated.Size = new System.Drawing.Size(116, 16);
            this.labelCreated.TabIndex = 7;
            this.labelCreated.Text = "Application Created:";
            // 
            // LabelCreatedValue
            // 
            this.LabelCreatedValue.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCreatedValue.Location = new System.Drawing.Point(176, 73);
            this.LabelCreatedValue.Name = "LabelCreatedValue";
            this.LabelCreatedValue.Size = new System.Drawing.Size(12, 16);
            this.LabelCreatedValue.TabIndex = 7;
            this.LabelCreatedValue.Text = "...";
            // 
            // labelApplicationDescriptionValue
            // 
            this.labelApplicationDescriptionValue.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplicationDescriptionValue.Location = new System.Drawing.Point(176, 41);
            this.labelApplicationDescriptionValue.Name = "labelApplicationDescriptionValue";
            this.labelApplicationDescriptionValue.Size = new System.Drawing.Size(12, 16);
            this.labelApplicationDescriptionValue.TabIndex = 8;
            this.labelApplicationDescriptionValue.Text = "...";
            // 
            // labelApplicationNameValue
            // 
            this.labelApplicationNameValue.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplicationNameValue.Location = new System.Drawing.Point(176, 13);
            this.labelApplicationNameValue.Name = "labelApplicationNameValue";
            this.labelApplicationNameValue.Size = new System.Drawing.Size(12, 16);
            this.labelApplicationNameValue.TabIndex = 8;
            this.labelApplicationNameValue.Text = "...";
            // 
            // labelDirectoryName
            // 
            this.labelDirectoryName.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDirectoryName.Location = new System.Drawing.Point(16, 106);
            this.labelDirectoryName.Name = "labelDirectoryName";
            this.labelDirectoryName.Size = new System.Drawing.Size(88, 16);
            this.labelDirectoryName.TabIndex = 4;
            this.labelDirectoryName.Text = "Directory Name";
            this.labelDirectoryName.Visible = false;
            // 
            // labelDirectoryNameValue
            // 
            this.labelDirectoryNameValue.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDirectoryNameValue.Location = new System.Drawing.Point(177, 106);
            this.labelDirectoryNameValue.Name = "labelDirectoryNameValue";
            this.labelDirectoryNameValue.Size = new System.Drawing.Size(12, 16);
            this.labelDirectoryNameValue.TabIndex = 8;
            this.labelDirectoryNameValue.Text = "...";
            this.labelDirectoryNameValue.Visible = false;
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Controls.Add(this.gridControlSettings);
            this.mainPanel.Controls.Add(this.gridControlDirectories);
            this.mainPanel.Location = new System.Drawing.Point(0, 153);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(821, 369);
            this.mainPanel.TabIndex = 10;
            // 
            // gridControlSettings
            // 
            this.gridControlSettings.Location = new System.Drawing.Point(0, 196);
            this.gridControlSettings.MainView = this.gridViewSettings;
            this.gridControlSettings.Name = "gridControlSettings";
            this.gridControlSettings.Size = new System.Drawing.Size(818, 199);
            this.gridControlSettings.TabIndex = 4;
            this.gridControlSettings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSettings,
            this.gridView2,
            this.gridView3});
            // 
            // gridViewSettings
            // 
            this.gridViewSettings.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSettingObjectId,
            this.gridColumnSettingKey,
            this.gridColumnSettingValue});
            this.gridViewSettings.GridControl = this.gridControlSettings;
            this.gridViewSettings.Name = "gridViewSettings";
            this.gridViewSettings.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewSettings.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewSettings.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            this.gridViewSettings.OptionsDetail.AllowZoomDetail = false;
            this.gridViewSettings.OptionsDetail.ShowDetailTabs = false;
            this.gridViewSettings.OptionsDetail.SmartDetailExpand = false;
            this.gridViewSettings.OptionsView.ShowGroupPanel = false;
            this.gridViewSettings.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumnSettingObjectId
            // 
            this.gridColumnSettingObjectId.Caption = "ObjectId";
            this.gridColumnSettingObjectId.FieldName = "ObjectId";
            this.gridColumnSettingObjectId.Name = "gridColumnSettingObjectId";
            this.gridColumnSettingObjectId.Visible = true;
            this.gridColumnSettingObjectId.VisibleIndex = 0;
            this.gridColumnSettingObjectId.Width = 79;
            // 
            // gridColumnSettingKey
            // 
            this.gridColumnSettingKey.Caption = "Key";
            this.gridColumnSettingKey.FieldName = "Key";
            this.gridColumnSettingKey.Name = "gridColumnSettingKey";
            this.gridColumnSettingKey.Visible = true;
            this.gridColumnSettingKey.VisibleIndex = 1;
            this.gridColumnSettingKey.Width = 153;
            // 
            // gridColumnSettingValue
            // 
            this.gridColumnSettingValue.Caption = "Value";
            this.gridColumnSettingValue.FieldName = "Value";
            this.gridColumnSettingValue.Name = "gridColumnSettingValue";
            this.gridColumnSettingValue.Visible = true;
            this.gridColumnSettingValue.VisibleIndex = 2;
            this.gridColumnSettingValue.Width = 568;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControlSettings;
            this.gridView2.Name = "gridView2";
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridControlSettings;
            this.gridView3.Name = "gridView3";
            // 
            // labelDirectoryDescription
            // 
            this.labelDirectoryDescription.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDirectoryDescription.Location = new System.Drawing.Point(16, 128);
            this.labelDirectoryDescription.Name = "labelDirectoryDescription";
            this.labelDirectoryDescription.Size = new System.Drawing.Size(118, 16);
            this.labelDirectoryDescription.TabIndex = 4;
            this.labelDirectoryDescription.Text = "Directory Description";
            this.labelDirectoryDescription.Visible = false;
            // 
            // labelDirectoryDescriptionValue
            // 
            this.labelDirectoryDescriptionValue.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDirectoryDescriptionValue.Location = new System.Drawing.Point(177, 131);
            this.labelDirectoryDescriptionValue.Name = "labelDirectoryDescriptionValue";
            this.labelDirectoryDescriptionValue.Size = new System.Drawing.Size(12, 16);
            this.labelDirectoryDescriptionValue.TabIndex = 8;
            this.labelDirectoryDescriptionValue.Text = "...";
            this.labelDirectoryDescriptionValue.Visible = false;
            // 
            // radioGroupVersions
            // 
            this.radioGroupVersions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioGroupVersions.Location = new System.Drawing.Point(516, 3);
            this.radioGroupVersions.Name = "radioGroupVersions";
            this.radioGroupVersions.Size = new System.Drawing.Size(302, 96);
            this.radioGroupVersions.TabIndex = 11;
            // 
            // ApplicationUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioGroupVersions);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.labelDirectoryDescriptionValue);
            this.Controls.Add(this.labelDirectoryNameValue);
            this.Controls.Add(this.labelApplicationNameValue);
            this.Controls.Add(this.labelApplicationDescriptionValue);
            this.Controls.Add(this.LabelCreatedValue);
            this.Controls.Add(this.labelCreated);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelDirectoryDescription);
            this.Controls.Add(this.labelDirectoryName);
            this.Controls.Add(this.labelApplicationName);
            this.Name = "ApplicationUserControl";
            this.Size = new System.Drawing.Size(821, 525);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVersions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDirectories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDirectories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupVersions.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlDirectories;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDirectories;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDirectoryDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDirectoryName;
        private DevExpress.XtraEditors.LabelControl labelApplicationName;
        private DevExpress.XtraEditors.LabelControl labelDescription;
        private DevExpress.XtraEditors.LabelControl labelCreated;
        private DevExpress.XtraEditors.LabelControl LabelCreatedValue;
        private DevExpress.XtraEditors.LabelControl labelApplicationDescriptionValue;
        private DevExpress.XtraEditors.LabelControl labelApplicationNameValue;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewVersions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelDirectoryName;
        private DevExpress.XtraEditors.LabelControl labelDirectoryNameValue;
        private DevExpress.XtraEditors.PanelControl mainPanel;
        private DevExpress.XtraGrid.GridControl gridControlSettings;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSettings;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSettingObjectId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSettingKey;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSettingValue;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.LabelControl labelDirectoryDescription;
        private DevExpress.XtraEditors.LabelControl labelDirectoryDescriptionValue;
        private DevExpress.XtraEditors.RadioGroup radioGroupVersions;
    }
}
