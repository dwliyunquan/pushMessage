using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Android.XiaoMi.Push
{
    public class MiHttpClient
    {
        private readonly HttpClient _client;

        public MiHttpClient(string appSercet)
        {
            _client = new HttpClient();

            var authorizationStr = $"key={appSercet}";
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationStr);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        public string HttpPost(string url, Dictionary<string, string> data)
        {
            VerifyServiceUrl(url);
            var stringContent = new FormUrlEncodedContent(data);
            var response =_client.PostAsync(url, stringContent).Result;
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
