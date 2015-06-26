using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace Popup_Dictionairy
{
    class SettingsManager
    {
        private static  SettingsManager _instance;

        

        private SettingsManager()
        {

        }

        public static SettingsManager Current 
        { 
            get 
            {
                return _instance??(_instance = new SettingsManager());
            }        
        
        }

        public int QuestionIntervalSeconds 
        { 
            get
            {
                return Properties.Settings.Default.QuestionIntervalSeconds;
            }
        }

        private void DisplayMessage(string message)
        {
            MessageBox.Show("The following parameter may not be defined: {0}", message);
        }
    }
}
