using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class SettingsRemoteStoreException  : SettingsException
    {
        private string _method;
        private string _url;

        public SettingsRemoteStoreException(string method, string url, string message): base(message)
        {
            _url = url;
            _method = method;
        }

        public string RequestUrl { get { return _url; } }
        public string Method { get { return _method; } }
    }
}
