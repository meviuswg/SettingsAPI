using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsManager
{
    internal class ConfigurationManager
    {
        private ConfigurationManager()
        {
            Url = System.Configuration.ConfigurationManager.AppSettings["settingsStoreEndpoint"];
            ApiKey = System.Configuration.ConfigurationManager.AppSettings["apiKey"];
        }

        public static ConfigurationManager Current{ get { return _instance ?? (_instance = new ConfigurationManager()); } }
        private static ConfigurationManager _instance;

        public string ApiKey { get; set; }
        public string Url { get; set; }
    }
}
