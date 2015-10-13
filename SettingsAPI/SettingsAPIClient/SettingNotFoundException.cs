using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class SettingNotFoundException : SettingsRemoteStoreException
    {
        public SettingNotFoundException(string method, string url) : base(method, url, "Requested item not found")
        {

        }

        public SettingNotFoundException(string method, string url, string item) : base(method, url, string.Format("'{0}' not found", item))
        {

        }
    }
}
