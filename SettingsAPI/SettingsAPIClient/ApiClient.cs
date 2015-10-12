using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    internal abstract class ApiClient<T>
    {
        protected string _url;
        protected string _apiKey;
        protected const int TIMEOUT = 5;

        public ApiClient(string url, string apiKey)
        {
            this._url = url;
            this._apiKey = apiKey;
        }

        protected async Task<T> Get(string url)
        {
            HttpClient client = CreateClient();

            var response = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                throw new SettingsException(response.StatusCode.ToString());
            }
        }

        protected async Task<bool> Post(string url, T data)
        {
            var response = await CreateClient().PostAsJsonAsync(url, data);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                throw new SettingsException(response.StatusCode.ToString());
            }
        }

        private HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = TimeSpan.FromSeconds(TIMEOUT);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}