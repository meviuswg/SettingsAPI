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
        public SettingAccessDeniedException(HttpRequestMessage request, string message) : base(request, message)
        {

        } 
    }
}
