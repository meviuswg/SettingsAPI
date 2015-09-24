using System;
using System.Windows.Forms;

namespace PopupDictionairy.App
{
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();
            FillForm();
        }

        private void pfButtonOK_Click(object sender, EventArgs e)
        {
            //Save data and close form
            SettingsManager.Current.QuestionIntervalSeconds = int.Parse(pfTxtInterval.Text) * 1000; //Quick and dirty
            SettingsManager.Current.Save();
            this.Close();
        }

        public void FillForm()
        {
            pfTxtInterval.Text = (SettingsManager.Current.QuestionIntervalSeconds / 1000).ToString();
        }
    }
}