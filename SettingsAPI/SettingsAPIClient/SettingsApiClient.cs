using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public abstract class ApiClient<T>
    {
        protected const int TIMEOUT = 5;
        protected string _apiKey;
        protected string _baseUrl;

        public ApiClient(string url, string apiKey)
        {
            if (!url.EndsWith("/")) url = url.Substring(0, url.Length - 2);

            this._baseUrl = url;
            this._apiKey = apiKey;
        }

        public abstract string LoalPath { get; }

        public virtual async Task<T> Get(string key = "")
        {
            string endpoint = string.Empty;
            HttpResponseMessage responseMessage = null;
            try
            {
                HttpClient client = CreateHttpClient();
                endpoint = GetEndpoint(key);

                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseContentRead);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    await handleNotOk(response);
                }
            }
            catch (Exception ex)
            {
                HandleException(responseMessage, ex);
            }

            return default(T);

        }

        public virtual async Task<bool> Post(T data)
        {
            string endpoint = string.Empty;
            HttpResponseMessage responseMessage = null;
            try
            {
                HttpClient client = CreateHttpClient();
                endpoint = GetEndpoint();

                responseMessage = await client.PostAsJsonAsync(GetEndpoint(), data);

                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return await handleNotOk(responseMessage);
                }
            }
            catch (Exception ex)
            {
                HandleException(responseMessage, ex);
            }

            return false;
        }

        public virtual string GetEndpoint(string key = "")
        {
            string url = string.Concat(_baseUrl, "/", LoalPath, "/", key, string.Format("?apikey={0}", _apiKey));
            return url;
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = TimeSpan.FromSeconds(TIMEOUT);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private void HandleException(HttpResponseMessage message, Exception ex1)
        {
            if (message == null)
            {
                throw new SettingsException("Failed to initialize client", ex1);
            }

            try
            {
                throw ex1;
            }
            catch (OperationCanceledException ex)
            {
                throw new SettingsStoreException(message.RequestMessage, ex);
            }
            catch (TimeoutException ex)
            {
                throw new SettingsStoreException(message.RequestMessage, ex);
            }
            catch (HttpRequestException ex)
            {
                throw new SettingsStoreException(message.RequestMessage, ex);
            }
            catch (SettingsStoreException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SettingsException("Error", ex);
            }
        }

        private async Task<bool> handleNotOk(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new SettingsStoreException(response.RequestMessage, "Store unavailable");
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new SettingAccessDeniedException(response.RequestMessage);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new SettingAccessDeniedException(response.RequestMessage);
            }

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new SettingAccessDeniedException(response.RequestMessage);
            }

            string message = await response.Content.ReadAsStringAsync();

            throw new SettingsStoreException(response.RequestMessage, string.Format("{0} {1}", response.StatusCode, message));
        }
    }
}