using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Popup_Dictionairy
{
    internal class SettingsManager : INotifyPropertyChanged
    {
        private static SettingsManager _instance;

        public event EventHandler<int> QuestionIntervalChanged;

        private SettingsManager()
        {
        }

        public static SettingsManager Current
        {
            get
            {
                return _instance ?? (_instance = new SettingsManager());
            }
        }

        public int QuestionIntervalSeconds
        {
            get
            {
                return Properties.Settings.Default.QuestionIntervalSeconds;
            }
            set
            {
                if (QuestionIntervalSeconds != value)
                {
                    Properties.Settings.Default.QuestionIntervalSeconds = value;

                    OnQuestionIntervalChanged(value);
                }
            }
        }

        private void DisplayMessage(string parameterName)
        {
            MessageBox.Show("The following parameter may not be defined: {0}", parameterName);
        }

        public void Save()
        {
            Properties.Settings.Default.Save();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void OnQuestionIntervalChanged(int interval)
        {
            if (QuestionIntervalChanged != null)
            {
                QuestionIntervalChanged(this, interval);
            }
        }
    }
}