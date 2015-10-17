namespace SettingsManager
{
    partial class ApplicationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationForm));
            this.gridViewDirectories = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnDirectoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDirectoryDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridViewApplications = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.columnApplicationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.columnApplicationDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridViewSettings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnObjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSettingsValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSettingsKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridViewVersions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnVersionNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnVersionCreated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.settingsApplicationBindingSource = new System.Windows.Forms.BindingSource();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.bar5 = new DevExpress.XtraBars.Bar();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonBack = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemNewItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonItemDeleteItem = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonMiniToolbar1 = new DevExpress.XtraBars.Ribbon.RibbonMiniToolbar();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu();
            this.mainPanel = new DevExpress.XtraEditors.PanelControl();
            this.barButtonGroup2 = new DevExpress.XtraBars.BarButtonGroup();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDirectories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewApplications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVersions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsApplicationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridViewDirectories
            // 
            this.gridViewDirectories.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDirectoryName,
            this.gridColumnDirectoryDescription});
            this.gridViewDirectories.GridControl = this.gridControl1;
            this.gridViewDirectories.Name = "gridViewDirectories";
            this.gridViewDirectories.OptionsCustomization.AllowGroup = false;
            this.gridViewDirectories.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnDirectoryName
            // 
            this.gridColumnDirectoryName.Caption = "Name";
            this.gridColumnDirectoryName.FieldName = "Description";
            this.gridColumnDirectoryName.Name = "gridColumnDirectoryName";
            this.gridColumnDirectoryName.Visible = true;
            this.gridColumnDirectoryName.VisibleIndex = 0;
            // 
            // gridColumnDirectoryDescription
            // 
            this.gridColumnDirectoryDescription.Caption = "Description";
            this.gridColumnDirectoryDescription.FieldName = "Name";
            this.gridColumnDirectoryDescription.Name = "gridColumnDirectoryDescription";
            this.gridColumnDirectoryDescription.Visible = true;
            this.gridColumnDirectoryDescription.VisibleIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridViewApplications;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(875, 281);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewApplications,
            this.gridViewSettings,
            this.gridViewVersions,
            this.gridViewDirectories});
            // 
            // gridViewApplications
            // 
            this.gridViewApplications.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.columnApplicationName,
            this.columnApplicationDescription});
            this.gridViewApplications.GridControl = this.gridControl1;
            this.gridViewApplications.Name = "gridViewApplications";
            this.gridViewApplications.OptionsBehavior.Editable = false;
            this.gridViewApplications.OptionsCustomization.AllowGroup = false;
            this.gridViewApplications.OptionsEditForm.ShowOnDoubleClick = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewApplications.OptionsView.ShowDetailButtons = false;
            this.gridViewApplications.OptionsView.ShowGroupPanel = false;
            this.gridViewApplications.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewApplications.DoubleClick += new System.EventHandler(this.gridViewApplications_DoubleClick_1);
            // 
            // columnApplicationName
            // 
            this.columnApplicationName.Caption = "Name";
            this.columnApplicationName.FieldName = "Name";
            this.columnApplicationName.Name = "columnApplicationName";
            this.columnApplicationName.Visible = true;
            this.columnApplicationName.VisibleIndex = 0;
            this.columnApplicationName.Width = 126;
            // 
            // columnApplicationDescription
            // 
            this.columnApplicationDescription.Caption = "Description";
            this.columnApplicationDescription.FieldName = "Description";
            this.columnApplicationDescription.Name = "columnApplicationDescription";
            this.columnApplicationDescription.Visible = true;
            this.columnApplicationDescription.VisibleIndex = 1;
            this.columnApplicationDescription.Width = 731;
            // 
            // gridViewSettings
            // 
            this.gridViewSettings.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnObjectId,
            this.gridColumnSettingsValue,
            this.gridColumnSettingsKey});
            this.gridViewSettings.GridControl = this.gridControl1;
            this.gridViewSettings.Name = "gridViewSettings";
            this.gridViewSettings.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewSettings.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewSettings.OptionsCustomization.AllowGroup = false;
            this.gridViewSettings.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewSettings.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnObjectId
            // 
            this.gridColumnObjectId.Caption = "ObjectId";
            this.gridColumnObjectId.Name = "gridColumnObjectId";
            this.gridColumnObjectId.Visible = true;
            this.gridColumnObjectId.VisibleIndex = 0;
            this.gridColumnObjectId.Width = 99;
            // 
            // gridColumnSettingsValue
            // 
            this.gridColumnSettingsValue.Caption = "Value";
            this.gridColumnSettingsValue.FieldName = "Value";
            this.gridColumnSettingsValue.Name = "gridColumnSettingsValue";
            this.gridColumnSettingsValue.Visible = true;
            this.gridColumnSettingsValue.VisibleIndex = 1;
            this.gridColumnSettingsValue.Width = 552;
            // 
            // gridColumnSettingsKey
            // 
            this.gridColumnSettingsKey.Caption = "Key";
            this.gridColumnSettingsKey.FieldName = "Key";
            this.gridColumnSettingsKey.Name = "gridColumnSettingsKey";
            this.gridColumnSettingsKey.Visible = true;
            this.gridColumnSettingsKey.VisibleIndex = 2;
            this.gridColumnSettingsKey.Width = 206;
            // 
            // gridViewVersions
            // 
            this.gridViewVersions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnVersionNumber,
            this.gridColumnVersionCreated});
            this.gridViewVersions.GridControl = this.gridControl1;
            this.gridViewVersions.Name = "gridViewVersions";
            this.gridViewVersions.OptionsCustomization.AllowGroup = false;
            this.gridViewVersions.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnVersionNumber
            // 
            this.gridColumnVersionNumber.Caption = "Version";
            this.gridColumnVersionNumber.FieldName = "Versions.Version";
            this.gridColumnVersionNumber.Name = "gridColumnVersionNumber";
            this.gridColumnVersionNumber.Visible = true;
            this.gridColumnVersionNumber.VisibleIndex = 1;
            // 
            // gridColumnVersionCreated
            // 
            this.gridColumnVersionCreated.Caption = "Created";
            this.gridColumnVersionCreated.FieldName = "Versions.Created";
            this.gridColumnVersionCreated.Name = "gridColumnVersionCreated";
            this.gridColumnVersionCreated.Visible = true;
            this.gridColumnVersionCreated.VisibleIndex = 0;
            // 
            // settingsApplicationBindingSource
            // 
            this.settingsApplicationBindingSource.DataSource = typeof(SettingsAPIClient.SettingsApplication);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Refresh";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.LargeGlyph")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Offset = 162;
            this.bar1.Text = "Tools";
            // 
            // bar4
            // 
            this.bar4.BarName = "Custom 5";
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 2;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar4.Text = "Custom 5";
            // 
            // bar5
            // 
            this.bar5.BarName = "Custom 6";
            this.bar5.DockCol = 0;
            this.bar5.DockRow = 3;
            this.bar5.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar5.Text = "Custom 6";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barButtonItem1,
            this.barButtonBack,
            this.barButtonItemNewItem,
            this.barButtonItem3,
            this.barButtonGroup1,
            this.barButtonItemDeleteItem,
            this.barButtonGroup2});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 8;
            this.ribbonControl1.MiniToolbars.Add(this.ribbonMiniToolbar1);
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(875, 143);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // barButtonBack
            // 
            this.barButtonBack.Caption = "Back";
            this.barButtonBack.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonBack.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonBack.Glyph")));
            this.barButtonBack.Id = 2;
            this.barButtonBack.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonBack.LargeGlyph")));
            this.barButtonBack.Name = "barButtonBack";
            this.barButtonBack.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonBack.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonBack_ItemClick_1);
            // 
            // barButtonItemNewItem
            // 
            this.barButtonItemNewItem.Caption = "Add";
            this.barButtonItemNewItem.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemNewItem.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemNewItem.Glyph")));
            this.barButtonItemNewItem.Id = 3;
            this.barButtonItemNewItem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemNewItem.LargeGlyph")));
            this.barButtonItemNewItem.Name = "barButtonItemNewItem";
            this.barButtonItemNewItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonAdd_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItem3.Id = 4;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonGroup1
            // 
            this.barButtonGroup1.Caption = "Actions";
            this.barButtonGroup1.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonGroup1.Id = 5;
            this.barButtonGroup1.Name = "barButtonGroup1";
            // 
            // barButtonItemDeleteItem
            // 
            this.barButtonItemDeleteItem.Caption = "Delete";
            this.barButtonItemDeleteItem.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemDeleteItem.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemDeleteItem.Glyph")));
            this.barButtonItemDeleteItem.Id = 6;
            this.barButtonItemDeleteItem.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemDeleteItem.LargeGlyph")));
            this.barButtonItemDeleteItem.Name = "barButtonItemDeleteItem";
            this.barButtonItemDeleteItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonDelete_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "MainMenu";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonBack);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "MainMenu";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemNewItem);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemDeleteItem);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Actions";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 419);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(875, 31);
            // 
            // applicationMenu1
            // 
            this.applicationMenu1.Name = "applicationMenu1";
            this.applicationMenu1.Ribbon = this.ribbonControl1;
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mainPanel.Controls.Add(this.gridControl1);
            this.mainPanel.Location = new System.Drawing.Point(0, 141);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(875, 281);
            this.mainPanel.TabIndex = 7;
            // 
            // barButtonGroup2
            // 
            this.barButtonGroup2.Caption = "Create Version";
            this.barButtonGroup2.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonGroup2.Id = 7;
            this.barButtonGroup2.Name = "barButtonGroup2";
            // 
            // ApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 450);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "ApplicationForm";
            this.Ribbon = this.ribbonControl1;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "Settings Application Manager";
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDirectories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewApplications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVersions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsApplicationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraBars.Bar bar5;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonMiniToolbar ribbonMiniToolbar1;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu1;
        private DevExpress.XtraBars.BarButtonItem barButtonBack;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNewItem;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDeleteItem;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewVersions;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVersionCreated;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVersionNumber;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewApplications;
        private DevExpress.XtraGrid.Columns.GridColumn columnApplicationName;
        private DevExpress.XtraGrid.Columns.GridColumn columnApplicationDescription;
        private DevExpress.XtraEditors.PanelControl mainPanel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDirectories;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSettings;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDirectoryName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnObjectId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSettingsValue;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSettingsKey;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDirectoryDescription;
        private System.Windows.Forms.BindingSource settingsApplicationBindingSource;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup2;
    }
}

