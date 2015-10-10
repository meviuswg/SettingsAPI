using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    public class ApiKey : IApiKey
    {
        private string _key;

        public ApiKey(string apiKey)
        {
            _key = apiKey;
        }
        public string Key
        {
            get
            {
                return _key;
            }
        }
    }
}
