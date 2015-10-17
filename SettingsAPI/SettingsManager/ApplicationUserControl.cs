using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SettingsAPIClient;

namespace SettingsManager
{
    public enum ApplicationControlLevel
    {
        Directory, Setting
    }
    public partial class ApplicationUserControl : UserControl
    {
        private SettingsApplication application;
        SettingsAPIClient.SettingsManager settingsManager;
        BindingList<SettingsDirectory> directoryBinding;

        public ApplicationUserControl()
        {
            InitializeComponent(); 
        }


        public ApplicationUserControl(SettingsAPIClient.SettingsManager settingsManager)
        {
            InitializeComponent();

            this.application = settingsManager.Application;
            this.settingsManager = settingsManager;

        
            Task.Run(() => Init()).Wait();
        }

        public ApplicationControlLevel Level { get; set; }

        private async Task Init()
        {
            gridControlSettings.Visible = false;
            gridControlDirectories.Visible = true;
            gridControlDirectories.Dock = DockStyle.Fill;
            gridControlSettings.Dock = DockStyle.Fill;
 
            Level = ApplicationControlLevel.Directory;

          

            ShowDirectory(null);

            foreach (var item in application.Versions)
            {
                radioGroupVersions.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(item, string.Format("Version: {0} Created: {1}", item.Version, item.Created)));
            }

            radioGroupVersions.EditValue = application.Versions.First();
            radioGroupVersions.Properties.SelectedIndexChanged += Properties_SelectedIndexChanged;
            labelApplicationNameValue.Text = application.Name;
            labelApplicationDescriptionValue.Text = application.Description;
            LabelCreatedValue.Text = application.Created.ToString();

            await SetDirectoryBinding(application.Name);
        }

        private async Task SetDirectoryBinding(string applicationName)
        {
            if(await settingsManager.OpenApplicationAsync(applicationName))
            {
                directoryBinding = new BindingList<SettingsDirectory>(settingsManager.Application.Directories);

                directoryBinding.AddingNew += DirectoryBinding_AddingNew;
                directoryBinding.ListChanged += DirectoryBinding_ListChanged;

                gridControlDirectories.DataSource = directoryBinding;
            } 
        }

        private void DirectoryBinding_ListChanged(object sender, ListChangedEventArgs e)
        {
          
        }

        private void DirectoryBinding_AddingNew(object sender, AddingNewEventArgs e)
        {
           
        }

    
 

        public void BackButtonClicked()
        {
            if(Level == ApplicationControlLevel.Setting)
            {
                gridControlDirectories.Visible = true;
                gridControlSettings.Visible = false;

                ShowDirectory(null);

                Level = ApplicationControlLevel.Directory;
            }
        }

        public async Task RefreshButtonClicked()
        {
            if (Level == ApplicationControlLevel.Directory)
            {
                await SetDirectoryBinding(application.Name);
            }
            else
            {
                await OpenSettings();
            }
        }

        public async Task NewItemButtonClicked()
        {
            if (Level == ApplicationControlLevel.Directory)
            {
                DirectoryEditForm form = new DirectoryEditForm(true, settingsManager);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    SettingsDirectory newDir = new SettingsDirectory();
                    newDir.Name = form.DirectoryName;
                    newDir.Description = form.DirectoryDescription;

                    if (await settingsManager.CreateDirectoryAsync(application.Name, newDir.Name, newDir.Description))
                    {
                        directoryBinding.Add(newDir);
                    } 
                }
            }
            else
            {
                gridViewSettings.AddNewRow();
            }
        }

        public async Task DeleteItemButtonClicked()
        {
            if(Level == ApplicationControlLevel.Directory)
            {
                var directory = gridViewDirectories.GetRow(gridViewDirectories.FocusedRowHandle) as SettingsDirectory;

                if(directory != null)
                {
                    if(string.Equals(directory.Name, "root") || string.Equals(directory.Name, "system"))
                    {
                        MessageBox.Show("This directory can not be deleted", "System directory", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if(MessageBox.Show(string.Format("Are you sure you want to delete directory {0} and all its settings?", directory.Name),"Delete Directory", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==  DialogResult.Yes)
                    {
                       await settingsManager.DeleteDirectoryAsync(application.Name, directory.Name);
                    }
                }
            }
            else
            {

            }
        }

        private void ShowDirectory(SettingsDirectory directory)
        {
            if (directory != null)
            {
                labelDirectoryDescription.Visible = true;
                labelDirectoryDescriptionValue.Visible = true;
                labelDirectoryDescriptionValue.Text = directory.Description;

                labelDirectoryName.Visible = true;
                labelDirectoryNameValue.Visible = true;
                labelDirectoryNameValue.Text = directory.Name;
            }
            else
            {
                labelDirectoryDescription.Visible = false;
                labelDirectoryName.Visible = false;
                labelDirectoryDescriptionValue.Visible = false;
                labelDirectoryNameValue.Visible = false;
            }
        }

        private void gridDirectories_DoubleClick(object sender, EventArgs e)
        {

        }

        private async void gridViewDirectories_DoubleClick(object sender, EventArgs e)
        {
            await OpenSettings();
        }

        private int CurrentVersion
        {
            get
            {
                return ((SettingsVersion)radioGroupVersions.EditValue).Version;
            }
        }

        private async Task OpenSettings()
        {
            gridControlDirectories.Visible = false;
            gridControlSettings.Visible = true;
            var directory = gridViewDirectories.GetRow(gridViewDirectories.FocusedRowHandle) as SettingsDirectory;

            Level = ApplicationControlLevel.Setting;

            if (directory != null)
            {
                ShowDirectory(directory);

                if (await settingsManager.OpenDirectoryAsync(application.Name, CurrentVersion, directory.Name))
                {
                    gridControlDirectories.Visible = false;
                    gridControlSettings.Visible = true;

                    gridControlSettings.DataSource = settingsManager.Items;
                }
            }
        }

        private async void Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Level == ApplicationControlLevel.Setting)
            {
                await OpenSettings();
            }
        }
    }
}
