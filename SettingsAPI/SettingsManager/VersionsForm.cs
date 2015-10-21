using SettingsAPIClient;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingsManager
{
    public partial class VersionsForm : Form
    {
        private SettingsAPIClient.SettingsManager settingsManager;
        private string applicationName;
        private int highVersion;
        private SettingsApplication application;
        public VersionsForm(string applicationName)
        {
            InitializeComponent();
            this.settingsManager = new SettingsAPIClient.SettingsManager(ConfigurationManager.Current.Url, ConfigurationManager.Current.ApiKey);
            this.Shown += VersionsForm_Shown;

            this.applicationName = applicationName;
            Task.Run(() => LoadApplication());  
            gridViewVersions.DoubleClick += GridViewVersions_DoubleClick;
        }

        private void GridViewVersions_DoubleClick(object sender, EventArgs e)
        {
            SettingsVersion version = gridViewVersions.GetRow(gridViewVersions.FocusedRowHandle) as SettingsVersion;

            if (version != null)
            {
                Version = version;
                this.Close();
            }
        }

        public SettingsVersion Version { get; set; }

        private async void VersionsForm_Shown(object sender, EventArgs e)
        {
            await LoadApplication();
        }

        private async void barButtonItemNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                await settingsManager.CreateApplicationVersionAsync(applicationName, highVersion + 1);
                await LoadApplication();
                Version = application.Versions.OrderByDescending(v => v.Created).First();
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void barButtonItemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void barButtonItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var version = gridViewVersions.GetRow(gridViewVersions.FocusedRowHandle) as SettingsVersion;

                if (version != null)
                {
                    if (application.Versions.Count == 1)
                    {
                        MessageBox.Show("This version can not be deleted as it is the only version", "Only version", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (MessageBox.Show(string.Format("Are you sure you want to delete version of application {0} and all the settings it contains?", version.Version, applicationName), "Delete Application Version and Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        await settingsManager.DeleteApplicationVersionAsync(applicationName, version.Version);
                        await LoadApplication();

                        Version = application.Versions.OrderByDescending(v => v.Created).First();
                    }
                }
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadApplication()
        {
            application = await settingsManager.GetApplication(applicationName); 

            highVersion = application.Versions.Max(v => v.Version);

            gridControlVersions.DataSource = application.Versions;
        }
    }
}