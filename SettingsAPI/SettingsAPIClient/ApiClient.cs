using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    internal abstract class ApiClient<T>
    {
        protected string _baseUrl;
        protected string _apiKey;
        protected const int TIMEOUT = 5;

        public ApiClient(string url, string apiKey)
        {
            if (!url.EndsWith("/")) url = url.Substring(0, url.Length - 2);

            this._baseUrl = url;
            this._apiKey = apiKey;
        }

        public virtual async Task<T> Get(string key = "")
        {
            HttpClient client = CreateClient();

            var response = await client.GetAsync(GetEndpoint(key), HttpCompletionOption.ResponseContentRead);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            else
            {
                await HandleNotAcceptedStatus(response);

                return default(T);
            }
        }

        public virtual async Task<bool> Post(T data)
        {
            var response = await CreateClient().PostAsJsonAsync(GetEndpoint(), data);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return await HandleNotAcceptedStatus(response);
            }
        }

        private async Task<bool> HandleNotAcceptedStatus(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new SettingAccessDeniedException(response.RequestMessage.Method.Method, response.RequestMessage.RequestUri.ToString());
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new SettingNotFoundException(response.RequestMessage.Method.Method, response.RequestMessage.RequestUri.ToString());
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                throw new SettingAccessDeniedException(response.RequestMessage.Method.Method, response.RequestMessage.RequestUri.ToString());
            }

            string message = await response.Content.ReadAsStringAsync();

            throw new SettingsRemoteStoreException(response.RequestMessage.Method.Method, response.RequestMessage.RequestUri.ToString(), message); 

        }

        private HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = TimeSpan.FromSeconds(TIMEOUT);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        protected abstract string LoalPath { get; }

        protected virtual string GetEndpoint(string key = "")
        {
            string url = string.Concat(_baseUrl, "/", LoalPath, "/", key, string.Format("?apikey={0}", _apiKey));
            return url;
        }

    }
}