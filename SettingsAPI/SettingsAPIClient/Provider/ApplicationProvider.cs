using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient.Provider
{
    internal class ApplicationProvider : ApiClient<SettingsApplication>
    {
        private string applicationName; 

        public ApplicationProvider(string url, string apiKey, string applicationName) : base(url, apiKey)
        {
            this.applicationName = applicationName; 
        }

        protected override string LoalPath { get { return string.Concat("application", "/" , applicationName); } }
 
    }
}
