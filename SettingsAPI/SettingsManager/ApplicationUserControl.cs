using DevExpress.XtraBars;
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
        }

        public ApplicationControlLevel Level { get; set; }

        public async Task Init()
        {
            this.CurrentVersion = settingsManager.Application.Versions.OrderByDescending(v => v.Created).First();

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
            try
            {
                OnShowProgress();

                if (await settingsManager.OpenApplicationAsync(applicationName))
                {
                    directoryBinding = new BindingList<SettingsDirectory>(settingsManager.Application.Directories);
                    gridControlDirectories.DataSource = directoryBinding;
                }
            }
            finally
            {
                OnHideProgress();
            }
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
                DirectoryEditForm form = new DirectoryEditForm(null, settingsManager);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        OnShowProgress();

                        var newDirectory = form.Directory;

                        if (await settingsManager.CreateDirectoryAsync(Application.Name, newDirectory.Name, newDirectory.Description))
                        {
                            directoryBinding.Add(newDirectory);
                        }
                    }
                    finally
                    {
                        OnHideProgress();
                    }
                }
            }
            else
            {
                SettingEditForm form = new SettingEditForm(null, settingsManager);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        OnShowProgress();

                        await settingsManager.CurrentDirectory.SaveAsync(form.Setting);
                        await OpenSettings();
                    }
                    finally
                    {
                        OnHideProgress();
                    }
                }
            }
        }

        public async Task CopyButtonClicked()
        {
            if (Level == ApplicationControlLevel.Directory)
            {
                var directory = gridViewDirectories.GetRow(gridViewDirectories.FocusedRowHandle) as SettingsDirectory;

                if (directory == null)
                    return;

                SettingsDirectory ediDirectory = new SettingsDirectory { Name = directory.Name, Description = directory.Description };

                DirectoryEditForm form = new DirectoryEditForm(ediDirectory, settingsManager);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        OnShowProgress();

                        if (await settingsManager.CopyDirectoryAsync(Application.Name, directory.Name, ediDirectory.Name, CurrentVersion.Version))
                        {
                            directory.Name = ediDirectory.Name;
                            directory.Description = ediDirectory.Description;

                            //TODO: Copy description
                            await settingsManager.UpdateDirectoryAsync(Application.Name, directory.Name, directory.Name, ediDirectory.Description);

                            await RefreshButtonClicked();
                        }
                    }
                    finally
                    {
                        OnHideProgress();
                    }
                }

            }
            if(Level == ApplicationControlLevel.Directory)
            {
               await EditButtonClicked();
            }
        } 

        public async Task EditButtonClicked()
        {
            if (Level == ApplicationControlLevel.Directory)
            {
                var directory = gridViewDirectories.GetRow(gridViewDirectories.FocusedRowHandle) as SettingsDirectory;

                if (directory == null)
                    return;

                SettingsDirectory ediDirectory = new SettingsDirectory { Name = directory.Name, Description = directory.Description };

                DirectoryEditForm form = new DirectoryEditForm(ediDirectory, settingsManager);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        OnShowProgress();
                        if (await settingsManager.UpdateDirectoryAsync(Application.Name, directory.Name, ediDirectory.Name, ediDirectory.Description))
                        {
                            directory.Name = ediDirectory.Name;
                            directory.Description = ediDirectory.Description;
                            await RefreshButtonClicked();
                        }
                    }
                    finally
                    {
                        OnHideProgress();
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
                        try
                        {
                            OnShowProgress();

                            await settingsManager.CurrentDirectory.SaveAsync(form.Setting);
                            await RefreshButtonClicked();
                        }
                        finally
                        {
                            OnHideProgress();
                        }
                    }
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
                    if (ConfirmMessageBox.Show(string.Format("Are you sure you want to delete directory {0} and all its settings?", directory.Name)) == DialogResult.OK)
                    {
                        try
                        {
                            OnShowProgress();

                            await settingsManager.DeleteDirectoryAsync(Application.Name, directory.Name);
                            await RefreshButtonClicked();
                        }
                        finally
                        {
                            OnHideProgress();
                        }
                    }
                }
            }
            else
            {
                var setting = gridViewSettings.GetRow(gridViewSettings.FocusedRowHandle) as Setting;

                if (setting != null)
                {
                    if (ConfirmMessageBox.Show(string.Format("Are you sure you want to delete setting key {0} for Object {1}.?", setting.Key, setting.ObjectId)) == DialogResult.OK)
                    {
                        try
                        {
                            OnShowProgress();

                            await settingsManager.CurrentDirectory.DeleteAsync(setting.ObjectId, setting.Key);
                            await RefreshButtonClicked();
                        }
                        finally
                        {
                            OnHideProgress();
                        }
                    }
                }
            }
        }

        private void ShowDirectory(SettingsDirectory directory)
        {
            if (directory != null)
            {
                PathText = string.Format(string.Format("/{0}/{1}/{2}/", Application.Name, CurrentVersion.Version, directory.Name));
            }
        }

        private async void gridViewDirectories_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                await OpenSettings();
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private SettingsVersion _currentVersion;

        public SettingsVersion CurrentVersion
        {
            get { return _currentVersion; }
            set
            {
                if (_currentVersion == null || _currentVersion.Version != value.Version)
                {
                    PathText = string.Format(string.Format("/{0}/{1}/", Application.Name, value.Version));
                    _currentVersion = value;
                }
            }
        }

        public event EventHandler ShowProgress;

        public EventHandler HideProgress;

        private void OnShowProgress()
        {
            if (ShowProgress != null)
            {
                ShowProgress(this, EventArgs.Empty);
            }
        }

        private void OnHideProgress()
        {
            if (HideProgress != null)
            {
                HideProgress(this, EventArgs.Empty);
            }
        }

        private async Task OpenSettings()
        {
            gridControlDirectories.Visible = false;
            gridControlSettings.Visible = true;
            var directory = gridViewDirectories.GetRow(gridViewDirectories.FocusedRowHandle) as SettingsDirectory;

            Level = ApplicationControlLevel.Setting;

            try
            {
                OnShowProgress();

                if (directory != null)
                {
                    ShowDirectory(directory);

                    if (await settingsManager.OpenDirectoryAsync(Application.Name, CurrentVersion.Version, directory.Name))
                    {
                        gridControlDirectories.Visible = false;
                        gridControlSettings.Visible = true;

                        gridControlSettings.DataSource = settingsManager.CurrentDirectory.Items;
                    }
                }
            }
            finally
            {
                OnHideProgress();
            }
        }
         
        public BarItem Path { get; set; }

        public string PathText
        {
            get { return Path.Caption; }
            set { Path.Caption = string.Format("Path: {0}", value); }
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