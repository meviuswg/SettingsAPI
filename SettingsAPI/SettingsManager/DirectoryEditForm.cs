using SettingsAPIClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingsManager
{
    public partial class DirectoryEditForm : Form
    {
        SettingsAPIClient.SettingsManager settingsManager;
        private SettingsDirectory _directory;

        public DirectoryEditForm(SettingsDirectory directory, SettingsAPIClient.SettingsManager settingsManager)
        {
            InitializeComponent();
            this._directory = directory;
            this.settingsManager = settingsManager;

            if(_directory != null)
            {
                textName.Text = _directory.Name;
                textDescription.Text = _directory.Description;
            }

        }
         

        private async Task<bool> ValidateInputAsync()
        { 
            if (string.IsNullOrWhiteSpace(textName.Text))
            {
                textName.ErrorText = "Enter a Name";
                return false;
            }
            else
            {
                if (_directory == null && await settingsManager.DirectoryExists(textName.Text))
                {
                    textName.ErrorText = "Directory name already in use";
                    return false;
                }

                return true;
            }
        }

        public SettingsDirectory Directory{ get { return _directory; } }

        private async void simpleButtonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (await ValidateInputAsync())
                {
                    if (_directory == null)
                    {
                        _directory = new SettingsDirectory();
                    }

                    _directory.Name = textName.Text;
                    _directory.Description = textDescription.Text;

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }

        private void simpleCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
