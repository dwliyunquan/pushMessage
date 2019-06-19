using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Android.XiaoMi.Push
{
    /// <summary>
    /// 小米推送
    /// </summary>
    public interface IXiaoMiPush
    {
        string PushByRegid(Dictionary<string,string> dicPara, List<string> deviceTokens);

        string PushByAlia(Dictionary<string, string> dicPara, List<string> deviceTokens);
    }

    /// <summary>
    /// 
    /// </summary>
    public class XiaoMiPush : IXiaoMiPush
    {
        private readonly MiHttpClient _miHttpClient;

        private string _pushRegidUrl = "https://api.xmpush.xiaomi.com/v3/message/regid";

        private string _pushAliasUrl = "https://api.xmpush.xiaomi.com/v3/message/alias";

        public XiaoMiPush(XiaoMiOptions options)
        {
            _miHttpClient = new MiHttpClient(options.AppSercet);
        }

        public string PushByRegid(Dictionary<string, string> dicPara, List<string> deviceTokens)
        {
            try
            {
                var deviceTokenStr = string.Join(",", deviceTokens);
                dicPara.Add("registration_id", deviceTokenStr);
                var resultStr = _miHttpClient.HttpPost(_pushRegidUrl, dicPara);
                var resultInfo = JsonConvert.DeserializeObject<PushXiaoMiMessageResult>(resultStr);
                if (resultInfo.Code == 0)
                    return resultInfo.Result;
                throw new Exception(resultStr);
            }
            catch (Exception ex)
            {
                throw new Exception($"获取PushByRegid异常:{ex.Message}");
            }
        }

        public string PushByAlia(Dictionary<string, string> dicPara, List<string> alias)
        {
            try
            {
                var aliasStr = string.Join(",", alias);
                dicPara.Add("alias", aliasStr);
                var resultStr = _miHttpClient.HttpPost(_pushAliasUrl, dicPara);
                var resultInfo = JsonConvert.DeserializeObject<PushXiaoMiMessageResult>(resultStr);
                if (resultInfo.Code == 0)
                    return resultInfo.Result;
                throw new Exception(resultStr);
            }
            catch (Exception ex)
            {
                throw new Exception($"获取PushByAlia异常:{ex.Message}");
            }
        }
    }


    public class PushXiaoMiMessageResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
}
