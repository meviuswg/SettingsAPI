using SettingsAPIClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingsManager
{
    public enum ApplicationControlLevel
    {
        Directory, Setting
    }

    public partial class ApplicationUserControl : UserControl
    {
        public SettingsApplication Application;
        private SettingsAPIClient.SettingsManager settingsManager;
        private BindingList<SettingsDirectory> directoryBinding;

        public ApplicationUserControl()
        {
            InitializeComponent();
        }

        public ApplicationUserControl(SettingsAPIClient.SettingsManager settingsManager)
        {
            InitializeComponent();

            this.Application = settingsManager.Application;
            this.settingsManager = settingsManager;

            this.CurrentVersion = settingsManager.Application.Versions.OrderByDescending(v => v.Created).First();
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

            labelApplicationNameValue.Text = Application.Name;
            labelApplicationDescriptionValue.Text = Application.Description;
            LabelCreatedValue.Text = Application.Created.ToString();

            await SetDirectoryBinding(Application.Name);
        }

        private async Task SetDirectoryBinding(string applicationName)
        {
            if (await settingsManager.OpenApplicationAsync(applicationName))
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
            if (Level == ApplicationControlLevel.Setting)
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
                await SetDirectoryBinding(Application.Name);
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

                    if (await settingsManager.CreateDirectoryAsync(Application.Name, newDir.Name, newDir.Description))
                    {
                        directoryBinding.Add(newDir);
                    }
                }
            }
            else
            {
                SettingEditForm form = new SettingEditForm(null, settingsManager);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    await settingsManager.SaveAsync(form.Setting);
                }
            }
        }

        public async Task DeleteItemButtonClicked()
        {
            if (Level == ApplicationControlLevel.Directory)
            {
                var directory = gridViewDirectories.GetRow(gridViewDirectories.FocusedRowHandle) as SettingsDirectory;

                if (directory != null)
                {
                    if (string.Equals(directory.Name, "root") || string.Equals(directory.Name, "system"))
                    {
                        MessageBox.Show("This directory can not be deleted", "System directory", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (MessageBox.Show(string.Format("Are you sure you want to delete directory {0} and all its settings?", directory.Name), "Delete Directory", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        await settingsManager.DeleteDirectoryAsync(Application.Name, directory.Name);
                        await RefreshButtonClicked();
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
                labelDirectoryDescriptionValue.Visible = true;
                labelDirectoryDescriptionValue.Text = directory.Description;

                labelDirectoryName.Visible = true;
                labelDirectoryNameValue.Visible = true;
                labelDirectoryNameValue.Text = directory.Name;
            }
            else
            {
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

        private SettingsVersion _currentVersion;

        public SettingsVersion CurrentVersion
        {
            get { return _currentVersion; }
            set
            {
                if (_currentVersion == null || _currentVersion.Version != value.Version)
                {
                    _currentVersion = value;
                    labelCurrentVersion.Text = string.Format("V{0} Created: {1}", _currentVersion.Version, _currentVersion.Created);

                    if (Level == ApplicationControlLevel.Setting)
                    {
                        Task.Run(() => OpenSettings());
                    }
                }
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

                if (await settingsManager.OpenDirectoryAsync(Application.Name, CurrentVersion.Version, directory.Name))
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

        public async Task EditButtonClicked()
        {
            if (Level == ApplicationControlLevel.Directory)
            {
                DirectoryEditForm form = new DirectoryEditForm(true, settingsManager);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    SettingsDirectory newDir = new SettingsDirectory();
                    newDir.Name = form.DirectoryName;
                    newDir.Description = form.DirectoryDescription;

                    if (await settingsManager.CreateDirectoryAsync(Application.Name, newDir.Name, newDir.Description))
                    {
                        directoryBinding.Add(newDir);
                    }
                }
            }
            else
            {
                var setting = gridViewSettings.GetRow(gridViewSettings.FocusedRowHandle) as Setting;

                if (setting != null)
                {
                    SettingEditForm form = new SettingEditForm(setting, settingsManager);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        await settingsManager.SaveAsync(form.Setting);
                    }
                }
            }
        }

        private async void gridViewSettings_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                await EditButtonClicked();
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}