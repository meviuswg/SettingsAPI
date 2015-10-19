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
    public partial class ApplicationEditForm : Form
    {
        SettingsAPIClient.SettingsManager settingsManager;
        private SettingsApplication application;

        public ApplicationEditForm(SettingsApplication application, SettingsAPIClient.SettingsManager settingsManager)
        {
            InitializeComponent();
            this.settingsManager = settingsManager;
            this.application = application; 

            if(application != null)
            {
                textName.Text = application.Name;
                textDescription.Text = application.Description;
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
                if (application == null)
                {
                    application = new SettingsApplication();
                }

                if (!string.Equals(application.Name, textName.Text, StringComparison.CurrentCultureIgnoreCase) && await settingsManager.ApplicationExists(textName.Text))
                {
                    textName.ErrorText = "Application name already in use.";
                    return false;
                } 

                application.Name = textName.Text.Trim().Replace("  ", " ");
                application.Description = textDescription.Text.Trim().Replace("  ", " ");

                return true;
            }
        }

        public SettingsApplication Application { get { return application; } }

        private async void simpleButtonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (await ValidateInputAsync())
                {
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
