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
            this.mainPanel = new DevExpress.XtraEditors.PanelControl();
            this.gridControlSettings = new DevExpress.XtraGrid.GridControl();
            this.gridViewSettings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnSettingObjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSettingKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSettingValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnValueType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSettingInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSettingCreated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSettingModified = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            this.SuspendLayout();
            // 
            // gridViewVersions
            // 
            this.gridViewVersions.GridControl = this.gridControlDirectories;
            this.gridViewVersions.Name = "gridViewVersions";
            // 
            // gridControlDirectories
            // 
            this.gridControlDirectories.Location = new System.Drawing.Point(3, 3);
            this.gridControlDirectories.MainView = this.gridViewDirectories;
            this.gridControlDirectories.Name = "gridControlDirectories";
            this.gridControlDirectories.Size = new System.Drawing.Size(800, 199);
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
            this.labelApplicationName.Location = new System.Drawing.Point(4, 5);
            this.labelApplicationName.Name = "labelApplicationName";
            this.labelApplicationName.Size = new System.Drawing.Size(67, 16);
            this.labelApplicationName.TabIndex = 4;
            this.labelApplicationName.Text = "Application:";
            // 
            // labelDescription
            // 
            this.labelDescription.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(3, 27);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(68, 16);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Description:";
            // 
            // labelCreated
            // 
            this.labelCreated.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreated.Location = new System.Drawing.Point(263, 5);
            this.labelCreated.Name = "labelCreated";
            this.labelCreated.Size = new System.Drawing.Size(50, 16);
            this.labelCreated.TabIndex = 7;
            this.labelCreated.Text = "Created:";
            // 
            // LabelCreatedValue
            // 
            this.LabelCreatedValue.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCreatedValue.Location = new System.Drawing.Point(319, 5);
            this.LabelCreatedValue.Name = "LabelCreatedValue";
            this.LabelCreatedValue.Size = new System.Drawing.Size(12, 16);
            this.LabelCreatedValue.TabIndex = 7;
            this.LabelCreatedValue.Text = "...";
            // 
            // labelApplicationDescriptionValue
            // 
            this.labelApplicationDescriptionValue.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplicationDescriptionValue.Location = new System.Drawing.Point(77, 27);
            this.labelApplicationDescriptionValue.Name = "labelApplicationDescriptionValue";
            this.labelApplicationDescriptionValue.Size = new System.Drawing.Size(12, 16);
            this.labelApplicationDescriptionValue.TabIndex = 8;
            this.labelApplicationDescriptionValue.Text = "...";
            // 
            // labelApplicationNameValue
            // 
            this.labelApplicationNameValue.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplicationNameValue.Location = new System.Drawing.Point(77, 5);
            this.labelApplicationNameValue.Name = "labelApplicationNameValue";
            this.labelApplicationNameValue.Size = new System.Drawing.Size(12, 16);
            this.labelApplicationNameValue.TabIndex = 8;
            this.labelApplicationNameValue.Text = "...";
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mainPanel.Controls.Add(this.gridControlSettings);
            this.mainPanel.Controls.Add(this.gridControlDirectories);
            this.mainPanel.Location = new System.Drawing.Point(0, 49);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(821, 473);
            this.mainPanel.TabIndex = 10;
            // 
            // gridControlSettings
            // 
            this.gridControlSettings.Location = new System.Drawing.Point(3, 221);
            this.gridControlSettings.MainView = this.gridViewSettings;
            this.gridControlSettings.Name = "gridControlSettings";
            this.gridControlSettings.Size = new System.Drawing.Size(800, 199);
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
            this.gridColumnSettingValue,
            this.gridColumnValueType,
            this.gridColumnSettingInfo,
            this.gridColumnSettingCreated,
            this.gridColumnSettingModified});
            this.gridViewSettings.GridControl = this.gridControlSettings;
            this.gridViewSettings.Name = "gridViewSettings";
            this.gridViewSettings.OptionsBehavior.Editable = false;
            this.gridViewSettings.OptionsView.ShowGroupPanel = false;
            this.gridViewSettings.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewSettings.DoubleClick += new System.EventHandler(this.gridViewSettings_DoubleClick);
            // 
            // gridColumnSettingObjectId
            // 
            this.gridColumnSettingObjectId.Caption = "ObjectId";
            this.gridColumnSettingObjectId.FieldName = "ObjectId";
            this.gridColumnSettingObjectId.Name = "gridColumnSettingObjectId";
            this.gridColumnSettingObjectId.Visible = true;
            this.gridColumnSettingObjectId.VisibleIndex = 0;
            this.gridColumnSettingObjectId.Width = 131;
            // 
            // gridColumnSettingKey
            // 
            this.gridColumnSettingKey.Caption = "Key";
            this.gridColumnSettingKey.FieldName = "Key";
            this.gridColumnSettingKey.Name = "gridColumnSettingKey";
            this.gridColumnSettingKey.Visible = true;
            this.gridColumnSettingKey.VisibleIndex = 1;
            this.gridColumnSettingKey.Width = 96;
            // 
            // gridColumnSettingValue
            // 
            this.gridColumnSettingValue.Caption = "Value";
            this.gridColumnSettingValue.FieldName = "Value";
            this.gridColumnSettingValue.Name = "gridColumnSettingValue";
            this.gridColumnSettingValue.Visible = true;
            this.gridColumnSettingValue.VisibleIndex = 2;
            this.gridColumnSettingValue.Width = 130;
            // 
            // gridColumnValueType
            // 
            this.gridColumnValueType.Caption = "Type";
            this.gridColumnValueType.FieldName = "ValueType";
            this.gridColumnValueType.Name = "gridColumnValueType";
            this.gridColumnValueType.Visible = true;
            this.gridColumnValueType.VisibleIndex = 5;
            this.gridColumnValueType.Width = 95;
            // 
            // gridColumnSettingInfo
            // 
            this.gridColumnSettingInfo.Caption = "Info";
            this.gridColumnSettingInfo.FieldName = "Info";
            this.gridColumnSettingInfo.Name = "gridColumnSettingInfo";
            this.gridColumnSettingInfo.Visible = true;
            this.gridColumnSettingInfo.VisibleIndex = 6;
            this.gridColumnSettingInfo.Width = 104;
            // 
            // gridColumnSettingCreated
            // 
            this.gridColumnSettingCreated.Caption = "Created";
            this.gridColumnSettingCreated.FieldName = "Created";
            this.gridColumnSettingCreated.Name = "gridColumnSettingCreated";
            this.gridColumnSettingCreated.Visible = true;
            this.gridColumnSettingCreated.VisibleIndex = 3;
            this.gridColumnSettingCreated.Width = 131;
            // 
            // gridColumnSettingModified
            // 
            this.gridColumnSettingModified.Caption = "Modified";
            this.gridColumnSettingModified.FieldName = "Modified";
            this.gridColumnSettingModified.Name = "gridColumnSettingModified";
            this.gridColumnSettingModified.Visible = true;
            this.gridColumnSettingModified.VisibleIndex = 4;
            this.gridColumnSettingModified.Width = 95;
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
            // ApplicationUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.labelApplicationNameValue);
            this.Controls.Add(this.labelApplicationDescriptionValue);
            this.Controls.Add(this.LabelCreatedValue);
            this.Controls.Add(this.labelCreated);
            this.Controls.Add(this.labelDescription);
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
        private DevExpress.XtraEditors.PanelControl mainPanel;
        private DevExpress.XtraGrid.GridControl gridControlSettings;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSettings;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSettingObjectId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSettingKey;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSettingValue;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnValueType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSettingInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSettingCreated;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSettingModified;
    }
}
