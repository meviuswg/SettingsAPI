using DevExpress.XtraBars;
using SettingsAPIClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingsManager
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private static SettingsAPIClient.SettingsManager settingsManager;
        private string apiKey;
        private ApplicationUserControl applicationControl;
        private string url;

        public MainForm()
        {
            InitializeComponent();

            url = ConfigurationManager.AppSettings["settingsStoreEndpoint"];
            apiKey = ConfigurationManager.AppSettings["apiKey"]; 
            ShowProgress();
            this.Shown += ApplicationForm_Shown;
            ShowStartScreen();
            SetupEvents();
        }

        private void SetupEvents()
        {
            this.KeyPreview = true;
            this.KeyDown += ApplicationForm_KeyDown;
        }



        private void ApplicationForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                barButtonDelete_ItemClick(this, null);
            }
        }

        private async void ApplicationForm_Shown(object sender, EventArgs e)
        {
            await GetApplications(); 
            HideProgress();
        }

        private async void barButtonAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (applicationControl != null)
                {
                    await applicationControl.NewItemButtonClicked();
                }
                else
                {
                    SettingsApplication newApplication = new SettingsApplication();

                    ApplicationEditForm form = new ApplicationEditForm(newApplication, settingsManager);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (await settingsManager.CreateApplicationAsync(newApplication.Name, newApplication.Description))
                        {
                            await GetApplications();
                        }
                    }
                }
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void barButtonDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (applicationControl != null)
                {
                    await applicationControl.DeleteItemButtonClicked();
                }
                else
                {
                    var application = gridViewApplications.GetRow(gridViewApplications.FocusedRowHandle) as SettingsApplication;

                    if (application != null)
                    {
                        if (string.Equals(application.Name, "root") || string.Equals(application.Name, "system"))
                        {
                            MessageBox.Show("This application can not be deleted", "System directory", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (ConfirmMessageBox.Show(string.Format("Are you sure you want to delete application {0} and all its settings?", application.Name)) == DialogResult.OK)
                        {
                            await settingsManager.DeleteApplicationAsync(application.Name);
                            await GetApplications();
                        }
                    }
                }
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void barButtonItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (applicationControl != null)
                {
                    await applicationControl.RefreshButtonClicked();
                }
                await GetApplications();
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItemAccess_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private async void barButtonItemChangeKey_ItemClick(object sender, ItemClickEventArgs e)
        {
            AskApiKeyForm form = new AskApiKeyForm(apiKey);

            if (form.ShowDialog() == DialogResult.OK)
            {
                apiKey = form.ApiKey;
                await GetApplications();
                ShowStartScreen();
            }
        }

        private async void barButtonItemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (applicationControl == null)
                {
                    var application = gridViewApplications.GetRow(gridViewApplications.FocusedRowHandle) as SettingsApplication;

                    if (application != null)
                    {
                        SettingsApplication editApplication = new SettingsApplication();
                        editApplication.Name = application.Name;
                        editApplication.Description = application.Description;
                        ApplicationEditForm form = new ApplicationEditForm(editApplication, settingsManager);

                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            if (await settingsManager.UpdateApplicationAsync(application.Name, editApplication.Name, editApplication.Description))
                            {
                                application.Name = editApplication.Name;
                                application.Description = editApplication.Description;
                            }
                        }
                    }
                }
                else
                {
                    await applicationControl.EditButtonClicked();
                }
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItemExitApplication_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItemHome_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (applicationControl != null)
                applicationControl.BackButtonClicked();

            ShowStartScreen();
        }

        public void ShowProgress()
        {
            barEditItemProgress.Visibility = BarItemVisibility.Always;
        }

        public void HideProgress()
        {
            barEditItemProgress.Visibility = BarItemVisibility.Never;
        }

        private async void barButtonItemVersions_ItemClick(object sender, ItemClickEventArgs e)
        {
            VersionsForm form = new VersionsForm(applicationControl.Application.Name, settingsManager);
            form.ShowDialog();

            if (form.Version != null)
            {
                applicationControl.CurrentVersion = form.Version;
                await applicationControl.RefreshButtonClicked();
            }
        }

        private void buttonBack_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            if (applicationControl != null)
            {
                if (applicationControl.Level == ApplicationControlLevel.Setting)
                {
                    applicationControl.BackButtonClicked();
                }
                else
                {
                    mainPanel.Controls.Remove(applicationControl);
                    ShowStartScreen();
                }
            }
        }

        private async Task GetApplications()
        {
            try
            {
                if(string.IsNullOrWhiteSpace(apiKey))
                {
                    AskApiKeyForm form = new AskApiKeyForm(apiKey);
                    form.ShowDialog();

                    if (string.IsNullOrWhiteSpace(form.ApiKey))
                    {
                        MessageBox.Show("No API Key. Application will exit.");
                        this.Close();
                    }

                    apiKey = form.ApiKey;
                }
                settingsManager = new SettingsAPIClient.SettingsManager(url, apiKey);

                IEnumerable<SettingsApplication> applications = null;
                try
                {
                    applications = await settingsManager.GetApplications();
                }
                catch (AggregateException ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
                gridControl1.DataSource = applications.ToList();
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void gridViewApplications_DoubleClick_1(object sender, EventArgs e)
        {
            try
            {
                var application = gridViewApplications.GetRow(gridViewApplications.FocusedRowHandle) as SettingsApplication;

                if (application != null)
                {
                    await settingsManager.OpenApplicationAsync(application.Name);
                    gridControl1.Visible = false;

                    applicationControl = new ApplicationUserControl(settingsManager);
                    applicationControl.ShowProgress += (a, b) => ShowProgress();
                    applicationControl.HideProgress += (a, b) => HideProgress();
                    applicationControl.Path = barStaticItemPath;

                    await applicationControl.Init();


                    applicationControl.Dock = DockStyle.Fill;
                    mainPanel.Controls.Add(applicationControl);
                    barButtonBack.Visibility = BarItemVisibility.Always;

                    ribbonPageGroupApplicationActions.Visible = true;
                    barButtonBack.Visibility = BarItemVisibility.Always;
                }
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowStartScreen()
        {
            gridControl1.Visible = true;
            applicationControl = null;
            ribbonPageGroupApplicationActions.Visible = false;
            barButtonBack.Visibility = BarItemVisibility.Never;
            barStaticItemPath.Caption = string.Format("Path: {0}", url);

        }
    }
}