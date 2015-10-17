namespace SettingsManager
{
    partial class VersionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionsForm));
            this.gridControlVersions = new DevExpress.XtraGrid.GridControl();
            this.gridViewVersions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.barButtonItemNew = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gridColumnVersion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCreated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCopy = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlVersions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVersions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlVersions
            // 
            this.gridControlVersions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlVersions.Location = new System.Drawing.Point(0, 52);
            this.gridControlVersions.MainView = this.gridViewVersions;
            this.gridControlVersions.Name = "gridControlVersions";
            this.gridControlVersions.Size = new System.Drawing.Size(359, 118);
            this.gridControlVersions.TabIndex = 0;
            this.gridControlVersions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewVersions});
            // 
            // gridViewVersions
            // 
            this.gridViewVersions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnVersion,
            this.gridColumnCreated});
            this.gridViewVersions.GridControl = this.gridControlVersions;
            this.gridViewVersions.Name = "gridViewVersions";
            this.gridViewVersions.OptionsBehavior.Editable = false;
            this.gridViewVersions.OptionsCustomization.AllowGroup = false;
            this.gridViewVersions.OptionsView.ShowDetailButtons = false;
            this.gridViewVersions.OptionsView.ShowGroupPanel = false;
            this.gridViewVersions.OptionsView.ShowIndicator = false;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barButtonItemNew,
            this.barButtonItemDelete,
            this.barButtonItemCopy});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 4;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal;
            this.ribbonControl1.ShowCategoryInCaption = false;
            this.ribbonControl1.ShowQatLocationSelector = false;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(359, 52);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 170);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(359, 27);
            // 
            // barButtonItemNew
            // 
            this.barButtonItemNew.Caption = "New";
            this.barButtonItemNew.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemNew.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemNew.Glyph")));
            this.barButtonItemNew.Id = 1;
            this.barButtonItemNew.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemNew.LargeGlyph")));
            this.barButtonItemNew.Name = "barButtonItemNew";
            this.barButtonItemNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemNew_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Start";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemNew);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemDelete);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemCopy);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Start";
            // 
            // gridColumnVersion
            // 
            this.gridColumnVersion.Caption = "Version";
            this.gridColumnVersion.FieldName = "Version";
            this.gridColumnVersion.Name = "gridColumnVersion";
            this.gridColumnVersion.Visible = true;
            this.gridColumnVersion.VisibleIndex = 0;
            this.gridColumnVersion.Width = 121;
            // 
            // gridColumnCreated
            // 
            this.gridColumnCreated.Caption = "Created";
            this.gridColumnCreated.FieldName = "Created";
            this.gridColumnCreated.Name = "gridColumnCreated";
            this.gridColumnCreated.Visible = true;
            this.gridColumnCreated.VisibleIndex = 1;
            this.gridColumnCreated.Width = 236;
            // 
            // barButtonItemDelete
            // 
            this.barButtonItemDelete.Caption = "Delete";
            this.barButtonItemDelete.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemDelete.Glyph")));
            this.barButtonItemDelete.Id = 2;
            this.barButtonItemDelete.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemDelete.LargeGlyph")));
            this.barButtonItemDelete.Name = "barButtonItemDelete";
            this.barButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDelete_ItemClick);
            // 
            // barButtonItemCopy
            // 
            this.barButtonItemCopy.Caption = "Copy";
            this.barButtonItemCopy.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItemCopy.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemCopy.Glyph")));
            this.barButtonItemCopy.Id = 3;
            this.barButtonItemCopy.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemCopy.LargeGlyph")));
            this.barButtonItemCopy.Name = "barButtonItemCopy";
            this.barButtonItemCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCopy_ItemClick);
            // 
            // VersionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 197);
            this.Controls.Add(this.gridControlVersions);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(375, 236);
            this.Name = "VersionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Versions";
            ((System.ComponentModel.ISupportInitialize)(this.gridControlVersions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVersions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlVersions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewVersions;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNew;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVersion;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCreated;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCopy;
    }
}