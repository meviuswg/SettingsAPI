namespace SettingsManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.bar5 = new DevExpress.XtraBars.Bar();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonBack = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemNewItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDeleteItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAccess = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemVersions = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemChangeKey = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemExitApplication = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemHome = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItemPath = new DevExpress.XtraBars.BarStaticItem();
            this.barEditItemProgress = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            this.barButtonItemCopy = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonMiniToolbar1 = new DevExpress.XtraBars.Ribbon.RibbonMiniToolbar(this.components);
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupApplicationActions = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupOther = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.mainPanel = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDirectories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewApplications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVersions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
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
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridViewApplications;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(642, 522);
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
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Refresh";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.LargeGlyph")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRefresh_ItemClick);
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
            this.barButtonItemDeleteItem,
            this.barButtonItemAccess,
            this.barButtonItemVersions,
            this.barButtonItemChangeKey,
            this.barButtonItemEdit,
            this.barButtonItemExitApplication,
            this.barButtonItemHome,
            this.barStaticItemPath,
            this.barEditItemProgress,
            this.barButtonItemCopy});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 18;
            this.ribbonControl1.MiniToolbars.Add(this.ribbonMiniToolbar1);
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMarqueeProgressBar1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal;
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowCategoryInCaption = false;
            this.ribbonControl1.ShowQatLocationSelector = false;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(646, 79);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
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
            // barButtonItemAccess
            // 
            this.barButtonItemAccess.Caption = "Access";
            this.barButtonItemAccess.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemAccess.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemAccess.Glyph")));
            this.barButtonItemAccess.Id = 8;
            this.barButtonItemAccess.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemAccess.LargeGlyph")));
            this.barButtonItemAccess.Name = "barButtonItemAccess";
            this.barButtonItemAccess.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAccess_ItemClick);
            // 
            // barButtonItemVersions
            // 
            this.barButtonItemVersions.Caption = "Versions";
            this.barButtonItemVersions.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemVersions.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemVersions.Glyph")));
            this.barButtonItemVersions.Id = 9;
            this.barButtonItemVersions.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemVersions.LargeGlyph")));
            this.barButtonItemVersions.Name = "barButtonItemVersions";
            this.barButtonItemVersions.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemVersions_ItemClick);
            // 
            // barButtonItemChangeKey
            // 
            this.barButtonItemChangeKey.Caption = "Settings";
            this.barButtonItemChangeKey.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemChangeKey.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemChangeKey.Glyph")));
            this.barButtonItemChangeKey.Id = 10;
            this.barButtonItemChangeKey.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemChangeKey.LargeGlyph")));
            this.barButtonItemChangeKey.Name = "barButtonItemChangeKey";
            this.barButtonItemChangeKey.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemChangeKey_ItemClick);
            // 
            // barButtonItemEdit
            // 
            this.barButtonItemEdit.Caption = "Edit";
            this.barButtonItemEdit.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemEdit.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemEdit.Glyph")));
            this.barButtonItemEdit.Id = 11;
            this.barButtonItemEdit.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemEdit.LargeGlyph")));
            this.barButtonItemEdit.Name = "barButtonItemEdit";
            this.barButtonItemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemEdit_ItemClick);
            // 
            // barButtonItemExitApplication
            // 
            this.barButtonItemExitApplication.Caption = "Exit";
            this.barButtonItemExitApplication.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemExitApplication.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemExitApplication.Glyph")));
            this.barButtonItemExitApplication.Id = 12;
            this.barButtonItemExitApplication.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemExitApplication.LargeGlyph")));
            this.barButtonItemExitApplication.Name = "barButtonItemExitApplication";
            this.barButtonItemExitApplication.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemExitApplication_ItemClick);
            // 
            // barButtonItemHome
            // 
            this.barButtonItemHome.Caption = "Home";
            this.barButtonItemHome.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemHome.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemHome.Glyph")));
            this.barButtonItemHome.Id = 13;
            this.barButtonItemHome.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemHome.LargeGlyph")));
            this.barButtonItemHome.Name = "barButtonItemHome";
            this.barButtonItemHome.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemHome_ItemClick);
            // 
            // barStaticItemPath
            // 
            this.barStaticItemPath.Id = 15;
            this.barStaticItemPath.Name = "barStaticItemPath";
            this.barStaticItemPath.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barEditItemProgress
            // 
            this.barEditItemProgress.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barEditItemProgress.Caption = "Loading...";
            this.barEditItemProgress.Edit = this.repositoryItemMarqueeProgressBar1;
            this.barEditItemProgress.Id = 16;
            this.barEditItemProgress.Name = "barEditItemProgress";
            this.barEditItemProgress.Width = 100;
            // 
            // repositoryItemMarqueeProgressBar1
            // 
            this.repositoryItemMarqueeProgressBar1.Name = "repositoryItemMarqueeProgressBar1";
            // 
            // barButtonItemCopy
            // 
            this.barButtonItemCopy.Caption = "Copy";
            this.barButtonItemCopy.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemCopy.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemCopy.Glyph")));
            this.barButtonItemCopy.Id = 17;
            this.barButtonItemCopy.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemCopy.LargeGlyph")));
            this.barButtonItemCopy.Name = "barButtonItemCopy";
            this.barButtonItemCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCopy_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroupApplicationActions,
            this.ribbonPageGroupOther});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Start";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemHome);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonBack);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemExitApplication);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "MainMenu";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemNewItem);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemEdit);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemDeleteItem);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemCopy);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Actions";
            // 
            // ribbonPageGroupApplicationActions
            // 
            this.ribbonPageGroupApplicationActions.ItemLinks.Add(this.barButtonItemAccess);
            this.ribbonPageGroupApplicationActions.ItemLinks.Add(this.barButtonItemVersions);
            this.ribbonPageGroupApplicationActions.Name = "ribbonPageGroupApplicationActions";
            this.ribbonPageGroupApplicationActions.Text = "Application";
            this.ribbonPageGroupApplicationActions.Visible = false;
            // 
            // ribbonPageGroupOther
            // 
            this.ribbonPageGroupOther.ItemLinks.Add(this.barButtonItemChangeKey);
            this.ribbonPageGroupOther.Name = "ribbonPageGroupOther";
            this.ribbonPageGroupOther.Text = "Other";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.barStaticItemPath);
            this.ribbonStatusBar1.ItemLinks.Add(this.barEditItemProgress);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 605);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(646, 31);
            // 
            // applicationMenu1
            // 
            this.applicationMenu1.Name = "applicationMenu1";
            this.applicationMenu1.Ribbon = this.ribbonControl1;
            // 
            // mainPanel
            // 
            this.mainPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.mainPanel.Controls.Add(this.gridControl1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 79);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(646, 526);
            this.mainPanel.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 636);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(504, 311);
            this.Name = "MainForm";
            this.Ribbon = this.ribbonControl1;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "Settings Application Manager";
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDirectories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewApplications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVersions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem barButtonItemAccess;
        private DevExpress.XtraBars.BarButtonItem barButtonItemVersions;
        private DevExpress.XtraBars.BarButtonItem barButtonItemChangeKey;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupApplicationActions;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupOther;
        private DevExpress.XtraBars.BarButtonItem barButtonItemEdit;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExitApplication;
        private DevExpress.XtraBars.BarButtonItem barButtonItemHome;
        private DevExpress.XtraBars.BarStaticItem barStaticItemPath;
        private DevExpress.XtraBars.BarEditItem barEditItemProgress;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCopy;
    }
}

