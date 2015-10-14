﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public abstract class ApiClient
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

        public abstract string LocalPath { get; }

        protected virtual async Task<T> Get<T>()
        {
            return await Get<T>(string.Empty);
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

                responseMessage = await client.GetAsync(endpoint, HttpCompletionOption.ResponseContentRead);

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
                try
                {
                    return await responseMessage.Content.ReadAsAsync<T>();
                }
                catch(Exception ex)
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
            HttpClient client = new HttpClient();
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