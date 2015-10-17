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
    public partial class ApplicationEditForm : Form
    {
        SettingsAPIClient.SettingsManager settingsManager;

        public ApplicationEditForm(bool creatingNew, SettingsAPIClient.SettingsManager settingsManager)
        {
            InitializeComponent();
            CreateNew = creatingNew;
            this.settingsManager = settingsManager;

        }

        public bool CreateNew { get; set; }

        private async Task<bool> ValidateInputAsync()
        {

            if (string.IsNullOrWhiteSpace(textName.Text))
            {
                textName.ErrorText = "Enter a Name";
                return false;
            }
            else
            { 
                if (await settingsManager.ExistsApplicationAsync(textName.Text))
                {
                    textName.ErrorText = "Application name already in use.";
                    return false;
                }

                return true;
            }
        }

        public string ApplicationName { get { return textName.Text; } }

        public string ApplicationDescrption { get { return textDescription.Text; } }

        private async void simpleButtonOk_Click(object sender, EventArgs e)
        {
            if (await ValidateInputAsync())
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void simpleCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
