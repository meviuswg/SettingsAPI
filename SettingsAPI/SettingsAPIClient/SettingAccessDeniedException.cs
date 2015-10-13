using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class SettingAccessDeniedException : SettingsRemoteStoreException
    {
        public SettingAccessDeniedException(string method, string url) : base(method, url, "Access to the resource was denied")
        {

        }


    }
}
