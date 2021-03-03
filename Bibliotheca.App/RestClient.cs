using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheca.App
{
    public class RestClient
    {
        private readonly string endpoint;
        public RestClient(string endpoint)
        {
            this.endpoint = endpoint;
        }

        public static RestClient CreateClient(string endpoint)
        {
            return new RestClient(endpoint);
        }
        public void ExecutePut(string url, object body)
        {
            using (var client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"{endpoint}/{url}"),
                    Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, new ContentType("application/json").MediaType)
                })
                {

                    var response = client.SendAsync(request).GetAwaiter().GetResult();

                    if (response.Content == null || response.StatusCode != HttpStatusCode.OK)
                        throw new ApplicationException($"Error executing: {HttpMethod.Put} {endpoint}/{url}", new Exception($"Status code: {response.StatusCode}"));
                }
            }
        }
        public void ExecuteDelete(string url)
        {
            using (var client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"{endpoint}/{url}")
                })
                {

                    var response = client.SendAsync(request).GetAwaiter().GetResult();

                    if (response.Content == null || response.StatusCode != HttpStatusCode.OK)
                        throw new ApplicationException($"Error executing: {HttpMethod.Delete} {endpoint}/{url}", new Exception($"Status code: {response.StatusCode}"));
                }
            }
        }
        public async Task<T> ExecutePost<T>(string url, object body)
        {
            using (var client = new HttpClient())
            {
                using (
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri($"{endpoint}/{url}")
                    })
                {

                    if (body != null)
                    {
                        request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, new ContentType("application/json").MediaType);
                    }

                    var response = await client.SendAsync(request).ConfigureAwait(false);

                    if (response.Content == null || response.StatusCode != HttpStatusCode.OK)
                        throw new ApplicationException($"Error executing: {HttpMethod.Post} {endpoint}/{url}", new Exception($"Status code: {response.StatusCode}"));

                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
            }
        }
        public void ExecutePost(string url, object body)
        {
            using (var client = new HttpClient())
            {
                using (
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri($"{endpoint}/{url}")
                    })
                {

                    if (body != null)
                    {
                        request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, new ContentType("application/json").MediaType);
                    }

                    var response = client.SendAsync(request).GetAwaiter().GetResult();

                    if (response.Content == null || response.StatusCode != HttpStatusCode.OK)
                        throw new ApplicationException($"Error executing: {HttpMethod.Post} {endpoint}/{url}", new Exception($"Status code: {response.StatusCode}"));
                }
            }

        }
        public async Task<T> ExecuteGet<T>(string url)
        {
            using (var client = new HttpClient())
            {
                using (
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{endpoint}/{url}")
                    })
                {

                    var response = await client.SendAsync(request).ConfigureAwait(false);

                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
            }
        }
        public async Task<T> ExecuteGet<T>(string url, object body)
        {
            using (var client = new HttpClient())
            {
                using (
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{endpoint}/{url}"),
                        Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, new ContentType("application/json").MediaType),
                    })
                {

                    var response = await client.SendAsync(request).ConfigureAwait(false);

                    if (response.Content == null || response.StatusCode != HttpStatusCode.OK)
                        throw new ApplicationException($"Error executing: {HttpMethod.Get} {endpoint}/{url}", new Exception($"Status code: {response.StatusCode}"));

                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
            }
        }
        public async Task<T> Execute<T>(string url, HttpMethod method, object body = null)
        {
            using (var client = new HttpClient())
            {
                using (
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = method,
                        RequestUri = new Uri($"{endpoint}/{url}")
                    })
                {

                    if (body != null)
                    {
                        request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, new ContentType("application/json").MediaType);
                    }

                    var response = await client.SendAsync(request).ConfigureAwait(false);

                    if (response.Content == null || response.StatusCode != HttpStatusCode.OK)
                        throw new ApplicationException($"Error executing: {method} {endpoint}/{url}", new Exception($"Status code: {response.StatusCode}"));

                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
            }
        }
        public async void Execute(string url, HttpMethod method, object body = null)
        {
            using (var client = new HttpClient())
            {
                using (
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = method,
                        RequestUri = new Uri($"{endpoint}/{url}")
                    })
                {

                    if (body != null)
                    {
                        request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, new ContentType("application/json").MediaType);
                    }

                    var response = await client.SendAsync(request).ConfigureAwait(false);

                    if (response.Content == null || response.StatusCode != HttpStatusCode.OK)
                        throw new ApplicationException($"Error executing: {method} {endpoint}/{url}", new Exception($"Status code: {response.StatusCode}"));
                }
            }
        }
    }
}
