using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Popup_Dictionairy
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
            SettingsManager.Current.QuestionIntervalSeconds = int.Parse(pfTxtInterval.Text); //Quick and dirty
            SettingsManager.Current.Save();
            this.Close();
            
        }

        public void FillForm()
        {
            pfTxtInterval.Text = SettingsManager.Current.QuestionIntervalSeconds.ToString();
        }


    }
}
