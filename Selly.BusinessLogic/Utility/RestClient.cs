using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Selly.BusinessLogic.Utility
{
    public static class RestClient
    {
        #region public methods

        public static async Task<T> GetAsync<T>(string baseUrl, IDictionary<string, string> parameters, IDictionary<string, string> headers = null)
            where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return null;
            }

            using (var client = new HttpClient())
            {
                SetupHttpClient(client, baseUrl, headers);
                try
                {
                    var url = baseUrl;
                    if (parameters != null && parameters.Count > 0)
                    {
                        var queryString = GenerateQueryString(parameters);
                        if (!string.IsNullOrWhiteSpace(queryString))
                        {
                            url = $"{url}{queryString}";
                        }
                    }

                    var response = await client.GetAsync(url).ConfigureAwait(false);
                    if (response == null || !response.IsSuccessStatusCode)
                    {
                        return null;
                    }

                    var stringifiedContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    return string.IsNullOrWhiteSpace(stringifiedContent) ? null : GetResponseData<T>(stringifiedContent);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static async Task<T> PostAsync<T>(string baseUrl, T data, Dictionary<string, string> headers = null) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return null;
            }

            using (var client = new HttpClient())
            {
                SetupHttpClient(client, baseUrl, headers);
                try
                {
                    var postContentString = JsonConvert.SerializeObject(data);
                    var postContent = new StringContent(postContentString, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(baseUrl, postContent).ConfigureAwait(false);
                    if (response == null || !response.IsSuccessStatusCode)
                    {
                        return null;
                    }

                    var stringifiedContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    return string.IsNullOrWhiteSpace(stringifiedContent) ? null : GetResponseData<T>(stringifiedContent);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static async Task<TU> PostAsync<T, TU>(string baseUrl, T data, Dictionary<string, string> headers = null) where T : class, new()
            where TU : class, new()
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return null;
            }

            using (var client = new HttpClient())
            {
                SetupHttpClient(client, baseUrl, headers);
                try
                {
                    var postContentString = JsonConvert.SerializeObject(data);
                    var postContent = new StringContent(postContentString, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(baseUrl, postContent).ConfigureAwait(false);
                    if (response == null || !response.IsSuccessStatusCode)
                    {
                        return null;
                    }

                    var stringifiedContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    return string.IsNullOrWhiteSpace(stringifiedContent) ? null : GetResponseData<TU>(stringifiedContent);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        #endregion

        #region private methods

        private static T GetResponseData<T>(string stringfiedContent) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(stringfiedContent))
            {
                return null;
            }

            var response = JsonConvert.DeserializeObject<T>(stringfiedContent);

            return response;
        }

        private static string GenerateQueryString(IDictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return null;
            }

            var sb = new StringBuilder();
            sb.Append("?");

            foreach (var entry in parameters)
            {
                sb.Append($"{entry.Key}={entry.Value}");
                sb.Append("&");
            }
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        private static void SetupHttpClient(HttpClient client, string baseUrl, IDictionary<string, string> headers)
        {
            client.Timeout = TimeSpan.FromSeconds(30);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(baseUrl);

            if (headers == null)
            {
                return;
            }

            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        #endregion
    }
}