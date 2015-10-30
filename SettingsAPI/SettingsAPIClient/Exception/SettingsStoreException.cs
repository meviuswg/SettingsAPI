using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public class SettingsStoreException: SettingsException
    {
        public SettingsStoreException(HttpRequestMessage request, string message): base(message)
        {
            if (request != null)
            {
                Method = request.Method;
                RequestUri = request.RequestUri;
            }
        }

        public SettingsStoreException(HttpRequestMessage request, Exception ex) : base("Store Exception", ex)
        {
            if (request != null)
            {
                Method = request.Method;
                RequestUri = request.RequestUri;
            }
        } 

        public HttpMethod Method { get; private set; }
        public Uri RequestUri { get; private set; }
    }
}
