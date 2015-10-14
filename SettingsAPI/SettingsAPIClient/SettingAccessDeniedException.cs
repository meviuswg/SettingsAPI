using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class SettingAccessDeniedException : SettingsStoreException
    {
        public SettingAccessDeniedException(HttpRequestMessage message) : base(message, "Access to the resource was denied")
        {

        } 
    }
}
