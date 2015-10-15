using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class SettingNotFoundException : SettingsStoreException
    {
        public SettingNotFoundException(HttpRequestMessage message) : base(message, "Resource not found")
        {

        }

        public SettingNotFoundException(string message) : base(null, message)
        {

        }
    }
}
