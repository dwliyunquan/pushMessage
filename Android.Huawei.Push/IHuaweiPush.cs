using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Android.Message;
using Newtonsoft.Json;

namespace Android.Huawei.Push
{
    /// <summary>
    /// 华为推送
    /// </summary>
    public interface IHuaweiPush
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        string GetAccessToken();

        /// <summary>
        /// 消息推送
        /// </summary>
        /// <param name="accessToken">授权Token</param>
        /// <param name="payloadStr"></param>
        /// <param name="deviceTokens">token传输时需要带上双引号[""1"",""2""]</param>
        string PushMessage(string accessToken, string payloadStr, List<string> deviceTokens);
    }

    /// <summary>
    /// 华为推送
    /// </summary>
    public class HuaweiPush : IHuaweiPush
    {
        private string AppId { get; }

        private string AppSecret { get; }

        private readonly ServiceClient _serviceClient;

        private readonly string _authorizationUrl = "https://login.cloud.huawei.com/oauth2/v2/token";

        private readonly string _pushUrl = "https://api.push.hicloud.com/pushsend.do";

        public HuaweiPush(HuaweiPushOptions options)
        {
            AppId = options.AppId;
            AppSecret = options.AppSecret;


            var nspctx = "{\"ver\":\"1\",\"appId\":\"" + AppId + "\"}";
            var encodeNsp = HttpUtility.UrlEncode(nspctx, Encoding.Default);
            _pushUrl = $"{_pushUrl}?nsp_ctx={encodeNsp}";
            _serviceClient = new ServiceClient();
        }

        /// <summary>
        /// 获取授权Token
        /// </summary>
        public string GetAccessToken()
        {
            try
            {
                var data = $"grant_type=client_credentials&client_secret={AppSecret}&client_id={AppId}";
                var resultData = _serviceClient.HttpPostUseForm(_authorizationUrl, data);
                var accessTokenInfo = JsonConvert.DeserializeObject<AccessTokenResult>(resultData);
                if (!string.IsNullOrEmpty(accessTokenInfo.Access_token))
                    return accessTokenInfo.Access_token;
                throw new Exception(resultData);
            }
            catch (Exception ex)
            {
                throw new Exception($"获取AccessToken异常:{ex.Message}");
            }

        }

        /// <summary>
        /// 消息推送
        /// </summary>
        /// <param name="accessToken">授权Token</param>
        /// <param name="payloadStr"></param>
        /// <param name="deviceTokens">token传输时需要带上双引号[""1"",""2""]</param>
        public string PushMessage(string accessToken, string payloadStr, List<string> deviceTokens)
        {
            try
            {
                payloadStr=HttpUtility.UrlEncode(payloadStr, Encoding.Default);
                var deviceTokenStr = $"[{string.Join(',', deviceTokens.ToArray())}]";
                deviceTokenStr= HttpUtility.UrlEncode(deviceTokenStr, Encoding.Default);
                var pushBody = $"access_token={accessToken}&nsp_svc=openpush.message.api.send&nsp_ts={DateTime.Now.ToTimestamp()}" +
                               $"&payload={payloadStr}&device_token_list={deviceTokenStr}";
                var resultData = _serviceClient.HttpPostUseForm(_pushUrl, pushBody);
                var pushInfo = JsonConvert.DeserializeObject<PushResult>(resultData);
                if (pushInfo.Code == "80000000")
                    return pushInfo.RequestId;

                throw new Exception(resultData);
            }
            catch (Exception ex)
            {
                throw new Exception($"PushMessage异常:{ex.Message}");
            }
        }
    }
}
