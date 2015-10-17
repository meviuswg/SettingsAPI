using SettingsAPIClient;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace SettingsManager
{
    public partial class ApplicationForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private string apiKey;
        private string url;
        static SettingsAPIClient.SettingsManager settingsManager;

        public ApplicationForm()
        {
            InitializeComponent();

            url = ConfigurationManager.AppSettings["settingsStoreEndpoint"];

            Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                await GetApplications();
            });
        }


        private async Task GetApplications()
        {

            apiKey = "ad99185c881446deae6eda61bf40990a";

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

        private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (applicationControl != null)
            {
                await applicationControl.RefreshButtonClicked();
            }
            await GetApplications();
        }


        private async void gridViewApplications_DoubleClick_1(object sender, EventArgs e)
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

            }
        }
        ApplicationUserControl applicationControl;


        private void CreateApplication_Click(object sender, EventArgs e)
        {

        }

        private void CreateDirectory_Click(object sender, EventArgs e)
        {

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
                    gridControl1.Visible = true;
                    applicationControl = null;
                }
            }

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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }
    }
}