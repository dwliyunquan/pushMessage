using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Android.Message
{
    public class ServiceClient
    {
        private readonly HttpClient _client;

        public ServiceClient()
        {
            _client = new HttpClient();
        }

        public string HttpPostUseForm(string url, string data)
        {
            VerifyServiceUrl(url);
            var stringContent = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = _client.PostAsync(url, stringContent).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        public string HttpGet(string url)
        {
            VerifyServiceUrl(url);
            var httpResponseMessage = _client.GetAsync(url).Result;
            return httpResponseMessage.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        public string HttpPost(string url, Dictionary<string, object> data)
        {
            VerifyServiceUrl(url);
            var serializerData =Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(serializerData, Encoding.UTF8, "application/json");
            var response =_client.PostAsync(url, stringContent).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        public string HttpPost<T>(string url, T data)
        {
            VerifyServiceUrl(url);
            var serializerData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(serializerData, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(url, stringContent).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        public string HttpPost(string url, string serializerData)
        {
            VerifyServiceUrl(url);
            var stringContent = new StringContent(serializerData, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(url, stringContent).Result;
            return response.Content.ReadAsStringAsync().Result;
        }



        private void VerifyServiceUrl(string serviceUrl)
        {
            var uri = new Uri(serviceUrl);
            if (!uri.IsAbsoluteUri)
                throw new InvalidCastException($"{serviceUrl} 并非是一个合法的网络地址。");
        }
    }
}
