using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    internal class SettingProvider : ApiClient<Setting[]>
    {
        private SettingsStore _store;

        public SettingProvider(string url, string apiKey, SettingsStore store) : base(url, apiKey)
        {
            this._store = store;
        }

        protected string Endpoint { get { return string.Concat(_url, "/settings/", _store.Application, "/", _store.Version, "/", _store.Name, "/", _store.ObjectId); } }

        internal async Task<Setting[]> ExecuteReadFromRemoteStore(string key = "")
        {
            return await Get(GetUrl(key));
        }

        internal async Task<bool> ExecuteSaveToRemoteStore(Setting[] settings)
        {
            return await Post(GetUrl(), settings);
        }

        private string GetUrl(string key = "")
        {
            string url = string.Concat(Endpoint, "/", key, string.Format("?apikey={0}", _apiKey));
            return url;
        }

    }
}
