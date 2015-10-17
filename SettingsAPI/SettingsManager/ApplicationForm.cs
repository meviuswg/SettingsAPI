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
    public partial class ApplicationForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private string apiKey;
        private string url;
        private static SettingsAPIClient.SettingsManager settingsManager;

        public ApplicationForm()
        {
            InitializeComponent();

            url = ConfigurationManager.AppSettings["settingsStoreEndpoint"];
            apiKey = ConfigurationManager.AppSettings["apiKey"];

            this.Shown += ApplicationForm_Shown;
        }

        private async void ApplicationForm_Shown(object sender, EventArgs e)
        {
            await GetApplications();
        }

        private async Task GetApplications()
        {
            try
            {
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

        private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private ApplicationUserControl applicationControl;

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

        private void ShowStartScreen()
        {
            gridControl1.Visible = true;
            applicationControl = null;
            ribbonPageGroupApplicationActions.Visible = false;
            barButtonBack.Visibility = BarItemVisibility.Never;
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
                    ApplicationEditForm form = new ApplicationEditForm(true, settingsManager);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (await settingsManager.CreateApplicationAsync(form.ApplicationName, form.ApplicationDescrption))
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
                        if (MessageBox.Show(string.Format("Are you sure you want to delete application {0} and all its settings?", application.Name), "Delete Application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

        private void barButtonItemAccess_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void barButtonItemVersions_ItemClick(object sender, ItemClickEventArgs e)
        {
            VersionsForm form = new VersionsForm(applicationControl.Application.Name, settingsManager);
            form.ShowDialog();

            if (form.Version != null)
            {
                applicationControl.CurrentVersion = form.Version;
            }
        }

        private async void barButtonItemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (applicationControl == null)
            {
                var application = gridViewApplications.GetRow(gridViewApplications.FocusedRowHandle) as SettingsApplication;

                if (application != null)
                {
                    ApplicationEditForm form = new ApplicationEditForm(false, settingsManager);

                    if (form.ShowDialog() == DialogResult.OK)
                    {

                    }

                }
            }
            else
            {
               await applicationControl.EditButtonClicked(); 
            }
        }
    }
}