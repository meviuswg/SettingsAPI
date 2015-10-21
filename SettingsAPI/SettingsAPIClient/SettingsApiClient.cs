using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public abstract class ApiClient
    {
        protected const int TIMEOUT = 10;
        protected string _apiKey;
        protected string _baseUrl;

        public ApiClient(string url, string apiKey)
        {
            if (url.EndsWith("/")) url = url.Substring(0, url.Length - 1);

            this._baseUrl = url;
            this._apiKey = apiKey;
        }

        public abstract string LocalPath { get; }

        protected virtual async Task<T> Get<T>()
        {
            return await Get<T>(string.Empty);
        }

        protected virtual async Task<bool> Exists(string localPath)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                HttpClient client = CreateHttpClient();

                string endpoint = GetEndpoint(localPath);

                Debug.WriteLine("GET:{0}", new[] { endpoint });
                responseMessage = await client.GetAsync(endpoint, HttpCompletionOption.ResponseContentRead);
            }
            catch (OperationCanceledException ex)
            {
                throw new SettingsStoreException(responseMessage.RequestMessage, ex);
            }
            catch (TimeoutException ex)
            {
                throw new SettingsException("The remote store operation did not complete within normal time. Reinitialize and try again.", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new SettingsException("Could not connect to remote store", ex);
            }
            catch (Exception ex)
            {
                throw new SettingsException(ex.Message, ex);
            }

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                return false;


            await handleNotOk(responseMessage); 

            return false;

        }

        protected virtual async Task<bool> Delete()
        {
            return await Delete(string.Empty);
        }

        protected virtual async Task<bool> Delete(string localPath)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                HttpClient client = CreateHttpClient();

                string endpoint = GetEndpoint(localPath);

                Debug.WriteLine("DEL:{0}", new[] { endpoint });
                responseMessage = await client.DeleteAsync(endpoint);
            }
            catch (OperationCanceledException ex)
            {
                throw new SettingsStoreException(responseMessage.RequestMessage, ex);
            }
            catch (TimeoutException ex)
            {
                throw new SettingsStoreException(responseMessage.RequestMessage, ex);
            }
            catch (HttpRequestException ex)
            {
                throw new SettingsStoreException(responseMessage.RequestMessage, ex);
            }
            catch (Exception ex)
            {
                throw new SettingsException(ex.Message, ex);
            }

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                await handleNotOk(responseMessage);
            }

            return false;
        }

        protected virtual async Task<T> Get<T>(string localPath)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                HttpClient client = CreateHttpClient();

                string endpoint = GetEndpoint(localPath);

                Debug.WriteLine("GET:{0}", new[] { endpoint });
                responseMessage = await client.GetAsync(endpoint, HttpCompletionOption.ResponseContentRead);
            }
            catch (OperationCanceledException ex)
            {
                throw new SettingsStoreException(responseMessage.RequestMessage, ex);
            }
            catch (TimeoutException ex)
            {
                throw new SettingsException("The remote store operation did not complete within normal time. Reinitialize and try again.", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new SettingsException("Could not connect to remote store", ex);
            }
            catch (Exception ex)
            {
                throw new SettingsException(ex.Message, ex);
            }

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    return await responseMessage.Content.ReadAsAsync<T>();
                }
                catch (Exception ex)
                {
                    throw new SettingsException("Error reading content", ex);
                }
            }
            else
            {
                await handleNotOk(responseMessage);
            }

            return default(T);
        }

        protected virtual async Task<bool> Post<T>(T data)
        {
            return await Post<T>(data, string.Empty);
        }

        protected virtual async Task<bool> Post<T>(T data, string localPath)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                HttpClient client = CreateHttpClient();

                string endpoint = GetEndpoint(localPath);

                Debug.WriteLine("POST:{0}", new[] { endpoint });
                responseMessage = await client.PostAsJsonAsync(endpoint, data);
            }
            catch (OperationCanceledException ex)
            {
                throw new SettingsStoreException(responseMessage.RequestMessage, ex);
            }
            catch (TimeoutException ex)
            {
                throw new SettingsStoreException(responseMessage.RequestMessage, ex);
            }
            catch (HttpRequestException ex)
            {
                throw new SettingsStoreException(responseMessage.RequestMessage, ex);
            }
            catch (Exception ex)
            {
                throw new SettingsException(ex.Message, ex);
            }

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return await handleNotOk(responseMessage);
            }
        }

        protected virtual async Task<bool> Put<T>(T data)
        {
            return await Post<T>(data, string.Empty);
        }

        protected virtual async Task<bool> Put<T>(T data, string localPath)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                HttpClient client = CreateHttpClient();
                string endpoint = GetEndpoint(localPath);

                Debug.WriteLine("PUT:{0}", new[] { endpoint });
                responseMessage = await client.PutAsJsonAsync(endpoint, data);
            }
            catch (OperationCanceledException ex)
            {
                throw new SettingsStoreException(responseMessage.RequestMessage, ex);
            }
            catch (TimeoutException ex)
            {
                throw new SettingsStoreException(responseMessage.RequestMessage, ex);
            }
            catch (HttpRequestException ex)
            {
                throw new SettingsStoreException(responseMessage.RequestMessage, ex);
            }
            catch (Exception ex)
            {
                throw new SettingsException(ex.Message, ex);
            }

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return await handleNotOk(responseMessage);
            }
        }

        public virtual string GetEndpoint(string replacementPath = "")
        {
            string lPath = LocalPath;
            if (!string.IsNullOrWhiteSpace(replacementPath))
                lPath = replacementPath;

            string url = string.Concat(_baseUrl, "/", lPath, string.Format("?apikey={0}", _apiKey));
            return url;
        }

        private HttpClient CreateHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler
            { 
             
            };

            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = TimeSpan.FromSeconds(TIMEOUT);
         
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
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
                throw new SettingNotFoundException(response.RequestMessage);
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