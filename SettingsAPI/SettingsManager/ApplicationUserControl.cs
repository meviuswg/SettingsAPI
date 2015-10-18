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
                DirectoryEditForm form = new DirectoryEditForm(true, settingsManager);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        OnShowProgress();

                        SettingsDirectory newDir = new SettingsDirectory();
                        newDir.Name = form.DirectoryName;
                        newDir.Description = form.DirectoryDescription;

                        if (await settingsManager.CreateDirectoryAsync(Application.Name, newDir.Name, newDir.Description))
                        {
                            directoryBinding.Add(newDir);
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

                        await settingsManager.SaveAsync(form.Setting);
                        await OpenSettings();
                    }
                    finally
                    {
                        OnHideProgress();
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
                    if (MessageBox.Show(string.Format("Are you sure you want to delete directory {0} and all its settings?", directory.Name), "Delete Directory", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

                        gridControlSettings.DataSource = settingsManager.Items;
                    }
                }
            }
            finally
            {
                OnHideProgress();
            }
        }

        public async Task EditButtonClicked()
        {
            if (Level == ApplicationControlLevel.Directory)
            {
                DirectoryEditForm form = new DirectoryEditForm(true, settingsManager);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        OnShowProgress();

                        SettingsDirectory newDir = new SettingsDirectory();
                        newDir.Name = form.DirectoryName;
                        newDir.Description = form.DirectoryDescription;

                        if (await settingsManager.CreateDirectoryAsync(Application.Name, newDir.Name, newDir.Description))
                        {
                            directoryBinding.Add(newDir);
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

                            await settingsManager.SaveAsync(form.Setting);
                            await OpenSettings();
                        }
                        finally
                        {
                            OnHideProgress();
                        }
                    }
                }
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